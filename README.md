# QuickSettings

Simple demonstration of how to save and load form settings.


# Usage

Load form settings
```
Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Settings.Load(Me, <Filename>)
End Sub
```

Save form settings
```
Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Settings.Save(Me, <Filename>)
End Sub
```

Replace `<Filename>` with a filename that will be used for the serialized XML data

# Adding new properties

In the Settings class you can define new properties that will be saved into the config file.
Keep in mind that not all types are serializable, if the `Settings.Save()` functions fails it means
that it could not succeed in doing so, i added a few basic properties as this demonstrates how some fields are best kept.

If you add new fields you need to update the following routines as well

- ApplyTo() method
```
Public Function ApplyTo(Form As Form) As Boolean Implements ISettings.ApplyTo
            Form.Text = Me.Title
            Form.Size = Me.Size
            Form.Location = Me.Location
            Form.BackColor = Color.FromArgb(Me.BackColor)
            Form.Font = New Font(Me.Font, Me.FontSize)
            Form.Field = Me.MyField '<-- Your new field
            Return True
End Function
```

- Create() method
```
Public Shared Function Create(Target As Form) As Settings
            Return New Settings With
                   {.Size = Target.Size,
                    .Title = Target.Text,
                    .Location = Target.Location,
                    .FontSize = Target.Font.Size,
                    .Font = Target.Font.FontFamily.Name,
                    .BackColor = Target.BackColor.ToArgb,
                    .MyField = Target.Field '<-- Your new field
                   }
End Function
```

- Settings Class
```
Public Property Size As Size
Public Property Font As String
Public Property FontSize As Single
Public Property Title As String
Public Property Location As Point
Public Property BackColor As Integer
Public Property MyField as String '<-- Your new field
```

# XML settings structure

```
<?xml version="1.0" encoding="utf-8"?>
<Settings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Size>
    <Width>834</Width>
    <Height>651</Height>
  </Size>
  <Font>Microsoft Sans Serif</Font>
  <FontSize>8.25</FontSize>
  <Title>Form1</Title>
  <Location>
    <X>420</X>
    <Y>242</Y>
  </Location>
  <BackColor>-986896</BackColor>
</Settings>
```
