Option Explicit

Include "Util.vbs"

On Error Resume Next

call ParseKeywordTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

call IsAlphabetUpperTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

call IsAlphabetLowerTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

call IsAlphabetTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

call IsSnakeTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

call IsKebabTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

call ConvertToCamelTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

call ConvertToSnakeOrKebabTest
If Err.Number <> 0 Then MsgBox Err.Number & ", " & Err.Description & " ... " & Err.Source: Err.Clear

' ==============================================================================
' ParseKeyword Test.
' Args   : 
' Return : 
' ==============================================================================
Sub ParseKeywordTest()

	Dim errorMsg: errorMsg = "ParseKeyword Failed"
	Dim ret

	ret = ParseKeyword("")
	call Assert (ret = False, errorMsg)

	ret = ParseKeyword("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_")
	call Assert ((UBound(ret) - LBound(ret)) = 0, errorMsg)
	call Assert (ret(0)(0) = 1 And ret(0)(1) = 64, errorMsg)

	ret = ParseKeyword(" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_")
	call Assert ((UBound(ret) - LBound(ret)) = 0, errorMsg)
	call Assert (ret(0)(0) = 2 And ret(0)(1) = 64, errorMsg)

	ret = ParseKeyword("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_ !""#$%&'()=^~\|@`[{;+:*]},<.>/?\")
	call Assert ((UBound(ret) - LBound(ret)) = 0, errorMsg)
	call Assert (ret(0)(0) = 1 And ret(0)(1) = 64, errorMsg)

	ret = ParseKeyword("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_ abc あいうえお全角𠮷 def")
	call Assert ((UBound(ret) - LBound(ret)) = 2, errorMsg & " a")
	call Assert (ret(0)(0) =  1 And ret(0)(1) = 64, errorMsg & " b")
	call Assert (ret(1)(0) = 66 And ret(1)(1) = 3, errorMsg & " c")
	call Assert (ret(2)(0) = 80 And ret(2)(1) = 3, errorMsg & " d")

	ret = ParseKeyword(vbTab & "abc abc" & vbLf & "def")
	call Assert ((UBound(ret) - LBound(ret)) = 2, errorMsg)
	call Assert (ret(0)(0) =  2 And ret(0)(1) = 3, errorMsg)
	call Assert (ret(1)(0) =  6 And ret(1)(1) = 3, errorMsg)
	call Assert (ret(2)(0) = 10 And ret(2)(1) = 3, errorMsg)

End Sub

' ==============================================================================
' IsAlphabetUpper Test.
' Args   : 
' Return : 
' ==============================================================================
Sub IsAlphabetUpperTest()

	Dim errorMsg: errorMsg = "IsAlphabetUpper Failed"
	Dim ret

	ret = IsAlphabetUpper(""): call Assert (ret = False, errorMsg)
	ret = IsAlphabetUpper("0"): call Assert (ret = False, errorMsg)
	ret = IsAlphabetUpper("-"): call Assert (ret = False, errorMsg)

	ret = IsAlphabetUpper("A"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("B"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("C"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("D"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("E"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("F"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("G"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("H"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("I"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("J"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("K"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("L"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("M"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("N"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("O"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("P"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("Q"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("R"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("S"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("T"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("U"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("V"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("W"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("X"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("Y"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetUpper("Z"): call Assert (ret = True, errorMsg)
   
End Sub

' ==============================================================================
' IsAlphabetLower Test.
' Args   : 
' Return : 
' ==============================================================================
Sub IsAlphabetLowerTest()

	Dim errorMsg: errorMsg = "IsAlphabetLower Failed"
	Dim ret

	ret = IsAlphabetLower(""): call Assert (ret = False, errorMsg)
	ret = IsAlphabetLower("0"): call Assert (ret = False, errorMsg)
	ret = IsAlphabetLower("-"): call Assert (ret = False, errorMsg)

	ret = IsAlphabetLower("a"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("b"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("c"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("d"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("e"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("f"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("g"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("h"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("i"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("j"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("k"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("l"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("m"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("n"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("o"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("p"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("q"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("r"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("s"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("t"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("u"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("v"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("w"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("x"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("y"): call Assert (ret = True, errorMsg)
	ret = IsAlphabetLower("z"): call Assert (ret = True, errorMsg)
	  
End Sub

' ==============================================================================
' IsAlphabetLower Test.
' Args   : 
' Return : 
' ==============================================================================
Sub IsAlphabetTest()

	Dim errorMsg: errorMsg = "IsAlphabet Failed"
	Dim ret

	ret = IsAlphabet(""): call Assert (ret = False, errorMsg)
	ret = IsAlphabet("0"): call Assert (ret = False, errorMsg)
	ret = IsAlphabet("-"): call Assert (ret = False, errorMsg)

	ret = IsAlphabet("A"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("B"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("C"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("D"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("E"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("F"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("G"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("H"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("I"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("J"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("K"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("L"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("M"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("N"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("O"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("P"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("Q"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("R"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("S"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("T"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("U"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("V"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("W"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("X"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("Y"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("Z"): call Assert (ret = True, errorMsg)

	ret = IsAlphabet("a"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("b"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("c"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("d"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("e"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("f"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("g"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("h"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("i"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("j"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("k"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("l"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("m"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("n"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("o"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("p"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("q"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("r"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("s"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("t"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("u"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("v"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("w"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("x"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("y"): call Assert (ret = True, errorMsg)
	ret = IsAlphabet("z"): call Assert (ret = True, errorMsg)
	  
End Sub

' ==============================================================================
' IsSnake Test.
' Args   : 
' Return : 
' ==============================================================================
Sub IsSnakeTest()

	Dim errorMsg: errorMsg = "IsSnake Failed"
	Dim ret

	ret = IsSnake("")         : call Assert (ret = False, errorMsg)
	ret = IsSnake("-")        : call Assert (ret = False, errorMsg)
	ret = IsSnake("abc123=;:"): call Assert (ret = False, errorMsg)
	ret = IsSnake("_")        : call Assert (ret = True, errorMsg)
	ret = IsSnake("abc123_")  : call Assert (ret = True, errorMsg)
	ret = IsSnake("_abc123")  : call Assert (ret = True, errorMsg)
	ret = IsSnake("abc_123")  : call Assert (ret = True, errorMsg)
	ret = IsSnake("abc_123_") : call Assert (ret = True, errorMsg)

End Sub

' ==============================================================================
' IsKebab Test.
' Args   : 
' Return : 
' ==============================================================================
Sub IsKebabTest()

	Dim errorMsg: errorMsg = "IsKebab Failed"
	Dim ret

	ret = IsKebab("")         : call Assert (ret = False, errorMsg)
	ret = IsKebab("_")        : call Assert (ret = False, errorMsg)
	ret = IsKebab("abc123=;:"): call Assert (ret = False, errorMsg)
	ret = IsKebab("-")        : call Assert (ret = True, errorMsg)
	ret = IsKebab("abc123-")  : call Assert (ret = True, errorMsg)
	ret = IsKebab("-abc123")  : call Assert (ret = True, errorMsg)
	ret = IsKebab("abc-123")  : call Assert (ret = True, errorMsg)
	ret = IsKebab("abc-123-") : call Assert (ret = True, errorMsg)

End Sub

' ==============================================================================
' ConvertToCamel Test.
' Args   : 
' Return : 
' ==============================================================================
Sub ConvertToCamelTest()

	Dim errorMsg: errorMsg = "ConvertToCamel Failed"
	Dim ret

	ret = ConvertToCamel("", False)                                 : call Assert (ret = "", errorMsg)

	' 単語としてみなされない文字
	ret = ConvertToCamel("!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", False)                                : call Assert (ret = "!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)
	ret = ConvertToCamel(vbTab & vbCr & vbLf & vbCrLf & "!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", False) : call Assert (ret = vbTab & vbCr & vbLf & vbCrLf & "!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)
	ret = ConvertToCamel("あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", False)               : call Assert (ret = "あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)

	' 単純な文字変換
	ret = ConvertToCamel("camelCase", False)    : call Assert (ret = "camelCase", errorMsg)
	ret = ConvertToCamel("camel-case", False)   : call Assert (ret = "camelCase", errorMsg)
	ret = ConvertToCamel("camel_case", False)   : call Assert (ret = "camelCase", errorMsg)
	ret = ConvertToCamel("-camel-case-", False) : call Assert (ret = "camelCase", errorMsg)
	ret = ConvertToCamel("_camel_case_", False) : call Assert (ret = "camelCase", errorMsg)
	ret = ConvertToCamel("CAMEL_CASE", False) : call Assert (ret = "camelCase", errorMsg)
	ret = ConvertToCamel("CAMEL-CASE", False) : call Assert (ret = "camelCase", errorMsg)
	ret = ConvertToCamel("_CAMEL-CASE_", False) : call Assert (ret = "camelCase", errorMsg)

	' 単純な文字変換（Upper Case）
	ret = ConvertToCamel("camelCase", True)    : call Assert (ret = "CamelCase", errorMsg)
	ret = ConvertToCamel("camel-case", True)   : call Assert (ret = "CamelCase", errorMsg)
	ret = ConvertToCamel("camel_case", True)   : call Assert (ret = "CamelCase", errorMsg)
	ret = ConvertToCamel("-camel-case-", True) : call Assert (ret = "CamelCase", errorMsg)
	ret = ConvertToCamel("_camel_case_", True) : call Assert (ret = "CamelCase", errorMsg)
	ret = ConvertToCamel("CAMEL_CASE", True) : call Assert (ret = "CamelCase", errorMsg)
	ret = ConvertToCamel("CAMEL-CASE", True) : call Assert (ret = "CamelCase", errorMsg)
	ret = ConvertToCamel("-CAMEL-CASE-", True) : call Assert (ret = "CamelCase", errorMsg)

	' 2語以上の文字変換
	ret = ConvertToCamel("appleTree orangeTree"  , False) : call Assert (ret = "appleTree orangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree orange-tree", False) : call Assert (ret = "appleTree orangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree orange_tree", False) : call Assert (ret = "appleTree orangeTree", errorMsg)
	ret = ConvertToCamel("APPLE_TREE ORANGE_TREE", False) : call Assert (ret = "appleTree orangeTree", errorMsg)

	ret = ConvertToCamel("appleTree" & vbLf & "orangeTree"  , False) : call Assert (ret = "appleTree" & vbLf & "orangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree" & vbLf & "orange-tree", False) : call Assert (ret = "appleTree" & vbLf & "orangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree" & vbLf & "orange_tree", False) : call Assert (ret = "appleTree" & vbLf & "orangeTree", errorMsg)

	ret = ConvertToCamel("appleTree" & vbCrLf & "orangeTree"  , False) : call Assert (ret = "appleTree" & vbCrLf & "orangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree" & vbCrLf & "orange-tree", False) : call Assert (ret = "appleTree" & vbCrLf & "orangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree" & vbCrLf & "orange_tree", False) : call Assert (ret = "appleTree" & vbCrLf & "orangeTree", errorMsg)

	ret = ConvertToCamel("appleTree" & vbTab & "orangeTree"  , False) : call Assert (ret = "appleTree" & vbTab & "orangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree" & vbTab & "orange-tree", False) : call Assert (ret = "appleTree" & vbTab & "orangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree" & vbTab & "orange_tree", False) : call Assert (ret = "appleTree" & vbTab & "orangeTree", errorMsg)

	ret = ConvertToCamel("appleTree=orangeTree"  , False) : call Assert (ret = "appleTree=orangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree=orange-tree", False) : call Assert (ret = "appleTree=orangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree=orange_tree", False) : call Assert (ret = "appleTree=orangeTree", errorMsg)

	ret = ConvertToCamel("apple_tree=orange_tree あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", False) : call Assert (ret = "appleTree=orangeTree あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)

	' 2語以上の文字変換（Upper Case）
	ret = ConvertToCamel("appleTree orangeTree"  , True) : call Assert (ret = "AppleTree OrangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree orange-tree", True) : call Assert (ret = "AppleTree OrangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree orange_tree", True) : call Assert (ret = "AppleTree OrangeTree", errorMsg)

	ret = ConvertToCamel("appleTree" & vbLf & "orangeTree"  , True) : call Assert (ret = "AppleTree" & vbLf & "OrangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree" & vbLf & "orange-tree", True) : call Assert (ret = "AppleTree" & vbLf & "OrangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree" & vbLf & "orange_tree", True) : call Assert (ret = "AppleTree" & vbLf & "OrangeTree", errorMsg)

	ret = ConvertToCamel("appleTree" & vbCrLf & "orangeTree"  , True) : call Assert (ret = "AppleTree" & vbCrLf & "OrangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree" & vbCrLf & "orange-tree", True) : call Assert (ret = "AppleTree" & vbCrLf & "OrangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree" & vbCrLf & "orange_tree", True) : call Assert (ret = "AppleTree" & vbCrLf & "OrangeTree", errorMsg)

	ret = ConvertToCamel("appleTree" & vbTab & "orangeTree"  , True) : call Assert (ret = "AppleTree" & vbTab & "OrangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree" & vbTab & "orange-tree", True) : call Assert (ret = "AppleTree" & vbTab & "OrangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree" & vbTab & "orange_tree", True) : call Assert (ret = "AppleTree" & vbTab & "OrangeTree", errorMsg)

	ret = ConvertToCamel("appleTree=orangeTree"  , True) : call Assert (ret = "AppleTree=OrangeTree", errorMsg)
	ret = ConvertToCamel("apple-tree=orange-tree", True) : call Assert (ret = "AppleTree=OrangeTree", errorMsg)
	ret = ConvertToCamel("apple_tree=orange_tree", True) : call Assert (ret = "AppleTree=OrangeTree", errorMsg)

	ret = ConvertToCamel("apple_tree=orange_tree あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", True) : call Assert (ret = "AppleTree=OrangeTree あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)

End Sub

' ==============================================================================
' ConvertToSnakeOrKebab Test.
' Args   : 
' Return : 
' ==============================================================================
Sub ConvertToSnakeOrKebabTest()

	Dim errorMsg: errorMsg = "ConvertToSnakeOrKebab Failed"
	Dim ret

	ret = ConvertToSnakeOrKebab("", "-", False): call Assert (ret = "", errorMsg)

	' 単語としてみなされない文字
	ret = ConvertToSnakeOrKebab("!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", "-", False)                                : call Assert (ret = "!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)
	ret = ConvertToSnakeOrKebab(vbTab & vbCr & vbLf & vbCrLf & "!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", "-", False) : call Assert (ret = vbTab & vbCr & vbLf & vbCrLf & "!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)
	ret = ConvertToSnakeOrKebab("あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", "-", False)               : call Assert (ret = "あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)

	' 単純な文字変換
	ret = ConvertToSnakeOrKebab("snakeCase", "-", False)    : call Assert (ret = "snake-case", errorMsg)
	ret = ConvertToSnakeOrKebab("snake-case", "-", False)   : call Assert (ret = "snake-case", errorMsg)
	ret = ConvertToSnakeOrKebab("snake_case", "-", False)   : call Assert (ret = "snake-case", errorMsg)
	ret = ConvertToSnakeOrKebab("-snake-case-", "-", False) : call Assert (ret = "-snake-case-", errorMsg)
	ret = ConvertToSnakeOrKebab("_snake_case_", "-", False) : call Assert (ret = "-snake-case-", errorMsg)

	' 単純な文字変換（Upper Case）
	ret = ConvertToSnakeOrKebab("snakeCase", "-", True)    : call Assert (ret = "SNAKE-CASE", errorMsg)
	ret = ConvertToSnakeOrKebab("snake-case", "-", True)   : call Assert (ret = "SNAKE-CASE", errorMsg)
	ret = ConvertToSnakeOrKebab("snake_case", "-", True)   : call Assert (ret = "SNAKE-CASE", errorMsg)
	ret = ConvertToSnakeOrKebab("-snake-case-", "-", True) : call Assert (ret = "-SNAKE-CASE-", errorMsg)
	ret = ConvertToSnakeOrKebab("_snake_case_", "-", True) : call Assert (ret = "-SNAKE-CASE-", errorMsg)

	' 2語以上の文字変換
	ret = ConvertToSnakeOrKebab("appleTree orangeTree"  , "-", False) : call Assert (ret = "apple-tree orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree orange-tree", "-", False) : call Assert (ret = "apple-tree orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree orange_tree", "-", False) : call Assert (ret = "apple-tree orange-tree", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree" & vbLf & "orangeTree"  , "-", False) : call Assert (ret = "apple-tree" & vbLf & "orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree" & vbLf & "orange-tree", "-", False) : call Assert (ret = "apple-tree" & vbLf & "orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree" & vbLf & "orange_tree", "-", False) : call Assert (ret = "apple-tree" & vbLf & "orange-tree", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree" & vbCrLf & "orangeTree"  , "-", False) : call Assert (ret = "apple-tree" & vbCrLf & "orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree" & vbCrLf & "orange-tree", "-", False) : call Assert (ret = "apple-tree" & vbCrLf & "orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree" & vbCrLf & "orange_tree", "-", False) : call Assert (ret = "apple-tree" & vbCrLf & "orange-tree", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree" & vbTab & "orangeTree"  , "-", False) : call Assert (ret = "apple-tree" & vbTab & "orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree" & vbTab & "orange-tree", "-", False) : call Assert (ret = "apple-tree" & vbTab & "orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree" & vbTab & "orange_tree", "-", False) : call Assert (ret = "apple-tree" & vbTab & "orange-tree", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree=orangeTree"  , "-", False) : call Assert (ret = "apple-tree=orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree=orange-tree", "-", False) : call Assert (ret = "apple-tree=orange-tree", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree=orange_tree", "-", False) : call Assert (ret = "apple-tree=orange-tree", errorMsg)

	ret = ConvertToSnakeOrKebab("apple_tree=orange_tree あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", "-", False) : call Assert (ret = "apple-tree=orange-tree あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)

	' 2語以上の文字変換（Upper Case）
	ret = ConvertToSnakeOrKebab("appleTree orangeTree"  , "-", True) : call Assert (ret = "APPLE-TREE ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree orange-tree", "-", True) : call Assert (ret = "APPLE-TREE ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree orange_tree", "-", True) : call Assert (ret = "APPLE-TREE ORANGE-TREE", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree" & vbLf & "orangeTree"  , "-", True) : call Assert (ret = "APPLE-TREE" & vbLf & "ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree" & vbLf & "orange-tree", "-", True) : call Assert (ret = "APPLE-TREE" & vbLf & "ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree" & vbLf & "orange_tree", "-", True) : call Assert (ret = "APPLE-TREE" & vbLf & "ORANGE-TREE", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree" & vbCrLf & "orangeTree"  , "-", True) : call Assert (ret = "APPLE-TREE" & vbCrLf & "ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree" & vbCrLf & "orange-tree", "-", True) : call Assert (ret = "APPLE-TREE" & vbCrLf & "ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree" & vbCrLf & "orange_tree", "-", True) : call Assert (ret = "APPLE-TREE" & vbCrLf & "ORANGE-TREE", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree" & vbTab & "orangeTree"  , "-", True) : call Assert (ret = "APPLE-TREE" & vbTab & "ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree" & vbTab & "orange-tree", "-", True) : call Assert (ret = "APPLE-TREE" & vbTab & "ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree" & vbTab & "orange_tree", "-", True) : call Assert (ret = "APPLE-TREE" & vbTab & "ORANGE-TREE", errorMsg)

	ret = ConvertToSnakeOrKebab("appleTree=orangeTree"  , "-", True) : call Assert (ret = "APPLE-TREE=ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple-tree=orange-tree", "-", True) : call Assert (ret = "APPLE-TREE=ORANGE-TREE", errorMsg)
	ret = ConvertToSnakeOrKebab("apple_tree=orange_tree", "-", True) : call Assert (ret = "APPLE-TREE=ORANGE-TREE", errorMsg)

	ret = ConvertToSnakeOrKebab("apple_tree=orange_tree あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", "-", True) : call Assert (ret = "APPLE-TREE=ORANGE-TREE あいうえおｱｲｳｴｵ全角𠮷!""#$%&'()=^~\|@`[{;+:*]},<.>/?\", errorMsg)

End Sub

' Assert Method
Sub Assert(a, strOnFail)

	If a = False Then
		Err.Raise vbObjectError + 99999, , strOnFail
	End If

End Sub

' Include Method
Sub Include(strFile)

	Dim objFSO   : Set objFSO = CreateObject("Scripting.FileSystemObject")
	Dim objStream: Set objStream = objFSO.OpenTextFile(objFSO.GetFile(WScript.ScriptFullName).ParentFolder & "\" & strFile, 1, False, -1)

	ExecuteGlobal objStream.ReadAll() 
	objStream.Close

End Sub