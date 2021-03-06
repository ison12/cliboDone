Option Explicit

' ==============================================================================
' キーワード情報を解析する。
'
' 戻り値は配列。配列の各要素は、さらに配列となる。
'
' 各要素の配列の1番目は、文字列の開始インデックス（1はじまり）
' 各要素の配列の2番目は、文字列の長さ
'
' Args   : contents ... contents string.
' Return : キーワード情報配列
' ==============================================================================
Function ParseKeyword(contents)

	Dim isInit: isInit = False

	Dim ret()
	Dim retLen: retLen = 0

	Dim isBeginWord: isBeginWord = False
	Dim beginIndex: beginIndex = 0

	Dim i
	Dim str, strLen, chr, chrCode

	str = contents
	strLen = Len(contents)

	For i = 1 To strLen

		chr = Mid(str, i, 1)
		chrCode = AscW(chr)

		If chrCode = 45 _
			Or  chrCode = 95 _
			Or  (48 <= chrCode And chrCode <= 57) _
			Or  (65 <= chrCode And chrCode <= 90) _
			Or  (97 <= chrCode And chrCode <= 122) _
		Then
			' -
			' _
			' 0 ~ 9
			' A ~ Z
			' a ~ z

			If isBeginWord = False Then
				beginIndex = i
				isBeginWord = True
			End If

		Else

			If isBeginWord = True Then

				ReDim Preserve ret(retLen): isInit = True
				ret(retLen) = Array(beginIndex, i - beginIndex)
				retLen = retLen + 1

				isBeginWord = False
			End If
		
		End If

	Next

	If isBeginWord = True Then
		ReDim Preserve ret(retLen): isInit = True
		ret(retLen) = Array(beginIndex, i - beginIndex)
		retLen = retLen + 1
	End If

	' Return
	If isInit = True Then
		ParseKeyword = ret
	Else
		ParseKeyword = False
	End If

End Function

' ==============================================================================
' アルファベットの大文字かどうかを判定する
'
' args   : char ... character.
' return : True 大文字, False 大文字ではない
' ==============================================================================
Function IsAlphabetUpper(char)

	If char = "" Then
		IsAlphabetUpper = False
		Exit Function
	End If

	Dim chrCode
	chrCode = AscW(char)

	If (65 <= chrCode And chrCode <= 90) Then
		IsAlphabetUpper = True
		Exit Function
	End If

	IsAlphabetUpper = False

End Function

' ==============================================================================
' アルファベットの小文字かどうかを判定する
'
' args   : char ... character.
' return : True 小文字, False 小文字ではない
' ==============================================================================
Function IsAlphabetLower(char)

	If char = "" Then
		IsAlphabetLower = False
		Exit Function
	End If

	Dim chrCode
	chrCode = AscW(char)

	If (97 <= chrCode And chrCode <= 122) Then
		IsAlphabetLower = True
		Exit Function
	End If

	IsAlphabetLower = False

End Function

' ==============================================================================
' アルファベットかどうかを判定する
'
' args   : char ... character.
' return : True アルファベット, False アルファベットではない
' ==============================================================================
Function IsAlphabet(char)

	If char = "" Then
		IsAlphabet = False
		Exit Function
	End If

	Dim chrCode
	chrCode = AscW(char)

	If (65 <= chrCode And chrCode <= 90) Or _
	   (97 <= chrCode And chrCode <= 122) Then
	   IsAlphabet = True
		Exit Function
	End If

	IsAlphabet = False

End Function

' ==============================================================================
' スネークケース判定
' Example) snake_value
'
' Args   : contents ... contents string.
' Return : True 定数, False 定数ではない
' ==============================================================================
Function IsSnake(contents)

	Dim i
	Dim str, strLen, chr, chrCode

	str = contents
	strLen = Len(contents)

	For i = 1 To strLen

		chr = Mid(str, i, 1)
		chrCode = AscW(chr)

		If chrCode = 95 _
		Then
			' _

			' Return
			IsSnake = True
			Exit Function
		End If

	Next

	' Return
	IsSnake = False

End Function

' ==============================================================================
' ケバブ判定
' Example) kebab-value
'
' Args   : contents ... contents string.
' Return : True ケバブ, False ケバブではない
' ==============================================================================
Function IsKebab(contents)

	Dim i
	Dim str, strLen, chr, chrCode

	str = contents
	strLen = Len(contents)

	For i = 1 To strLen

		chr = Mid(str, i, 1)
		chrCode = AscW(chr)

		If chrCode = 45 _
		Then
			' -

			' Return
			IsKebab = True
			Exit Function
		End If

	Next

	' Return
	IsKebab = False

End Function

