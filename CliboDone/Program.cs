using CliboDone.Forms;
using CliboDone.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CliboDone
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var appDuplicateChecker = new AppDuplicateChecker("CliboDone-c6839605-a231-4230-9bc8-48204cb13698"))
            {
                if (!appDuplicateChecker.Check())
                {
                    // 二重起動しているため終了
                    return;
                }

                FMain mainForm = null;

                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    mainForm = new FMain();
                    Application.Run(mainForm);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    // 正常終了または例外発生時に念のためフォームの破棄処理を実行する
                    // ※フォーム内にアンマネージドリソースがあるため（クリップボードビューアの利用など）、確実に破棄を実施したい
                    if (mainForm != null)
                    {
                        mainForm.Destroy();
                    }

                }
            }
        }
    }
}
