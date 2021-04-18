Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports System.Windows.Controls.Primitives
Imports System.Windows.Automation
Class MainWindow

    Dim colordialog1 As New ColorDialog

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub button_Copy_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles button_Copy.Click
        If txtR.Text <> "" And txtG.Text <> "" And txtB.Text <> "" Then
            colordialog1.Color = Color.FromArgb(txtR.Text, txtG.Text, txtB.Text)
        End If
        colordialog1.FullOpen = True
        If colordialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtR.Text = colordialog1.Color.R
            txtG.Text = colordialog1.Color.G
            txtB.Text = colordialog1.Color.B
        End If
    End Sub

    Private Sub txtB_GotFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles txtB.GotFocus, txtG.GotFocus, txtR.GotFocus
        c = sender
    End Sub



    Private Sub txtB_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtB.KeyDown, txtG.KeyDown, txtR.KeyDown
        Select Case e.Key
            Case Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9, Key.NumPad0, Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.Tab
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
        If sender.text.contains(" ") Then
            sender.text = sender.text.replace(" ", "")
            sender.CaretIndex = sender.Text.Length
        End If

    End Sub

    Public Sub Check(ByVal value As System.Windows.Controls.TextBox)
        Try
            If value.Text > 255 Then
                value.Text = 255
                value.CaretIndex = value.Text.Length
            End If
        Catch ex As Exception
            value.Text = 0
        End Try
    End Sub

    Dim hex As New HexConvertors

    Private Sub txtB_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles txtB.TextChanged, txtG.TextChanged, txtR.TextChanged
        Check(sender)
        If txtR.Text <> "" And txtG.Text <> "" And txtB.Text <> "" Then
            Try
                lblSample.Background = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(txtR.Text, txtG.Text, txtB.Text))
                txtHex.Text = "#" & hex.rgbToHex(txtR.Text, txtG.Text, txtB.Text)
            Catch ex As Exception
                txtHex.Text = ""
            End Try
        End If
    End Sub
    Dim c As System.Windows.Controls.TextBox

    Private Sub txtB_MouseWheel(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseWheelEventArgs) Handles txtB.MouseWheel, txtG.MouseWheel, txtR.MouseWheel
        If e.Delta = 120 Then
            c.Text = (Convert.ToInt32(c.Text) + 1).ToString()
            c.CaretIndex = c.Text.Length
        ElseIf e.Delta = -120 Then
            If c.Text > 0 Then
                c.Text = (Convert.ToInt32(c.Text) - 1).ToString()
                c.CaretIndex = c.Text.Length
            End If
        End If
    End Sub


    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        txtR.Text = 0
        txtG.Text = 0
        txtB.Text = 0
        txtR.Focus()

        txtR.Tag = Colors.Red
        txtG.Tag = Colors.Green
        txtB.Tag = Colors.Blue
        txtHex.Tag = Colors.Gray


        HtxtR.Tag = Colors.Red
        HtxtG.Tag = Colors.Green
        HtxtB.Tag = Colors.Blue
        HtxtHex.Tag = Colors.Gray

        txtC.Tag = Colors.Cyan
        txtM.Tag = Colors.Magenta
        txtY.Tag = Colors.Yellow
        txtK.Tag = Colors.Gray
        txtHexToCMYK.Tag = Colors.Gray

    End Sub

    Private Sub expander_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles RBGHex.Collapsed, HexCMYK.Collapsed, HEXRGB.Collapsed
        sender.Height = 192 - 163
    End Sub

    Private Sub expander_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles RBGHex.Expanded, HexCMYK.Expanded, HEXRGB.Expanded
        sender.Height = 192
    End Sub

    Private Sub HtxtHex_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles HtxtHex.TextChanged
        If HtxtHex.Text.Length = 7 Then
            Try
                Dim rgb() As Int32 = hex.HexToRgb(HtxtHex.Text)
                HtxtR.Text = rgb(0)
                HtxtG.Text = rgb(1)
                HtxtB.Text = rgb(2)
                hlblSample.Background = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(rgb(0), rgb(1), rgb(2)))
                txtHexToCMYK.Text = HtxtHex.Text
            Catch ex As Exception
                HtxtHex.Text = ""
            End Try
        ElseIf HtxtHex.Text.Length = 6 Then
            Try
                Dim rgb() As Int32 = hex.HexToRgb("#" & HtxtHex.Text)
                HtxtR.Text = rgb(0)
                HtxtG.Text = rgb(1)
                HtxtB.Text = rgb(2)
                hlblSample.Background = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(rgb(0), rgb(1), rgb(2)))
                txtHexToCMYK.Text = HtxtHex.Text
            Catch ex As Exception
                HtxtHex.Text = ""
            End Try
        End If

    End Sub

    Private Sub txtHexToCMYK_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles txtHexToCMYK.TextChanged
        If txtHexToCMYK.Text.Length = 7 Then
            Try
                Dim rgb() As Int32 = hex.HexToRgb(txtHexToCMYK.Text)
                Dim cmyk() As Double = hex.hexToCMYK(txtHexToCMYK.Text)
                txtC.Text = Math.Round(cmyk(0), 3)
                txtM.Text = Math.Round(cmyk(1), 3)
                txtY.Text = Math.Round(cmyk(2), 3)
                txtK.Text = Math.Round(cmyk(3), 3)
                lblCmyk.Background = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(rgb(0), rgb(1), rgb(2)))
            Catch ex As Exception
                txtHexToCMYK.Text = ""
            End Try
        ElseIf txtHexToCMYK.Text.Length = 6 Then
            Try
                Dim rgb() As Int32 = hex.HexToRgb("#" & txtHexToCMYK.Text)
                Dim cmyk() As Double = hex.hexToCMYK("#" & txtHexToCMYK.Text)
                Dim cm As New ColorConverter
                txtC.Text = Math.Round(cmyk(0), 2)
                txtM.Text = Math.Round(cmyk(1), 2)
                txtY.Text = Math.Round(cmyk(2), 2)
                txtK.Text = Math.Round(cmyk(3), 2)
                lblCmyk.Background = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(rgb(0), rgb(1), rgb(2)))
            Catch ex As Exception
                txtHexToCMYK.Text = ""
            End Try
        End If
    End Sub

    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAbout.Click
        'Dim d As Double = 12.1255
        'MsgBox(String.Format("{0} : {1}", d, Math.Round(d, 3)))
        'Return
        Dim Abt As New About
        Abt.ShowDialog()
    End Sub

    Dim help As Boolean

    Dim helpPop As Popup




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        If help = True Then
            help = False
        Else
            MessageBox.Show("Help Will Appear as ToolTip when you take your cursor over a control", "Color Convertor Help", MessageBoxButtons.OK, MessageBoxIcon.Information)
            help = True
        End If
    End Sub

    Private Sub txtK_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles txtK.MouseEnter, txtY.MouseEnter, txtM.MouseEnter, txtC.MouseEnter, txtHexToCMYK.MouseEnter, HtxtB.MouseEnter, HtxtG.MouseEnter, HtxtR.MouseEnter, HtxtHex.MouseEnter, txtHex.MouseEnter, txtB.MouseEnter, txtG.MouseEnter, txtR.MouseEnter
        If help = True Then
            Dim bor As New Border
            bor.CornerRadius = New CornerRadius(2)
            bor.BorderThickness = New Thickness(2)
            Dim txtblock As New TextBlock
            txtblock.Text = AutomationProperties.GetHelpText(sender)
            txtblock.FontSize = 15
            txtblock.FontWeight = FontWeights.Bold
            bor.Child = txtblock
            Dim eff As New Effects.DropShadowBitmapEffect
            eff.Color = sender.tag
            eff.Direction = 0.05
            eff.ShadowDepth = 0.05
            bor.BitmapEffect = eff
            sender.ToolTip = bor
        Else
            sender.ToolTip = Nothing
        End If
    End Sub

    Private Sub button_Copy_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles button_Copy.MouseMove
        If help = True Then
            Dim bor As New Border
            bor.CornerRadius = New CornerRadius(2)
            bor.BorderThickness = New Thickness(2)
            Dim txtblock As New TextBlock
            txtblock.Text = AutomationProperties.GetHelpText(sender)
            txtblock.FontSize = 15
            txtblock.FontWeight = FontWeights.Bold
            bor.Child = txtblock
            Dim eff As New Effects.DropShadowBitmapEffect
            eff.Color = sender.tag
            eff.Direction = 1
            eff.ShadowDepth = 1
            bor.BitmapEffect = eff
            bor.BitmapEffect = eff
            sender.ToolTip = bor
        Else
            sender.ToolTip = Nothing
        End If
    End Sub

    Private Sub button_Copy_ToolTipOpening(ByVal sender As Object, ByVal e As System.Windows.Controls.ToolTipEventArgs) Handles button_Copy.ToolTipOpening
        
    End Sub
