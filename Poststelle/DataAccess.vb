Option Strict On

Imports System.Data
Imports System.Data.SQLite
Imports System.IO

Public Module DatabaseConfig

    Public Const DatabaseFilePath As String = "Poststelle.db"
    Public Const ConnectionString As String = "Data Source=Poststelle.db;"
    Public Const SharedConnectionString As String = "FullUri=file:Poststelle.db?cache=shared"

End Module

Public Class SettingsRecord

    Public Property Id As Integer
    Public Property AutoDbBackupEnabled As Boolean
    Public Property AutoDbBackupTime As Integer
    Public Property AutoDbBackupPath As String

End Class

Public Class RecipientRecord

    Public Property Id As Integer
    Public Property Name As String
    Public Property Abladestelle As String
    Public Property Mandant As String

End Class

Public Class PackageRecord

    Public Property Mandant As String
    Public Property Datum As String
    Public Property Abladestelle As String
    Public Property Sender As String
    Public Property SendungsNummer As String
    Public Property Empfaenger As String
    Public Property Unterschrift As String
    Public Property Gedruckt As String

End Class

Public Class RecipientFilter

    Public Property Name As String
    Public Property Abladestelle As String
    Public Property Mandant As String

End Class

Public Class PackageFilter

    Public Property Datum As String
    Public Property Mandant As String
    Public Property Sender As String
    Public Property SendungsNummer As String
    Public Property Empfaenger As String
    Public Property Gedruckt As String

End Class

Public Class SettingsRepository

    Public Function DatabaseExists() As Boolean

        Return File.Exists(DatabaseConfig.DatabaseFilePath)

    End Function

    Public Sub CreateDatabase()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()
        End Using

    End Sub

    Public Sub CreateSchema()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            ExecuteNonQuery(connection, "CREATE TABLE Packete (" &
                                        "ID INTEGER Not NULL," &
                                        "Mandant TEXT," &
                                        "Datum TEXT," &
                                        "Abladestelle TEXT," &
                                        "Sender TEXT," &
                                        "SendungsNummer TEXT," &
                                        "Empfaenger TEXT," &
                                        "Unterschrift TEXT," &
                                        "Gedruckt TEXT," &
                                        "PRIMARY KEY(ID));")

            ExecuteNonQuery(connection, "CREATE TABLE Empfaenger (" &
                                        "ID INTEGER Not NULL," &
                                        "Name TEXT," &
                                        "Abladestelle TEXT," &
                                        "Mandant TEXT," &
                                        "PRIMARY KEY(ID));")

            ExecuteNonQuery(connection, "CREATE TABLE Einstellungen (" &
                                        "ID INTEGER Not NULL," &
                                        "AutodbBackup TEXT," &
                                        "AutodbBackupTime TEXT," &
                                        "AutodbBackupPfad TEXT," &
                                        "PRIMARY KEY(ID));")
        End Using

    End Sub

    Public Sub InsertDefaultSettings()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("INSERT INTO Einstellungen (AutodbBackup, AutodbBackupTime, AutodbBackupPfad) VALUES (0, 30, @AutodbBackupPfad)", connection)
                AddTextParameter(command, "@AutodbBackupPfad", "PoststelleBackup.db")
                command.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Public Function GetSettings(id As Integer) As SettingsRecord

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("SELECT ID, AutodbBackup, AutodbBackupTime, AutodbBackupPfad FROM Einstellungen WHERE ID = @Id", connection)
                AddIntegerParameter(command, "@Id", id)

                Using reader As SQLiteDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Return New SettingsRecord With {
                            .Id = Convert.ToInt32(reader("ID")),
                            .AutoDbBackupEnabled = Convert.ToString(reader("AutodbBackup")) = "1",
                            .AutoDbBackupTime = Convert.ToInt32(reader("AutodbBackupTime")),
                            .AutoDbBackupPath = Convert.ToString(reader("AutodbBackupPfad"))
                        }
                    End If
                End Using
            End Using
        End Using

        Return Nothing

    End Function

    Public Sub UpdateSettings(settings As SettingsRecord)

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("UPDATE Einstellungen SET AutodbBackup = @AutodbBackup, AutodbBackupTime = @AutodbBackupTime, AutodbBackupPfad = @AutodbBackupPfad WHERE ID = @Id", connection)
                AddIntegerParameter(command, "@AutodbBackup", If(settings.AutoDbBackupEnabled, 1, 0))
                AddIntegerParameter(command, "@AutodbBackupTime", settings.AutoDbBackupTime)
                AddTextParameter(command, "@AutodbBackupPfad", settings.AutoDbBackupPath)
                AddIntegerParameter(command, "@Id", settings.Id)
                command.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Private Sub ExecuteNonQuery(connection As SQLiteConnection, sql As String)

        Using command As SQLiteCommand = connection.CreateCommand()
            command.CommandText = sql
            command.ExecuteNonQuery()
        End Using

    End Sub

