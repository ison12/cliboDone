Option Explicit

' ==============================================================================
' Main entry point.
'
' Args   : scriptDirPath     ... Own script directory path.
'        : clipboardContents ... Clipboard contents.
'        : templateContents  ... The contents of the Template.txt file.
' Return : Result of conversion.
' ==============================================================================
Function Main(scriptDirPath, clipboardContents, templateContents)

	' Return variable
	Dim ret
	ret = LCase(clipboardContents)

	' Return
	Main = ret

End Function
