Imports System.Collections.ObjectModel

Public Class BlockCollection
    Inherits ObservableCollection(Of BlockItem)

    Public Sub AddItem(source As String)
        Me.Add(New BlockItem(Me, source))
    End Sub

End Class