End Class

Public Class RecipientRepository

    Public Function GetDistinctNames() As List(Of String)

        Return GetDistinctValues("SELECT DISTINCT Name FROM Empfaenger", "Name")

    End Function

    Public Function GetDistinctAbladestellen() As List(Of String)

        Return GetDistinctValues("SELECT DISTINCT Abladestelle FROM Empfaenger", "Abladestelle")

    End Function

    Public Function GetDistinctMandants() As List(Of String)

        Return GetDistinctValues("SELECT DISTINCT Mandant FROM Empfaenger", "Mandant")

    End Function

    Public Function FindByName(name As String) As RecipientRecord

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("SELECT ID, Name, Abladestelle, Mandant FROM Empfaenger WHERE Name = @Name", connection)
                AddTextParameter(command, "@Name", name)

                Using reader As SQLiteDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Return New RecipientRecord With {
                            .Id = Convert.ToInt32(reader("ID")),
                            .Name = Convert.ToString(reader("Name")),
                            .Abladestelle = Convert.ToString(reader("Abladestelle")),
                            .Mandant = Convert.ToString(reader("Mandant"))
                        }
                    End If
                End Using
            End Using
        End Using

        Return Nothing

    End Function

    Public Sub Upsert(recipient As RecipientRecord)

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("INSERT OR REPLACE INTO Empfaenger (Name, Abladestelle, Mandant) VALUES (@Name, @Abladestelle, @Mandant)", connection)
                AddTextParameter(command, "@Name", recipient.Name)
                AddTextParameter(command, "@Abladestelle", recipient.Abladestelle)
                AddTextParameter(command, "@Mandant", recipient.Mandant)
                command.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Public Function Search(filter As RecipientFilter) As DataTable

        Using connection As New SQLiteConnection(DatabaseConfig.SharedConnectionString)
            Using command As New SQLiteCommand("SELECT * FROM Empfaenger WHERE Name LIKE @Name AND Abladestelle LIKE @Abladestelle AND Mandant LIKE @Mandant", connection)
                AddTextParameter(command, "@Name", filter.Name)
                AddTextParameter(command, "@Abladestelle", filter.Abladestelle)
                AddTextParameter(command, "@Mandant", filter.Mandant)

                Using adapter As New SQLiteDataAdapter(command)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    Return table
                End Using
            End Using
        End Using

    End Function

    Public Sub SaveChanges(data As DataTable)

        Using adapter As New SQLiteDataAdapter("SELECT * FROM Empfaenger", DatabaseConfig.SharedConnectionString)
            Using builder As New SQLiteCommandBuilder(adapter)
                adapter.Update(data)
            End Using
        End Using

    End Sub

    Private Function GetDistinctValues(query As String, columnName As String) As List(Of String)

        Dim values As New List(Of String)()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand(query, connection)
                Using reader As SQLiteDataReader = command.ExecuteReader()
                    Do While reader.Read()
                        values.Add(Convert.ToString(reader(columnName)))
                    Loop
                End Using
            End Using
        End Using

        Return values

    End Function

End Class

