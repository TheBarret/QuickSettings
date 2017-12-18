Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization

Namespace Serializer
    Public Class Writer
        Public Shared Function Save(Of T)(Filename As String, Target As T) As Boolean
            If (Target.GetType.IsSerializable) Then
                Try
                    Using writer As XmlWriter = XmlWriter.Create(Path.GetFullPath(Filename),
                           New XmlWriterSettings() With
                               {.Indent = True, .Encoding = Encoding.UTF8,
                                .ConformanceLevel = ConformanceLevel.Document})
                        Call New XmlSerializer(GetType(T)).Serialize(writer, Target)
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