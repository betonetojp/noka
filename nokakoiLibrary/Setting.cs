using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace noka
{
    public class Setting
    {
        private static Data _data = new();
        private static ConsoleData _consoleData = new();

        #region 設定データクラス
        /// <summary>
        /// 設定データクラス
        /// </summary>
        public class Data
        {
            public Point Location { get; set; }
            public Size Size { get; set; } = new Size(240, 240);
            public bool TopMost { get; set; } = false;
            public int CutLength { get; set; } = 40;
            public int CutNameLength { get; set; } = 8;
            public double Opacity { get; set; } = 1.00;
            public bool DisplayTime { get; set; } = true;
            public bool ShowOnlyFollowees { get; set; } = false;
            public string Npub { get; set; } = string.Empty;
            public string UnkownPicture { get; set; } = string.Empty;
        }
        #endregion

        #region コンソール設定データクラス
        /// <summary>
        /// コンソール設定データクラス
        /// </summary>
        public class ConsoleData
        {
            public int CutLength { get; set; } = 40;
            public int CutNameLength { get; set; } = 8;
            public bool DisplayTime { get; set; } = true;
            public bool ShowOnlyFollowees { get; set; } = false;
            public string Npub { get; set; } = string.Empty;
        }
        #endregion

        #region プロパティ
        public static Point Location
        {
            get
            {
                return _data.Location;
            }
            set
            {
                _data.Location = value;
            }
        }
        public static Size Size
        {
            get
            {
                return _data.Size;
            }
            set
            {
                _data.Size = value;
            }
        }
        public static bool TopMost
        {
            get
            {
                return _data.TopMost;
            }
            set
            {
                _data.TopMost = value;
            }
        }
        public static int CutLength
        {
            get
            {
                return _data.CutLength;
            }
            set
            {
                _data.CutLength = value;
            }
        }
        public static int CutNameLength
        {
            get
            {
                return _data.CutNameLength;
            }
            set
            {
                _data.CutNameLength = value;
            }
        }
        public static double Opacity
        {
            get
            {
                return _data.Opacity;
            }
            set
            {
                _data.Opacity = value;
            }
        }
        public static bool DisplayTime
        {
            get
            {
                return _data.DisplayTime;
            }
            set
            {
                _data.DisplayTime = value;
            }
        }
        public static bool ShowOnlyFollowees
        {
            get
            {
                return _data.ShowOnlyFollowees;
            }
            set
            {
                _data.ShowOnlyFollowees = value;
            }
        }
        public static string Npub
        {
            get
            {
                return _data.Npub;
            }
            set
            {
                _data.Npub = value;
            }
        }
        public static string UnkownPicture
        {
            get
            {
                return _data.UnkownPicture;
            }
            set
            {
                _data.UnkownPicture = value;
            }
        }
        #endregion

        #region コンソールプロパティ
        public static int ConloleCutLength
        {
            get
            {
                return _consoleData.CutLength;
            }
            set
            {
                _consoleData.CutLength = value;
            }
        }
        public static int ConloleCutNameLength
        {
            get
            {
                return _consoleData.CutNameLength;
            }
            set
            {
                _consoleData.CutNameLength = value;
            }
        }
        public static bool ConloleDisplayTime
        {
            get
            {
                return _consoleData.DisplayTime;
            }
            set
            {
                _consoleData.DisplayTime = value;
            }
        }
        public static bool ConloleShowOnlyFollowees
        {
            get
            {
                return _consoleData.ShowOnlyFollowees;
            }
            set
            {
                _consoleData.ShowOnlyFollowees = value;
            }
        }
        public static string ConloleNpub
        {
            get
            {
                return _consoleData.Npub;
            }
            set
            {
                _consoleData.Npub = value;
            }
        }
        #endregion

        #region 設定ファイル操作
        /// <summary>
        /// 設定ファイル読み込み
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Load(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Data));
                var xmlSettings = new XmlReaderSettings();
                using var streamReader = new StreamReader(path, Encoding.UTF8);
                using var xmlReader = XmlReader.Create(streamReader, xmlSettings);
                _data = serializer.Deserialize(xmlReader) as Data ?? _data;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 設定ファイル書き込み
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Save(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Data));
                using var streamWriter = new StreamWriter(path, false, Encoding.UTF8);
                serializer.Serialize(streamWriter, _data);
                streamWriter.Flush();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// コンソール設定ファイル読み込み
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool LoadConsoleData(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ConsoleData));
                var xmlSettings = new XmlReaderSettings();
                using var streamReader = new StreamReader(path, Encoding.UTF8);
                using var xmlReader = XmlReader.Create(streamReader, xmlSettings);
                _consoleData = serializer.Deserialize(xmlReader) as ConsoleData ?? _consoleData;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// コンソール設定ファイル書き込み
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveConsoleData(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ConsoleData));
                using var streamWriter = new StreamWriter(path, false, Encoding.UTF8);
                serializer.Serialize(streamWriter, _consoleData);
                streamWriter.Flush();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }
}
