Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports System.Text

Public Class Form1

    Private headerDict As New Dictionary(Of String, Integer)
    Private contentList As New List(Of String())
    Private newContentList As New List(Of String())
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
            OpenCsv(ofd.FileName)
            newFileName = Path.Combine(Path.GetDirectoryName(ofd.FileName), Path.GetFileNameWithoutExtension(ofd.FileName) & "_clean." & Path.GetExtension(ofd.FileName))
            Tssl_File.Text = ofd.FileName
        End If
        '
        ofd.Dispose()
    End Sub


    Private Sub Tsb_Clean_Click(sender As Object, e As EventArgs) Handles Tsb_Clean.Click
        Clean()
        contentList.Clear()
        contentList.AddRange(newContentList.ToArray)
        ShowContent(contentList)
    End Sub


    Private Sub Tsb_Combine_Click(sender As Object, e As EventArgs) Handles Tsb_Combine.Click
        Combine()
        contentList.Clear()
        contentList.AddRange(newContentList.ToArray)
        ShowContent(contentList)
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
                            If t.IndexOf(","c) >= 0 Then line &= """"
                            line &= t
                            If t.IndexOf(","c) >= 0 Then line &= """"
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
                    MsgBox("Write error: " & ex.Message, "Bitwarden Vault Cleaner")
                End Try
            End If
            '
            sfd.Dispose()
            Me.Cursor = Cursors.Default
        End If
    End Sub


    Private Sub ShowContent(cList As List(Of String()))
        If cList.Count <= 0 Then Exit Sub
        '
        Dim i As Integer = 0
        Dim lvi As ListViewItem
        Dim lviList As New List(Of ListViewItem)
        Dim ch As ColumnHeader
        Dim headerWidthList As New List(Of Integer)
        Dim hw As Integer
        '
        'wenn schon ColumnHeader vorhanden dann Breite merken
        If Lv_Content.Columns.Count > 0 Then
            For Each ch In Lv_Content.Columns
                If i > 0 Then headerWidthList.Add(ch.Width)
                i += 1
            Next
        End If
        '
        Lv_Content.SuspendLayout()
        Lv_Content.Clear()
        Lv_Content.Columns.Add("ROW", 40)
        '
        i = 0
        For Each h As String In header
            If headerWidthList.Count >= i + 1 Then hw = headerWidthList.ElementAt(i) Else hw = 80
            Lv_Content.Columns.Add(h, hw)
            i += 1
        Next
        '
        i = 0
        For Each s As String() In cList
            lvi = New ListViewItem(i.ToString)
            '
            For Each t As String In s
                lvi.SubItems.Add(t)
            Next
            '
            lviList.Add(lvi)
            i += 1
        Next
        '
        Lv_Content.Items.AddRange(lviList.ToArray)
        Lv_Content.ResumeLayout()
        Lv_Content.Refresh()
    End Sub


    Private Sub OpenCsv(filename As String)
        Dim reader As TextFieldParser = My.Computer.FileSystem.OpenTextFieldParser(filename)

        If reader.LineNumber > 0 Then
            Dim currentRow As String()
            Dim i As Integer = 0
            Dim hc As Integer = 0
            Dim cb As New CheckBox
            '
            reader.TextFieldType = FieldType.Delimited
            reader.Delimiters = {","}
            '
            contentList.Clear()
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
                            cb = New CheckBox With {.Text = currentField, .Checked = True}
                            Flp_Titel.Controls.Add(cb)
                        Next
                    Else
                        contentList.Add(currentRow)
                    End If
                    '
                    i += 1
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.", "Bitwarden Vault Cleaner")
                End Try
                '
            End While
            '
            Flp_Titel.Refresh()
            ShowContent(contentList)
        End If
    End Sub


    Private Sub Clean()
        Dim z As Integer = 0
        Dim checkedHeaders As New List(Of Integer)
        '
        newContentList = New List(Of String())
        '
        For Each cb As CheckBox In Flp_Titel.Controls
            If cb.Checked Then checkedHeaders.Add(z)
            z += 1
        Next
        '
        If checkedHeaders.Count > 0 AndAlso contentList.Count > 0 Then
            Dim i As Integer = 0
            Dim srch As String
            Dim d As Integer = 0
            '
            z = 0
            Me.Cursor = Cursors.WaitCursor
            '
            For Each s As String() In contentList
                srch = String.Empty
                i = 0
                '
                'Suchstring erstellen
                For Each t As String In s
                    If checkedHeaders.Contains(i) Then srch &= t
                    i += 1
                Next
                '
                If FindDouble(srch, z, checkedHeaders) < 0 Then
                    newContentList.Add(s)
                Else
                    d += 1
                End If
                '
                z += 1
                Tsl_Info.Text = "Processed: " & z.ToString & " / " & contentList.Count.ToString & "   Double entries: " & d.ToString
            Next
        End If
        '
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub Combine()
        Dim z As Integer = 0
        Dim checkedHeaders As New List(Of Integer)
        Dim combCol As Integer = -1
        Dim workDict As New Dictionary(Of Integer, String())
        '
        newContentList = New List(Of String())
        '
        For Each s As String() In contentList
            workDict.Add(z, s)
            z += 1
        Next
        '
        z = 0
        For Each cb As CheckBox In Flp_Titel.Controls
            If cb.Text.ToLower.IndexOf("_uri") > 0 Then
                combCol = z
            Else
                checkedHeaders.Add(z)
            End If
            z += 1
        Next
        '
        If combCol >= 0 AndAlso contentList.Count > 0 Then
            Dim i As Integer = 0
            Dim fnd As Integer
            Dim srch As String
            Dim d As Integer = 0
            Dim s As String()
            '
            z = 0
            Me.Cursor = Cursors.WaitCursor
            '
            While z <= contentList.Count - 1
                s = contentList.ElementAt(z)
                '
                'Suchstring erstellen
                srch = String.Empty
                i = 0
                For Each t As String In s
                    If i <> combCol Then srch &= t
                    i += 1
                Next
                '
                Do
                    fnd = FindDouble(srch, z, checkedHeaders)
                    If fnd >= 0 AndAlso fnd <> z Then
                        s(combCol) &= "," & contentList.ElementAt(fnd)(combCol)
                        d += 1
                        contentList.RemoveAt(fnd) 'gefundenes Element löschen um ewiges Zufügen von URLs zu verhindern
                    End If
                Loop Until fnd < 0
                '
                newContentList.Add(s)
                z += 1
                Tsl_Info.Text = "Processed: " & z.ToString & " / " & contentList.Count.ToString & "   Combined entries: " & d.ToString
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

End Class
