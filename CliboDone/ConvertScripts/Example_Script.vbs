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
	
	' Replace with contents variable in template string.
	If Not IsEmpty(templateContents) Then
		ret = Replace(templateContents, "${clipboardContents}", clipboardContents)
	Else
		ret = clipboardContents
	End If

	' Return
	Main = ret

End Function
