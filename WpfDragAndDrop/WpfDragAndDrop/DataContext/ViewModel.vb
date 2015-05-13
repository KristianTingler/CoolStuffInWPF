Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class ViewModel
    Implements INotifyPropertyChanged

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private Shared _Instance As ViewModel = New ViewModel()
    Public Shared ReadOnly Property Instance As ViewModel
        Get
            Return (_Instance)
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _Blocks As BlockCollection = New BlockCollection()
    Public ReadOnly Property Blocks As BlockCollection
        Get
            Return (Me._Blocks)
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _DragDrop As DragDrop = New DragDrop()
    Public ReadOnly Property DragDrop As DragDrop
        Get
            Return (Me._DragDrop)
        End Get
    End Property

    Public Sub Init()
        Dim width As Double = 65
        Dim height As Double = 65

        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector1.png")
        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector2.png")
        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector3.png")

        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector4.png")
        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector5.png")
        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector6.png")

        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector7.png")
        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector8.png")
        Me.Blocks.AddItem("/WpfDragAndDrop;component/Resources/Hector9.png")
    End Sub

#Region "Property Changed Stuff"

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Private Sub NotifyPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

#End Region

End Class