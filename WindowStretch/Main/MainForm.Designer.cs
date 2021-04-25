﻿
namespace WindowStretch.Main
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.updateBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.modeBoxT = new System.Windows.Forms.ComboBox();
            this.alwaysTopChkT = new System.Windows.Forms.CheckBox();
            this.allowExcessChkT = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.modeBoxW = new System.Windows.Forms.ComboBox();
            this.alwaysTopChkW = new System.Windows.Forms.CheckBox();
            this.allowExcessChkW = new System.Windows.Forms.CheckBox();
            this.leftNumW = new System.Windows.Forms.NumericUpDown();
            this.watchTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolMitem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameStartMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.resizeTab = new System.Windows.Forms.TabPage();
            this.startTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.appUriTxt = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.startWithMeChk = new System.Windows.Forms.CheckBox();
            this.screenshotTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.scrshotSaveTxt = new System.Windows.Forms.TextBox();
            this.selectScrshotFolderBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.takeScrshotBtn = new System.Windows.Forms.Button();
            this.scrshotDragLbl = new System.Windows.Forms.Label();
            this.scrshotTakeAndOpenChk = new System.Windows.Forms.CheckBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.scrshotFolderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftNumW)).BeginInit();
            this.notifyIconMenu.SuspendLayout();
            this.mainTabs.SuspendLayout();
            this.resizeTab.SuspendLayout();
            this.startTab.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.screenshotTab.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(3, 2);
            this.updateBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(64, 20);
            this.updateBtn.TabIndex = 3;
            this.updateBtn.Text = "再適用";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.updateBtn, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(430, 169);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.modeBoxT, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.alwaysTopChkT, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.allowExcessChkT, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 26);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(209, 111);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 26.25F);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 47);
            this.label2.TabIndex = 0;
            this.label2.Text = "縦長のとき";
            // 
            // modeBoxT
            // 
            this.modeBoxT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeBoxT.FormattingEnabled = true;
            this.modeBoxT.Location = new System.Drawing.Point(3, 49);
            this.modeBoxT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.modeBoxT.Name = "modeBoxT";
            this.modeBoxT.Size = new System.Drawing.Size(165, 20);
            this.modeBoxT.TabIndex = 1;
            this.modeBoxT.SelectedIndexChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // alwaysTopChkT
            // 
            this.alwaysTopChkT.AutoSize = true;
            this.alwaysTopChkT.Location = new System.Drawing.Point(3, 73);
            this.alwaysTopChkT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.alwaysTopChkT.Name = "alwaysTopChkT";
            this.alwaysTopChkT.Size = new System.Drawing.Size(60, 16);
            this.alwaysTopChkT.TabIndex = 2;
            this.alwaysTopChkT.Text = "最前面";
            this.alwaysTopChkT.UseVisualStyleBackColor = true;
            this.alwaysTopChkT.CheckedChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // allowExcessChkT
            // 
            this.allowExcessChkT.AutoSize = true;
            this.allowExcessChkT.Location = new System.Drawing.Point(3, 93);
            this.allowExcessChkT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.allowExcessChkT.Name = "allowExcessChkT";
            this.allowExcessChkT.Size = new System.Drawing.Size(130, 16);
            this.allowExcessChkT.TabIndex = 3;
            this.allowExcessChkT.Text = "少しのはみ出しを許容";
            this.allowExcessChkT.UseVisualStyleBackColor = true;
            this.allowExcessChkT.CheckedChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.modeBoxW, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.alwaysTopChkW, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.allowExcessChkW, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(218, 26);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(209, 111);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 26.25F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "横長のとき";
            // 
            // modeBoxW
            // 
            this.modeBoxW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeBoxW.FormattingEnabled = true;
            this.modeBoxW.Location = new System.Drawing.Point(3, 49);
            this.modeBoxW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.modeBoxW.Name = "modeBoxW";
            this.modeBoxW.Size = new System.Drawing.Size(165, 20);
            this.modeBoxW.TabIndex = 1;
            this.modeBoxW.SelectedIndexChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // alwaysTopChkW
            // 
            this.alwaysTopChkW.AutoSize = true;
            this.alwaysTopChkW.Location = new System.Drawing.Point(3, 73);
            this.alwaysTopChkW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.alwaysTopChkW.Name = "alwaysTopChkW";
            this.alwaysTopChkW.Size = new System.Drawing.Size(60, 16);
            this.alwaysTopChkW.TabIndex = 2;
            this.alwaysTopChkW.Text = "最前面";
            this.alwaysTopChkW.UseVisualStyleBackColor = true;
            this.alwaysTopChkW.CheckedChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // allowExcessChkW
            // 
            this.allowExcessChkW.AutoSize = true;
            this.allowExcessChkW.Location = new System.Drawing.Point(3, 93);
            this.allowExcessChkW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.allowExcessChkW.Name = "allowExcessChkW";
            this.allowExcessChkW.Size = new System.Drawing.Size(130, 16);
            this.allowExcessChkW.TabIndex = 3;
            this.allowExcessChkW.Text = "少しのはみ出しを許容";
            this.allowExcessChkW.UseVisualStyleBackColor = true;
            this.allowExcessChkW.CheckedChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // leftNumW
            // 
            this.leftNumW.Location = new System.Drawing.Point(157, 3);
            this.leftNumW.Name = "leftNumW";
            this.leftNumW.Size = new System.Drawing.Size(120, 19);
            this.leftNumW.TabIndex = 4;
            // 
            // watchTimer
            // 
            this.watchTimer.Enabled = true;
            this.watchTimer.Interval = 1000;
            this.watchTimer.Tick += new System.EventHandler(this.watchTimer_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyIconMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "WindowStretch";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshMItem,
            this.showToolMitem,
            this.gameStartMItem,
            this.toolStripSeparator1,
            this.exitMItem});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(206, 98);
            // 
            // refreshMItem
            // 
            this.refreshMItem.Name = "refreshMItem";
            this.refreshMItem.Size = new System.Drawing.Size(205, 22);
            this.refreshMItem.Text = "サイズ変更の設定を再適用";
            this.refreshMItem.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // showToolMitem
            // 
            this.showToolMitem.Name = "showToolMitem";
            this.showToolMitem.Size = new System.Drawing.Size(205, 22);
            this.showToolMitem.Text = "ツールの設定を表示";
            this.showToolMitem.Click += new System.EventHandler(this.showToolMitem_Click);
            // 
            // gameStartMItem
            // 
            this.gameStartMItem.Name = "gameStartMItem";
            this.gameStartMItem.Size = new System.Drawing.Size(205, 22);
            this.gameStartMItem.Text = "ゲームを起動する";
            this.gameStartMItem.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // exitMItem
            // 
            this.exitMItem.Name = "exitMItem";
            this.exitMItem.Size = new System.Drawing.Size(205, 22);
            this.exitMItem.Text = "ツールを終了";
            this.exitMItem.Click += new System.EventHandler(this.exitMItem_Click);
            // 
            // mainTabs
            // 
            this.mainTabs.Controls.Add(this.resizeTab);
            this.mainTabs.Controls.Add(this.startTab);
            this.mainTabs.Controls.Add(this.screenshotTab);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Location = new System.Drawing.Point(0, 0);
            this.mainTabs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(444, 199);
            this.mainTabs.TabIndex = 3;
            // 
            // resizeTab
            // 
            this.resizeTab.Controls.Add(this.tableLayoutPanel1);
            this.resizeTab.Location = new System.Drawing.Point(4, 22);
            this.resizeTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resizeTab.Name = "resizeTab";
            this.resizeTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resizeTab.Size = new System.Drawing.Size(436, 173);
            this.resizeTab.TabIndex = 1;
            this.resizeTab.Text = "サイズの自動調整";
            this.resizeTab.UseVisualStyleBackColor = true;
            // 
            // startTab
            // 
            this.startTab.Controls.Add(this.tableLayoutPanel3);
            this.startTab.Location = new System.Drawing.Point(4, 22);
            this.startTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startTab.Name = "startTab";
            this.startTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startTab.Size = new System.Drawing.Size(436, 173);
            this.startTab.TabIndex = 0;
            this.startTab.Text = "対象アプリの起動";
            this.startTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.appUriTxt, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.startBtn, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.startWithMeChk, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(430, 43);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // appUriTxt
            // 
            this.appUriTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appUriTxt.Location = new System.Drawing.Point(3, 2);
            this.appUriTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.appUriTxt.Name = "appUriTxt";
            this.appUriTxt.Size = new System.Drawing.Size(354, 19);
            this.appUriTxt.TabIndex = 0;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(363, 2);
            this.startBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(64, 19);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "起動する";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // startWithMeChk
            // 
            this.startWithMeChk.AutoSize = true;
            this.startWithMeChk.Location = new System.Drawing.Point(3, 25);
            this.startWithMeChk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startWithMeChk.Name = "startWithMeChk";
            this.startWithMeChk.Size = new System.Drawing.Size(155, 16);
            this.startWithMeChk.TabIndex = 2;
            this.startWithMeChk.Text = "ツール起動時に一緒に起動";
            this.startWithMeChk.UseVisualStyleBackColor = true;
            // 
            // screenshotTab
            // 
            this.screenshotTab.Controls.Add(this.tableLayoutPanel7);
            this.screenshotTab.Location = new System.Drawing.Point(4, 22);
            this.screenshotTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.screenshotTab.Name = "screenshotTab";
            this.screenshotTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.screenshotTab.Size = new System.Drawing.Size(436, 173);
            this.screenshotTab.TabIndex = 2;
            this.screenshotTab.Text = "スクリーンショット";
            this.screenshotTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(430, 169);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(424, 39);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "保存するフォルダ";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.scrshotSaveTxt, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.selectScrshotFolderBtn, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 14);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(418, 23);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // scrshotSaveTxt
            // 
            this.scrshotSaveTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrshotSaveTxt.Location = new System.Drawing.Point(3, 2);
            this.scrshotSaveTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scrshotSaveTxt.Name = "scrshotSaveTxt";
            this.scrshotSaveTxt.Size = new System.Drawing.Size(342, 19);
            this.scrshotSaveTxt.TabIndex = 1;
            // 
            // selectScrshotFolderBtn
            // 
            this.selectScrshotFolderBtn.Location = new System.Drawing.Point(351, 2);
            this.selectScrshotFolderBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectScrshotFolderBtn.Name = "selectScrshotFolderBtn";
            this.selectScrshotFolderBtn.Size = new System.Drawing.Size(64, 19);
            this.selectScrshotFolderBtn.TabIndex = 2;
            this.selectScrshotFolderBtn.Text = "選択...";
            this.selectScrshotFolderBtn.UseVisualStyleBackColor = true;
            this.selectScrshotFolderBtn.Click += new System.EventHandler(this.selectScrshotFolderBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.tableLayoutPanel6);
            this.groupBox1.Location = new System.Drawing.Point(3, 45);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(424, 62);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.takeScrshotBtn, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.scrshotDragLbl, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.scrshotTakeAndOpenChk, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 14);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(418, 46);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // takeScrshotBtn
            // 
            this.takeScrshotBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.takeScrshotBtn.Location = new System.Drawing.Point(62, 2);
            this.takeScrshotBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.takeScrshotBtn.Name = "takeScrshotBtn";
            this.takeScrshotBtn.Size = new System.Drawing.Size(85, 22);
            this.takeScrshotBtn.TabIndex = 0;
            this.takeScrshotBtn.Text = "撮影";
            this.takeScrshotBtn.UseVisualStyleBackColor = true;
            // 
            // scrshotDragLbl
            // 
            this.scrshotDragLbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.scrshotDragLbl.AutoSize = true;
            this.scrshotDragLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scrshotDragLbl.Location = new System.Drawing.Point(215, 0);
            this.scrshotDragLbl.Name = "scrshotDragLbl";
            this.scrshotDragLbl.Size = new System.Drawing.Size(197, 26);
            this.scrshotDragLbl.TabIndex = 1;
            this.scrshotDragLbl.Text = "ここからドラッグすると、ほかのアプリに画像を貼り付けます。";
            // 
            // scrshotTakeAndOpenChk
            // 
            this.scrshotTakeAndOpenChk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.scrshotTakeAndOpenChk.AutoSize = true;
            this.scrshotTakeAndOpenChk.Location = new System.Drawing.Point(22, 28);
            this.scrshotTakeAndOpenChk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scrshotTakeAndOpenChk.Name = "scrshotTakeAndOpenChk";
            this.scrshotTakeAndOpenChk.Size = new System.Drawing.Size(165, 16);
            this.scrshotTakeAndOpenChk.TabIndex = 2;
            this.scrshotTakeAndOpenChk.Text = "撮影時に画像をビューワで開く";
            this.scrshotTakeAndOpenChk.UseVisualStyleBackColor = true;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.statusBar.Location = new System.Drawing.Point(0, 177);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusBar.Size = new System.Drawing.Size(444, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 4;
            this.statusBar.Text = "sss";
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(51, 17);
            this.statusLbl.Text = "ステータス";
            // 
            // scrshotFolderDlg
            // 
            this.scrshotFolderDlg.Description = "スクリーンショットを保存するフォルダを指定してください。";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(444, 199);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WindowStretch";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftNumW)).EndInit();
            this.notifyIconMenu.ResumeLayout(false);
            this.mainTabs.ResumeLayout(false);
            this.resizeTab.ResumeLayout(false);
            this.resizeTab.PerformLayout();
            this.startTab.ResumeLayout(false);
            this.startTab.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.screenshotTab.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox alwaysTopChkW;
        private System.Windows.Forms.ComboBox modeBoxW;
        private System.Windows.Forms.NumericUpDown leftNumW;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox modeBoxT;
        private System.Windows.Forms.CheckBox alwaysTopChkT;
        private System.Windows.Forms.Timer watchTimer;
        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox allowExcessChkT;
        private System.Windows.Forms.CheckBox allowExcessChkW;
        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TabPage startTab;
        private System.Windows.Forms.TabPage resizeTab;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox appUriTxt;
        private System.Windows.Forms.CheckBox startWithMeChk;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshMItem;
        private System.Windows.Forms.ToolStripMenuItem showToolMitem;
        private System.Windows.Forms.ToolStripMenuItem gameStartMItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMItem;
        private System.Windows.Forms.TabPage screenshotTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox scrshotSaveTxt;
        private System.Windows.Forms.Button selectScrshotFolderBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button takeScrshotBtn;
        private System.Windows.Forms.Label scrshotDragLbl;
        private System.Windows.Forms.CheckBox scrshotTakeAndOpenChk;
        private System.Windows.Forms.FolderBrowserDialog scrshotFolderDlg;
    }
}

