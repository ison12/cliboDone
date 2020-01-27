using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CliboDone.Utils
{
    /// <summary>
    /// WinAPI関連のエラーメッセージユーティリティ。
    /// </summary>
    public static class WinAPIErrorMessageUtil
    {
        [DllImport("kernel32.dll")]
        private static extern uint FormatMessage(
            uint dwFlags,
            IntPtr lpSource,
            uint dwMessageId,
            uint dwLanguageId,
            StringBuilder lpBuffer,
            int nSize,
            IntPtr Arguments);

        private const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        /// <summary>
        /// エラーコードを取得する。
        /// </summary>
        /// <returns>エラーコード</returns>
        public static int GetErrorCode()
        {
            return Marshal.GetLastWin32Error();
        }

        /// <summary>
        /// エラーコードに対応するエラーメッセージを取得する。
        /// </summary>
        /// <param name="errorCode">エラーコード</param>
        /// <returns>エラーメッセージ</returns>
        public static string GetErrorMessage(int errorCode)
        {
            StringBuilder message = new StringBuilder(1024);

            FormatMessage(
                FORMAT_MESSAGE_FROM_SYSTEM,
                IntPtr.Zero,
                (uint)errorCode,
                0,
                message,
                message.Capacity,
                IntPtr.Zero);

            return message.ToString();
        }

        /// <summary>
        /// エラーコードに対応するエラーメッセージを取得する。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public static string GetErrorMessage()
        {
            return GetErrorMessage(GetErrorCode());
        }

    }
}
