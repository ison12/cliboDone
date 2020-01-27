using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliboDone.Scripts
{
    /// <summary>
    /// 変換スクリプト情報
    /// </summary>
    public class ConvertScriptInfo
    {
        /// <summary>
        /// 識別子
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// テンプレートファイルパス
        /// </summary>
        public string TemplateFilePath { get; set; }

        /// <summary>
        /// スクリプトファイルパス
        /// </summary>
        public string ScriptFilePath { get; set; }
    }
}
