﻿namespace noka
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetting));
            textBoxNpub = new TextBox();
            textBoxCutLength = new TextBox();
            label1 = new Label();
            trackBarOpacity = new TrackBar();
            checkBoxTopMost = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            linkLabelIcons8 = new LinkLabel();
            label6 = new Label();
            labelVersion = new Label();
            labelOpacity = new Label();
            checkBoxShowOnlyFollowees = new CheckBox();
            label4 = new Label();
            textBoxCutNameLength = new TextBox();
            comboBoxGhosts = new ComboBox();
            label7 = new Label();
            checkBoxSoleGhostsOnly = new CheckBox();
            tabControlSettings = new TabControl();
            tabPage1 = new TabPage();
            checkBoxChat = new CheckBox();
            checkBoxReaction = new CheckBox();
            checkBoxNote = new CheckBox();
            label9 = new Label();
            checkBoxMinimizeToTray = new CheckBox();
            tabPage2 = new TabPage();
            label8 = new Label();
            textBoxDefaultPicture = new TextBox();
            buttonReload = new Button();
            buttonSave = new Button();
            buttonDelete = new Button();
            dataGridViewSoloGhosts = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).BeginInit();
            tabControlSettings.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSoloGhosts).BeginInit();
            SuspendLayout();
            // 
            // textBoxNpub
            // 
            textBoxNpub.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxNpub.BorderStyle = BorderStyle.FixedSingle;
            textBoxNpub.ImeMode = ImeMode.Disable;
            textBoxNpub.Location = new Point(73, 139);
            textBoxNpub.MaxLength = 136;
            textBoxNpub.Name = "textBoxNpub";
            textBoxNpub.PlaceholderText = "npub1 . . .";
            textBoxNpub.Size = new Size(191, 23);
            textBoxNpub.TabIndex = 8;
            // 
            // textBoxCutLength
            // 
            textBoxCutLength.BorderStyle = BorderStyle.FixedSingle;
            textBoxCutLength.ImeMode = ImeMode.Disable;
            textBoxCutLength.Location = new Point(94, 6);
            textBoxCutLength.MaxLength = 4;
            textBoxCutLength.Name = "textBoxCutLength";
            textBoxCutLength.Size = new Size(26, 23);
            textBoxCutLength.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 8);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 0;
            label1.Text = "Cut content at";
            // 
            // trackBarOpacity
            // 
            trackBarOpacity.BackColor = SystemColors.ButtonFace;
            trackBarOpacity.Location = new Point(126, 26);
            trackBarOpacity.Maximum = 100;
            trackBarOpacity.Minimum = 20;
            trackBarOpacity.Name = "trackBarOpacity";
            trackBarOpacity.Size = new Size(118, 45);
            trackBarOpacity.TabIndex = 3;
            trackBarOpacity.TickFrequency = 20;
            trackBarOpacity.Value = 100;
            trackBarOpacity.Scroll += TrackBarOpacity_Scroll;
            // 
            // checkBoxTopMost
            // 
            checkBoxTopMost.AutoSize = true;
            checkBoxTopMost.Location = new Point(6, 64);
            checkBoxTopMost.Name = "checkBoxTopMost";
            checkBoxTopMost.Size = new Size(101, 19);
            checkBoxTopMost.TabIndex = 5;
            checkBoxTopMost.Text = "Always on top";
            checkBoxTopMost.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(126, 8);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 0;
            label2.Text = "Opacity";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 141);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 0;
            label3.Text = "public key";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.GrayText;
            label5.Location = new Point(91, 209);
            label5.Name = "label5";
            label5.Size = new Size(126, 15);
            label5.TabIndex = 0;
            label5.Text = "Monochrome icons by";
            // 
            // linkLabelIcons8
            // 
            linkLabelIcons8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            linkLabelIcons8.AutoSize = true;
            linkLabelIcons8.Location = new Point(223, 209);
            linkLabelIcons8.Name = "linkLabelIcons8";
            linkLabelIcons8.Size = new Size(41, 15);
            linkLabelIcons8.TabIndex = 12;
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
            labelVersion.Location = new Point(6, 209);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(37, 15);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "v0.6.0";
            // 
            // labelOpacity
            // 
            labelOpacity.Location = new Point(203, 8);
            labelOpacity.Name = "labelOpacity";
            labelOpacity.Size = new Size(41, 15);
            labelOpacity.TabIndex = 0;
            labelOpacity.Text = "100%";
            labelOpacity.TextAlign = ContentAlignment.TopRight;
            // 
            // checkBoxShowOnlyFollowees
            // 
            checkBoxShowOnlyFollowees.AutoSize = true;
            checkBoxShowOnlyFollowees.Location = new Point(6, 89);
            checkBoxShowOnlyFollowees.Name = "checkBoxShowOnlyFollowees";
            checkBoxShowOnlyFollowees.Size = new Size(134, 19);
            checkBoxShowOnlyFollowees.TabIndex = 6;
            checkBoxShowOnlyFollowees.Text = "Show only followees";
            checkBoxShowOnlyFollowees.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 37);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 0;
            label4.Text = "Cut name at";
            // 
            // textBoxCutNameLength
            // 
            textBoxCutNameLength.BorderStyle = BorderStyle.FixedSingle;
            textBoxCutNameLength.ImeMode = ImeMode.Disable;
            textBoxCutNameLength.Location = new Point(94, 35);
            textBoxCutNameLength.MaxLength = 4;
            textBoxCutNameLength.Name = "textBoxCutNameLength";
            textBoxCutNameLength.Size = new Size(26, 23);
            textBoxCutNameLength.TabIndex = 4;
            // 
            // comboBoxGhosts
            // 
            comboBoxGhosts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxGhosts.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGhosts.FormattingEnabled = true;
            comboBoxGhosts.Location = new Point(79, 35);
            comboBoxGhosts.Name = "comboBoxGhosts";
            comboBoxGhosts.Size = new Size(156, 23);
            comboBoxGhosts.TabIndex = 10;
            comboBoxGhosts.SelectionChangeCommitted += comboBoxGhosts_SelectionChangeCommitted;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 38);
            label7.Name = "label7";
            label7.Size = new Size(67, 15);
            label7.TabIndex = 0;
            label7.Text = "Main ghost";
            // 
            // checkBoxSoleGhostsOnly
            // 
            checkBoxSoleGhostsOnly.AutoSize = true;
            checkBoxSoleGhostsOnly.Location = new Point(6, 64);
            checkBoxSoleGhostsOnly.Name = "checkBoxSoleGhostsOnly";
            checkBoxSoleGhostsOnly.Size = new Size(190, 19);
            checkBoxSoleGhostsOnly.TabIndex = 12;
            checkBoxSoleGhostsOnly.Text = "Send DSSTP to sole ghosts only";
            checkBoxSoleGhostsOnly.UseVisualStyleBackColor = true;
            // 
            // tabControlSettings
            // 
            tabControlSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlSettings.Controls.Add(tabPage1);
            tabControlSettings.Controls.Add(tabPage2);
            tabControlSettings.Location = new Point(12, 12);
            tabControlSettings.Name = "tabControlSettings";
            tabControlSettings.SelectedIndex = 0;
            tabControlSettings.Size = new Size(280, 257);
            tabControlSettings.TabIndex = 1;
            tabControlSettings.SelectedIndexChanged += TabControlSettings_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(checkBoxChat);
            tabPage1.Controls.Add(checkBoxReaction);
            tabPage1.Controls.Add(checkBoxNote);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(checkBoxMinimizeToTray);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(textBoxCutLength);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(textBoxCutNameLength);
            tabPage1.Controls.Add(checkBoxTopMost);
            tabPage1.Controls.Add(checkBoxShowOnlyFollowees);
            tabPage1.Controls.Add(trackBarOpacity);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(labelOpacity);
            tabPage1.Controls.Add(labelVersion);
            tabPage1.Controls.Add(linkLabelIcons8);
            tabPage1.Controls.Add(textBoxNpub);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(272, 229);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            // 
            // checkBoxChat
            // 
            checkBoxChat.AutoSize = true;
            checkBoxChat.Location = new Point(200, 169);
            checkBoxChat.Name = "checkBoxChat";
            checkBoxChat.Size = new Size(64, 19);
            checkBoxChat.TabIndex = 11;
            checkBoxChat.Text = "42:chat";
            checkBoxChat.UseVisualStyleBackColor = true;
            // 
            // checkBoxReaction
            // 
            checkBoxReaction.AutoSize = true;
            checkBoxReaction.Location = new Point(116, 168);
            checkBoxReaction.Name = "checkBoxReaction";
            checkBoxReaction.Size = new Size(78, 19);
            checkBoxReaction.TabIndex = 10;
            checkBoxReaction.Text = "7:reaction";
            checkBoxReaction.UseVisualStyleBackColor = true;
            // 
            // checkBoxNote
            // 
            checkBoxNote.AutoSize = true;
            checkBoxNote.Location = new Point(51, 168);
            checkBoxNote.Name = "checkBoxNote";
            checkBoxNote.Size = new Size(59, 19);
            checkBoxNote.TabIndex = 9;
            checkBoxNote.Text = "1:note";
            checkBoxNote.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 169);
            label9.Name = "label9";
            label9.Size = new Size(35, 15);
            label9.TabIndex = 15;
            label9.Text = "kinds";
            // 
            // checkBoxMinimizeToTray
            // 
            checkBoxMinimizeToTray.AutoSize = true;
            checkBoxMinimizeToTray.Location = new Point(6, 114);
            checkBoxMinimizeToTray.Name = "checkBoxMinimizeToTray";
            checkBoxMinimizeToTray.Size = new Size(150, 19);
            checkBoxMinimizeToTray.TabIndex = 7;
            checkBoxMinimizeToTray.Text = "Minimize to system tray";
            checkBoxMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(textBoxDefaultPicture);
            tabPage2.Controls.Add(buttonReload);
            tabPage2.Controls.Add(buttonSave);
            tabPage2.Controls.Add(buttonDelete);
            tabPage2.Controls.Add(dataGridViewSoloGhosts);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(comboBoxGhosts);
            tabPage2.Controls.Add(checkBoxSoleGhostsOnly);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(272, 229);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "SSP ghosts";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 8);
            label8.Name = "label8";
            label8.Size = new Size(67, 15);
            label8.TabIndex = 0;
            label8.Text = "Default pic.";
            // 
            // textBoxDefaultPicture
            // 
            textBoxDefaultPicture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDefaultPicture.BorderStyle = BorderStyle.FixedSingle;
            textBoxDefaultPicture.ImeMode = ImeMode.Disable;
            textBoxDefaultPicture.Location = new Point(79, 6);
            textBoxDefaultPicture.MaxLength = 136;
            textBoxDefaultPicture.Name = "textBoxDefaultPicture";
            textBoxDefaultPicture.Size = new Size(185, 23);
            textBoxDefaultPicture.TabIndex = 9;
            textBoxDefaultPicture.Text = "https://robohash.org/{npub}?set=set4&size=128x128";
            // 
            // buttonReload
            // 
            buttonReload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonReload.Image = Properties.Resources.icons8_reload_16;
            buttonReload.Location = new Point(241, 35);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(23, 23);
            buttonReload.TabIndex = 11;
            buttonReload.UseVisualStyleBackColor = true;
            buttonReload.Click += buttonReload_Click;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSave.Location = new Point(189, 198);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 15;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += ButtonSave_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDelete.Location = new Point(6, 198);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 14;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // dataGridViewSoloGhosts
            // 
            dataGridViewSoloGhosts.AllowUserToResizeRows = false;
            dataGridViewSoloGhosts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Yu Gothic UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewSoloGhosts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewSoloGhosts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSoloGhosts.Location = new Point(6, 89);
            dataGridViewSoloGhosts.MultiSelect = false;
            dataGridViewSoloGhosts.Name = "dataGridViewSoloGhosts";
            dataGridViewSoloGhosts.RowHeadersVisible = false;
            dataGridViewSoloGhosts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSoloGhosts.Size = new Size(258, 103);
            dataGridViewSoloGhosts.TabIndex = 13;
            // 
            // FormSetting
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(304, 281);
            Controls.Add(tabControlSettings);
            Controls.Add(label6);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(320, 320);
            Name = "FormSetting";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Setting";
            TopMost = true;
            Load += FormSetting_Load;
            Shown += FormSetting_Shown;
            KeyDown += FormSetting_KeyDown;
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).EndInit();
            tabControlSettings.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSoloGhosts).EndInit();
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
        private Label label5;
        private LinkLabel linkLabelIcons8;
        private Label label6;
        private Label labelVersion;
        private Label labelOpacity;
        internal CheckBox checkBoxShowOnlyFollowees;
        private Label label4;
        internal TextBox textBoxCutNameLength;
        private ComboBox comboBoxGhosts;
        private Label label7;
        internal CheckBox checkBoxSoleGhostsOnly;
        private TabControl tabControlSettings;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridViewSoloGhosts;
        private Button buttonDelete;
        private Button buttonSave;
        private Button buttonReload;
        private Label label8;
        internal TextBox textBoxDefaultPicture;
        internal CheckBox checkBoxMinimizeToTray;
        private Label label9;
        internal CheckBox checkBoxReaction;
        internal CheckBox checkBoxNote;
        internal CheckBox checkBoxChat;
    }
}