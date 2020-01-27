using CliboDone.Utils;
using MSScriptControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliboDone.Scripts
{
    /// <summary>
    /// Script実行クラス。
    /// ※使用にはMicrosoft Script Control 1.0の参照設定が必要
    /// </summary>
    public class ScriptExecutor
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScriptExecutor()
        {

        }

        /// <summary>
        /// スクリプトを実行する。
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="code">コード</param>
        /// <param name="language">スクリプト言語（VBScript or JavaScript）</param>
        /// <param name="funcName">関数名</param>
        /// <param name="funcParams">関数パラメータリスト</param>
        /// <returns></returns>
        public dynamic Exec(string code, string language, string funcName, List<object> funcParams)
        {
            IScriptControl scriptCtrl = new ScriptControl();
            scriptCtrl.Language = language;

            // 実行するスクリプトを指定
            scriptCtrl.AddCode(code);

            // 関数への引数を指定
            List<object> paramList = new List<object>(funcParams.Count);
            foreach (var item in funcParams)
            {
                paramList.Add(item);
            }

            // 名前を指定してVBScriptの関数を実行
            var result = scriptCtrl.Run(funcName, paramList.ToArray());

            return result;
        }

        /// <summary>
        /// スクリプトを実行する。
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="codeFilePath">コードのファイルパス</param>
        /// <param name="language">スクリプト言語（VBScript or JavaScript）</param>
        /// <param name="funcName">関数名</param>
        /// <param name="funcParams">関数パラメータリスト</param>
        /// <returns></returns>
        public dynamic ExecViaFile(string codeFilePath, string language, string funcName, List<object> funcParams)
        {
            return Exec(FileUtil.ReadFileContents(codeFilePath, Encoding.GetEncoding("utf-16")), language, funcName, funcParams);
        }
    }
}
