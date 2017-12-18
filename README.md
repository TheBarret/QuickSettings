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

Replace `<Filename>` with a filename that will be used the serialized XML data

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

# Adding new properties

In the Settings class you can define new properties that will be saved into the config file.
Keep in mind that not all types are serializable, if the `Settings.Save()` functions fails it means
that it could not succeed in doing so, i added a few basic properties as this demonstrates how some fields are best kept.

Example
```
Public Property Size As Size
Public Property Font As String
Public Property FontSize As Single
Public Property Title As String
Public Property Location As Point
Public Property BackColor As Integer
```

