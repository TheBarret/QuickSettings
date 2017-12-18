Imports System.IO
Imports System.Xml.Serialization

Namespace Serializer
    Public Class Reader
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
    End Class
End Namespace