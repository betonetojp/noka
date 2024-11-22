namespace noka
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            buttonStart = new Button();
            textBoxTimeline = new TextBox();
            buttonStop = new Button();
            buttonSetting = new Button();
            buttonRelayList = new Button();
            labelRelays = new Label();
            toolTipRelays = new ToolTip(components);
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            formOpenToolStripMenuItem = new ToolStripMenuItem();
            settingToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStart.Image = Properties.Resources.icons8_start_16;
            buttonStart.Location = new Point(211, 246);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(23, 23);
            buttonStart.TabIndex = 2;
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += ButtonStart_Click;
            // 
            // textBoxTimeline
            // 
            textBoxTimeline.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTimeline.BackColor = SystemColors.Control;
            textBoxTimeline.BorderStyle = BorderStyle.FixedSingle;
            textBoxTimeline.Location = new Point(12, 12);
            textBoxTimeline.MaxLength = 0;
            textBoxTimeline.Multiline = true;
            textBoxTimeline.Name = "textBoxTimeline";
            textBoxTimeline.ReadOnly = true;
            textBoxTimeline.ScrollBars = ScrollBars.Vertical;
            textBoxTimeline.Size = new Size(280, 228);
            textBoxTimeline.TabIndex = 0;
            textBoxTimeline.TabStop = false;
            textBoxTimeline.MouseEnter += TextBoxTimeline_MouseEnter;
            textBoxTimeline.MouseLeave += TextBoxTimeline_MouseLeave;
            // 
            // buttonStop
            // 
            buttonStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStop.Enabled = false;
            buttonStop.Image = Properties.Resources.icons8_stop_16;
            buttonStop.Location = new Point(240, 246);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(23, 23);
            buttonStop.TabIndex = 3;
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += ButtonStop_Click;
            // 
            // buttonSetting
            // 
            buttonSetting.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSetting.Image = Properties.Resources.icons8_setting_16;
            buttonSetting.Location = new Point(269, 246);
            buttonSetting.Name = "buttonSetting";
            buttonSetting.Size = new Size(23, 23);
            buttonSetting.TabIndex = 4;
            buttonSetting.UseVisualStyleBackColor = true;
            buttonSetting.Click += ButtonSetting_Click;
            // 
            // buttonRelayList
            // 
            buttonRelayList.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonRelayList.Image = Properties.Resources.icons8_list_16;
            buttonRelayList.Location = new Point(182, 246);
            buttonRelayList.Name = "buttonRelayList";
            buttonRelayList.Size = new Size(23, 23);
            buttonRelayList.TabIndex = 1;
            buttonRelayList.UseVisualStyleBackColor = true;
            buttonRelayList.Click += ButtonRelayList_Click;
            // 
            // labelRelays
            // 
            labelRelays.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelRelays.AutoEllipsis = true;
            labelRelays.ForeColor = SystemColors.GrayText;
            labelRelays.Location = new Point(12, 250);
            labelRelays.Name = "labelRelays";
            labelRelays.Size = new Size(164, 15);
            labelRelays.TabIndex = 0;
            labelRelays.Text = "Relay info";
            labelRelays.TextAlign = ContentAlignment.TopRight;
            labelRelays.MouseClick += FormMain_MouseClick;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "noka";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { formOpenToolStripMenuItem, settingToolStripMenuItem, toolStripMenuItem1, closeToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(132, 76);
            // 
            // formOpenToolStripMenuItem
            // 
            formOpenToolStripMenuItem.Name = "formOpenToolStripMenuItem";
            formOpenToolStripMenuItem.Size = new Size(131, 22);
            formOpenToolStripMenuItem.Text = "Form open";
            formOpenToolStripMenuItem.Click += FormOpenToolStripMenuItem_Click;
            // 
            // settingToolStripMenuItem
            // 
            settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            settingToolStripMenuItem.Size = new Size(131, 22);
            settingToolStripMenuItem.Text = "Setting";
            settingToolStripMenuItem.Click += SettingToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(128, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(131, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += CloseToolStripMenuItem_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(304, 281);
            Controls.Add(labelRelays);
            Controls.Add(buttonRelayList);
            Controls.Add(buttonSetting);
            Controls.Add(buttonStop);
            Controls.Add(textBoxTimeline);
            Controls.Add(buttonStart);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MinimumSize = new Size(240, 240);
            Name = "FormMain";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.Manual;
            Text = "noka";
            TopMost = true;
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            SizeChanged += FormMain_SizeChanged;
            KeyDown += FormMain_KeyDown;
            MouseClick += FormMain_MouseClick;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonStart;
        private TextBox textBoxTimeline;
        private Button buttonStop;
        private Button buttonSetting;
        private Button buttonRelayList;
        private Label labelRelays;
        private ToolTip toolTipRelays;
        internal NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem formOpenToolStripMenuItem;
        private ToolStripMenuItem settingToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
    }
}
