using CliboDone.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CliboDone.Configs
{
    /// <summary>
    /// アプリケーション設定ファイル
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// デフォルトファイル名
        /// </summary>
        private static readonly string FILE_DEFAULT_NAME = "CliboDone.config";

        /// <summary>
        /// ファイルパス
        /// </summary>
        private string filePath;

        /// <summary>
        /// 変換スクリプトのメニュー項目の最後に選択された値
        /// </summary>
        public string ConvertScriptMenuItemLastSelected { get; set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        public AppConfig(string filePath = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                // デフォルトのファイルパスを設定
                filePath = Path.Combine(FileUtil.GetExecuteDirPath(), FILE_DEFAULT_NAME);
            }

            this.filePath = filePath;
        }

        /// <summary>
        /// 読み込み処理。
        /// </summary>
        public void Read()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.XmlResolver = null;
            xmlDoc.Load(filePath);

            var rootNode = xmlDoc.DocumentElement;

            {
                var node = rootNode.SelectSingleNode("convertScriptMenuItemLastSelected") as XmlElement;
                ConvertScriptMenuItemLastSelected = node.InnerText;
            }
        }

        /// <summary>
        /// 読み込み処理。
        /// </summary>
        public void Write()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.XmlResolver = null;
            xmlDoc.Load(filePath);

            var rootNode = xmlDoc.DocumentElement;

            {
                var node = rootNode.SelectSingleNode("convertScriptMenuItemLastSelected") as XmlElement;
                node.InnerText = ConvertScriptMenuItemLastSelected;
            }

            xmlDoc.Save(filePath);
        }
    }
}
