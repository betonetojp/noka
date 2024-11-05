using NNostr.Client;
using NNostr.Client.Protocols;
using SSTPLib;
using System.Diagnostics;

namespace noka
{
    public partial class FormMain : Form
    {
        #region フィールド
        //private readonly NostrAccess _nostrAccess = new();

        private readonly string _configPath = Path.Combine(Application.StartupPath, "noka.config");

        private readonly FormSetting _formSetting = new();
        private FormManiacs _formManiacs = new();
        private FormRelayList _formRelayList = new();

        private string _npub = string.Empty;
        private string _npubHex = string.Empty;
        private string _defaultPicture = string.Empty;

        /// <summary>
        /// フォロイー公開鍵のハッシュセット
        /// </summary>
        private readonly HashSet<string> _followeesHexs = [];
        /// <summary>
        /// ユーザー辞書
        /// </summary>
        internal Dictionary<string, User?> Users = [];
        /// <summary>
        /// キーワード通知
        /// </summary>
        internal KeywordNotifier Notifier = new();

        private int _cutLength;
        private int _cutNameLength;
        private bool _showOnlyFollowees;

        private double _tempOpacity = 1.00;

        private static readonly DSSTPSender _ds = new("SakuraUnicode");
        private readonly string _SSTPMethod = "NOTIFY SSTP/1.1";
        private readonly Dictionary<string, string> _baseSSTPHeader = new(){
            {"Charset","UTF-8"},
            {"SecurityLevel","local"},
            {"Sender","noka"},
            {"Option","nobreak,notranslate"},
            {"Event","OnNostr"},
            {"Reference0","Nostr/0.4"}
        };

        private string _ghostName = string.Empty;
        private bool _soleGhostsOnly = false;
        // 重複イベントIDを保存するリスト
        private readonly LinkedList<string> _displayedEventIds = new();
        private List<SoleGhost> _soleGhosts = [new SoleGhost(), new SoleGhost()];
        #endregion

