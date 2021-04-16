# WindowStretch
ウィンドウサイズと位置を、縦横比を維持しつつ最大化する。だいたいWin版「ウマ娘 プリティーダービー」用。

## インストール
1. [.NET 5.0 Runtime](https://dotnet.microsoft.com/download/dotnet/5.0/runtime/) をインストールしてください。
2. [Releases](https://github.com/seeker3600/WindowStretch/releases) から最新版のバイナリをダウンロードし、中身を解凍してください。

## アンインストール
解凍したフォルダを削除してください。設定データはAppData内に保存していますので、別途削除が必要です。

## 使い方
* 本ツールを管理者権限で起動してください。ウマ娘を操作するには管理者権限が必要です。
* 最小化すると、タスクトレイに格納されます。
	* トレイアイコンをクリックすると再度表示します。
	* トレイアイコンを右クリックすると、コンテキストメニューを表示します。

### 「サイズの自動調整」タブ

![size](img/size.png)

* 横長・縦長それぞれで「何もしない」「全画面表示」「デスクトップ最大表示」を選択します。
* 全画面表示は、強制的に最前面に表示します。
	* 「少しのはみ出しを許容」にチェックすると、画面をはみ出させて隙間を埋めます。

### 「対象アプリの起動」タブ

![start](img/start.png)

* アプリのスキーム(起動URI)かパスを指定して、本ツールからウマ娘が起動できます。
* 本ツール起動時に、ウマ娘を一緒に起動することもできます。

## TODO
* [x] アプリの起動
* [x] マニフェストの設定
* [ ] スクリーンショット
* [ ] 録画
* [x] コンテキストメニュー
* [ ] 固定値をなくす
* [ ] リファクタリング

## 連絡
イシューやプルリク、 @seeker7200 (twitter) へどうぞ。

## ライセンス
本ツールは暫定MITとします。関連ライブラリ等のライセンス整理は進行中です。