Public Class PackageRepository

    Public Function GetDistinctSenders() As List(Of String)

        Return GetDistinctValues("SELECT DISTINCT Sender FROM Packete", "Sender")

    End Function

    Public Function GetDistinctMandants(Optional datum As String = Nothing) As List(Of String)

        If String.IsNullOrEmpty(datum) Then
            Return GetDistinctValues("SELECT DISTINCT Mandant FROM Packete", "Mandant")
        End If

        Dim values As New List(Of String)()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("SELECT DISTINCT Mandant FROM Packete WHERE Datum = @Datum", connection)
                AddTextParameter(command, "@Datum", datum)

                Using reader As SQLiteDataReader = command.ExecuteReader()
                    Do While reader.Read()
                        values.Add(Convert.ToString(reader("Mandant")))
                    Loop
                End Using
            End Using
        End Using

        Return values

    End Function

    Public Sub Insert(packageRecord As PackageRecord)

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("INSERT INTO Packete (Mandant, Datum, Abladestelle, Sender, SendungsNummer, Empfaenger, Unterschrift, Gedruckt) VALUES (@Mandant, @Datum, @Abladestelle, @Sender, @SendungsNummer, @Empfaenger, @Unterschrift, @Gedruckt)", connection)
                AddTextParameter(command, "@Mandant", packageRecord.Mandant)
                AddTextParameter(command, "@Datum", packageRecord.Datum)
                AddTextParameter(command, "@Abladestelle", packageRecord.Abladestelle)
                AddTextParameter(command, "@Sender", packageRecord.Sender)
                AddTextParameter(command, "@SendungsNummer", packageRecord.SendungsNummer)
                AddTextParameter(command, "@Empfaenger", packageRecord.Empfaenger)
                AddTextParameter(command, "@Unterschrift", packageRecord.Unterschrift)
                AddTextParameter(command, "@Gedruckt", packageRecord.Gedruckt)
                command.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Public Sub MarkPrinted(filter As PackageFilter)

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("UPDATE Packete SET Gedruckt = @GedrucktNeu WHERE Datum LIKE @Datum AND Mandant LIKE @Mandant AND Sender LIKE @Sender AND SendungsNummer Like @SendungsNummer AND Empfaenger LIKE @Empfaenger AND Gedruckt LIKE @Gedruckt", connection)
                AddTextParameter(command, "@GedrucktNeu", "1")
                AddTextParameter(command, "@Datum", filter.Datum)
                AddTextParameter(command, "@Mandant", filter.Mandant)
                AddTextParameter(command, "@Sender", filter.Sender)
                AddTextParameter(command, "@SendungsNummer", filter.SendungsNummer)
                AddTextParameter(command, "@Empfaenger", filter.Empfaenger)
                AddTextParameter(command, "@Gedruckt", filter.Gedruckt)
                command.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Public Function Search(filter As PackageFilter) As DataTable

        Using connection As New SQLiteConnection(DatabaseConfig.SharedConnectionString)
            Using command As New SQLiteCommand("SELECT * FROM Packete WHERE Datum LIKE @Datum AND Mandant LIKE @Mandant AND Sender LIKE @Sender AND SendungsNummer Like @SendungsNummer AND Empfaenger LIKE @Empfaenger AND Gedruckt LIKE @Gedruckt", connection)
                AddTextParameter(command, "@Datum", filter.Datum)
                AddTextParameter(command, "@Mandant", filter.Mandant)
                AddTextParameter(command, "@Sender", filter.Sender)
                AddTextParameter(command, "@SendungsNummer", filter.SendungsNummer)
                AddTextParameter(command, "@Empfaenger", filter.Empfaenger)
                AddTextParameter(command, "@Gedruckt", filter.Gedruckt)

                Using adapter As New SQLiteDataAdapter(command)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    Return table
                End Using
            End Using
        End Using

    End Function

    Public Sub SaveChanges(data As DataTable)

        Using adapter As New SQLiteDataAdapter("SELECT * FROM Packete", DatabaseConfig.SharedConnectionString)
            Using builder As New SQLiteCommandBuilder(adapter)
                adapter.Update(data)
            End Using
        End Using

    End Sub

    Private Function GetDistinctValues(query As String, columnName As String) As List(Of String)

        Dim values As New List(Of String)()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand(query, connection)
                Using reader As SQLiteDataReader = command.ExecuteReader()
                    Do While reader.Read()
                        values.Add(Convert.ToString(reader(columnName)))
                    Loop
                End Using
            End Using
        End Using

        Return values

    End Function

End Class
