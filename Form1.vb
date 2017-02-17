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
            writeOutput(internalTable)
        Catch ex As MySqlException
            Console.WriteLine("Feil ved tilkobling til databasen: " &
            ex.Message)
        Finally
            connection.Dispose()
        End Try
    End Sub

    Private Sub writeOutput(internalTable As DataTable)
        Dim row As DataRow
        ListBox1.Items.Clear()
        For Each row In internalTable.Rows
            ListBox1.Items.Add(String.Format("{0}, {1} {2}", row("id"), row("fornavn"), row("etternavn")))
        Next row
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        connectAndWrite(String.Format("SELECT * FROM personer WHERE fornavn LIKE ""%{0}%""", TextBox1.Text))
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        connectAndWrite(String.Format("SELECT * FROM personer WHERE epost LIKE ""%{0}%""", TextBox2.Text))
    End Sub
End Class