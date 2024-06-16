using NNostr.Client;
using NNostr.Client.Protocols;
using SSTPLib;
using System.Diagnostics;

namespace noka
{
    class Program
    {
        #region フィールド
        private static readonly NostrAccess _nostrAccess = new();

        private static readonly string _configPath = Path.Combine(Tools.GetAppPath(), "nokaConsole.config");

        private static string _npub = string.Empty;
        private static string _npubHex = string.Empty;

        /// <summary>
        /// フォロイー公開鍵のハッシュセット
        /// </summary>
        private static readonly HashSet<string> _followeesHexs = [];
        /// <summary>
        /// ユーザー辞書
        /// </summary>
        internal static Dictionary<string, User?> Users = [];
        /// <summary>
        /// キーワード通知
        /// </summary>
        internal static KeywordNotifier Notifier = new();

        private static int _cutLength;
        private static int _cutNameLength;
        private static bool _displayTime;
        private static bool _showOnlyFollowees;

        private static readonly DSSTPSender _ds = new("SakuraUnicode");
        private static readonly string _SSTPMethod = "NOTIFY SSTP/1.1";
        private static readonly Dictionary<string, string> _baseSSTPHeader = new(){
            {"Charset","UTF-8"},
            {"SecurityLevel","local"},
            {"Sender","nokakoi"},
            {"Option","nobreak,notranslate"},
            {"Event","OnNostr"},
            {"Reference0","Nostr/0.4"}
        };

        private static string _ghostName = string.Empty;
        // 重複イベントIDを保存するリスト
        private static readonly LinkedList<string> _displayedEventIds = new();
        #endregion

        static void Main()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Setting.LoadConsoleData(_configPath);
            _cutLength = Setting.ConloleCutLength;
            _cutNameLength = Setting.ConloleCutNameLength;
            _displayTime = Setting.ConloleDisplayTime;
            _showOnlyFollowees = Setting.ConloleShowOnlyFollowees;
            _npub = Setting.ConloleNpub;
            Users = Tools.LoadUsers();
            Setting.SaveConsoleData(_configPath);
            try
            {
                _npubHex = _npub.ConvertToHex();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            Start();
        }

        static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            // Ctrl + Cキー押下時の処理
            _nostrAccess.CloseSubscriptions();
            _nostrAccess.DisconnectAndDispose();

            Tools.SaveUsers(Users);

            _ds.Dispose();
        }

