Public Class DragDrop

    Public Sub Register(fe As FrameworkElement)
        If (fe IsNot Nothing) Then
            AddHandler fe.MouseLeftButtonDown, AddressOf FrameworkElement_MouseLeftButtonDown
            AddHandler fe.MouseLeftButtonUp, AddressOf FrameworkElement_MouseLeftButtonUp
            AddHandler fe.MouseMove, AddressOf FrameworkElement_MouseMove
        End If
    End Sub

    Private movableElement As FrameworkElement = Nothing

    Private Sub FrameworkElement_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is FrameworkElement)) Then
            Dim fe As FrameworkElement = CType(sender, FrameworkElement)

            e.Handled = True

            fe.CaptureMouse()
        End If
    End Sub

    Private Sub FrameworkElement_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is FrameworkElement)) Then
            Dim fe As FrameworkElement = CType(sender, FrameworkElement)

            e.Handled = True

            If ((Me.movableElement IsNot Nothing) AndAlso (Me.movableElement.Parent IsNot Nothing) AndAlso (TypeOf Me.movableElement.Parent Is Grid)) Then
                Dim htr As HitTestResult = VisualTreeHelper.HitTest(Me.movableElement.Parent, e.GetPosition(Me.movableElement.Parent))

                If ((htr IsNot Nothing) AndAlso (htr.VisualHit IsNot Nothing) AndAlso (TypeOf htr.VisualHit Is FrameworkElement)) Then
                    Dim target As FrameworkElement = CType(htr.VisualHit, FrameworkElement)

                    If (target IsNot Nothing) Then
                        target = Me.GetDropElement(target)
                    End If

                    If ((target IsNot Nothing) AndAlso (TypeOf target Is IDropTarget)) Then
                        Dim t As IDropTarget = CType(target, IDropTarget)
                        Dim pos As EDropPos = Me.GetDropPosition(target, e)

                        If (t IsNot Me.movableElement.DataContext) Then
                            t.Drop(Me.movableElement.DataContext, pos)
                        End If
                    End If
                End If

                Dim parent As Grid = CType(Me.movableElement.Parent, Grid)

                parent.Children.Remove(Me.movableElement)
            End If

            Me.movableElement = Nothing

            fe.ReleaseMouseCapture()
        End If
    End Sub

    Private Function GetDropElement(target As FrameworkElement) As FrameworkElement
        If (target IsNot Nothing) Then
            If (TypeOf target Is IDropTarget) Then
                Return (target)
            End If

            Dim element As FrameworkElement = target.Parent

            Do
                If ((element IsNot Nothing) AndAlso (TypeOf element Is IDropTarget)) Then
                    Return (element)
                Else
                    element = element.Parent
                End If
            Loop While (element IsNot Nothing)
        End If

        Return (Nothing)
    End Function

    Private Function GetDropPosition(target As FrameworkElement, e As MouseButtonEventArgs) As EDropPos
        Dim p As Point = e.GetPosition(target)

        If (p.X <= 20) Then
            Return (EDropPos.Left)
        End If

        If (p.Y <= 20) Then
            Return (EDropPos.Top)
        End If

        If ((target.ActualWidth - 20) <= p.X) Then
            Return (EDropPos.Right)
        End If

        If ((target.ActualHeight - 20) <= p.Y) Then
            Return (EDropPos.Bottom)
        End If

        Return (EDropPos.Center)
    End Function

    Private Sub FrameworkElement_MouseMove(sender As Object, e As MouseEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is FrameworkElement)) Then
            Dim fe As FrameworkElement = CType(sender, FrameworkElement)

            If (fe.IsMouseCaptured) Then
                Dim p As Point = e.MouseDevice.GetPosition(fe.Parent)
                Dim content As Object = Application.Current.MainWindow.Content

                If ((content IsNot Nothing) AndAlso (TypeOf content Is Grid)) Then
                    Dim g As Grid = CType(content, Grid)

                    If (Me.movableElement Is Nothing) Then
                        Dim rtb As RenderTargetBitmap = New RenderTargetBitmap(fe.ActualWidth, fe.ActualHeight, 100, 100, PixelFormats.Default)

                        rtb.Render(fe)

                        Dim img As Image = New Image()
                        img.Width = fe.ActualWidth
                        img.Height = fe.ActualHeight
                        img.HorizontalAlignment = HorizontalAlignment.Left
                        img.VerticalAlignment = VerticalAlignment.Top
                        img.Source = rtb
                        img.DataContext = fe.DataContext

                        Me.movableElement = img
                        g.Children.Add(Me.movableElement)
                    End If

                    Me.movableElement.Margin = New Thickness(p.X + 1, p.Y + 1, 0, 0)
                End If
            End If
        End If
    End Sub

End Class