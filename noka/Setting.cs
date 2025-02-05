﻿using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace noka
{
    public class Setting
    {
        private static Data _data = new();

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
            public bool ShowOnlyFollowees { get; set; } = false;
            public bool MinimizeToTray { get; set; } = false;
            public string Npub { get; set; } = string.Empty;
            public bool ShowNetes { get; set; } = true;
            public bool ShowReactions { get; set; } = true;
            public bool ShowChats { get; set; } = true;
            public string DefaultPicture { get; set; } = "https://robohash.org/{npub}?set=set4&size=128x128";
            public string Ghost { get; set; } = string.Empty;
            public bool SoleGhostsOnly { get; set; } = false;
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
        public static bool MinimizeToTray
        {
            get
            {
                return _data.MinimizeToTray;
            }
            set
            {
                _data.MinimizeToTray = value;
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
        public static bool ShowNetes
        {
            get
            {
                return _data.ShowNetes;
            }
            set
            {
                _data.ShowNetes = value;
            }
        }
        public static bool ShowReactions
        {
            get
            {
                return _data.ShowReactions;
            }
            set
            {
                _data.ShowReactions = value;
            }
        }
        public static bool ShowChats
        {
            get
            {
                return _data.ShowChats;
            }
            set
            {
                _data.ShowChats = value;
            }
        }
        public static string DefaultPicture
        {
            get
            {
                return _data.DefaultPicture;
            }
            set
            {
                _data.DefaultPicture = value;
            }
        }
        public static string Ghost
        {
            get
            {
                return _data.Ghost;
            }
            set
            {
                _data.Ghost = value;
            }
        }
        public static bool SoleGhostsOnly
        {
            get
            {
                return _data.SoleGhostsOnly;
            }
            set
            {
                _data.SoleGhostsOnly = value;
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
        #endregion
    }
}
