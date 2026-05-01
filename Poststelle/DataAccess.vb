Option Strict On

Imports System.Data
Imports System.Data.SQLite
Imports System.IO

Public Module DatabaseConfig

    Private ReadOnly appDataDirectoryValue As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Poststelle")
    Private ReadOnly legacyDatabaseFilePathValue As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Poststelle.db")

    Public ReadOnly Property ApplicationDataDirectory As String
        Get
            EnsureDataDirectory()
            Return appDataDirectoryValue
        End Get
    End Property

    Public ReadOnly Property DatabaseFilePath As String
        Get
            EnsureDataDirectory()
            MigrateLegacyDatabaseIfNeeded()
            Return Path.Combine(appDataDirectoryValue, "Poststelle.db")
        End Get
    End Property

    Public ReadOnly Property DefaultBackupFilePath As String
        Get
            EnsureDataDirectory()
            Return Path.Combine(appDataDirectoryValue, "PoststelleBackup.db")
        End Get
    End Property

    Public ReadOnly Property ConnectionString As String
        Get
            Return "Data Source=" & DatabaseFilePath & ";"
        End Get
    End Property

    Public ReadOnly Property SharedConnectionString As String
        Get
            Return "Data Source=" & DatabaseFilePath & ";Cache=Shared;"
        End Get
    End Property

    Private Sub EnsureDataDirectory()

        If Not Directory.Exists(appDataDirectoryValue) Then

            Directory.CreateDirectory(appDataDirectoryValue)

        End If

    End Sub

    Private Sub MigrateLegacyDatabaseIfNeeded()

        If File.Exists(Path.Combine(appDataDirectoryValue, "Poststelle.db")) Then

            Exit Sub

        End If

        If Not File.Exists(legacyDatabaseFilePathValue) Then

            Exit Sub

        End If

        File.Copy(legacyDatabaseFilePathValue, Path.Combine(appDataDirectoryValue, "Poststelle.db"), overwrite:=False)

    End Sub

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

Public Class ScanRuleRecord

    Public Property Id As Integer
    Public Property Carrier As String
    Public Property TrackingPrefix As String
    Public Property Empfaenger As String

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

    Public Sub EnsureDatabaseReady()

        CreateDatabase()
        CreateSchema()
        EnsureDefaultSettings()

    End Sub

    Public Function DatabaseExists() As Boolean

        Return File.Exists(DatabaseConfig.DatabaseFilePath)

    End Function

    Public Function HasRequiredSchema() As Boolean

        If Not DatabaseExists() Then

            Return False

        End If

        Try

            Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
                connection.Open()

                Return TableExists(connection, "Packete") AndAlso
                       TableExists(connection, "Empfaenger") AndAlso
                       TableExists(connection, "Einstellungen")
            End Using
        Catch

            Return False

        End Try

    End Function

    Public Sub CreateDatabase()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()
        End Using

    End Sub

    Public Sub CreateSchema()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            ExecuteNonQuery(connection, "CREATE TABLE IF NOT EXISTS Packete (" &
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

            ExecuteNonQuery(connection, "CREATE TABLE IF NOT EXISTS Empfaenger (" &
                                        "ID INTEGER Not NULL," &
                                        "Name TEXT," &
                                        "Abladestelle TEXT," &
                                        "Mandant TEXT," &
                                        "PRIMARY KEY(ID));")

            ExecuteNonQuery(connection, "CREATE TABLE IF NOT EXISTS Einstellungen (" &
                                        "ID INTEGER Not NULL," &
                                        "AutodbBackup TEXT," &
                                        "AutodbBackupTime TEXT," &
                                        "AutodbBackupPfad TEXT," &
                                        "PRIMARY KEY(ID));")

            ExecuteNonQuery(connection, "CREATE TABLE IF NOT EXISTS ScanRegeln (" &
                                        "ID INTEGER Not NULL," &
                                        "Carrier TEXT," &
                                        "TrackingPrefix TEXT," &
                                        "Empfaenger TEXT," &
                                        "PRIMARY KEY(ID));")

            ExecuteNonQuery(connection, "CREATE UNIQUE INDEX IF NOT EXISTS IX_ScanRegeln_Carrier_Prefix ON ScanRegeln(Carrier, TrackingPrefix);")
        End Using

    End Sub

    Public Sub InsertDefaultSettings()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("INSERT INTO Einstellungen (ID, AutodbBackup, AutodbBackupTime, AutodbBackupPfad) VALUES (1, 0, 30, @AutodbBackupPfad)", connection)
                AddTextParameter(command, "@AutodbBackupPfad", DatabaseConfig.DefaultBackupFilePath)
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

    Private Function TableExists(connection As SQLiteConnection, tableName As String) As Boolean

        Using command As New SQLiteCommand("SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @TableName", connection)
            AddTextParameter(command, "@TableName", tableName)
            Return Convert.ToInt32(command.ExecuteScalar()) > 0
        End Using

    End Function

    Private Sub EnsureDefaultSettings()

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("SELECT COUNT(*) FROM Einstellungen WHERE ID = 1", connection)
                If Convert.ToInt32(command.ExecuteScalar()) = 0 Then
                    Using insertCommand As New SQLiteCommand("INSERT INTO Einstellungen (ID, AutodbBackup, AutodbBackupTime, AutodbBackupPfad) VALUES (1, 0, 30, @AutodbBackupPfad)", connection)
                        AddTextParameter(insertCommand, "@AutodbBackupPfad", DatabaseConfig.DefaultBackupFilePath)
                        insertCommand.ExecuteNonQuery()
                    End Using
                End If
            End Using
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

Public Class ScanRuleRepository

    Public Function FindRecipientByCarrierAndPrefix(carrier As String, trackingPrefix As String) As String

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("SELECT Empfaenger FROM ScanRegeln WHERE Carrier = @Carrier AND TrackingPrefix = @TrackingPrefix", connection)
                AddTextParameter(command, "@Carrier", carrier)
                AddTextParameter(command, "@TrackingPrefix", trackingPrefix)

                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                    Return Convert.ToString(result)
                End If
            End Using
        End Using

        Return String.Empty

    End Function

    Public Sub Upsert(rule As ScanRuleRecord)

        Using connection As New SQLiteConnection(DatabaseConfig.ConnectionString)
            connection.Open()

            Using command As New SQLiteCommand("INSERT OR REPLACE INTO ScanRegeln (ID, Carrier, TrackingPrefix, Empfaenger) VALUES ((SELECT ID FROM ScanRegeln WHERE Carrier = @Carrier AND TrackingPrefix = @TrackingPrefix), @Carrier, @TrackingPrefix, @Empfaenger)", connection)
                AddTextParameter(command, "@Carrier", rule.Carrier)
                AddTextParameter(command, "@TrackingPrefix", rule.TrackingPrefix)
                AddTextParameter(command, "@Empfaenger", rule.Empfaenger)
                command.ExecuteNonQuery()
            End Using
        End Using

    End Sub

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
