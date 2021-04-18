Public Class About
    WithEvents rch As New System.Windows.Forms.RichTextBox()

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        ' Set the title of the form.

        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Title = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Content = My.Application.Info.ProductName
        Me.LabelVersion.Content = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyRight.Content = My.Application.Info.Copyright
        Me.LabelCompanyName.Content = My.Application.Info.CompanyName
        Dim doc As New FlowDocument

        rch.Text = My.Application.Info.Description
        rch.BackColor = System.Drawing.Color.White
        rch.DetectUrls = True
        rch.ReadOnly = True
        rch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        WindowsFormsHost1.Child = rch

        '        This is a richTextBox. And this is a
        '                <Hyperlink NavigateUri="http://www.microsoft.com">Hyperlink</Hyperlink> .
    End Sub


    Private Sub txtdescription_MouseRightButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        e.Handled = False
    End Sub

    Private Sub rch_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles rch.LinkClicked
        Process.Start("iexplore.exe", e.LinkText)
    End Sub
End Class
