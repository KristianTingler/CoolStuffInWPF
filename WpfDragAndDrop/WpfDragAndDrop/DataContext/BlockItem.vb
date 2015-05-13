Imports System.ComponentModel

Public Class BlockItem
    Implements INotifyPropertyChanged

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _Parent As BlockCollection = Nothing
    Public ReadOnly Property Parent As BlockCollection
        Get
            Return (Me._Parent)
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _ImageSource As String = Nothing
    Public ReadOnly Property ImageSource As String
        Get
            Return (Me._ImageSource)
        End Get
    End Property

    Public Sub New(parent As BlockCollection, source As String)
        Me._Parent = parent
        Me._ImageSource = source
    End Sub

#Region "Property Changed Stuff"

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

#End Region

End Class