Class MainWindow

    Public ReadOnly Property DC As ViewModel
        Get
            Return (TryCast(Me.DataContext, ViewModel))
        End Get
    End Property

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        Me.DataContext = ViewModel.Instance
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        If (Me.DC IsNot Nothing) Then
            Me.DC.Init()
        End If
    End Sub

End Class