using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliboDone.Clipboard
{
    /// <summary>
    /// クリップボードへの書き込みイベントデータクラス。
    /// </summary>
    public class DrawClipboardEventArgs : EventArgs
    {
        /// <summary>
        /// 空のイベントオブジェクト
        /// </summary>
        public new static readonly DrawClipboardEventArgs Empty = new DrawClipboardEventArgs();
    }
}
