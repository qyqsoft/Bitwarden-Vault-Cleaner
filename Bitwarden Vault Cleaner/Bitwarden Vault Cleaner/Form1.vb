Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports System.Text

Public Class Form1

    Private headerDict As New Dictionary(Of String, Integer)
    Private initColWidthList As New List(Of Single)
    Private contentList As New List(Of String())
    Private newContentList As New List(Of String())
    Private deleteContentIndexList As New List(Of Integer)
    Private processedContentIndexList As New List(Of Integer)
    Private header As String() = {}
    Private newFileName As String = String.Empty

    Private Sub Tsb_Open_Click(sender As Object, e As EventArgs) Handles Tsb_Open.Click
        Dim ofd As New OpenFileDialog
        '
        With ofd
            .AddExtension = True
            .CheckFileExists = True
            .CheckPathExists = True
            .Filter = "CSV|*.csv"
            .Multiselect = False
            .Title = "Open Bitwarden CSV Vault"
        End With
        '
        If ofd.ShowDialog = DialogResult.OK AndAlso Not String.IsNullOrEmpty(ofd.FileName) AndAlso File.Exists(ofd.FileName) Then
            initColWidthList.Clear()
            Lv_Content.Items.Clear()
            Lv_Content.Columns.Clear()
            Flp_Titel.Controls.Clear()
            OpenCsv(ofd.FileName)
            newFileName = Path.Combine(Path.GetDirectoryName(ofd.FileName), Path.GetFileNameWithoutExtension(ofd.FileName) & "_clean." & Path.GetExtension(ofd.FileName))
            Tssl_Source.Text = "Source: " & ofd.FileName
        End If
        '
        ofd.Dispose()
    End Sub


    Private Sub Tsb_Clean_Click(sender As Object, e As EventArgs) Handles Tsb_Clean.Click
        Dim changes As Form2_Chganges
        Dim dr As DialogResult
        Dim i As Integer = 0
        '
        If contentList.Count > 0 Then
            Clean()
            '
            If deleteContentIndexList.Count > 0 Then
                changes = New Form2_Chganges
                ShowContent(changes.Listview_NewContent, contentList, deleteContentIndexList, Color.MistyRose, processedContentIndexList)
                changes.Label_Changes.Text = Tsl_Info.Text
                For Each cb As CheckBox In Flp_Titel.Controls
                    If cb.Checked Then
                        changes.Flp_Titel.Controls.Add(New Label With {.Text = cb.Text})
                        '
                        For Each ch As ColumnHeader In changes.Listview_NewContent.Columns
                            If cb.Text = ch.Text Then ch.Text = "* " & ch.Text
                        Next
                    End If
                Next
                '
                'Spaltenbreite vom Hauptfenster übernehmen
                If Lv_Content.Columns.Count > 0 Then
                    For Each ch As ColumnHeader In Lv_Content.Columns
                        'If i > 0 Then changes.Listview_NewContent.Columns(i).Width = ch.Width
                        If i > 0 Then
                            If changes.Listview_NewContent.Columns(i).Text.IndexOf("* ") = 0 Then
                                changes.Listview_NewContent.Columns(i).Width = ch.Width + 20
                            Else
                                changes.Listview_NewContent.Columns(i).Width = ch.Width
                            End If
                        End If
                        i += 1
                    Next
                End If
                '
                dr = changes.ShowDialog
                If dr = DialogResult.OK Then
                    contentList.Clear()
                    contentList.AddRange(newContentList.ToArray)
                    ShowContent(Lv_Content, contentList, New List(Of Integer), Color.WhiteSmoke, New List(Of Integer))
                End If
                '
                changes.Close()
                changes.Dispose()
            Else
                MsgBox("No double entries found with your" & vbNewLine & "selected criteria for comparison.", MsgBoxStyle.OkOnly, "Bitwarden Vault Cleaner")
            End If
        End If
    End Sub


    Private Sub Tsb_Combine_Click(sender As Object, e As EventArgs) Handles Tsb_Combine.Click
        Dim changes As Form2_Chganges
        Dim dr As DialogResult
        Dim i As Integer = 0
        '
        If contentList.Count > 0 Then
            Combine()
            '
            If deleteContentIndexList.Count > 0 Then
                changes = New Form2_Chganges
                ShowContent(changes.Listview_NewContent, contentList, deleteContentIndexList, Color.WhiteSmoke, processedContentIndexList)
                changes.Label_Changes.Text = Tsl_Info.Text
                '
                'Spaltenbreite vom Hauptfenster übernehmen
                If Lv_Content.Columns.Count > 0 Then
                    For Each ch As ColumnHeader In Lv_Content.Columns
                        If i > 0 Then changes.Listview_NewContent.Columns(i).Width = ch.Width
                        i += 1
                    Next
                End If
                '
                dr = changes.ShowDialog
                If dr = DialogResult.OK Then
                    contentList.Clear()
                    contentList.AddRange(newContentList.ToArray)
                    ShowContent(Lv_Content, contentList, New List(Of Integer), Color.White, New List(Of Integer))
                End If
                '
                changes.Close()
                changes.Dispose()
            Else
                MsgBox("No entries can be combined.", MsgBoxStyle.OkOnly, "Bitwarden Vault Cleaner")
            End If
        End If
    End Sub


    Private Sub Tsb_Save_Click(sender As Object, e As EventArgs) Handles Tsb_Save.Click
        If Not String.IsNullOrEmpty(newFileName) AndAlso newContentList.Count > 0 Then
            Dim sfd As New SaveFileDialog
            Dim fs As FileStream
            Dim line As String
            '
            With sfd
                .AddExtension = True
                .AutoUpgradeEnabled = True
                .DefaultExt = "csv"
                .FileName = Path.GetFileName(newFileName)
                .Filter = "CSV|*.csv"
                .InitialDirectory = Path.GetFullPath(newFileName)
                .OverwritePrompt = True
                .Title = "Save Bitwarden CSV Vault"
                .ValidateNames = True
            End With
            '
            If sfd.ShowDialog = DialogResult.OK Then
                Me.Cursor = Cursors.WaitCursor
                '
                Try
                    fs = sfd.OpenFile()
                    line = String.Empty
                    '
                    For Each h As String In header
                        line &= h & ","
                    Next
                    '
                    line = line.TrimEnd(New Char() {","c}) & vbCrLf
                    fs.Write(Encoding.UTF8.GetBytes(line), 0, line.Length)
                    '
                    For Each s As String() In contentList
                        line = String.Empty
                        '
                        For Each t As String In s
                            If t.Contains(",") Then line &= """"
                            line &= t
                            If t.Contains(",") Then line &= """"
                            line &= ","
                        Next
                        '
                        line = line.TrimEnd(New Char() {","c}) & "," & vbCrLf
                        fs.Write(Encoding.UTF8.GetBytes(line), 0, line.Length)
                    Next
                    '
                    fs.Close()
                    fs.Dispose()
                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("Write error: " & ex.Message, MsgBoxStyle.OkOnly, "Bitwarden Vault Cleaner")
                End Try
            End If
            '
            sfd.Dispose()
            Me.Cursor = Cursors.Default
        End If
    End Sub


    Private Sub ShowContent(lView As ListView, cList As List(Of String()), markIndexList As List(Of Integer), markColor As Color, orgIndexList As List(Of Integer))
        If cList.Count <= 0 Then Exit Sub
        '
        Dim i As Integer = 0
        Dim lvi As ListViewItem
        Dim ch As ColumnHeader
        Dim chList As New List(Of ColumnHeader)
        Dim lviList As New List(Of ListViewItem)
        Dim headerWidthList As New List(Of Integer)
        Dim hw As Integer
        '
        'wenn schon ColumnHeader vorhanden dann Breite merken
        If lView.Columns.Count > 0 Then
            For Each ch In lView.Columns
                If i > 0 Then headerWidthList.Add(ch.Width)
                i += 1
            Next
        Else
            For Each hw In initColWidthList
                headerWidthList.Add(Math.Max(10, hw + 6))
            Next
        End If
        '
        ch = New ColumnHeader With {.Name = "ROW", .Text = "ROW", .Width = 40}
        chList.Add(ch)
        '
        i = 0
        For Each h As String In header
            If headerWidthList.Count >= i + 1 Then hw = headerWidthList.ElementAt(i) Else hw = 80
            ch = New ColumnHeader With {.Name = "colhead_" & h, .Text = h, .Width = hw}
            chList.Add(ch)
            i += 1
        Next
        '
        i = 0
        For Each s As String() In cList
            lvi = New ListViewItem(i.ToString)
            If orgIndexList.Contains(i) Then lvi.BackColor = Color.Honeydew
            If markIndexList.Contains(i) Then lvi.BackColor = markColor
            '
            For Each t As String In s
                lvi.SubItems.Add(t)
            Next
            '
            lviList.Add(lvi)
            i += 1
        Next
        '
        If lView Is Lv_Content Then Tssl_Rows.Text = "Rows: " & i.ToString
        '
        FillListview(lView, chList, lviList)
    End Sub


    Private Sub FillListview(lv As ListView, columnList As List(Of ColumnHeader), itemsList As List(Of ListViewItem))
        lv.SuspendLayout()
        lv.Clear()
        lv.Columns.AddRange(columnList.ToArray)
        lv.Items.AddRange(itemsList.ToArray)
        lv.ResumeLayout()
        lv.Refresh()
    End Sub


    Private Sub OpenCsv(filename As String)
        Dim reader As TextFieldParser = My.Computer.FileSystem.OpenTextFieldParser(filename)
        '
        If reader.LineNumber > 0 Then
            Dim currentRow() As String
            Dim i As Integer = 0
            Dim hc As Integer = 0
            Dim cb As New CheckBox
            '
            reader.TextFieldType = FieldType.Delimited
            reader.Delimiters = {","}
            '
            headerDict.Clear()
            initColWidthList.Clear()
            contentList.Clear()
            newContentList.Clear()
            deleteContentIndexList.Clear()
            processedContentIndexList.Clear()
            header = {}
            Flp_Titel.Controls.Clear()
            '
            While Not reader.EndOfData
                Try
                    currentRow = reader.ReadFields()
                    '
                    If i = 0 Then
                        header = currentRow
                        headerDict.Clear()
                        For Each h As String In header
                            headerDict.Add(h, hc)
                            hc += 1
                        Next
                        '
                        For Each currentField As String In currentRow
                            cb = New CheckBox With {.Text = currentField}
                            Select Case currentField
                                Case "type", "name", "login_username", "login_password", "login_totp"
                                    cb.Checked = True
                                Case "folder", "favorite", "notes", "fields", "login_uri"
                                    cb.Checked = False
                                Case Else
                                    cb.Checked = True
                            End Select
                            Flp_Titel.Controls.Add(cb)
                            initColWidthList.Add(GetTextSize(currentField))
                        Next
                    Else
                        contentList.Add(currentRow)
                        SetInitColumnWidth(currentRow)
                    End If
                    '
                    i += 1
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.", MsgBoxStyle.OkOnly, "Bitwarden Vault Cleaner")
                End Try
                '
            End While
            '
            Flp_Titel.Refresh()
            ShowContent(Lv_Content, contentList, New List(Of Integer), Color.White, New List(Of Integer))
        End If
    End Sub


    Private Function GetTextSize(txt As String) As Single
        Dim g As Graphics = Me.CreateGraphics
        Dim sz As SizeF
        '
        g.PageUnit = GraphicsUnit.Pixel  ' ist default
        sz = g.MeasureString(txt, Lv_Content.Font)
        g.Dispose()
        '
        Return sz.Width
    End Function


    Private Sub SetInitColumnWidth(entry() As String)
        Dim i As Integer = 0
        '
        For Each s As String In entry
            initColWidthList.Item(i) = Math.Max(initColWidthList.Item(i), GetTextSize(s))
            i += 1
        Next
    End Sub


    Private Function CleanUriString(uri As String) As String
        Dim ret As String = String.Empty
        Dim u() As String = uri.Split(","c)
        '
        For Each s As String In u
            ret &= s.TrimEnd("/"c) & ","
        Next
        '
        Return ret.TrimEnd(",c")
    End Function


    Private Function NoDoubleUri(uri As String) As String
        Dim ret As String = String.Empty
        Dim uriList As New List(Of String)(uri.Split(","c))
        Dim i As Integer = 0
        Dim k As Integer
        '
        If uriList.Count > 0 Then
            Do
                k = uriList.LastIndexOf(uriList.Item(i))
                If k > i Then uriList.RemoveAt(k)
                i += 1
            Loop Until i >= uriList.Count
        End If
        '
        For Each u As String In uriList
            ret &= u & ","
        Next
        '
        Return ret.TrimEnd(",c")
    End Function


    Private Sub Clean()
        Dim z As Integer = 0
        Dim checkMask As New List(Of Boolean)
        Dim uriIndexList As New List(Of Integer)
        '
        newContentList = New List(Of String())
        deleteContentIndexList = New List(Of Integer)
        processedContentIndexList = New List(Of Integer)
        '
        For Each cb As CheckBox In Flp_Titel.Controls
            If cb.Checked Then checkMask.Add(True) Else checkMask.Add(False)
            If cb.Text.ToLower.Contains("_uri") Or cb.Text.ToLower.Contains("_url") Then uriIndexList.Add(z)
            z += 1
        Next
        '
        If z > 0 AndAlso contentList.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            '
            Dim workList As New List(Of String())(contentList)
            Dim s() As String
            Dim fnd As Integer
            Dim d As Integer = 0
            Dim u As String
            Dim i As Integer
            '
            z = 0
            While z <= workList.Count - 1
                s = workList.ElementAt(z)
                '                '
                Do
                    fnd = FindDouble(s, workList, checkMask)
                    '
                    If fnd > 0 AndAlso fnd <> z Then
                        For i = 0 To s.Length - 1
                            If uriIndexList.Contains(i) Then u = CleanUriString(workList(fnd)(i)) Else u = workList(fnd)(i)
                            If Not s(i).Contains(u) Then
                                If s(i).Length <= 0 Then s(i) &= workList(fnd)(i) Else s(i) &= "," & workList(fnd)(i)
                            End If
                        Next
                        processedContentIndexList.Add(z + d)
                        deleteContentIndexList.Add(fnd + d)
                        workList.RemoveAt(fnd) 'gefundenes Element löschen um nicht mehrmals gefunden zu werden
                        d += 1
                    End If
                Loop Until fnd < 0
                '
                If uriIndexList.Count > 0 Then
                    For Each i In uriIndexList
                        s(i) = NoDoubleUri(s(i))
                    Next
                End If
                '
                newContentList.Add(s)
                z += 1
                Tsl_Info.Text = "Processed: " & (z + d).ToString & " / " & contentList.Count.ToString & "   Double entries: " & d.ToString
            End While
            '

        End If
        '
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub Combine()
        Dim z As Integer = 0
        Dim checkMask As New List(Of Boolean)
        Dim uriIndexList As New List(Of Integer)
        '
        newContentList = New List(Of String())
        deleteContentIndexList = New List(Of Integer)
        processedContentIndexList = New List(Of Integer)
        '
        For Each cb As CheckBox In Flp_Titel.Controls
            If cb.Text.ToLower.Contains("_uri") Or cb.Text.ToLower.Contains("_url") Then
                checkMask.Add(False)
                uriIndexList.Add(z)
            Else
                checkMask.Add(True)
            End If
            z += 1
        Next
        '
        If z > 0 AndAlso contentList.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            '
            Dim workList As New List(Of String())(contentList)
            Dim s() As String
            Dim fnd As Integer
            Dim d As Integer = 0
            '
            z = 0
            While z <= workList.Count - 1
                s = workList.ElementAt(z)
                '
                Do
                    fnd = FindDouble(s, workList, checkMask)
                    '
                    If fnd >= 0 AndAlso fnd > z Then
                        For Each i As Integer In uriIndexList
                            If Not s(i).Contains(workList.ElementAt(fnd)(i)) Then s(i) &= "," & workList.ElementAt(fnd)(i)
                        Next
                        '
                        processedContentIndexList.Add(contentList.IndexOf(s))
                        deleteContentIndexList.Add(fnd + d)
                        '
                        d += 1
                        workList.RemoveAt(fnd) 'gefundenes Element löschen um ewiges Zufügen von URLs zu verhindern
                    End If
                Loop Until fnd < 0
                '
                newContentList.Add(s)
                z += 1
                Tsl_Info.Text = "Processed: " & (z + d).ToString & " / " & contentList.Count.ToString & "   Combined entries: " & d.ToString
            End While
        End If
        '
        Me.Cursor = Cursors.Default
    End Sub


    Private Function FindDouble(search As String, skip As Integer, searchList As List(Of Integer)) As Integer
        Dim found As Integer = -1
        Dim z As Integer = 0
        Dim i As Integer
        Dim line As String
        '
        For Each s As String() In contentList
            line = String.Empty
            i = 0
            '
            For Each t As String In s
                If searchList.Contains(i) Then line &= t
                i += 1
            Next
            '
            If search = line AndAlso z <> skip Then
                found = z
                Exit For
            End If
            '
            z += 1
        Next
        '
        Return found
    End Function


    Private Function FindDouble(searchItem() As String, searchList As List(Of String()), checkMask As List(Of Boolean)) As Integer
        Dim found As Integer = -1
        Dim z As Integer = 0
        Dim skip As Integer = searchList.IndexOf(searchItem)
        '
        For Each s() As String In searchList
            If z <> skip AndAlso SameEntry(searchItem, s, checkMask) Then
                found = z
                Exit For
            End If
            '
            z += 1
        Next
        '
        Return found
    End Function


    Private Function SameEntry(entry1() As String, entry2() As String, checkMask As List(Of Boolean)) As Boolean
        Dim ret As Boolean
        '
        If entry1.Length = entry2.Length AndAlso entry1.Length = checkMask.Count Then
            ret = True
            '
            For i As Integer = 0 To entry1.Length - 1
                If checkMask.Item(i) AndAlso entry1(i) <> entry2(i) Then
                    ret = False
                    Exit For
                End If
            Next
        Else
            ret = False
        End If
        '
        Return ret
    End Function

End Class
