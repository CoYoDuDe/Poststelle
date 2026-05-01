Option Strict On

Imports System.Data
Imports System.Data.SQLite

Public Module SQLiteHelpers

    Public Sub AddTextParameter(command As SQLiteCommand, parameterName As String, value As String)

        command.Parameters.Add(parameterName, DbType.String).Value = value

    End Sub

    Public Sub AddIntegerParameter(command As SQLiteCommand, parameterName As String, value As Integer)

        command.Parameters.Add(parameterName, DbType.Int32).Value = value

    End Sub

End Module