' ==============================================================================
' Convert to camel.
' Example 1) db-tables → dbTables
' Example 3) db_tables → dbTables
' Example 1) db-tables → DbTables
' Example 3) db_tables → DbTables
'
' Args   : contents          ... Contents.
'        : isUpper           ... True Upper case, False Lower case.
' Return : Result of conversion.
' ==============================================================================
Function ConvertToCamel(contents, isUpper)

	' ---------------------------------------------
	' キーワード情報を解析する
	' ---------------------------------------------
	Dim parseInfo
	Dim parseInfoList

	parseInfoList = ParseKeyword(contents)
	If IsArray(parseInfoList) = False Then
		ConvertToCamel = contents
		Exit Function
	End If

	' ---------------------------------------------
	' 解析したキーワード情報リストを順次探索する
	' ---------------------------------------------
	Dim i, j, k
	Dim ret
	Dim keyword, keywordChar, keywordLen, keywordConv, isNextUpper

	j = 1
	For i = LBound(parseInfoList) To UBound(parseInfoList)

		parseInfo = parseInfoList(i)

		' ---------------------------------------------
		' 解析したキーワード情報の前方の文字列を取得
		' ---------------------------------------------
		If j < parseInfo(0) Then
			ret = ret & Mid(contents, j, parseInfo(0) - j)
		End If

		' ---------------------------------------------
		' 解析したキーワード情報の本体を変換する
		' ---------------------------------------------
		keyword = Mid(contents, parseInfo(0), parseInfo(1))
		keyword = Replace(keyword, "-", "_") ' ハイフンもアンダーバーとして取り扱う

		If IsSnake(keyword) Or IsKebab(keyword) Then

			keyword = LCase(keyword) ' スネークケースまたはケバブケースなので一旦小文字に変換する

			isNextUpper = False
			keywordConv = ""
			keywordLen = Len(keyword)
			For k = 1 To keywordLen
				keywordChar = Mid(keyword, k, 1)
				if keywordChar = "_" Then
					' アンダーバーの場合は文字として付与しない
					isNextUpper = True
				Else
					If isNextUpper = True Then
						' 前回の文字がアンダーバーなら小文字に変換する
						keywordConv = keywordConv & UCase(keywordChar)
						isNextUpper = False
					Else
						' そのまま文字を結合する
						keywordConv = keywordConv & keywordChar
					End If
				End If
			Next
		Else

			keywordConv = keyword
		End If

		If Len(keywordConv) >= 1 Then
			If isUpper Then
				keywordConv = UCase(Mid(keywordConv, 1, 1)) & Mid(keywordConv, 2)
			Else
				keywordConv = LCase(Mid(keywordConv, 1, 1)) & Mid(keywordConv, 2)
			End If
		End If

		ret = ret & keywordConv

		j = parseInfo(0) + parseInfo(1)
	Next

	' ---------------------------------------------
	' 解析したキーワード情報の後方の文字列を取得
	' ---------------------------------------------
	If j <= Len(contents) Then
		ret = ret & Mid(contents, j)
	End If

	' Return
	ConvertToCamel = ret

End Function

' ==============================================================================
' Convert to snake or kebab.
' Example 1) DBTables → db-tables
' Example 2) DBTables → DB_TABLES
' Example 3) DBTables → db-tables
' Example 4) DBTables → DB-TABLES
'
' Args   : contents          ... Contents.
'        : delimiter         ... Delimiter word.
'        : isUpper           ... True Upper case, False Lower case.
' Return : Result of conversion.
' ==============================================================================
Function ConvertToSnakeOrKebab(contents, delimiter, isUpper)

	' ---------------------------------------------
	' キーワード情報を解析する
	' ---------------------------------------------
	Dim parseInfo
	Dim parseInfoList

	parseInfoList = ParseKeyword(contents)
	If IsArray(parseInfoList) = False Then
		ConvertToSnakeOrKebab = contents
		Exit Function
	End If

	' ---------------------------------------------
	' 解析したキーワード情報リストを順次探索する
	' ---------------------------------------------
	Dim i, j, k
	Dim ret
	Dim keyword, keywordChar, keywordNextChar, keywordLen, keywordConv, isBeforeUpper

	j = 1
	For i = LBound(parseInfoList) To UBound(parseInfoList)

		parseInfo = parseInfoList(i)

		' ---------------------------------------------
		' 解析したキーワード情報の前方の文字列を取得
		' ---------------------------------------------
		If j < parseInfo(0) Then
			ret = ret & Mid(contents, j, parseInfo(0) - j)
		End If

		' ---------------------------------------------
		' 解析したキーワード情報の本体を変換する
		' ---------------------------------------------
		keyword = Mid(contents, parseInfo(0), parseInfo(1))
		keyword = Replace(keyword, "-", delimiter)
		keyword = Replace(keyword, "_", delimiter)

		If IsSnake(keyword) Or IsKebab(keyword) Then

			keywordConv = keyword
		Else

			isBeforeUpper = False
			keywordConv = ""
			keywordLen = Len(keyword)
			For k = 1 To keywordLen
				keywordChar = Mid(keyword, k, 1)
				keywordNextChar = Mid(keyword, k + 1, 1)
				if k > 1 And IsAlphabet(keywordChar) And IsAlphabetUpper(keywordChar) Then
					' ※DBTables → db_tables のように変換したいので大文字が連続して続く場合の考慮を行う
					' 大文字の場合
					If isBeforeUpper = True And _
					  ( _
					      keywordNextChar = "" Or _
						 (keywordNextChar <> "" And (Not IsAlphabet(keywordNextChar) Or (IsAlphabet(keywordNextChar) And IsAlphabetUpper(keywordNextChar)))) _
					  ) Then
						' 直前の文字が大文字で次回の文字も大文字なら delimiter は付与しない
						keywordConv = keywordConv & keywordChar
					Else
						' 大文字なので delimiter を付与
						keywordConv = keywordConv & delimiter & keywordChar
					End If
				Else
					' そのまま文字を結合する
					keywordConv = keywordConv & keywordChar
				End If

				If IsAlphabet(keywordChar) And IsAlphabetUpper(keywordChar) Then
					isBeforeUpper = True
				Else
					isBeforeUpper = False
				End If

			Next
		End If

		If isUpper Then
			keywordConv = UCase(keywordConv)
		Else
			keywordConv = LCase(keywordConv)
		End If

		ret = ret & keywordConv

		j = parseInfo(0) + parseInfo(1)
	Next

	' ---------------------------------------------
	' 解析したキーワード情報の後方の文字列を取得
	' ---------------------------------------------
	If j <= Len(contents) Then
		ret = ret & Mid(contents, j)
	End If

	' Return
	ConvertToSnakeOrKebab = ret

End Function
