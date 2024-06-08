namespace noka
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetting));
            textBoxNpub = new TextBox();
            textBoxCutLength = new TextBox();
            label1 = new Label();
            trackBarOpacity = new TrackBar();
            checkBoxTopMost = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            checkBoxDisplayTime = new CheckBox();
            label5 = new Label();
            linkLabelIcons8 = new LinkLabel();
            label6 = new Label();
            labelVersion = new Label();
            labelOpacity = new Label();
            checkBoxShowOnlyFollowees = new CheckBox();
            label4 = new Label();
            textBoxCutNameLength = new TextBox();
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).BeginInit();
            SuspendLayout();
            // 
            // textBoxNpub
            // 
            textBoxNpub.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxNpub.BorderStyle = BorderStyle.FixedSingle;
            textBoxNpub.ImeMode = ImeMode.Disable;
            textBoxNpub.Location = new Point(79, 145);
            textBoxNpub.MaxLength = 136;
            textBoxNpub.Name = "textBoxNpub";
            textBoxNpub.PlaceholderText = "npub1 . . .";
            textBoxNpub.Size = new Size(193, 23);
            textBoxNpub.TabIndex = 13;
            // 
            // textBoxCutLength
            // 
            textBoxCutLength.BorderStyle = BorderStyle.FixedSingle;
            textBoxCutLength.ImeMode = ImeMode.Disable;
            textBoxCutLength.Location = new Point(100, 37);
            textBoxCutLength.MaxLength = 4;
            textBoxCutLength.Name = "textBoxCutLength";
            textBoxCutLength.Size = new Size(26, 23);
            textBoxCutLength.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 0;
            label1.Text = "Cut content at";
            // 
            // trackBarOpacity
            // 
            trackBarOpacity.Location = new Point(145, 31);
            trackBarOpacity.Maximum = 100;
            trackBarOpacity.Minimum = 20;
            trackBarOpacity.Name = "trackBarOpacity";
            trackBarOpacity.Size = new Size(127, 45);
            trackBarOpacity.TabIndex = 3;
            trackBarOpacity.TickFrequency = 20;
            trackBarOpacity.Value = 100;
            trackBarOpacity.Scroll += TrackBarOpacity_Scroll;
            // 
            // checkBoxTopMost
            // 
            checkBoxTopMost.AutoSize = true;
            checkBoxTopMost.Location = new Point(12, 12);
            checkBoxTopMost.Name = "checkBoxTopMost";
            checkBoxTopMost.Size = new Size(101, 19);
            checkBoxTopMost.TabIndex = 1;
            checkBoxTopMost.Text = "Always on top";
            checkBoxTopMost.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(145, 13);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 0;
            label2.Text = "Opacity";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 147);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 0;
            label3.Text = "public key";
            // 
            // checkBoxDisplayTime
            // 
            checkBoxDisplayTime.AutoSize = true;
            checkBoxDisplayTime.Location = new Point(12, 95);
            checkBoxDisplayTime.Name = "checkBoxDisplayTime";
            checkBoxDisplayTime.Size = new Size(145, 19);
            checkBoxDisplayTime.TabIndex = 5;
            checkBoxDisplayTime.Text = "Display time and name";
            checkBoxDisplayTime.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.GrayText;
            label5.Location = new Point(99, 237);
            label5.Name = "label5";
            label5.Size = new Size(126, 15);
            label5.TabIndex = 0;
            label5.Text = "Monochrome icons by";
            // 
            // linkLabelIcons8
            // 
            linkLabelIcons8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkLabelIcons8.AutoSize = true;
            linkLabelIcons8.Location = new Point(231, 237);
            linkLabelIcons8.Name = "linkLabelIcons8";
            linkLabelIcons8.Size = new Size(41, 15);
            linkLabelIcons8.TabIndex = 15;
            linkLabelIcons8.TabStop = true;
            linkLabelIcons8.Text = "Icons8";
            linkLabelIcons8.LinkClicked += LinkLabelIcons8_LinkClicked;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(100, 121);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 14;
            // 
            // labelVersion
            // 
            labelVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(12, 237);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(37, 15);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "v0.1.0";
            // 
            // labelOpacity
            // 
            labelOpacity.Location = new Point(231, 13);
            labelOpacity.Name = "labelOpacity";
            labelOpacity.Size = new Size(41, 15);
            labelOpacity.TabIndex = 0;
            labelOpacity.Text = "100%";
            labelOpacity.TextAlign = ContentAlignment.TopRight;
            // 
            // checkBoxShowOnlyFollowees
            // 
            checkBoxShowOnlyFollowees.AutoSize = true;
            checkBoxShowOnlyFollowees.Location = new Point(12, 120);
            checkBoxShowOnlyFollowees.Name = "checkBoxShowOnlyFollowees";
            checkBoxShowOnlyFollowees.Size = new Size(134, 19);
            checkBoxShowOnlyFollowees.TabIndex = 12;
            checkBoxShowOnlyFollowees.Text = "Show only followees";
            checkBoxShowOnlyFollowees.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 69);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 0;
            label4.Text = "Cut name at";
            // 
            // textBoxCutNameLength
            // 
            textBoxCutNameLength.BorderStyle = BorderStyle.FixedSingle;
            textBoxCutNameLength.ImeMode = ImeMode.Disable;
            textBoxCutNameLength.Location = new Point(100, 66);
            textBoxCutNameLength.MaxLength = 4;
            textBoxCutNameLength.Name = "textBoxCutNameLength";
            textBoxCutNameLength.Size = new Size(26, 23);
            textBoxCutNameLength.TabIndex = 4;
            // 
            // FormSetting
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(284, 261);
            Controls.Add(label4);
            Controls.Add(textBoxCutNameLength);
            Controls.Add(checkBoxShowOnlyFollowees);
            Controls.Add(labelOpacity);
            Controls.Add(labelVersion);
            Controls.Add(label6);
            Controls.Add(linkLabelIcons8);
            Controls.Add(label5);
            Controls.Add(checkBoxDisplayTime);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(checkBoxTopMost);
            Controls.Add(trackBarOpacity);
            Controls.Add(label1);
            Controls.Add(textBoxCutLength);
            Controls.Add(textBoxNpub);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(300, 300);
            Name = "FormSetting";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Setting";
            TopMost = true;
            Load += FormSetting_Load;
            KeyDown += FormSetting_KeyDown;
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox textBoxNpub;
        private Label label1;
        internal TextBox textBoxCutLength;
        internal TrackBar trackBarOpacity;
        internal CheckBox checkBoxTopMost;
        private Label label2;
        private Label label3;
        internal CheckBox checkBoxDisplayTime;
        private Label label5;
        private LinkLabel linkLabelIcons8;
        private Label label6;
        private Label labelVersion;
        private Label labelOpacity;
        internal CheckBox checkBoxShowOnlyFollowees;
        private Label label4;
        internal TextBox textBoxCutNameLength;
    }
}