◆ 動作環境

Windows11 22H2 (x64)
.NET 8.0
※ランタイムが必要です。インストールしていない場合は初回起動時の案内に従ってください。


◆ noka.exe

Nostrのリレーサーバーに接続してグローバルタイムラインをリアルタイムに表示するアプリケーションです。

初期設定では、やぶみリレー（wss://yabu.me/）とこじらリレー（wss://r.kojira.io/）に接続します。
左上の『リレーボタン』からリレーの追加削除と使用リレーの選択ができます。

設定でフォロータイムラインにすることもできます（npubの入力が必要です）。
右上の『設定ボタン』またはESCキーで設定画面が開きます。

本体の余白を右クリックまたはF10キーでユーザーミュートとキーワード通知の設定画面が開きます。

タイムラインを『伺か』(SSP)に流すことができます。
https://ssp.shillest.net/

GhostSpeakerと棒読みちゃんを組み合わせて読み上げさせるのがおすすめです。
https://github.com/apxxxxxxe/GhostSpeaker
https://chi.usamimi.info/Program/Application/BouyomiChan/

「伺か」用ゴーストnostalkのNostrイベント通知(Nostr/0.3)に対応しアバター画像を送信できます。
https://github.com/nikolat/nostalk


◆ 更新履歴

2024/06/15 ver. 0.1.2
プロフィールのtagsに絵文字があるとプロフィールを取得できなっかった問題を修正しました。
SSTP Nostr通知イベント Nostr/0.4に対応しました。
※それに伴ない、picture未取得時に代替pictureを送っていたのを廃止しました。
キーワード通知（Open file notification）送信時に利用リレー情報を含めないように変更しました。

2024/06/13 ver. 0.1.1
リレー表示がはみ出さないように修正しました。
設定ファイルのパスを絶対パスに修正ました。

2024/06/09 ver. 0.1.0
初公開


◆ Nostrクライアントライブラリ

NNostr
https://github.com/Kukks/NNostr
内のNNostr.Client Ver0.0.49を一部変更して利用しています。


◆ DirectSSTP送信ライブラリ

DirectSSTPTester
https://github.com/nikolat/DirectSSTPTester
内のSSTPLib Ver4.0.0を利用しています。
