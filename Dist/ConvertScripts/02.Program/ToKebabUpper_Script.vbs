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

	Include scriptDirPath & "\Util.vbs"

	' Return
	Main = ConvertToSnakeOrKebab(clipboardContents, "-", True)

End Function

' Include Method
Sub Include(strFile)

	Dim objFSO   : Set objFSO = CreateObject("Scripting.FileSystemObject")
	Dim objStream: Set objStream = objFSO.OpenTextFile(strFile, 1, False, -1)

	ExecuteGlobal objStream.ReadAll() 
	objStream.Close

End Sub