using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliboDone.Scripts
{
    /// <summary>
    /// 変換スクリプトリストのローダー。
    /// </summary>
    public class ConvertScriptListLoader
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ConvertScriptListLoader()
        {

        }

        /// <summary>
        /// 変換スクリプトをロードする。
        /// </summary>
        /// <param name="rootDirPath">ルートディレクトリパス</param>
        /// <param name="searchDirPath">検索ディレクトリパス</param>
        /// <param name="depth">階層（0 ... 最初、1 ... 次の階層）</param>
        /// <returns>変換スクリプトリスト</returns>
        public List<ConvertScriptInfo> Load(string rootDirPath, string searchDirPath, int depth = 0)
        {
            var ret = new List<ConvertScriptInfo>();

            string convertScriptsDir = searchDirPath;

            /*
             * 第二階層を読み込む
             */
            if (depth == 0)
            {
                var scriptDirPathList = Directory.GetDirectories(convertScriptsDir);

                // ディレクトリ名の昇順で並び替え
                Array.Sort(scriptDirPathList, (val1, val2) => {
                    return val1.CompareTo(val2);
                });

                foreach (var dir in scriptDirPathList)
                {
                    var subList = Load(rootDirPath /* ルートディレクトリ */, dir /* 検索ディレクトリ */, depth + 1);
                    ret.AddRange(subList);
                }
            }

            /*
             * 第一階層を読み込む
             */
            var scriptFilePathList = Directory.GetFiles(convertScriptsDir, "*.vbs");

            // ファイル名の昇順で並び替え
            Array.Sort(scriptFilePathList, (val1, val2) => {
                return val1.CompareTo(val2);
            });

            foreach (var scriptFilePath in scriptFilePathList)
            {
                // ファイル例）
                // "Example_Script.vbs"
                // "Example_Template.txt"
                var scriptFileName = Path.GetFileName(scriptFilePath);
                var scriptFileNameSplitted = scriptFileName.Split('_');

                if (!(scriptFileNameSplitted.Length == 2))
                {
                    // アンダーバーを区切りにして文字列が2つ存在しない場合は不正なファイルなのでスキップする
                    continue;
                }

                var templateFilePath = Path.Combine(Path.GetDirectoryName(scriptFilePath), scriptFileNameSplitted[0] + "_Template.txt");

                if (!File.Exists(templateFilePath))
                {
                    // テンプレートファイルが見つからない場合
                    // ※テンプレートファイルを使わない可能性もあるので、見つからなくても問題なしとする
                }

                var info = new ConvertScriptInfo()
                {
                    // 階層を表す識別子を設定 ... 例）第一階層、Example。第二階層、Dir\Example
                    Id = (searchDirPath.Substring(rootDirPath.Length).Trim('\\') + "\\" + scriptFileNameSplitted[0]).Trim('\\'),
                    Name = scriptFileNameSplitted[0],
                    ScriptFilePath = scriptFilePath,
                    TemplateFilePath = templateFilePath,
                };

                ret.Add(info);
            }

            return ret;
        }

    }
}
