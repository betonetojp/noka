using SSTPLib;
using System.Diagnostics;

namespace noka
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();
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

        private void ButtonPrefer_Click(object sender, EventArgs e)
        {
            textBoxPreferredGhost.Text = comboBoxGhosts.Text;
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxPreferredGhost.Text = string.Empty;
        }

        private void SearchGhost()
        {
            comboBoxGhosts.Items.Clear();
            SakuraFMO fmo = new("SakuraUnicode");
            fmo.Update(true);
            string[] names = fmo.GetGhostNames();
            if (names.Length > 0)
            {
                comboBoxGhosts.Items.AddRange(names);
                comboBoxGhosts.SelectedIndex = 0;
                if (!string.IsNullOrEmpty(textBoxPreferredGhost.Text))
                {
                    comboBoxGhosts.SelectedItem = textBoxPreferredGhost.Text;
                }
            }
        }
    }
}
