using SSTPLib;
using System.Diagnostics;

namespace noka
{
    public partial class FormSetting : Form
    {
        internal string _mainGhost = "";
        private List<SoleGhost> _soleGhosts = [];
        public FormSetting()
        {
            InitializeComponent();

            // ボタンの画像をDPIに合わせて表示
            float scale = CreateGraphics().DpiX / 96f;
            int size = (int)(16 * scale);
            if (scale < 2.0f)
            {
                buttonReload.Image = new Bitmap(Properties.Resources.icons8_reload_16, size, size);
            }
            else
            {
                buttonReload.Image = new Bitmap(Properties.Resources.icons8_reload_32, size, size);
            }
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            labelOpacity.Text = $"{trackBarOpacity.Value}%";
            SearchGhost();


        }

        private void TrackBarOpacity_Scroll(object sender, EventArgs e)
        {
            labelOpacity.Text = $"{trackBarOpacity.Value}%";
            if (null != Owner)
            {
                Owner.Opacity = trackBarOpacity.Value / 100.0;
            }
        }

        private void LinkLabelIcons8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelIcons8.LinkVisited = true;
            var app = new ProcessStartInfo
            {
                FileName = "https://icons8.com",
                UseShellExecute = true
            };
            Process.Start(app);
        }

        private void FormSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void SearchGhost()
        {
            SakuraFMO fmo = new("SakuraUnicode");
            fmo.Update(true);
            string[] names = ["", .. fmo.GetGhostNames()];

            comboBoxGhosts.Items.Clear();
            comboBoxGhosts.Items.AddRange(names);
            comboBoxGhosts.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(_mainGhost))
            {
                comboBoxGhosts.SelectedItem = _mainGhost;
            }

            dataGridViewSoloGhosts.Rows.Clear();
            dataGridViewSoloGhosts.Columns.Clear();
            var npubColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "npub",
                Name = "npub",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            dataGridViewSoloGhosts.Columns.Add(npubColumn);
            var ghostColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Sole ghost",
                Name = "ghost"
            };

            ghostColumn.DataSource = names;
            dataGridViewSoloGhosts.Columns.Add(ghostColumn);

            _soleGhosts = Tools.LoadSoleGhosts();
            foreach (var ghost in _soleGhosts)
            {
                dataGridViewSoloGhosts.Rows.Add(ghost.Npub);
            }

            for (int i = 0; i < _soleGhosts.Count; i++)
            {
                dataGridViewSoloGhosts.Rows[i].Cells[0].Value = _soleGhosts[i].Npub;
                if (names.Contains(_soleGhosts[i].GhostName))
                {
                    dataGridViewSoloGhosts.Rows[i].Cells[1].Value = _soleGhosts[i].GhostName;
                }
            }
        }

        private void FormSetting_Shown(object sender, EventArgs e)
        {
            dataGridViewSoloGhosts.ClearSelection();
        }

        private void TabControlSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewSoloGhosts.ClearSelection();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            // 選択された行を削除
            foreach (DataGridViewRow row in dataGridViewSoloGhosts.SelectedRows)
            {
                // 新規行は無視
                if (row.IsNewRow)
                {
                    continue;
                }

                dataGridViewSoloGhosts.Rows.Remove(row);
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

            List<SoleGhost> soleGhosts = [];
            foreach (DataGridViewRow row in dataGridViewSoloGhosts.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    var npub = row.Cells[0].Value.ToString();
                    if (npub != null)
                    {
                        var ghostName = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "";
                        var soleGhost = new SoleGhost
                        {
                            Npub = npub,
                            GhostName = ghostName
                        };
                        soleGhosts.Add(soleGhost);
                    }
                }
            }
            Tools.SaveSoleGhosts(soleGhosts);
        }

        private void comboBoxGhosts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mainGhost = comboBoxGhosts.Text;
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            SearchGhost();

            dataGridViewSoloGhosts.ClearSelection();
        }
    }
}
