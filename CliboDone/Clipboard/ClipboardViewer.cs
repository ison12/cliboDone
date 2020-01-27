using CliboDone.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CliboDone.Clipboard
{
    /// <summary>
    /// クリップボードビューア。
    /// </summary>
    /// <example>
    /// public partial class FMain : Form
    /// {
    ///     private ClipboardViewer clipboardViewer;
    /// 
    ///     public FMain()
    ///     {
    ///         InitializeComponent();
    ///     }
    /// 
    ///     public void Destroy()
    ///     {
    ///         if (clipboardViewer != null)
    ///         {
    ///             clipboardViewer.Dispose();
    ///             clipboardViewer = null;
    ///         }
    ///     }
    /// 
    ///     private void FMain_Load(object sender, EventArgs e)
    ///     {
    ///         clipboardViewer = new ClipboardViewer.ClipboardViewer(this.Handle);
    ///         clipboardViewer.DrawClipBoardEventHandler += OnDrawClipboard;
    ///     }
    /// 
    ///     private void FMain_FormClosed(object sender, FormClosedEventArgs e)
    ///     {
    ///         Destroy();
    ///     }
    /// 
    ///     protected override void WndProc(ref Message m)
    ///     {
    ///         if (clipboardViewer != null)
    ///         {
    ///             clipboardViewer.WndProc(ref m);
    ///         }
    /// 
    ///         base.WndProc(ref m);
    ///     }
    /// 
    ///     private void OnDrawClipboard(object sender, DrawClipboardEventArgs e)
    ///     {
    ///         // こちらに処理を定義する
    ///     }
    ///     
    ///     ...
    /// }
    /// </example>
    public class ClipboardViewer : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        private const int WM_DRAWCLIPBOARD = 0x0308;
        private const int WM_CHANGECBCHAIN = 0x030D;

        /// <summary>
        /// ビューアーのウィンドウハンドル
        /// </summary>
        private IntPtr hWndViewer;

        /// <summary>
        /// ビューアー設定後のウィンドウハンドル（元々設定済みの最上位ウィンドウハンドル）
        /// </summary>
        private IntPtr hWndViewerNext;

        /// <summary>
        /// クリップボードに内容に変更があると発生するデリゲート。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        public delegate void DrawClipboardEventHandler(object sender, DrawClipboardEventArgs e);

        /// <summary>
        /// クリップボードに内容に変更があると発生するイベント。
        /// </summary>
        public event DrawClipboardEventHandler DrawClipBoardEventHandler;

        /// <summary>
        /// コンストラクタ。
        /// クリップボードビューアを設定する。
        /// </summary>
        /// <param name="hWndViewer">ビューアのウィンドウハンドル</param>
        public ClipboardViewer(IntPtr hWndViewer)
        {
            this.hWndViewer = hWndViewer;
            this.hWndViewerNext = SetClipboardViewer(this.hWndViewer);

            // SetClipboardViewerの戻り値がNULLの可能性もあるので、GetErrorCodeメソッドでエラーコードをチェックする
            int errorCode = WinAPIErrorMessageUtil.GetErrorCode();
            if (errorCode != 0 && errorCode != 1008)
            {
                // エラー発生時
                ThrowWinAPIException(errorCode);
            }
        }

        /// <summary>
        /// 後始末処理。
        /// クリップボードビューアの解放を実施する。
        /// </summary>
        public void Dispose()
        {
            if (!ChangeClipboardChain(this.hWndViewer, this.hWndViewerNext))
            {
                // エラー発生時
                ThrowWinAPIException(WinAPIErrorMessageUtil.GetErrorCode());
            }
        }

        /// <summary>
        /// ウィンドウプロシージャ。
        /// </summary>
        /// <param name="m">メッセージオブジェクト</param>
        public void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    // クリップボードにデータが書き込まれた際のウィンドウメッセージ

                    if (this.hWndViewerNext != IntPtr.Zero)
                    {
                        // チェイン先のビューアが存在する
                        SendMessage(this.hWndViewerNext, m.Msg, m.WParam, m.LParam);
                    }

                    DrawClipBoardEventHandler?.Invoke(this, DrawClipboardEventArgs.Empty);

                    break;

                case WM_CHANGECBCHAIN:
                    // クリップボード・ビューア・チェーンが更新された際のウィンドウメッセージ

                    if (m.WParam == this.hWndViewerNext)
                    {
                        this.hWndViewerNext = m.LParam;
                    }
                    else if (this.hWndViewerNext != IntPtr.Zero)
                    {
                        // チェイン先のビューアが存在する
                        SendMessage(this.hWndViewerNext, m.Msg, m.WParam, m.LParam);
                    }

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// WinAPIのエラー発生時に例外を発行する処理。
        /// </summary>
        /// <param name="errorCode">エラーコード</param>
        private void ThrowWinAPIException(int errorCode)
        {
            throw new Exception(
                string.Format("予期せぬエラーが発生しました。" + "\r\n" + "ErrorCode = {0}" + "\r\n" + "ErrorMessage = {1}"
                            , errorCode
                            , WinAPIErrorMessageUtil.GetErrorMessage(errorCode)
                )
            );
        }

    }
}
