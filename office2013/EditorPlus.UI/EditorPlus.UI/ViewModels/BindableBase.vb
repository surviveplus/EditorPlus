Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public MustInherit Class BindableBase
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Overridable Function SetProperty(Of T)(ByRef storage As T, value As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
        If Object.Equals(storage, value) Then Return False

        storage = value
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        Return True

    End Function

End Class
