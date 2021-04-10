
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.leftNumW = new System.Windows.Forms.NumericUpDown();
            this.watchTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.resizeTab = new System.Windows.Forms.TabPage();
            this.startTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.appUriTxt = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.startWithMeChk = new System.Windows.Forms.CheckBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftNumW)).BeginInit();
            this.mainTabs.SuspendLayout();
            this.resizeTab.SuspendLayout();
            this.startTab.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(3, 3);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(75, 23);
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
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(408, 167);
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(198, 126);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
            this.modeBoxT.Location = new System.Drawing.Point(3, 50);
            this.modeBoxT.Name = "modeBoxT";
            this.modeBoxT.Size = new System.Drawing.Size(192, 23);
            this.modeBoxT.TabIndex = 1;
            this.modeBoxT.SelectedIndexChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // alwaysTopChkT
            // 
            this.alwaysTopChkT.AutoSize = true;
            this.alwaysTopChkT.Location = new System.Drawing.Point(3, 79);
            this.alwaysTopChkT.Name = "alwaysTopChkT";
            this.alwaysTopChkT.Size = new System.Drawing.Size(62, 19);
            this.alwaysTopChkT.TabIndex = 2;
            this.alwaysTopChkT.Text = "最前面";
            this.alwaysTopChkT.UseVisualStyleBackColor = true;
            this.alwaysTopChkT.CheckedChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // allowExcessChkT
            // 
            this.allowExcessChkT.AutoSize = true;
            this.allowExcessChkT.Location = new System.Drawing.Point(3, 104);
            this.allowExcessChkT.Name = "allowExcessChkT";
            this.allowExcessChkT.Size = new System.Drawing.Size(130, 19);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(207, 38);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(198, 126);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
            this.modeBoxW.Location = new System.Drawing.Point(3, 50);
            this.modeBoxW.Name = "modeBoxW";
            this.modeBoxW.Size = new System.Drawing.Size(192, 23);
            this.modeBoxW.TabIndex = 1;
            this.modeBoxW.SelectedIndexChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // alwaysTopChkW
            // 
            this.alwaysTopChkW.AutoSize = true;
            this.alwaysTopChkW.Location = new System.Drawing.Point(3, 79);
            this.alwaysTopChkW.Name = "alwaysTopChkW";
            this.alwaysTopChkW.Size = new System.Drawing.Size(62, 19);
            this.alwaysTopChkW.TabIndex = 2;
            this.alwaysTopChkW.Text = "最前面";
            this.alwaysTopChkW.UseVisualStyleBackColor = true;
            this.alwaysTopChkW.CheckedChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // allowExcessChkW
            // 
            this.allowExcessChkW.AutoSize = true;
            this.allowExcessChkW.Location = new System.Drawing.Point(3, 104);
            this.allowExcessChkW.Name = "allowExcessChkW";
            this.allowExcessChkW.Size = new System.Drawing.Size(130, 19);
            this.allowExcessChkW.TabIndex = 3;
            this.allowExcessChkW.Text = "少しのはみ出しを許容";
            this.allowExcessChkW.UseVisualStyleBackColor = true;
            this.allowExcessChkW.CheckedChanged += new System.EventHandler(this.updateBtn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.updateBtn);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(81, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // leftNumW
            // 
            this.leftNumW.Location = new System.Drawing.Point(157, 3);
            this.leftNumW.Name = "leftNumW";
            this.leftNumW.Size = new System.Drawing.Size(120, 23);
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
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "WindowStretch";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // mainTabs
            // 
            this.mainTabs.Controls.Add(this.resizeTab);
            this.mainTabs.Controls.Add(this.startTab);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Location = new System.Drawing.Point(0, 0);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(425, 228);
            this.mainTabs.TabIndex = 3;
            // 
            // resizeTab
            // 
            this.resizeTab.Controls.Add(this.tableLayoutPanel1);
            this.resizeTab.Location = new System.Drawing.Point(4, 24);
            this.resizeTab.Name = "resizeTab";
            this.resizeTab.Padding = new System.Windows.Forms.Padding(3);
            this.resizeTab.Size = new System.Drawing.Size(417, 200);
            this.resizeTab.TabIndex = 1;
            this.resizeTab.Text = "サイズの自動調整";
            this.resizeTab.UseVisualStyleBackColor = true;
            // 
            // startTab
            // 
            this.startTab.Controls.Add(this.tableLayoutPanel3);
            this.startTab.Location = new System.Drawing.Point(4, 24);
            this.startTab.Name = "startTab";
            this.startTab.Padding = new System.Windows.Forms.Padding(3);
            this.startTab.Size = new System.Drawing.Size(417, 200);
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(411, 54);
            this.tableLayoutPanel3.TabIndex = 3;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // appUriTxt
            // 
            this.appUriTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appUriTxt.Location = new System.Drawing.Point(3, 3);
            this.appUriTxt.MinimumSize = new System.Drawing.Size(300, 4);
            this.appUriTxt.Name = "appUriTxt";
            this.appUriTxt.Size = new System.Drawing.Size(324, 23);
            this.appUriTxt.TabIndex = 0;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(333, 3);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "起動する";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // startWithMeChk
            // 
            this.startWithMeChk.AutoSize = true;
            this.startWithMeChk.Location = new System.Drawing.Point(3, 32);
            this.startWithMeChk.Name = "startWithMeChk";
            this.startWithMeChk.Size = new System.Drawing.Size(155, 19);
            this.startWithMeChk.TabIndex = 2;
            this.startWithMeChk.Text = "ツール起動時に一緒に起動";
            this.startWithMeChk.UseVisualStyleBackColor = true;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.statusBar.Location = new System.Drawing.Point(0, 206);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(425, 22);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(425, 228);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WindowStretch";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Resize);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftNumW)).EndInit();
            this.mainTabs.ResumeLayout(false);
            this.resizeTab.ResumeLayout(false);
            this.resizeTab.PerformLayout();
            this.startTab.ResumeLayout(false);
            this.startTab.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
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
    }
}

