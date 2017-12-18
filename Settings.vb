Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text

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
            Return Settings.Save(Filename, Settings.Create(Form))
        End Function
        Public Shared Function Load(Form As Form, Filename As String) As Boolean
            If (File.Exists(Filename)) Then
                Dim current As Settings = Nothing
                If (Settings.Load(Of Settings)(Filename, current)) Then
                    Return current.ApplyTo(Form)
                End If
            End If
            Return False
        End Function
        Public Shared Function Load(Of T As ISettings)(Filename As String, ByRef Settings As T) As Boolean
            If (File.Exists(Filename)) Then
                Try
                    Using fs As New FileStream(Path.GetFullPath(Filename), FileMode.Open, FileAccess.Read)
                        Dim value As Object = New XmlSerializer(GetType(T)).Deserialize(fs)
                        If (TypeOf value Is T) Then
                            Settings = CType(value, T)
                            Return True
                        End If
                    End Using
                Catch ex As Exception
                    Return False
                End Try
            End If
            Return False
        End Function
        Public Shared Function Save(Filename As String, Target As Object) As Boolean
            If (Target.GetType.IsSerializable) Then
                Try
                    Using writer As XmlWriter = XmlWriter.Create(Path.GetFullPath(Filename),
                           New XmlWriterSettings() With
                               {.Indent = True, .Encoding = Encoding.UTF8,
                                .ConformanceLevel = ConformanceLevel.Document})
                        Call New XmlSerializer(Target.GetType).Serialize(writer, Target)
                        Return True
                    End Using
                Catch ex As Exception
                    Return False
                End Try
            End If
            Return False
        End Function
    End Class
End Namespace
