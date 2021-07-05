# WindowStretch
ゲーム画面のウィンドウサイズを、縦横比を維持しつつ最大化します。現在はDMM版「ウマ娘 プリティーダービー」用。

## インストール

[こちら](https://github.com/seeker3600/WindowStretch/releases/download/v0.8.0/WindowStretch.zip) から最新版をダウンロードし、zipファイルを解凍してください。

* Windows8.1、または 10 の古いバージョンをご利用の方は、 [.net framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer) をインストールする必要があります。
* Windows 10 April 2018 Update (1803) より前のバージョンをご利用の場合、録画機能は使用できません。

## アンインストール
解凍したフォルダを削除してください。設定データはAppData内に保存していますので、別途削除が必要です。

## 使い方
* 「WindowStretch.exe」を管理者権限で起動してください。ウマ娘を操作するには管理者権限が必要です。
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
	* フォルダが存在しないと撮影に失敗します。あらかじめフォルダを作成してください。
	* 「エクスプローラで開く」ボタンを押して、フォルダが開けるか確認できます。
* 左下のチェックを入れると、撮影時に画像をビューワで開きます。
* 右の領域から別のアプリにドラッグ＆ドロップできます。ブラウザに画像を直接投稿するときに便利です。

### 「録画」タブ

![record](img/record.png)

* 動画を保存するフォルダを指定して「録画開始」ボタンを押すと、ウマ娘の動画を撮ります。
	* フォルダが存在しないと録画に失敗します。あらかじめフォルダを作成してください。
	* 「エクスプローラで開く」ボタンを押して、フォルダが開けるか確認できます。
* 録画中の音はすべて録音されます。ほかのソフトが音を出さないか注意してください。

### 「目盛り」タブ

![scale](img/scale.png)

* チェックを入れると、育成の体力ゲージの位置に目盛りを表示します。
* ゲームで何をしているかに関係なく「表示するだけ」なので注意してください。
	![scale_enabled](img/scale_enabled.png)


## 対応環境

Windows 10 Pro バージョン 2004 (x64) で動作を確認しています。
それ以外の環境で動く／動かない場合はご連絡ください。

## TODO
* [x] ツールを最小化しても非表示にならないことがある
* [x] 体力目盛り
* [ ] 録画のフレームレートを落としたい
* [ ] ミュート
* [x] ツールをトレイから出したら前面に出す
* [ ] リファクタリング
* ~~失敗しそうなときクリック防止~~
* ~~固定値をなくす~~

## 連絡
イシューやプルリク、 @seeker7200 (twitter) へどうぞ。

## ライセンス
MIT