        #region Startボタン
        // Startボタン
        private static async void Start()
        {
            try
            {
                int connectCount;
                if (null != _nostrAccess.Clients)
                {
                    connectCount = await _nostrAccess.ConnectAsync();
                }
                else
                {
                    connectCount = await _nostrAccess.ConnectAsync();
                    if (null != _nostrAccess.Clients)
                    {
                        _nostrAccess.Clients.EventsReceived += OnClientOnEventsReceived;
                    }
                }

                if (0 == connectCount)
                {
                    Console.WriteLine("> No relay enabled.");
                    return;
                }

                Console.WriteLine("> Connect.");

                _nostrAccess.Subscribe();

                Console.WriteLine("> Create subscription.");

                // ログイン済みの時
                if (!string.IsNullOrEmpty(_npubHex))
                {
                    // フォロイーを購読をする
                    _nostrAccess.SubscribeFollows(_npubHex);

                    // ログインユーザー表示名取得
                    var name = GetUserName(_npubHex);
                    Console.WriteLine($"> Login as {name}.");
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                Console.WriteLine("> Could not start.");
            }
        }
        #endregion

        #region イベント受信時処理
        /// <summary>
        /// イベント受信時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnClientOnEventsReceived(object? sender, (string subscriptionId, NostrEvent[] events) args)
        {
            Debug.WriteLine("_followeesHexs.Count " + _followeesHexs.Count);
            // タイムライン購読
            if (args.subscriptionId == _nostrAccess.SubscriptionId)
            {
                foreach (var nostrEvent in args.events)
                {
                    if (RemoveCompletedEventIds(nostrEvent.Id))
                    {
                        continue;
                    }

                    var content = nostrEvent.Content;
                    if (content != null)
                    {
                        // 時間表示
                        DateTimeOffset time;
                        int hour;
                        int minute;
                        string timeString = "- ";
                        if (nostrEvent.CreatedAt != null)
                        {
                            time = (DateTimeOffset)nostrEvent.CreatedAt;
                            time = time.LocalDateTime;
                            hour = time.Hour;
                            minute = time.Minute;
                            timeString = string.Format("{0:D2}", hour) + ":" + string.Format("{0:D2}", minute);
                        }

                        // フォロイーチェック
                        string headMark = "-";
                        string speaker = "\\1";
                        if (_followeesHexs.Contains(nostrEvent.PublicKey))
                        {
                            headMark = "*";
                            // 本体側がしゃべる
                            speaker = "\\0";
                        }

                        // リアクション
                        if (7 == nostrEvent.Kind)
                        {
                            // ログイン済みで自分へのリアクション
                            if (!string.IsNullOrEmpty(_npubHex) && nostrEvent.GetTaggedPublicKeys().Contains(_npubHex))
                            {
                                Users.TryGetValue(nostrEvent.PublicKey, out User? user);
                                // ユーザー表示名取得
                                string userName = GetUserName(nostrEvent.PublicKey);
                                // ユーザー表示名カット
                                if (userName.Length > _cutNameLength)
                                {
                                    userName = $"{userName[.._cutNameLength]}...";
                                }

                                // SSPに送る
                                if (null != _ds)
                                {
                                    NIP19.NostrEventNote nostrEventNote = new()
                                    {
                                        EventId = nostrEvent.Id,
                                        Relays = [string.Empty],
                                    };
                                    var nevent = nostrEventNote.ToNIP19();
                                    SearchGhost();
                                    Dictionary<string, string> SSTPHeader = new(_baseSSTPHeader)
                                    {
                                        { "Reference1", "7" }, // kind
                                        { "Reference2", content }, // content
                                        { "Reference3", user?.Name ?? "???" }, // name
                                        { "Reference4", user?.DisplayName ?? string.Empty }, // display_name
                                        { "Reference5", user?.Picture ?? Setting.UnkownPicture }, // picture
                                        { "Reference6", nevent }, // nevent1...
                                        { "Reference7", nostrEvent.PublicKey.ConvertToNpub() }, // npub1...
                                        { "Script", $"{speaker}リアクション {userName}\\n{content}\\e" }
                                    };
                                    string sstpmsg = _SSTPMethod + "\r\n" + String.Join("\r\n", SSTPHeader.Select(kvp => kvp.Key + ": " + kvp.Value.Replace("\n", "\\n"))) + "\r\n\r\n";
                                    string r = _ds.GetSSTPResponse(_ghostName, sstpmsg);
                                    //Debug.WriteLine(r);
                                }
                                // 画面に表示
                                Console.WriteLine("+" + (_displayTime ? timeString : string.Empty)
                                             + " " + userName + " " + content);
                            }
                        }
                        // テキストノート
                        if (1 == nostrEvent.Kind)
                        {
                            var userClient = nostrEvent.GetTaggedData("client");
                            var iSnokakoi = -1 < Array.IndexOf(userClient, "nokakoi");

                            // フォロイー限定表示オンでフォロイーじゃない時は表示しない
                            if (_showOnlyFollowees && !_followeesHexs.Contains(nostrEvent.PublicKey))
                            {
                                continue;
                            }

                            // ミュートしている時は表示しない
                            if (IsMuted(nostrEvent.PublicKey))
                            {
                                continue;
                            }

                            Users.TryGetValue(nostrEvent.PublicKey, out User? user);
                            // ユーザー表示名取得（ユーザー辞書メモリ節約のため↑のフラグ処理後に）
                            string userName = GetUserName(nostrEvent.PublicKey);
                            // ユーザー表示名カット
                            if (userName.Length > _cutNameLength)
                            {
                                userName = $"{userName[.._cutNameLength]}...";
                            }

                            // SSPに送る
                            if (null != _ds)
                            {
                                NIP19.NostrEventNote nostrEventNote = new()
                                {
                                    EventId = nostrEvent.Id,
                                    Relays = [string.Empty],
                                };
                                var nevent = nostrEventNote.ToNIP19();
                                SearchGhost();

                                string msg = content;
                                // 本文カット
                                if (msg.Length > _cutLength)
                                {
                                    msg = $"{msg[.._cutLength]}...";
                                }
                                Dictionary<string, string> SSTPHeader = new(_baseSSTPHeader)
                                {
                                    { "Reference1", "1" },
                                    { "Reference2", content }, // content
                                    { "Reference3", user?.Name ?? "???" }, // name
                                    { "Reference4", user?.DisplayName ?? string.Empty }, // display_name
                                    { "Reference5", user?.Picture ?? Setting.UnkownPicture }, // picture
                                    { "Reference6", nevent }, // nevent1...
                                    { "Reference7", nostrEvent.PublicKey.ConvertToNpub() }, // npub1...
                                    { "Script", $"{speaker}{userName}\\n{msg}\\e" }
                                };
                                string sstpmsg = _SSTPMethod + "\r\n" + String.Join("\r\n", SSTPHeader.Select(kvp => kvp.Key + ": " + kvp.Value.Replace("\n", "\\n"))) + "\r\n\r\n";
                                string r = _ds.GetSSTPResponse(_ghostName, sstpmsg);
                                //Debug.WriteLine(r);
                            }

                            // キーワード通知
                            var settings = Notifier.Settings;
                            if (Notifier.CheckPost(content) && settings.Open)
                            {
                                NIP19.NostrEventNote nostrEventNote = new()
                                {
                                    EventId = nostrEvent.Id,
                                    Relays = [string.Empty],
                                };
                                var nevent = nostrEventNote.ToNIP19();
                                var app = new ProcessStartInfo
                                {
                                    FileName = settings.FileName + nevent,
                                    UseShellExecute = true
                                };
                                try
                                {
                                    Process.Start(app);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                }
                            }

                            // 改行をスペースに置き換え
                            content = content.Replace('\n', ' ');
                            // 本文カット
                            if (content.Length > _cutLength)
                            {
                                content = $"{content[.._cutLength]}...";
                            }
                            // 画面に表示
                            Console.WriteLine((iSnokakoi ? "[n]" : string.Empty) + headMark
                                                 + (_displayTime ? $"{timeString} {userName}{Environment.NewLine}" : string.Empty)
                                                 + " " + content);
                            Debug.WriteLine($"{timeString} {userName} {content}");
                        }
                    }
                }
            }
            // フォロイー購読
            else if (args.subscriptionId == _nostrAccess.GetFolloweesSubscriptionId)
            {
                foreach (var nostrEvent in args.events)
                {
                    // フォローリスト
                    if (3 == nostrEvent.Kind)
                    {
                        var tags = nostrEvent.Tags;
                        foreach (var tag in tags)
                        {
                            // 公開鍵を保存
                            if ("p" == tag.TagIdentifier)
                            {
                                // 先頭を公開鍵と決めつけているが…
                                _followeesHexs.Add(tag.Data[0]);
                            }
                        }
                    }
                }
            }
            // プロフィール購読
            else if (args.subscriptionId == _nostrAccess.GetProfilesSubscriptionId)
            {
                //// ※nostrEventが返ってこない特定ユーザーがいる。ライブラリの問題か。
                foreach (var nostrEvent in args.events)
                {
                    if (RemoveCompletedEventIds(nostrEvent.Id))
                    {
                        continue;
                    }

                    // プロフィール
                    if (0 == nostrEvent.Kind && null != nostrEvent.Content && null != nostrEvent.PublicKey)
                    {
                        var newUserData = Tools.JsonToUser(nostrEvent.Content, nostrEvent.CreatedAt, Notifier.Settings.MuteMostr);
                        if (null != newUserData)
                        {
                            DateTimeOffset? cratedAt = DateTimeOffset.MinValue;
                            if (Users.TryGetValue(nostrEvent.PublicKey, out User? existingUserData))
                            {
                                cratedAt = existingUserData?.CreatedAt;
                            }
                            if (false == existingUserData?.Mute)
                            {
                                // 既にミュートオフのMostrアカウントのミュートを解除
                                newUserData.Mute = false;
                            }
                            if (null == cratedAt || (cratedAt < newUserData.CreatedAt))
                            {
                                newUserData.LastActivity = DateTime.Now;
                                // 辞書に追加（上書き）
                                Users[nostrEvent.PublicKey] = newUserData;
                                Debug.WriteLine($"cratedAt updated {cratedAt} -> {newUserData.CreatedAt}");
                                Debug.WriteLine($"プロフィール更新 {newUserData.LastActivity} {newUserData.DisplayName} {newUserData.Name}");

                                // コンソールでは毎回ファイル保存
                                Tools.SaveUsers(Users);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 複数リレーからの処理済みイベントを除外
        /// <summary>
        /// 複数リレーからの処理済みイベントを除外
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>処理済みイベントの有無</returns>
        private static bool RemoveCompletedEventIds(string eventId)
        {
            if (_displayedEventIds.Contains(eventId))
            {
                return true;
            }

            if (_displayedEventIds.Count >= 128)
            {
                _displayedEventIds.RemoveFirst();
            }
            _displayedEventIds.AddLast(eventId);
            return false;
        }
        #endregion

        #region SSPゴースト名を取得する
        /// <summary>
        /// SSPゴースト名を取得する
        /// </summary>
        private static void SearchGhost()
        {
            _ds.Update();
            SakuraFMO fmo = (SakuraFMO)_ds.FMO;
            var names = fmo.GetGhostNames();
            if (names.Length > 0)
            {
                _ghostName = names.First(); // とりあえず先頭で
                                            //Debug.Print(_ghostName);
            }
            else
            {
                _ghostName = string.Empty;
                //Debug.Print("ゴーストがいません");
            }
        }
        #endregion

        #region ユーザー表示名を取得する
        /// <summary>
        /// ユーザー表示名を取得する
        /// </summary>
        /// <param name="publicKeyHex">公開鍵HEX</param>
        /// <returns>ユーザー表示名</returns>
        private static string GetUserName(string publicKeyHex)
        {
            /*
            // 辞書にない場合プロフィールを購読する
            if (!_users.TryGetValue(publicKeyHex, out User? user))
            {
                SubscribeProfiles([publicKeyHex]);
            }
            */
            // kind 0 を毎回購読するように変更（頻繁にdisplay_name等を変更するユーザーがいるため）
            _nostrAccess.SubscribeProfiles([publicKeyHex]);

            // 情報があれば表示名を取得
            Users.TryGetValue(publicKeyHex, out User? user);
            string? userName = "???";
            if (null != user)
            {
                userName = user.DisplayName;
                // display_nameが無い場合は@nameとする
                if (null == userName || string.Empty == userName)
                {
                    userName = $"@{user.Name}";
                }
                // 取得日更新
                user.LastActivity = DateTime.Now;
                Debug.WriteLine($"ユーザー名取得 {user.LastActivity} {user.DisplayName} {user.Name}");
            }
            return userName;
        }
        #endregion

        #region ミュートされているか確認する
        /// <summary>
        /// ミュートされているか確認する
        /// </summary>
        /// <param name="publicKeyHex">公開鍵HEX</param>
        /// <returns>ミュートフラグ</returns>
        private static bool IsMuted(string publicKeyHex)
        {
            if (Users.TryGetValue(publicKeyHex, out User? user))
            {
                if (null != user)
                {
                    return user.Mute;
                }
            }
            return false;
        }
        #endregion
    }
}
