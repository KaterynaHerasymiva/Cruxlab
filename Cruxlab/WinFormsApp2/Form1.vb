Imports Cruxlab.Services

Public Class Form1

    Private ReadOnly _passwordValidator As IPasswordsValidator

    Public Sub New()
        _passwordValidator = New PasswordsValidator()
        InitializeComponent()
    End Sub

    Private Async Sub Button2_ClickAsync(sender As Object, e As EventArgs) Handles SubmitButton.Click
        Spinner.Start()
        Try
            Dim result As Integer = Await _passwordValidator.ValidatePasswordAsync(OpenFileDialog1.FileName)
        Finally
            Label2.Visible = True
            Spinner.Stop()
        End Try
    End Sub
    Private Function Wxx(s As Integer) As Integer
        Return s * 5
    End Function



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "Text|*.txt"
        Dim d As DialogResult = OpenFileDialog1.ShowDialog()

        If d = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
            SubmitButton.Enabled = True
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
