変換スクリプトは以下の2ファイルで1セット。
(1) [ファイル名]_Script.vbs
(2) [ファイル名]_Template.txt

(1)と(2)のファイルの文字コードは Unicode とする。

(1)のスクリプトファイルは以下のように、"Main"という名前で、引数は2つにすること。
第一引数は、(2)のファイルのコンテンツで、ファイルそのものが無い場合は、IsEmptyがTrueになる。
第二引数は、クリップボードにコピー（または切り取りされた内容）された文字列となる。
Function Main(templateContents, clipboardContents)

VBSのリファレンスは、以下のサイトを参考にすること。
https://docs.microsoft.com/ja-jp/previous-versions/windows/scripting/cc392489(v=msdn.10)