        #region コンストラクタ
        // コンストラクタ
        public FormMain()
        {
            InitializeComponent();

            // ボタンの画像をDPIに合わせて表示
            float scale = CreateGraphics().DpiX / 96f;
            int size = (int)(16 * scale);
            if (scale < 2.0f)
            {
                buttonRelayList.Image = new Bitmap(Properties.Resources.icons8_list_16, size, size);
                buttonStop.Image = new Bitmap(Properties.Resources.icons8_stop_16, size, size);
                buttonStart.Image = new Bitmap(Properties.Resources.icons8_start_16, size, size);
                buttonStop.Image = new Bitmap(Properties.Resources.icons8_stop_16, size, size);
                buttonSetting.Image = new Bitmap(Properties.Resources.icons8_setting_16, size, size);
            }
            else
            {
                buttonRelayList.Image = new Bitmap(Properties.Resources.icons8_list_32, size, size);
                buttonStart.Image = new Bitmap(Properties.Resources.icons8_start_32, size, size);
                buttonStop.Image = new Bitmap(Properties.Resources.icons8_stop_32, size, size);
                buttonSetting.Image = new Bitmap(Properties.Resources.icons8_setting_32, size, size);
            }

            Setting.Load(_configPath);
            Users = Tools.LoadUsers();

            Location = Setting.Location;
            if (new Point(0, 0) == Location)
            {
                StartPosition = FormStartPosition.CenterScreen;
            }
            Size = Setting.Size;
            TopMost = Setting.TopMost;
            _cutLength = Setting.CutLength;
            _cutNameLength = Setting.CutNameLength;
            Opacity = Setting.Opacity;
            if (0 == Opacity)
            {
                Opacity = 1;
            }
            _tempOpacity = Opacity;
            _showOnlyFollowees = Setting.ShowOnlyFollowees;
            _ghostName = Setting.Ghost;
            _soleGhostsOnly = Setting.SoleGhostsOnly;
            _npub = Setting.Npub;
            _defaultPicture = Setting.DefaultPicture;
            try
            {
                _npubHex = _npub.ConvertToHex();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            _formManiacs.MainForm = this;
        }
        #endregion

        #region Startボタン
        // Startボタン
        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                int connectCount;
                if (null != NostrAccess.Clients)
                {
                    connectCount = await NostrAccess.ConnectAsync();
                }
                else
                {
                    connectCount = await NostrAccess.ConnectAsync();
                    switch (connectCount)
                    {
                        case 0:
                            labelRelays.Text = "0 relays";
                            toolTipRelays.SetToolTip(labelRelays, string.Empty);
                            break;
                        case 1:
                            labelRelays.Text = NostrAccess.Relays[0].ToString();
                            toolTipRelays.SetToolTip(labelRelays, string.Join("\n", NostrAccess.Relays.Select(r => r.ToString())));
                            break;
                        default:
                            labelRelays.Text = $"{NostrAccess.Relays.Length} relays";
                            toolTipRelays.SetToolTip(labelRelays, string.Join("\n", NostrAccess.Relays.Select(r => r.ToString())));
                            break;
                    }
                    if (null != NostrAccess.Clients)
                    {
                        NostrAccess.Clients.EventsReceived += OnClientOnEventsReceived;
                    }
                }

                if (0 == connectCount)
                {
                    textBoxTimeline.Text = "> No relay enabled." + Environment.NewLine + textBoxTimeline.Text;
                    return;
                }

                textBoxTimeline.Text = string.Empty;
                textBoxTimeline.Text = "> Connect." + Environment.NewLine + textBoxTimeline.Text;

                NostrAccess.Subscribe();

                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                buttonStop.Focus();
                textBoxTimeline.Text = "> Create subscription." + Environment.NewLine + textBoxTimeline.Text;

                // ログイン済みの時
                if (!string.IsNullOrEmpty(_npubHex))
                {
                    // フォロイーを購読をする
                    NostrAccess.SubscribeFollows(_npubHex);

                    // ログインユーザー表示名取得
                    var name = GetUserName(_npubHex);
                    textBoxTimeline.Text = $"> Login as {name}." + Environment.NewLine + textBoxTimeline.Text;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                textBoxTimeline.Text = "> Could not start." + Environment.NewLine + textBoxTimeline.Text;
            }
        }
        #endregion