End Class

Public Class HexConvertors

    Public Function HexToRgb(ByVal HexValue As String) As Int32()
        Dim rgb() As Int32 = {ColorTranslator.FromHtml(HexValue).R, ColorTranslator.FromHtml(HexValue).G, ColorTranslator.FromHtml(HexValue).B}
        Return rgb
    End Function

    Public Function rgbToHex(ByVal Red, ByVal Green, ByVal Blue)
        Try
            Return toHex(Red) + toHex(Green) + toHex(Blue)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Function hexToCMYK(ByVal hex As String) As Double()
        Dim computedC As Double = 0
        Dim computedM As Double = 0
        Dim computedY As Double = 0
        Dim computedK As Double = 0

        Dim rgb() As Int32 = HexToRgb(hex)
        Dim r As Int32 = rgb(0)
        Dim g As Int32 = rgb(1)
        Dim b As Int32 = rgb(2)

        If r = 0 And g = 0 And b = 0 Then
            Return {0.0, 0.0, 0.0, 1.0}
        End If
        computedC = 1 - (r / 255)
        computedM = 1 - (g / 255)
        computedY = 1 - (b / 255)

        Dim minCMY As Double = Math.Min(computedC, Math.Min(computedM, computedY))
        computedC = (computedC - minCMY) / (1 - minCMY)
        computedM = (computedM - minCMY) / (1 - minCMY)
        computedY = (computedY - minCMY) / (1 - minCMY)
        computedK = minCMY
        Return {computedC, computedM, computedY, computedK}

    End Function

    Public Function toHex(ByVal n)
        n = Integer.Parse(n, 10)
        If (Not IsNumeric(n)) Then
            Return "00"
        End If

        n = Math.Max(0, Math.Min(n, 255))
        Return "0123456789ABCDEF".Chars((n - n Mod 16) / 16) + "0123456789ABCDEF".Chars(n Mod 16)
    End Function

    Public Function hexToR(ByVal h)
        Return Integer.Parse((cutHex(h)).substring(0, 2), 16)
    End Function

    Public Function hexToG(ByVal h)
        Return Integer.Parse((cutHex(h)).substring(2, 4), 16)
    End Function

    Public Function hexToB(ByVal h)
        Return Integer.Parse((cutHex(h)).substring(4, 6), 16)
    End Function

    Public Function cutHex(ByVal h As String)
        Return IIf(h.Chars(0) = "#", h.Replace("#", ""), h)
    End Function

End Class