using Npgsql;
using System.Data;

static public class Sign
{
    static public void Up(NpgsqlConnection conn)
    {
        Console.Clear();
        Console.WriteLine("FirstName: ");
        var FirstName = Console.ReadLine();
        Console.WriteLine("LastName: ");
        var LastName = Console.ReadLine();
        Console.WriteLine("Email: ");
        var Email = Console.ReadLine();
        Console.WriteLine("Password: ");
        var Password = Console.ReadLine();

        try
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO Users (  FirstName, LastName, Email, Password) VALUES  " +
                $"  ( '{FirstName}', '{LastName}', '{Email}', '{Password}')", conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nSuccess!");
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


    static public void In(NpgsqlConnection conn)
    {
        Console.Clear();
        Console.WriteLine("Email: ");
        var Email = Console.ReadLine();
        Console.WriteLine("Password: ");
        var Password = Console.ReadLine();
        int roleId = -1;
        int userId = -1;
        try
        {
            using (var cmd = new NpgsqlCommand($"SELECT * FROM Users WHERE Email = '{Email}' AND Password = '{Password}'", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);
                            roleId = reader.GetInt32(1);

                            Console.WriteLine("\nUser found");
                        }
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

            string roleName = Users.getRoleById(conn, roleId);
            if (string.IsNullOrEmpty(roleName))
            {
                Console.WriteLine("Role not found");
            }
            else
            {
                switch (roleName)
                {
                    case "Admin":
                        Admin.get(conn, userId.ToString());
                        break;
                    case "Student":
                        Student.get(conn, userId.ToString());
                        break;
                    case "Instructor":
                        Instructor.get(conn, userId.ToString());
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
}