        #region イベント受信時処理
        /// <summary>
        /// イベント受信時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnClientOnEventsReceived(object? sender, (string subscriptionId, NostrEvent[] events) args)
        {
            if (args.subscriptionId == NostrAccess.SubscriptionId)
            {
                #region タイムライン購読
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

                        if (7 == nostrEvent.Kind)
                        {
                            #region リアクション
                            // ログイン済みで自分へのリアクション
                            if (!string.IsNullOrEmpty(_npubHex) && nostrEvent.GetTaggedPublicKeys().Contains(_npubHex))
                            {
                                // プロフィール購読
                                NostrAccess.SubscribeProfiles([nostrEvent.PublicKey]);

                                // ユーザー取得
                                User? user = null;
                                int retryCount = 0;
                                while (retryCount < 10)
                                {
                                    Users.TryGetValue(nostrEvent.PublicKey, out user);
                                    // ユーザーが見つかった場合、ループを抜ける
                                    if (user != null)
                                    {
                                        break;
                                    }
                                    // 一定時間待機してから再試行
                                    await Task.Delay(500);
                                    retryCount++;
                                }

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
                                    //SearchGhost();
                                    _ds.Update();

                                    var defaultPicture = _defaultPicture.Replace("{pubkey}", nostrEvent.PublicKey)
                                                                    .Replace("{npub}", nostrEvent.PublicKey.ConvertToNpub());
                                    Dictionary<string, string> SSTPHeader = new(_baseSSTPHeader)
                                    {
                                        { "Reference1", $"{nostrEvent.Kind}" }, // kind
                                        { "Reference2", content }, // content
                                        { "Reference3", user?.Name ?? "???" }, // name
                                        { "Reference4", user?.DisplayName ?? string.Empty }, // display_name
                                        { "Reference5", string.IsNullOrEmpty(user?.Picture) ? defaultPicture : user?.Picture ?? defaultPicture }, // picture
                                        { "Reference6", nevent }, // nevent1...
                                        { "Reference7", nostrEvent.PublicKey.ConvertToNpub() }, // npub1...
                                        { "Script", $"{speaker}リアクション {userName}\\n{content}\\e" }
                                    };
                                    string sstpmsg = _SSTPMethod + "\r\n" + string.Join("\r\n", SSTPHeader.Select(kvp => kvp.Key + ": " + kvp.Value.Replace("\n", "\\n"))) + "\r\n\r\n";
                                    string r = _ds.GetSSTPResponse(_ghostName, sstpmsg);
                                    //Debug.WriteLine(r);
                                }
                                // 画面に表示
                                textBoxTimeline.Text = "+" + timeString + " " + userName + " " + content + Environment.NewLine + textBoxTimeline.Text;
                            }
                            #endregion
                        }
                        if (1 == nostrEvent.Kind || 42 == nostrEvent.Kind)
                        {
                            #region テキストノート
                            if (42 == nostrEvent.Kind)
                            {
                                headMark = "=";
                            }

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

                            // プロフィール購読
                            NostrAccess.SubscribeProfiles([nostrEvent.PublicKey]);

                            // ユーザー取得
                            User? user = null;
                            int retryCount = 0;
                            while (retryCount < 10)
                            {
                                Debug.WriteLine($"retryCount = {retryCount}");
                                Users.TryGetValue(nostrEvent.PublicKey, out user);
                                // ユーザーが見つかった場合、ループを抜ける
                                if (user != null)
                                {
                                    break;
                                }
                                // 一定時間待機してから再試行
                                await Task.Delay(500);
                                retryCount++;
                            }
                            // ユーザーが見つからない時は表示しない
                            if (null == user)
                            {
                                continue;
                            }

                            // 個別ゴーストチェック
                            bool isSole = false;
                            foreach (SoleGhost soleGhost in _soleGhosts)
                            {
                                if (soleGhost.Npub == nostrEvent.PublicKey.ConvertToNpub())
                                {
                                    isSole = true;
                                    break;
                                }
                            }

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
                                //SearchGhost();
                                _ds.Update();

                                string msg = content;
                                // 本文カット
                                if (msg.Length > _cutLength)
                                {
                                    msg = $"{msg[.._cutLength]}...";
                                }
                                var defaultPicture = _defaultPicture.Replace("{pubkey}", nostrEvent.PublicKey)
                                                                    .Replace("{npub}", nostrEvent.PublicKey.ConvertToNpub());
                                Dictionary<string, string> SSTPHeader = new(_baseSSTPHeader)
                                {
                                    { "Reference1", $"{nostrEvent.Kind}" },
                                    { "Reference2", content }, // content
                                    { "Reference3", user?.Name ?? "???" }, // name
                                    { "Reference4", user?.DisplayName ?? string.Empty }, // display_name
                                    { "Reference5", string.IsNullOrEmpty(user?.Picture) ? defaultPicture : user?.Picture ?? defaultPicture }, // picture
                                    { "Reference6", nevent }, // nevent1...
                                    { "Reference7", nostrEvent.PublicKey.ConvertToNpub() }, // npub1...
                                    { "Script", $"{speaker}{(isSole ? "" : userName)}\\n{msg}\\e" }
                                };
                                string sstpmsg = _SSTPMethod + "\r\n" + string.Join("\r\n", SSTPHeader.Select(kvp => kvp.Key + ": " + kvp.Value.Replace("\n", "\\n"))) + "\r\n\r\n";

                                string r;
                                foreach (SoleGhost soleGhost in _soleGhosts)
                                {
                                    if (soleGhost.Npub == nostrEvent.PublicKey.ConvertToNpub())
                                    {
                                        r = _ds.GetSSTPResponse(soleGhost.GhostName, sstpmsg);
                                        Debug.WriteLine(r);
                                    }
                                }

                                if (!isSole && !_soleGhostsOnly)
                                {
                                    r = _ds.GetSSTPResponse(_ghostName, sstpmsg);
                                    Debug.WriteLine(r);
                                }
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
                            textBoxTimeline.Text = (iSnokakoi ? "[n]" : string.Empty) + headMark
                                                 + $"{timeString} {userName}{Environment.NewLine}"
                                                 + " " + content + Environment.NewLine + textBoxTimeline.Text;
                            Debug.WriteLine($"{timeString} {userName} {content}");
                            #endregion
                        }
                    }
                }
                #endregion
            }
            else if (args.subscriptionId == NostrAccess.GetFolloweesSubscriptionId)
            {
                #region フォロイー購読
                foreach (var nostrEvent in args.events)
                {
                    // フォローリスト
                    if (3 == nostrEvent.Kind)
                    {
                        var tags = nostrEvent.Tags;
                        foreach (var tag in tags)
                        {
                            if ("p" == tag.TagIdentifier)
                            {
                                // 公開鍵をハッシュに保存
                                _followeesHexs.Add(tag.Data[0]);

                                // petnameをユーザー辞書に保存
                                if (2 < tag.Data.Count)
                                {
                                    Users.TryGetValue(tag.Data[0], out User? user);
                                    if (null != user)
                                    {
                                        user.PetName = tag.Data[2];
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            else if (args.subscriptionId == NostrAccess.GetProfilesSubscriptionId)
            {
                #region プロフィール購読
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
                            if (null == cratedAt || cratedAt < newUserData.CreatedAt)
                            {
                                newUserData.LastActivity = DateTime.Now;
                                newUserData.PetName = existingUserData?.PetName;
                                Tools.SaveUsers(Users);
                                // 辞書に追加（上書き）
                                Users[nostrEvent.PublicKey] = newUserData;
                                Debug.WriteLine($"cratedAt updated {cratedAt} -> {newUserData.CreatedAt}");
                                Debug.WriteLine($"プロフィール更新 {newUserData.LastActivity} {newUserData.DisplayName} {newUserData.Name}");
                            }
                        }
                    }
                }
                #endregion
            }
        }
        #endregion

        #region Stopボタン
        // Stopボタン
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if (null != NostrAccess.Clients)
            {
                try
                {
                    NostrAccess.CloseSubscriptions();
                    textBoxTimeline.Text = "> Close subscription." + Environment.NewLine + textBoxTimeline.Text;

                    _ = NostrAccess.Clients.Disconnect();
                    textBoxTimeline.Text = "> Disconnect." + Environment.NewLine + textBoxTimeline.Text;
                    NostrAccess.Clients.Dispose();
                    NostrAccess.Clients = null;

                    buttonStart.Enabled = true;
                    buttonStart.Focus();
                    buttonStop.Enabled = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ButtonStop_Click: " + ex.Message);
                    Debug.Print(ex.ToString());
                    textBoxTimeline.Text = "> Could not stop." + Environment.NewLine + textBoxTimeline.Text;
                }
            }
        }
        #endregion

        #region Settingボタン
        // Settingボタン
        private async void ButtonSetting_Click(object sender, EventArgs e)
        {
            // 開く前
            Opacity = _tempOpacity;
            _formSetting.checkBoxTopMost.Checked = TopMost;
            _formSetting.textBoxCutLength.Text = _cutLength.ToString();
            _formSetting.textBoxCutNameLength.Text = _cutNameLength.ToString();
            _formSetting.trackBarOpacity.Value = (int)(Opacity * 100);
            _formSetting.checkBoxShowOnlyFollowees.Checked = _showOnlyFollowees;
            _formSetting.textBoxNpub.Text = _npub;
            _formSetting.textBoxDefaultPicture.Text = _defaultPicture;
            _formSetting._mainGhost = _ghostName;
            _formSetting.checkBoxSoleGhostsOnly.Checked = _soleGhostsOnly;

            // 開く
            _formSetting.ShowDialog(this);

            // 閉じた後
            TopMost = _formSetting.checkBoxTopMost.Checked;
            if (!int.TryParse(_formSetting.textBoxCutLength.Text, out _cutLength))
            {
                _cutLength = 40;
            }
            else if (_cutLength < 1)
            {
                _cutLength = 1;
            }
            if (!int.TryParse(_formSetting.textBoxCutNameLength.Text, out _cutNameLength))
            {
                _cutNameLength = 8;
            }
            else if (_cutNameLength < 1)
            {
                _cutNameLength = 1;
            }
            Opacity = _formSetting.trackBarOpacity.Value / 100.0;
            _showOnlyFollowees = _formSetting.checkBoxShowOnlyFollowees.Checked;
            _tempOpacity = Opacity;
            _ghostName = _formSetting._mainGhost;
            _npub = _formSetting.textBoxNpub.Text;
            _defaultPicture = _formSetting.textBoxDefaultPicture.Text;
            _soleGhostsOnly = _formSetting.checkBoxSoleGhostsOnly.Checked;
            try
            {
                // 別アカウントログイン失敗に備えてクリアしておく
                _npubHex = string.Empty;
                _followeesHexs.Clear();

                // 公開鍵取得
                _npubHex = _npub.ConvertToHex();

                // ログイン済みの時
                if (!string.IsNullOrEmpty(_npubHex))
                {
                    int connectCount = await NostrAccess.ConnectAsync();
                    if (0 == connectCount)
                    {
                        textBoxTimeline.Text = "> No relay enabled." + Environment.NewLine + textBoxTimeline.Text;
                        return;
                    }

                    // フォロイーを購読をする
                    NostrAccess.SubscribeFollows(_npubHex);

                    // ログインユーザー表示名取得
                    var name = GetUserName(_npubHex);
                    textBoxTimeline.Text = $"> Login as {name}." + Environment.NewLine + textBoxTimeline.Text;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                textBoxTimeline.Text = "> Wrong npub." + Environment.NewLine + textBoxTimeline.Text;
            }

            Setting.TopMost = TopMost;
            Setting.CutLength = _cutLength;
            Setting.CutNameLength = _cutNameLength;
            Setting.Opacity = Opacity;
            Setting.ShowOnlyFollowees = _showOnlyFollowees;
            Setting.Ghost = _ghostName;
            Setting.SoleGhostsOnly = _soleGhostsOnly;
            Setting.Npub = _npub;
            Setting.DefaultPicture = _defaultPicture;

            Setting.Save(_configPath);

            RefleshGhosts();
        }
        #endregion

        #region 複数リレーからの処理済みイベントを除外
        /// <summary>
        /// 複数リレーからの処理済みイベントを除外
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>処理済みイベントの有無</returns>
        private bool RemoveCompletedEventIds(string eventId)
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

        #region 透明解除処理
        // マウス入った時
        private void TextBoxTimeline_MouseEnter(object sender, EventArgs e)
        {
            _tempOpacity = Opacity;
            Opacity = 1.00;
        }

        // マウス出た時
        private void TextBoxTimeline_MouseLeave(object sender, EventArgs e)
        {
            Opacity = _tempOpacity;
        }
        #endregion

        #region SSPゴースト名を取得する
        /// <summary>
        /// SSPゴースト名を取得する
        /// </summary>
        //private void SearchGhost()
        //{
        //    _ds.Update();
        //    SakuraFMO fmo = (SakuraFMO)_ds.FMO;
        //    var names = fmo.GetGhostNames();
        //    if (names.Length > 0)
        //    {
        //        _ghostName = names.First(); // とりあえず先頭で
        //        //Debug.Print(_ghostName);
        //    }
        //    else
        //    {
        //        _ghostName = string.Empty;
        //        //Debug.Print("ゴーストがいません");
        //    }
        //}
        #endregion

        #region ユーザー表示名を取得する
        /// <summary>
        /// ユーザー表示名を取得する
        /// </summary>
        /// <param name="publicKeyHex">公開鍵HEX</param>
        /// <returns>ユーザー表示名</returns>
        private string GetUserName(string publicKeyHex)
        {
            // 情報があれば表示名を取得
            Users.TryGetValue(publicKeyHex, out User? user);
            string? userName = "???";
            if (user != null)
            {
                userName = user.DisplayName;
                // display_nameが無い場合はnameとする
                if (string.IsNullOrEmpty(userName))
                {
                    userName = $"{user.Name}";
                }
                // petnameがある場合はpetnameとする
                if (!string.IsNullOrEmpty(user.PetName))
                {
                    userName = $"{user.PetName}";
                }
                // 取得日更新
                user.LastActivity = DateTime.Now;
                Tools.SaveUsers(Users);
                Debug.WriteLine($"ユーザー名取得: {user.DisplayName} @{user.Name} 📛{user.PetName}");
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
        private bool IsMuted(string publicKeyHex)
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

        #region 閉じる時
        // 閉じる時
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            NostrAccess.CloseSubscriptions();
            NostrAccess.DisconnectAndDispose();

            if (FormWindowState.Normal != WindowState)
            {
                // 最小化最大化状態の時、元の位置と大きさを保存
                Setting.Location = RestoreBounds.Location;
                Setting.Size = RestoreBounds.Size;
            }
            else
            {
                Setting.Location = Location;
                Setting.Size = Size;
            }
            Setting.Save(_configPath);
            Tools.SaveUsers(Users);
            Notifier.SaveSettings(); // 必要ないが更新日時をそろえるため
            Tools.SaveSoleGhosts(_soleGhosts);

            _ds.Dispose();      // FrmMsgReceiverのThread停止せず1000ms待たされるうえにプロセス残るので…
            Application.Exit(); // ←これで殺す。SSTLibに手を入れた方がいいが、とりあえず。
        }
        #endregion

        #region ロード時
        // ロード時
        private void FormMain_Load(object sender, EventArgs e)
        {
            RefleshGhosts();
            ButtonStart_Click(sender, e);
        }
        #endregion

        #region 画面表示切替
        // 画面表示切替
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ButtonSetting_Click(sender, e);
            }
            if (e.KeyCode == Keys.F10)
            {
                var ev = new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0);
                FormMain_MouseClick(sender, ev);
            }
        }
        #endregion

        #region マニアクス表示
        private void FormMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (null == _formManiacs || _formManiacs.IsDisposed)
                {
                    _formManiacs = new FormManiacs
                    {
                        MainForm = this
                    };
                }
                if (!_formManiacs.Visible)
                {
                    _formManiacs.Show(this);
                }
            }
        }
        #endregion

        #region リレーリスト表示
        private void ButtonRelayList_Click(object sender, EventArgs e)
        {
            _formRelayList = new FormRelayList();
            if (_formRelayList.ShowDialog(this) == DialogResult.OK)
            {
                ButtonStop_Click(sender, e);
                ButtonStart_Click(sender, e);
            }
            _formRelayList.Dispose();
        }
        #endregion

        #region 個別ゴーストリスト更新
        private void RefleshGhosts()
        {
            _soleGhosts = Tools.LoadSoleGhosts();
        }
        #endregion
    }
}
