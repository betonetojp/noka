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
    }
}
