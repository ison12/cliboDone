■変換スクリプトマニュアル

変換スクリプトは以下の2ファイルで1セット。
(1) [ファイル名]_Script.vbs
(2) [ファイル名]_Template.txt

(1)と(2)のファイルの文字コードは Unicode とする。

(1)のスクリプトファイルは、"Main"という名前で、引数は3つにすること。
第一引数は、スクリプトファイル自身のディレクトリパスが引き渡される。
第二引数は、(2)のファイルのコンテンツで、ファイルそのものが無い場合は空の文字列が引き渡される。
第三引数は、クリップボードにコピー（または切り取りされた内容）された文字列が引き渡される。

> Main関数のシグネチャ
Function Main(scriptDirPath, clipboardContents, templateContents)
    // ...
End Function

VBSのリファレンスは、以下のサイトを参考にすること。
https://docs.microsoft.com/ja-jp/previous-versions/windows/scripting/cc392489(v=msdn.10)
