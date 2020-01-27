using CliboDone.Forms;
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
