using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace noka
{
    public class NotifierSettings
    {
        [JsonPropertyName("keywords")]
        public List<string> Keywords { get; set; } = [];
        [JsonPropertyName("open_file")]
        public bool Open { get; set; }
        [JsonPropertyName("file_name")]
        public string FileName { get; set; } = string.Empty;
        [JsonPropertyName("mute_mostr")]
        public bool MuteMostr { get; set; }
    }

    public class KeywordNotifier
    {
        public NotifierSettings Settings { get; set; } = new();

        private List<string> _keywords = [];
        private bool _shouldOpenFile = false;
        private string _fileName = "https://njump.me/";
        private bool _muteMostr = false;

        //private readonly string _keywordsJsonPath = Path.Combine(Tools.GetAppPath(), "keywords.json");
        private readonly string _keywordsJsonPath = Path.Combine(Application.StartupPath, "keywords.json");
        private readonly JsonSerializerOptions _options = new()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true,
        };

        public KeywordNotifier()
        {
            LoadSettings();

            Settings = new NotifierSettings()
            {
                Keywords = _keywords,
                Open = _shouldOpenFile,
                FileName = _fileName,
                MuteMostr = _muteMostr
            };

            SaveSettings();
        }

        public void SaveSettings()
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(Settings, _options);
                File.WriteAllText(_keywordsJsonPath, jsonContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void LoadSettings()
        {
            if (File.Exists(_keywordsJsonPath))
            {
                try
                {
                    var jsonContent = File.ReadAllText(_keywordsJsonPath);
                    var settings = JsonSerializer.Deserialize<NotifierSettings>(jsonContent, _options);
                    if (settings != null)
                    {
                        _keywords = settings.Keywords;
                        _shouldOpenFile = settings.Open;
                        _fileName = settings.FileName;
                        _muteMostr = settings.MuteMostr;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public bool CheckPost(string post)
        {
            foreach (var keyword in _keywords)
            {
                if (post.Contains(keyword))
                {
                    return true;
                }
            }
            return false;
        }
    }
}