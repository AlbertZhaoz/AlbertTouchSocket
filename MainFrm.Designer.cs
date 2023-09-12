namespace AlbertTouchSocket
{
    partial class MainFrm
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
            components = new System.ComponentModel.Container();
            uiStyleManager = new Sunny.UI.UIStyleManager(components);
            uiContextMenuStrip = new Sunny.UI.UIContextMenuStrip();
            主题ToolStripMenuItem = new ToolStripMenuItem();
            经典蓝ToolStripMenuItem = new ToolStripMenuItem();
            时尚黑ToolStripMenuItem = new ToolStripMenuItem();
            蓝灰色ToolStripMenuItem = new ToolStripMenuItem();
            更新ToolStripMenuItem = new ToolStripMenuItem();
            关于ToolStripMenuItem = new ToolStripMenuItem();
            uiSplitContainer = new Sunny.UI.UISplitContainer();
            uiPanel2 = new Sunny.UI.UIPanel();
            uiSplitContainer1 = new Sunny.UI.UISplitContainer();
            uiListBoxServe = new Sunny.UI.UIListBox();
            uiPanel3 = new Sunny.UI.UIPanel();
            uiButtonEndServe = new Sunny.UI.UIButton();
            uiButtonStartServe = new Sunny.UI.UIButton();
            uiButtonSend = new Sunny.UI.UIButton();
            uiButtonClientDisConnect = new Sunny.UI.UIButton();
            uiButtonClientConnect = new Sunny.UI.UIButton();
            uiRichTextBoxClient = new Sunny.UI.UIRichTextBox();
            uiTextBoxPort = new Sunny.UI.UITextBox();
            uiipTextBoxIp = new Sunny.UI.UIIPTextBox();
            uiListBoxClientLog = new Sunny.UI.UIListBox();
            uiContextMenuStrip.SuspendLayout();
            uiSplitContainer.BeginInit();
            uiSplitContainer.Panel1.SuspendLayout();
            uiSplitContainer.Panel2.SuspendLayout();
            uiSplitContainer.SuspendLayout();
            uiPanel2.SuspendLayout();
            uiSplitContainer1.BeginInit();
            uiSplitContainer1.Panel1.SuspendLayout();
            uiSplitContainer1.Panel2.SuspendLayout();
            uiSplitContainer1.SuspendLayout();
            uiPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // uiContextMenuStrip
            // 
            uiContextMenuStrip.BackColor = Color.FromArgb(243, 249, 255);
            uiContextMenuStrip.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiContextMenuStrip.ImageScalingSize = new Size(20, 20);
            uiContextMenuStrip.Items.AddRange(new ToolStripItem[] { 主题ToolStripMenuItem, 更新ToolStripMenuItem, 关于ToolStripMenuItem });
            uiContextMenuStrip.Name = "uiContextMenuStrip";
            uiContextMenuStrip.Size = new Size(125, 100);
            // 
            // 主题ToolStripMenuItem
            // 
            主题ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 经典蓝ToolStripMenuItem, 时尚黑ToolStripMenuItem, 蓝灰色ToolStripMenuItem });
            主题ToolStripMenuItem.Name = "主题ToolStripMenuItem";
            主题ToolStripMenuItem.Size = new Size(124, 32);
            主题ToolStripMenuItem.Text = "主题";
            // 
            // 经典蓝ToolStripMenuItem
            // 
            经典蓝ToolStripMenuItem.Name = "经典蓝ToolStripMenuItem";
            经典蓝ToolStripMenuItem.Size = new Size(158, 32);
            经典蓝ToolStripMenuItem.Text = "经典蓝";
            经典蓝ToolStripMenuItem.Click += 经典蓝ToolStripMenuItem_Click;
            // 
            // 时尚黑ToolStripMenuItem
            // 
            时尚黑ToolStripMenuItem.Name = "时尚黑ToolStripMenuItem";
            时尚黑ToolStripMenuItem.Size = new Size(158, 32);
            时尚黑ToolStripMenuItem.Text = "时尚黑";
            时尚黑ToolStripMenuItem.Click += 时尚黑ToolStripMenuItem_Click;
            // 
            // 蓝灰色ToolStripMenuItem
            // 
            蓝灰色ToolStripMenuItem.Name = "蓝灰色ToolStripMenuItem";
            蓝灰色ToolStripMenuItem.Size = new Size(158, 32);
            蓝灰色ToolStripMenuItem.Text = "蓝灰色";
            蓝灰色ToolStripMenuItem.Click += 蓝灰色ToolStripMenuItem_Click;
            // 
            // 更新ToolStripMenuItem
            // 
            更新ToolStripMenuItem.Name = "更新ToolStripMenuItem";
            更新ToolStripMenuItem.Size = new Size(124, 32);
            更新ToolStripMenuItem.Text = "更新";
            // 
            // 关于ToolStripMenuItem
            // 
            关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            关于ToolStripMenuItem.Size = new Size(124, 32);
            关于ToolStripMenuItem.Text = "关于";
            // 
            // uiSplitContainer
            // 
            uiSplitContainer.Dock = DockStyle.Fill;
            uiSplitContainer.Location = new Point(0, 35);
            uiSplitContainer.MinimumSize = new Size(20, 20);
            uiSplitContainer.Name = "uiSplitContainer";
            // 
            // uiSplitContainer.Panel1
            // 
            uiSplitContainer.Panel1.Controls.Add(uiPanel2);
            // 
            // uiSplitContainer.Panel2
            // 
            uiSplitContainer.Panel2.Controls.Add(uiListBoxClientLog);
            uiSplitContainer.Size = new Size(1333, 697);
            uiSplitContainer.SplitterDistance = 545;
            uiSplitContainer.SplitterWidth = 11;
            uiSplitContainer.TabIndex = 1;
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(uiSplitContainer1);
            uiPanel2.Dock = DockStyle.Fill;
            uiPanel2.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiPanel2.Location = new Point(0, 0);
            uiPanel2.Margin = new Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Size = new Size(545, 697);
            uiPanel2.TabIndex = 1;
            uiPanel2.Text = "uiPanel";
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiSplitContainer1
            // 
            uiSplitContainer1.Dock = DockStyle.Fill;
            uiSplitContainer1.Location = new Point(0, 0);
            uiSplitContainer1.MinimumSize = new Size(20, 20);
            uiSplitContainer1.Name = "uiSplitContainer1";
            uiSplitContainer1.Orientation = Orientation.Horizontal;
            // 
            // uiSplitContainer1.Panel1
            // 
            uiSplitContainer1.Panel1.Controls.Add(uiListBoxServe);
            uiSplitContainer1.Panel1.Controls.Add(uiPanel3);
            // 
            // uiSplitContainer1.Panel2
            // 
            uiSplitContainer1.Panel2.Controls.Add(uiButtonSend);
            uiSplitContainer1.Panel2.Controls.Add(uiButtonClientDisConnect);
            uiSplitContainer1.Panel2.Controls.Add(uiButtonClientConnect);
            uiSplitContainer1.Panel2.Controls.Add(uiRichTextBoxClient);
            uiSplitContainer1.Panel2.Controls.Add(uiTextBoxPort);
            uiSplitContainer1.Panel2.Controls.Add(uiipTextBoxIp);
            uiSplitContainer1.Size = new Size(545, 697);
            uiSplitContainer1.SplitterDistance = 335;
            uiSplitContainer1.SplitterWidth = 11;
            uiSplitContainer1.TabIndex = 0;
            // 
            // uiListBoxServe
            // 
            uiListBoxServe.Dock = DockStyle.Fill;
            uiListBoxServe.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiListBoxServe.HoverColor = Color.FromArgb(155, 200, 255);
            uiListBoxServe.ItemSelectForeColor = Color.White;
            uiListBoxServe.Location = new Point(0, 76);
            uiListBoxServe.Margin = new Padding(4, 5, 4, 5);
            uiListBoxServe.MinimumSize = new Size(1, 1);
            uiListBoxServe.Name = "uiListBoxServe";
            uiListBoxServe.Padding = new Padding(2);
            uiListBoxServe.ShowText = false;
            uiListBoxServe.Size = new Size(545, 259);
            uiListBoxServe.Style = Sunny.UI.UIStyle.Custom;
            uiListBoxServe.TabIndex = 3;
            uiListBoxServe.Text = "uiListBox";
            // 
            // uiPanel3
            // 
            uiPanel3.Controls.Add(uiButtonEndServe);
            uiPanel3.Controls.Add(uiButtonStartServe);
            uiPanel3.Dock = DockStyle.Top;
            uiPanel3.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiPanel3.Location = new Point(0, 0);
            uiPanel3.Margin = new Padding(4, 5, 4, 5);
            uiPanel3.MinimumSize = new Size(1, 1);
            uiPanel3.Name = "uiPanel3";
            uiPanel3.Size = new Size(545, 76);
            uiPanel3.TabIndex = 2;
            uiPanel3.Text = null;
            uiPanel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiButtonEndServe
            // 
            uiButtonEndServe.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonEndServe.Location = new Point(146, 14);
            uiButtonEndServe.MinimumSize = new Size(1, 1);
            uiButtonEndServe.Name = "uiButtonEndServe";
            uiButtonEndServe.Radius = 44;
            uiButtonEndServe.Size = new Size(105, 44);
            uiButtonEndServe.TabIndex = 2;
            uiButtonEndServe.Text = "断开服务";
            uiButtonEndServe.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonEndServe.Click += uiButtonEndServe_Click;
            // 
            // uiButtonStartServe
            // 
            uiButtonStartServe.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonStartServe.Location = new Point(18, 14);
            uiButtonStartServe.MinimumSize = new Size(1, 1);
            uiButtonStartServe.Name = "uiButtonStartServe";
            uiButtonStartServe.Radius = 44;
            uiButtonStartServe.Size = new Size(105, 44);
            uiButtonStartServe.TabIndex = 1;
            uiButtonStartServe.Text = "开启服务";
            uiButtonStartServe.Click += uiButtonStartServe_Click;
            // 
            // uiButtonSend
            // 
            uiButtonSend.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonSend.Location = new Point(408, 147);
            uiButtonSend.MinimumSize = new Size(1, 1);
            uiButtonSend.Name = "uiButtonSend";
            uiButtonSend.Radius = 44;
            uiButtonSend.Size = new Size(105, 44);
            uiButtonSend.TabIndex = 5;
            uiButtonSend.Text = "发送";
            uiButtonSend.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonSend.Click += uiButtonSend_Click;
            // 
            // uiButtonClientDisConnect
            // 
            uiButtonClientDisConnect.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonClientDisConnect.Location = new Point(408, 84);
            uiButtonClientDisConnect.MinimumSize = new Size(1, 1);
            uiButtonClientDisConnect.Name = "uiButtonClientDisConnect";
            uiButtonClientDisConnect.Radius = 44;
            uiButtonClientDisConnect.Size = new Size(105, 44);
            uiButtonClientDisConnect.TabIndex = 4;
            uiButtonClientDisConnect.Text = "断开";
            uiButtonClientDisConnect.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            // 
            // uiButtonClientConnect
            // 
            uiButtonClientConnect.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonClientConnect.Location = new Point(408, 19);
            uiButtonClientConnect.MinimumSize = new Size(1, 1);
            uiButtonClientConnect.Name = "uiButtonClientConnect";
            uiButtonClientConnect.Radius = 44;
            uiButtonClientConnect.Size = new Size(105, 44);
            uiButtonClientConnect.TabIndex = 3;
            uiButtonClientConnect.Text = "连接";
            uiButtonClientConnect.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            uiButtonClientConnect.Click += uiButtonClientConnect_Click;
            // 
            // uiRichTextBoxClient
            // 
            uiRichTextBoxClient.FillColor = Color.White;
            uiRichTextBoxClient.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiRichTextBoxClient.Location = new Point(18, 84);
            uiRichTextBoxClient.Margin = new Padding(4, 5, 4, 5);
            uiRichTextBoxClient.MinimumSize = new Size(1, 1);
            uiRichTextBoxClient.Name = "uiRichTextBoxClient";
            uiRichTextBoxClient.Padding = new Padding(2);
            uiRichTextBoxClient.ShowText = false;
            uiRichTextBoxClient.Size = new Size(338, 225);
            uiRichTextBoxClient.Style = Sunny.UI.UIStyle.Custom;
            uiRichTextBoxClient.TabIndex = 3;
            uiRichTextBoxClient.Text = "TestInfo-666";
            uiRichTextBoxClient.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiTextBoxPort
            // 
            uiTextBoxPort.ButtonSymbolOffset = new Point(0, 0);
            uiTextBoxPort.DoubleValue = 7222D;
            uiTextBoxPort.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiTextBoxPort.IntValue = 7222;
            uiTextBoxPort.Location = new Point(214, 19);
            uiTextBoxPort.Margin = new Padding(4, 5, 4, 5);
            uiTextBoxPort.MinimumSize = new Size(1, 16);
            uiTextBoxPort.Name = "uiTextBoxPort";
            uiTextBoxPort.Padding = new Padding(5);
            uiTextBoxPort.ShowText = false;
            uiTextBoxPort.Size = new Size(92, 36);
            uiTextBoxPort.TabIndex = 2;
            uiTextBoxPort.Text = "7222";
            uiTextBoxPort.TextAlignment = ContentAlignment.MiddleLeft;
            uiTextBoxPort.Watermark = "";
            // 
            // uiipTextBoxIp
            // 
            uiipTextBoxIp.FillColor = Color.FromArgb(243, 249, 255);
            uiipTextBoxIp.FillColor2 = Color.FromArgb(235, 243, 255);
            uiipTextBoxIp.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiipTextBoxIp.Location = new Point(18, 19);
            uiipTextBoxIp.Margin = new Padding(4, 5, 4, 5);
            uiipTextBoxIp.MinimumSize = new Size(1, 1);
            uiipTextBoxIp.Name = "uiipTextBoxIp";
            uiipTextBoxIp.Padding = new Padding(1);
            uiipTextBoxIp.ShowText = false;
            uiipTextBoxIp.Size = new Size(188, 36);
            uiipTextBoxIp.Style = Sunny.UI.UIStyle.Custom;
            uiipTextBoxIp.TabIndex = 0;
            uiipTextBoxIp.Text = "127.0.0.1";
            uiipTextBoxIp.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiListBoxClientLog
            // 
            uiListBoxClientLog.Dock = DockStyle.Fill;
            uiListBoxClientLog.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiListBoxClientLog.HoverColor = Color.FromArgb(155, 200, 255);
            uiListBoxClientLog.ItemSelectForeColor = Color.White;
            uiListBoxClientLog.Location = new Point(0, 0);
            uiListBoxClientLog.Margin = new Padding(4, 5, 4, 5);
            uiListBoxClientLog.MinimumSize = new Size(1, 1);
            uiListBoxClientLog.Name = "uiListBoxClientLog";
            uiListBoxClientLog.Padding = new Padding(2);
            uiListBoxClientLog.ShowText = false;
            uiListBoxClientLog.Size = new Size(777, 697);
            uiListBoxClientLog.Style = Sunny.UI.UIStyle.Custom;
            uiListBoxClientLog.TabIndex = 0;
            uiListBoxClientLog.Text = "uiListBox1";
            // 
            // MainFrm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1333, 732);
            Controls.Add(uiSplitContainer);
            EscClose = true;
            ExtendBox = true;
            ExtendMenu = uiContextMenuStrip;
            Name = "MainFrm";
            Text = "MainFrm";
            ZoomScaleRect = new Rectangle(19, 19, 800, 450);
            uiContextMenuStrip.ResumeLayout(false);
            uiSplitContainer.Panel1.ResumeLayout(false);
            uiSplitContainer.Panel2.ResumeLayout(false);
            uiSplitContainer.EndInit();
            uiSplitContainer.ResumeLayout(false);
            uiPanel2.ResumeLayout(false);
            uiSplitContainer1.Panel1.ResumeLayout(false);
            uiSplitContainer1.Panel2.ResumeLayout(false);
            uiSplitContainer1.EndInit();
            uiSplitContainer1.ResumeLayout(false);
            uiPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIStyleManager uiStyleManager;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip;
        private ToolStripMenuItem 主题ToolStripMenuItem;
        private ToolStripMenuItem 经典蓝ToolStripMenuItem;
        private ToolStripMenuItem 时尚黑ToolStripMenuItem;
        private ToolStripMenuItem 蓝灰色ToolStripMenuItem;
        private ToolStripMenuItem 更新ToolStripMenuItem;
        private ToolStripMenuItem 关于ToolStripMenuItem;
        private Sunny.UI.UISplitContainer uiSplitContainer;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIButton uiButtonEndServe;
        private Sunny.UI.UIButton uiButtonStartServe;
        private Sunny.UI.UISplitContainer uiSplitContainer1;
        private Sunny.UI.UIListBox uiListBoxServe;
        private Sunny.UI.UIIPTextBox uiipTextBoxIp;
        private Sunny.UI.UIRichTextBox uiRichTextBoxClient;
        private Sunny.UI.UITextBox uiTextBoxPort;
        private Sunny.UI.UIButton uiButtonClientDisConnect;
        private Sunny.UI.UIButton uiButtonClientConnect;
        private Sunny.UI.UIButton uiButtonSend;
        private Sunny.UI.UIListBox uiListBoxClientLog;
    }
}