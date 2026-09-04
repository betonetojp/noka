# noka

Nostr のリレーサーバーに接続してグローバルタイムラインをリアルタイムに表示する軽量なデスクトップクライアントです。  
タイムラインの投稿を「伺か」(SSP) にリアルタイム送信する SSTP 連携機能を備えています。

---

## 動作環境
- **OS**: Windows 10 / Windows 11 (x64)
- **ランタイム**: [.NET 8.0 ランタイム (Desktop Runtime)](https://dotnet.microsoft.com/download/dotnet/8.0)

---

## 主な機能

- **タイムライン受信**: グローバルタイムラインおよびフォロータイムライン（npub指定時）のリアルタイム受信
- **表示 kind の選択**: テキストノート（kind: 1）、リアクション（kind: 7）、チャンネルメッセージ（kind: 42）
- **インデクサリレー連携**: プロフィール未取得のアカウントもインデクサリレー（`directory.yabu.me`）から非同期に自動取得
- **省略表示**: URL・画像・引用・メンションをコンパクトなアイコン・タグ形式に整形表示
- **ミュート機能**: アカウントミュートおよび指定単語を含む投稿のワードミュート
- **キーワード通知**: 指定キーワードを含む投稿を受信した際のアクション起動
- **タスクトレイ最小化**: 閉じるボタン時に終了せずタスクトレイに常駐可能
- **「伺か」(SSP) 連携**: DirectSSTP による投稿通知、専用ゴースト割り当て、アバター画像通知

---

## 操作方法・ショートカット

| 操作 | 動作 |
|---|---|
| **Start ボタン** | リレーへの接続を開始しタイムラインの購読を開始します |
| **Stop ボタン** | リレーの購読を停止し切断します（最新のプロフィール情報を保存） |
| **Relay ボタン** | 接続先リレーの追加・削除・有効/無効を設定する画面を開きます |
| **Setting ボタン** / `ESC` | 基本設定画面（npub、表示kind、不透明度、個別ゴースト等）を開きます |
| **余白右クリック** / `F10` | マニアック設定画面（ミュートアカウント一覧、ミュートワード、キーワード通知等）を開きます |
| **リレー情報ラベル（画面下部）** | ホバーで各リレーの接続ステータス（接続中 / ❌ 切断）をツールチップ表示します |
| **トレイアイコン左クリック** | ウィンドウの最小化 / 表示切り替えを行います |
| **トレイアイコン右クリック** | コンテキストメニュー（Setting / Quit）を開きます |

---

## タイムラインの表示仕様

投稿本文中の各種リンクや特殊記述は以下のように省略・置換表示されます：

| 表記 | 置換対象 |
|---|---|
| `［🗒️］` | 引用（`nostr:note...`, `nostr:nevent...` など） |
| `［🖼️］` | 画像URL（`.jpg`, `.jpeg`, `.png`, `.gif`, `.bmp`, `.webp`） |
| `［🔗］` | 一般URLリンク |
| `［👤名前］` | メンション（`nostr:npub...`, `nostr:nprofile...`） |

---

## 外部連携

### 「伺か」(SSP) 連携（DirectSSTP）
タイムラインに流れてくる投稿を「伺か」(SSP) にリアルタイムで送信できます。
- [SSP](https://ssp.shillest.net/) / [keshiki](https://keshiki.nobody.jp/)
- [GhostSpeaker](https://github.com/apxxxxxxe/GhostSpeaker) と [棒読みちゃん](https://chi.usamimi.info/Program/Application/BouyomiChan/) を組み合わせることで、タイムラインの音声読み上げも可能です。
- 「伺か」(SSP) 用ゴースト「[nostalk](https://github.com/nikolat/nostalk)」の Nostr イベント通知仕様 (Nostr/0.4) に対応し、アバター画像をゴースト側に送信できます。
- 特定の Nostr ユーザーごとに専用のゴーストを割り当てて通知することも可能です。
- 「伺か」用プラグイン「[nokauka](https://github.com/nikolat/nokauka)」を使用することで、SSP 側から noka の起動やアップデート確認が行えます。

---

## 各種設定ファイル
アプリの実行フォルダ内に以下の設定ファイルが保存されます：
- `noka.config`: ウィンドウ位置、不透明度、表示オプション等の基本設定（XML形式）
- `relays.json`: 接続先リレー情報
- `users.json`: プロフィール情報のキャッシュ（表示名、petname、ミュート設定等）
- `soleghosts.json`: ユーザーごとの専用ゴースト割り当て設定
- `keywordnotifier.json`: ミュートワードおよびキーワード通知設定

---

## 利用ライブラリ
- [NNostr](https://github.com/Kukks/NNostr) (NNostr.Client を一部変更して同梱)
- [DirectSSTPTester](https://github.com/nikolat/DirectSSTPTester) (SSTPLib を利用)
- [Icons8](https://icons8.com) (アイコン素材)
