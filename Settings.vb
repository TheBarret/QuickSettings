Imports System.IO

Namespace Serializer
    Public Interface ISettings
        Function ApplyTo(Form As Form) As Boolean
    End Interface
    <Serializable>
    Public Class Settings
        Implements ISettings
        Public Property Size As Size
        Public Property Font As String
        Public Property FontSize As Single
        Public Property Title As String
        Public Property Location As Point
        Public Property BackColor As Integer
        Public Function ApplyTo(Form As Form) As Boolean Implements ISettings.ApplyTo
            Form.Text = Me.Title
            Form.Size = Me.Size
            Form.Location = Me.Location
            Form.BackColor = Color.FromArgb(Me.BackColor)
            Form.Font = New Font(Me.Font, Me.FontSize)
            Return True
        End Function
        Public Shared Function Create(Target As Form) As Settings
            Return New Settings With
                   {.Size = Target.Size,
                    .Title = Target.Text,
                    .Location = Target.Location,
                    .FontSize = Target.Font.Size,
                    .Font = Target.Font.FontFamily.Name,
                    .BackColor = Target.BackColor.ToArgb
                   }
        End Function
        Public Shared Function Save(Form As Form, Filename As String) As Boolean
            Return Writer.Save(Filename, Settings.Create(Form))
        End Function
        Public Shared Function Load(Form As Form, Filename As String) As Boolean
            If (File.Exists(Filename)) Then
                Dim settings As Settings = Nothing
                If (Reader.Load(Of Settings)(Filename, settings)) Then
                    Return settings.ApplyTo(Form)
                End If
            End If
            Return False
        End Function
    End Class
End Namespace