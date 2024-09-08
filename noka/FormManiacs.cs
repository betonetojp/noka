﻿using System.ComponentModel;

namespace noka
{
    public partial class FormManiacs : Form
    {
        internal FormMain? MainForm { get; set; }
        public FormManiacs()
        {
            InitializeComponent();
        }

        private void FormManiacs_Load(object sender, EventArgs e)
        {
            if (MainForm != null)
            {
                dataGridViewUsers.Rows.Clear();
                foreach (var user in MainForm.Users)
                {
                    dataGridViewUsers.Rows.Add(
                        user.Value?.Mute,
                        user.Value?.LastActivity,
                        user.Key,
                        user.Value?.DisplayName,
                        user.Value?.Name,
                        user.Value?.Nip05,
                        user.Value?.Picture,
                        user.Value?.CreatedAt
                        );
                }
                dataGridViewUsers.Sort(dataGridViewUsers.Columns["last_activity"], ListSortDirection.Descending);
                dataGridViewUsers.ClearSelection();
                var settings = MainForm.Notifier.Settings;
                checkBoxOpenFile.Checked = settings.Open;
                textBoxFileName.Text = settings.FileName;
                textBoxKeywords.Text = string.Join("\r\n", settings.Keywords);
                checkBoxMuteMostr.Checked = settings.MuteMostr;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (MainForm != null)
            {
                Dictionary<string, User?> users = [];
                foreach (DataGridViewRow row in dataGridViewUsers.Rows)
                {
                    var pubkey = (string)row.Cells["pubkey"].Value;
                    if (null != pubkey)
                    {
                        var user = new User
                        {
                            Mute = (bool)(row.Cells["mute"].Value ?? false),
                            DisplayName = (string)row.Cells["display_name"].Value,
                            Name = (string)row.Cells["name"].Value,
                            Nip05 = (string)row.Cells["nip05"].Value,
                            Picture = (string)row.Cells["picture"].Value,
                            LastActivity = (DateTime?)row.Cells["last_activity"].Value ?? null,
                            CreatedAt = (DateTimeOffset?)row.Cells["created_at"].Value ?? null,
                        };
                        users.Add(pubkey, user);
                    }
                }
                MainForm.Users = users;
                var settings = MainForm.Notifier.Settings;
                settings.Open = checkBoxOpenFile.Checked;
                settings.FileName = textBoxFileName.Text;
                settings.Keywords = [.. textBoxKeywords.Text.Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries)];
                settings.MuteMostr = checkBoxMuteMostr.Checked;
            }
            Close();
        }

        private void FormManiacs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainForm != null)
            {
                Tools.SaveUsers(MainForm.Users);
                MainForm.Notifier.SaveSettings();
                MainForm.Users = Tools.LoadUsers();
                MainForm.Notifier = new KeywordNotifier();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewUsers.SelectedRows)
            {
                dataGridViewUsers.Rows.Remove(row);
            }
        }

        private void ButtonReload_Click(object sender, EventArgs e)
        {
            FormManiacs_Load(sender, e);
        }

        private void FormManiacs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                Close();
            }
        }
    }
}
