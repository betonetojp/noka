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
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonStart.Image = Properties.Resources.icons8_start_16;
            buttonStart.Location = new Point(131, 12);
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
            textBoxTimeline.Location = new Point(12, 41);
            textBoxTimeline.MaxLength = 0;
            textBoxTimeline.Multiline = true;
            textBoxTimeline.Name = "textBoxTimeline";
            textBoxTimeline.ReadOnly = true;
            textBoxTimeline.ScrollBars = ScrollBars.Vertical;
            textBoxTimeline.Size = new Size(200, 148);
            textBoxTimeline.TabIndex = 5;
            textBoxTimeline.TabStop = false;
            textBoxTimeline.MouseEnter += TextBoxTimeline_MouseEnter;
            textBoxTimeline.MouseLeave += TextBoxTimeline_MouseLeave;
            // 
            // buttonStop
            // 
            buttonStop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonStop.Enabled = false;
            buttonStop.Image = Properties.Resources.icons8_stop_16;
            buttonStop.Location = new Point(160, 12);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(23, 23);
            buttonStop.TabIndex = 3;
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += ButtonStop_Click;
            // 
            // buttonSetting
            // 
            buttonSetting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSetting.Image = Properties.Resources.icons8_setting_16;
            buttonSetting.Location = new Point(189, 12);
            buttonSetting.Name = "buttonSetting";
            buttonSetting.Size = new Size(23, 23);
            buttonSetting.TabIndex = 4;
            buttonSetting.UseVisualStyleBackColor = true;
            buttonSetting.Click += ButtonSetting_Click;
            // 
            // buttonRelayList
            // 
            buttonRelayList.Image = Properties.Resources.icons8_list_16;
            buttonRelayList.Location = new Point(12, 12);
            buttonRelayList.Name = "buttonRelayList";
            buttonRelayList.Size = new Size(23, 23);
            buttonRelayList.TabIndex = 1;
            buttonRelayList.UseVisualStyleBackColor = true;
            buttonRelayList.Click += ButtonRelayList_Click;
            // 
            // labelRelays
            // 
            labelRelays.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelRelays.AutoEllipsis = true;
            labelRelays.ForeColor = SystemColors.GrayText;
            labelRelays.Location = new Point(41, 16);
            labelRelays.Name = "labelRelays";
            labelRelays.Size = new Size(84, 15);
            labelRelays.TabIndex = 0;
            labelRelays.Text = "Relay info";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(224, 201);
            Controls.Add(labelRelays);
            Controls.Add(buttonRelayList);
            Controls.Add(buttonSetting);
            Controls.Add(buttonStop);
            Controls.Add(textBoxTimeline);
            Controls.Add(buttonStart);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MinimumSize = new Size(200, 200);
            Name = "FormMain";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.Manual;
            Text = "noka";
            TopMost = true;
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            KeyDown += FormMain_KeyDown;
            MouseClick += FormMain_MouseClick;
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
    }
}
