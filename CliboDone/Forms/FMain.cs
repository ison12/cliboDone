using CliboDone.Clipboard;
using CliboDone.Configs;
using CliboDone.Scripts;
using CliboDone.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CliboDone.Forms
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class FMain : Form
    {
        /// <summary>
        /// アプリケーション設定
        /// </summary>
        private AppConfig appConfig;

        /// <summary>
        /// クリップボードビューア
        /// </summary>
        private ClipboardViewer clipboardViewer;

        /// <summary>
        /// クリップボードの書き換え中フラグ
        /// </summary>
        private bool isEditingClipboard;

        /// <summary>
        /// 強制的にフォームをクローズさせるフラグ
        /// </summary>
        private bool forceFormClose;

        /// <summary>
        /// 変換スクリプトのディレクトリ名
        /// </summary>
        private static readonly string CONVERT_SCRIPTS_DIR_NAME = "ConvertScripts";

        /// <summary>
        /// マニュアルのファイルパス
        /// </summary>
        private static readonly string MANUAL_FILE_PATH = @"Manual.html";

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public FMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスが破棄された際の処理。
        /// </summary>
        public void Destroy()
        {
            if (clipboardViewer != null)
            {
                clipboardViewer.Dispose();
                clipboardViewer = null; // Destroyが二回以上呼び出されても良いようにnullクリアする
            }

            // 時折、常駐アイコンが残ったままになるのでDisposeメソッドを明示的に呼び出す
            notifyIcon.Dispose();
        }

        /// <summary>
        /// フォームのロード処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FMain_Load(object sender, EventArgs e)
        {
            appConfig = new AppConfig();
            appConfig.Read();

            LoadConvertScriptList();

            SelectConvertScriptMenuItem(menuGroupConvert, appConfig.ConvertScriptMenuItemLastSelected);

            clipboardViewer = new Clipboard.ClipboardViewer(this.Handle);
            clipboardViewer.DrawClipBoardEventHandler += OnDrawClipboard;
        }

        /// <summary>
        /// フォームアクティブ時の処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FMain_Activated(object sender, EventArgs e)
        {
            RefreshConvertScriptList();
        }

        /// <summary>
        /// フォームが閉じられる際の処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (forceFormClose)
            {
                // 強制クローズ
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Xボタン押下などの理由で閉じられた場合は、キャンセルしてフォーム自体を非表示にする
                e.Cancel = true;
                this.Visible = false;
            }
        }

        /// <summary>
        /// フォームが閉じられた際の処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            var selectedItem = GetSelectedMenuItem(menuGroupConvert);
            if (selectedItem != null)
            {
                var info = selectedItem.Tag as ConvertScriptInfo;
                appConfig.ConvertScriptMenuItemLastSelected = info.Id;
            }
            appConfig.Write();

            Destroy();
        }

        /// <summary>
        /// ウィンドウプロシージャ。
        /// </summary>
        /// <param name="m">メッセージ</param>
        protected override void WndProc(ref Message m)
        {
            if (clipboardViewer != null)
            {
                clipboardViewer.WndProc(ref m);
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// クリップボードにデータが書き込まれた際の処理。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnDrawClipboard(object sender, DrawClipboardEventArgs e)
        {
            if (!rdoConvEnabled.Checked)
            {
                txtResultMessage.Text = string.Format("変換がオフになっています");
                return;
            }

            if (!System.Windows.Forms.Clipboard.ContainsText())
            {
                // クリップボードの内容がテキストではない場合
                return;
            }

            if (isEditingClipboard)
            {
                // クリップボードの書き換え中のため処理を中断
                // --------------------------------------------
                // 本メソッド内で、Clipboard.SetTextを呼び出すため、本メソッドが再帰呼び出しされる。
                // そのため、二重に同じ処理が行われないようにフラグの判定を実施し処理を中断する。
                return;
            }

            try
            {
                // クリップボードの書き換え中のためフラグを変更
                isEditingClipboard = true;

                try
                {
                    var convertedText = ExecConvert(System.Windows.Forms.Clipboard.GetText());
                    System.Windows.Forms.Clipboard.SetText(convertedText, TextDataFormat.UnicodeText);

                    txtResultMessage.Text = string.Format("{0} ... 変換しました", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    txtResultMessage.Text = string.Format("エラー発生 ... {0}", ex.Message);
                }
            }
            finally
            {
                // クリップボードの書き換え終了のためフラグを変更
                isEditingClipboard = false;
            }
        }

        /// <summary>
        /// メニューの終了クリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.forceFormClose = true; // FormClosingイベントでアプリケーションの終了がキャンセルされないようにフラグをオンにする
            this.Close();
        }

        /// <summary>
        /// メニューの最新情報の読み込みクリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemConvertScriptRefresh_Click(object sender, EventArgs e)
        {
            RefreshConvertScriptList();
        }

        /// <summary>
        /// メニューの変換スクリプト生成クリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemConvertScriptCreate_Click(object sender, EventArgs e)
        {
            var tempTemplatePath = Path.Combine(FileUtil.GetExecuteDirPath(), CONVERT_SCRIPTS_DIR_NAME, "(Template)Template.txt");
            var tempScriptPath = Path.Combine(FileUtil.GetExecuteDirPath(), CONVERT_SCRIPTS_DIR_NAME, "(Template)Script.vbs");

            var createdFile = false;
            var fileNamePart = string.Empty;

            while (!createdFile)
            {
                fileNamePart = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                var desTemplatePath = Path.Combine(FileUtil.GetExecuteDirPath()
                                                 , CONVERT_SCRIPTS_DIR_NAME
                                                 , string.Format("{0}_Template.txt", fileNamePart));

                var desScriptPath = Path.Combine(FileUtil.GetExecuteDirPath()
                                                 , CONVERT_SCRIPTS_DIR_NAME
                                                 , string.Format("{0}_Script.vbs", fileNamePart));

                if (File.Exists(desTemplatePath) || File.Exists(desScriptPath))
                {
                    continue;
                }

                File.Copy(tempTemplatePath, desTemplatePath);
                File.Copy(tempScriptPath, desScriptPath);

                createdFile = true;
            }

            LoadConvertScriptList();

            MessageBox.Show(string.Format("{0}_Template.txt" + "\n" + "{0}_Script.vbs という名前でファイルを作成しました。", fileNamePart), "ファイル作成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// メニューの変換スクリプトのフォルダを開くクリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemOpenConvertScriptDir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(Path.Combine(FileUtil.GetExecuteDirPath(), CONVERT_SCRIPTS_DIR_NAME));
        }

        /// <summary>
        /// メニューの変換スクリプトクリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemConvertScripts_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in menuGroupConvert.DropDownItems)
            {
                /*
                 * 第二階層
                 */
                foreach (ToolStripMenuItem itemSub in item.DropDownItems)
                {
                    if (object.ReferenceEquals(itemSub, sender))
                    {
                        var info = itemSub.Tag as ConvertScriptInfo;

                        itemSub.CheckState = CheckState.Indeterminate;
                        txtTargetConvertScript.Text = info.Id;
                    }
                    else
                    {
                        itemSub.CheckState = CheckState.Unchecked;
                    }
                }

                /*
                 * 第一階層
                 */
                if (object.ReferenceEquals(item, sender))
                {
                    var info = item.Tag as ConvertScriptInfo;

                    item.CheckState = CheckState.Indeterminate;
                    txtTargetConvertScript.Text = info.Id;
                }
                else
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
        }

        /// <summary>
        /// メニューのマニュアルクリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemManual_Click(object sender, EventArgs e)
        {
            ShowManual();
        }

        /// <summary>
        /// メニューのバージョンクリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemVersion_Click(object sender, EventArgs e)
        {
            ShowVersionInfo();
        }

        /// <summary>
        /// メニューのウィンドウ非表示クリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void menuItemHide_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// コンテキストメニューのウィンドウ表示時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void contextMenuItemWindowShow_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        /// <summary>
        /// コンテキストメニューのウィンドウ非表示クリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void contextMenuItemWindowHide_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// コンテキストメニューの終了クリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void contextMenuItemExit_Click(object sender, EventArgs e)
        {
            this.forceFormClose = true; // FormClosingイベントでアプリケーションの終了がキャンセルされないようにフラグをオンにする
            this.Close();
        }

        /// <summary>
        /// タスクトレイアイコンのクリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 左クリック時は表示
                this.Visible = true;
            }
        }

        /// <summary>
        /// 変換有効有無クリック時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void rdoConvEnabled_Click(object sender, EventArgs e)
        {
            rdoConvEnabled.Checked = !rdoConvEnabled.Checked;
        }

        /// <summary>
        /// 変換有効有無変更時のイベントプロシージャ。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void rdoConvEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoConvEnabled.Checked)
            {
                rdoConvEnabled.Image = CliboDone.Properties.Resources.ConvEnabled;
            }
            else
            {
                rdoConvEnabled.Image = CliboDone.Properties.Resources.ConvDisabled;
            }
        }

        /// <summary>
        /// 変換スクリプトをロードする。
        /// </summary>
        private void LoadConvertScriptList()
        {
            string convertScriptsDir = Path.Combine(FileUtil.GetExecuteDirPath(), CONVERT_SCRIPTS_DIR_NAME);

            var loader = new ConvertScriptListLoader();
            var infos = loader.Load(convertScriptsDir /* ルートディレクトリ */, convertScriptsDir /* 検索ディレクトリ */);

            menuGroupConvert.DropDownItems.Clear();
            txtTargetConvertScript.Text = string.Empty;

            Dictionary<string, ToolStripMenuItem> menuItemMap = new Dictionary<string, ToolStripMenuItem>();

            foreach (var info in infos)
            {
                var idSplitted = info.Id.Split('\\');
                if (idSplitted.Length == 2)
                {
                    /*
                     * 第二階層
                     * 例）Dir\Example
                     */

                    menuItemMap.TryGetValue(idSplitted[0], out ToolStripMenuItem menuItemDir);
                    if (menuItemDir == null)
                    {
                        // ディレクトリのメニュー項目が未追加の場合は、追加を実施してメニュー項目としても追加
                        menuItemDir = new ToolStripMenuItem()
                        {
                            Text = idSplitted[0],
                        };
                        menuItemMap[idSplitted[0]] = menuItemDir;

                        menuGroupConvert.DropDownItems.Add(menuItemDir);
                    }

                    // ディレクトリ配下のファイルをメニュー項目として追加
                    var menuItem = new ToolStripMenuItem()
                    {
                        Text = info.Name,
                        Tag = info,
                        CheckState = CheckState.Unchecked,
                    };
                    menuItem.Click += menuItemConvertScripts_Click;

                    menuItemDir.DropDownItems.Add(menuItem);

                }
                else if (idSplitted.Length == 1)
                {
                    /*
                     * 第一階層
                     * 例）Example
                     */

                    // ファイルをメニュー項目として追加
                    var menuItem = new ToolStripMenuItem()
                    {
                        Text = info.Name,
                        Tag = info,
                        CheckState = CheckState.Unchecked,
                    };
                    menuItem.Click += menuItemConvertScripts_Click;

                    menuGroupConvert.DropDownItems.Add(menuItem);
                }
            }
        }

        /// <summary>
        /// 変換スクリプトリストを更新する。
        /// </summary>
        private void RefreshConvertScriptList()
        {
            // 現在選択されている情報を取得
            var menuItem = GetSelectedMenuItem(menuGroupConvert);
            ConvertScriptInfo itemInfo = null;
            if (menuItem != null)
            {
                itemInfo = menuItem.Tag as ConvertScriptInfo;
            }

            // リストをロードしなおす
            LoadConvertScriptList();

            // 選択状態を復元する
            if (itemInfo != null)
            {
                SelectConvertScriptMenuItem(menuGroupConvert, itemInfo.Id);
            }
        }

        /// <summary>
        /// 変換スクリプトメニュー項目を選択状態にする。
        /// </summary>
        /// <param name="menuGroup">メニューグループ</param>
        /// <param name="menuItemId">メニュー項目ID</param>
        private void SelectConvertScriptMenuItem(ToolStripMenuItem menuGroup, string menuItemId)
        {
            txtTargetConvertScript.Text = string.Empty;

            foreach (ToolStripMenuItem item in menuGroup.DropDownItems)
            {
                if (item.DropDownItems.Count > 0)
                {
                    SelectConvertScriptMenuItem(item, menuItemId);
                }

                var info = item.Tag as ConvertScriptInfo;
                if (info == null)
                {
                    continue;
                }

                if (info.Id == menuItemId)
                {
                    item.CheckState = CheckState.Indeterminate;
                    txtTargetConvertScript.Text = info.Id;
                }
                else
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
        }

        /// <summary>
        /// 選択されているメニュー項目の取得。
        /// </summary>
        /// <param name="menuGroup">メニューグループ</param>
        /// <returns>メニュー項目</returns>
        private ToolStripMenuItem GetSelectedMenuItem(ToolStripMenuItem menuGroup)
        {
            foreach (ToolStripMenuItem item in menuGroup.DropDownItems)
            {
                if (item.DropDownItems.Count > 0)
                {
                    var itemSub = GetSelectedMenuItem(item);
                    if (itemSub != null)
                    {
                        return itemSub;
                    }
                }

                if (item.CheckState == CheckState.Indeterminate)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 変換を実行する。
        /// </summary>
        /// <param name="clipboardContents">クリップボードコンテンツ</param>
        /// <returns>変換結果</returns>
        private string ExecConvert(string clipboardContents)
        {
            var convertScriptItem = GetSelectedMenuItem(menuGroupConvert);
            if (convertScriptItem == null)
            {
                // 変換スクリプトの指定なし
                throw new Exception("変換メニューが未選択です。");
            }

            ConvertScriptInfo scriptInfo = convertScriptItem.Tag as ConvertScriptInfo;

            var scriptContents = FileUtil.ReadFileContents(scriptInfo.ScriptFilePath, Encoding.GetEncoding("utf-16"));
            var templateContents = FileUtil.ReadFileContents(scriptInfo.TemplateFilePath, Encoding.GetEncoding("utf-16"));

            var scriptExecutor = new ScriptExecutor();
            var ret = scriptExecutor.Exec(scriptContents, "VBScript", "Main", new List<object>() {
                templateContents,
                clipboardContents
            });

            return ret as string;
        }

        /// <summary>
        /// マニュアルを表示する。
        /// </summary>
        private void ShowManual()
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(Path.Combine(FileUtil.GetExecuteDirPath(), MANUAL_FILE_PATH));
        }

        /// <summary>
        /// バージョン情報を表示する。
        /// </summary>
        private void ShowVersionInfo()
        {
            System.Reflection.AssemblyProductAttribute asmprd =
                (System.Reflection.AssemblyProductAttribute)
                Attribute.GetCustomAttribute(
                System.Reflection.Assembly.GetExecutingAssembly(),
                typeof(System.Reflection.AssemblyProductAttribute));

            // 自分自身のAssemblyを取得
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            // バージョンの取得
            System.Version ver = asm.GetName().Version;

            MessageBox.Show(string.Format("バージョン：{0}", ver.ToString()), string.Format("{0} バージョン情報", asmprd.Product), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
