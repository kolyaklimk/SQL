using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        string connString = "Server=localhost;Port=5432;Database=DB;User Id=postgres;Password=klim;";

        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    "1 - Sign In\n" +
                    "2 - Sign Up\n" +
                    "3 - User selection\n");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Sign.In(conn);
                        break;

                    case "2":
                        Sign.Up(conn);
                        break;

                    case "3":
                        Users.getAll(conn);
                        break;
                }
            }
        }
    }
}
