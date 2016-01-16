Class MainWindow

    Private Sub NiceButton_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is NiceButton)) Then
            Dim nb As NiceButton = CType(sender, NiceButton)

            nb.IsSelected = Not nb.IsSelected
        End If
    End Sub

End Class