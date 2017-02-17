Imports MySql.Data.MySqlClient
Public Class Form1
    Private serverName As String = "mysql.stud.iie.ntnu.no"
    Private databaseName As String = "sondg"
    Private userID As String = "sondg"
    Private password As String = "7ppasexr"
    Private connString As String = String.Format("Server={0}; Database={1}; Uid={2}; Pwd={3};", serverName, databaseName, userID, password)

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        connectAndWrite("SELECT * FROM personer")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        connectAndWrite("SELECT * FROM personer ORDER BY fornavn")
    End Sub

    Private Sub connectAndWrite(query As String)
        Dim connection As New MySqlConnection(connString)
        Try
            connection.Open()
            Console.WriteLine("Connecting with no issues.")
            'Lager sentrale objekter
            Dim sql As New MySqlCommand(query, connection)
            Dim dataAdapter As New MySqlDataAdapter
            Dim internalTable As New DataTable
            'Objektet "da" utfører spørringen og legger resultatet i "interntabell"
            dataAdapter.SelectCommand = sql
            dataAdapter.Fill(internalTable)
            'Har ikke lenger bruk for å være tilkoblet til databasen
            connection.Close()
            'En tabell har mange rader. DataRow-objektet kan lagre 1 rad om gangen
            Dim rad As DataRow
            Dim personid, fornavn, etternavn As String 'hjelpevariabler
            'Fyller listeboksen med ønsket informasjon
            ListBox1.Items.Clear() 'fjerner først eventuell gammel tekst
            For Each rad In internalTable.Rows
                personid = rad("id")
                fornavn = rad("fornavn")
                etternavn = rad("etternavn")
                ListBox1.Items.Add(personid & " " & fornavn & " " & etternavn)
            Next rad
        Catch feilmelding As MySqlException
            Console.WriteLine("Feil ved tilkobling til databasen: " &
            feilmelding.Message)
        Finally
            connection.Dispose()
        End Try
    End Sub
End Class