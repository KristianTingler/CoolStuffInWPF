Public Class BlockControl
    Implements IDropTarget

    Public ReadOnly Property DC As BlockItem
        Get
            Return (TryCast(Me.DataContext, BlockItem))
        End Get
    End Property

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        ViewModel.Instance.DragDrop.Register(Me)
    End Sub

    Public Sub DropIt(o As Object, pos As EDropPos) Implements IDropTarget.Drop
        If ((o IsNot Nothing) AndAlso (TypeOf o Is BlockItem) AndAlso (Me.DC IsNot Nothing)) Then
            Dim dragItem As BlockItem = CType(o, BlockItem)
            Dim indexMe As Integer = ViewModel.Instance.Blocks.IndexOf(Me.DC)
            Dim sourceIndex As Integer = ViewModel.Instance.Blocks.IndexOf(dragItem)

            If (pos = EDropPos.Center) Then
                Dim myItem As BlockItem = Me.DC
                ViewModel.Instance.Blocks(indexMe) = dragItem
                ViewModel.Instance.Blocks(sourceIndex) = myItem

            ElseIf (pos = EDropPos.Left) Then
                ViewModel.Instance.Blocks.Remove(dragItem)
                ViewModel.Instance.Blocks.Insert(indexMe, dragItem)

            ElseIf (pos = EDropPos.Right) Then
                ViewModel.Instance.Blocks.Remove(dragItem)

                If (indexMe > ViewModel.Instance.Blocks.Count) Then
                    ViewModel.Instance.Blocks.Add(dragItem)
                Else
                    ViewModel.Instance.Blocks.Insert(indexMe, dragItem)
                End If

            ElseIf (pos = EDropPos.Top) Then
                If (indexMe < sourceIndex) Then
                    Dim itemTemp As BlockItem = ViewModel.Instance.Blocks(sourceIndex)

                    If ((sourceIndex - 3) >= 0) Then
                        ViewModel.Instance.Blocks(sourceIndex) = ViewModel.Instance.Blocks(sourceIndex - 3)
                    End If

                    If ((sourceIndex - 6) >= 0) Then
                        ViewModel.Instance.Blocks(sourceIndex - 3) = ViewModel.Instance.Blocks(sourceIndex - 6)
                    End If

                    ViewModel.Instance.Blocks(indexMe) = itemTemp

                ElseIf (indexMe > sourceIndex) Then

                End If

            ElseIf (pos = EDropPos.Bottom) Then

            End If
        End If
    End Sub

End Class