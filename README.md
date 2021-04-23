# WindowStretch
ウィンドウサイズと位置を、縦横比を維持しつつ最大化します。現在はWin版「ウマ娘 プリティーダービー」用。

## インストール
1. [.NET 5.0 Runtime](https://dotnet.microsoft.com/download/dotnet/5.0/runtime/) をインストールしてください。
	* 「Run desktop apps」のDownload x64 or x86 をクリックすると、インストーラをダウンロードできます。
2. [Releases](https://github.com/seeker3600/WindowStretch/releases/) から最新版のバイナリをダウンロードし、中身を解凍してください。
	* Assets を展開して、中の「WindowStretch.zip」をダウンロードしてください。

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

* ウマ娘のスキーム(起動URI)を指定して、本ツールからウマ娘が起動できます。
	* デスクトップのショートカットを右クリックし、プロパティからURLを確認してください。
* 下のチェックを入れると、本ツール起動時にウマ娘を一緒に起動します。

### 「スクリーンショット」タブ

![screenshot](img/screenshot.png)

* 画像を保存するフォルダを指定して「撮影」ボタンを押すと、ウマ娘のスクリーンショットを撮ります。
* 左下のチェックを入れると、撮影時に画像をビューワで開きます。
* 右の領域から別のアプリにドラッグ＆ドロップできます。ブラウザに画像を直接投稿するときに便利です。

## 対応環境

Windows 10 Pro バージョン 2004 で動作を確認しています。
それ以外の環境で動く／動かない場合はご連絡ください。

## TODO
* [x] アプリの起動
* [x] マニフェストの設定
* [x] スクリーンショット
* [ ] 録画
* [x] コンテキストメニュー
* [ ] 固定値をなくす
* [ ] リファクタリング

## 連絡
イシューやプルリク、 @seeker7200 (twitter) へどうぞ。

## ライセンス
本ツールは暫定MITとします。関連ライブラリ等のライセンス整理は進行中です。
