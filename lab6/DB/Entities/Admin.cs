using Npgsql;

static public class Admin
{
    static public void get(NpgsqlConnection conn, string id)
    {
        using (var cmd = new NpgsqlCommand($"SELECT * FROM Users WHERE UserID = '{id}'", conn))
        {
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    int role = reader.GetInt32(1);
                    string firstName = reader.GetString(2);
                    string lastName = reader.GetString(3);
                    string email = reader.GetString(4);
                    string password = reader.GetString(5);

                    Console.Clear();
                    Console.WriteLine("Admin:");
                    Console.WriteLine($"UserID: {userId},\nRoleID: {role},\nFirstName: {firstName},\nLastName: {lastName},\nEmail: {email},\nPassword: {password}\n");
                }
            }
        }
        Console.ReadKey();
    }
}
