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
