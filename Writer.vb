Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization

Namespace Serializer
    Public Class Writer
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