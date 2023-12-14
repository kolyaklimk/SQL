using Npgsql;

static public class Users
{
    static public void getAll(NpgsqlConnection conn)
    {
        try
        {
            Console.Clear();
            using (var cmd = new NpgsqlCommand($"SELECT * FROM Users", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        int roleId = reader.GetInt32(1);
                        string firstName = reader.GetString(2);
                        string lastName = reader.GetString(3);
                        string email = reader.GetString(4);
                        string password = reader.GetString(5);

                        Console.WriteLine($"UserID: {userId},\nRoleID: {roleId},\nFirstName: {firstName},\nLastName: {lastName},\nEmail: {email},\nPassword: {password}\n");
                    }
                }
            }

            Console.WriteLine("Choose UserId: ");
            var id = Console.ReadLine();
            int role = -1;
            using (var cmd = new NpgsqlCommand($"SELECT * FROM Users WHERE UserID = '{id}'", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        role = reader.GetInt32(1);
                        string firstName = reader.GetString(2);
                        string lastName = reader.GetString(3);
                        string email = reader.GetString(4);
                        string password = reader.GetString(5);
                    }
                    else
                    {
                        Console.WriteLine("\nUser not found");

                        Console.WriteLine("Press any button to continue");
                        Console.ReadKey();
                        return;
                    }
                }
            }

            string roleName = getRoleById(conn, role);
            if (string.IsNullOrEmpty(roleName))
            {
                Console.WriteLine("Role not found");
            }
            else
            {
                switch (roleName)
                {
                    case "Admin":
                        Admin.get(conn, id);
                        break;
                    case "Student":
                        Student.get(conn, id);
                        break;
                    case "Instructor":
                        Instructor.get(conn, id);
                        break;
                }
                return;
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine("Error Npgsql: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("Press any button to continue");
        Console.ReadKey();
    }

    static public string getRoleById(NpgsqlConnection conn, int id)
    {
        using (var cmd = new NpgsqlCommand($"SELECT Name FROM Roles WHERE RoleID = '{id}'", conn))
        {
            string roleName = cmd.ExecuteScalar() as string;

            if (roleName != null)
            {
                return roleName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}