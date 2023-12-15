using Npgsql;

static public class Admin
{
    static public void get(NpgsqlConnection conn, string id)
    {
        while (true)
        {
            Console.Clear();

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
                        Console.WriteLine("Instructor:");
                        Console.WriteLine($"UserID: {userId},\nRoleID: {role},\nFirstName: {firstName},\nLastName: {lastName},\nEmail: {email},\nPassword: {password}\n");
                    }
                }
            }


            using (var cmd = new NpgsqlCommand($"SELECT * FROM UserActions ORDER BY DateAndTime DESC LIMIT 1;", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int actionid = reader.GetInt32(0);
                        int userid123 = reader.GetInt32(1);
                        DateTime date = reader.GetDateTime(2);
                        string desc = reader.GetString(3);

                        Console.WriteLine($"Last action: Actionid: {actionid}, UserId: {userid123}, DateTime: {date}, Description: {desc}\n");
                    }
                }
            }

            Console.WriteLine(
                "1 - Manage users\n" +
                "2 - Assign a role\n" +
                "3 - Create a new course\n" +
                "4 - Add material for the course\n" +
                "5 - Manage course materials\n" +
                "6 - Create an assignment in the course\n" +
                "7 - Create a post in the course forum\n" +
                "8 - Average grade for each students\n" +
                "9 - Back\n");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine("1 - Delete user\n" +
                            "2 - Update user\n" +
                            "3 - create user\n" +
                            "4 - Back");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                {
                                    using (var cmd3 = new NpgsqlCommand($"select * from users where not userid = {id}", conn))
                                    {
                                        using (var reader = cmd3.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                int userid = reader.GetInt32(0);
                                                var roleid2 = reader.GetValue(1);
                                                string firstname = reader.GetString(2);
                                                string lastname = reader.GetString(3);

                                                Console.WriteLine($"Userid: {userid}\nRoleid: {roleid2}\nFirstname: {firstname}\nLastname: {lastname}\n");
                                            }
                                        }
                                    }

                                    Console.WriteLine("\nSelect UserId");
                                    var userId = Console.ReadLine();

                                    using (var cmd = new NpgsqlCommand($"DELETE FROM useractions WHERE userid = {userId}", conn))
                                    {
                                        cmd.ExecuteNonQuery();

                                        using (var cmd3 = new NpgsqlCommand($"DELETE FROM users WHERE userid = {userId}", conn))
                                        {
                                            cmd3.ExecuteNonQuery();

                                            Console.WriteLine("You deleted user!");
                                            Console.WriteLine("Press any button to continue");
                                            Console.ReadKey();
                                        }
                                    }

                                }
                                break;
                            case "2":
                                {
                                    Console.Clear();

                                    using (var cmd3 = new NpgsqlCommand($"select * from users where not userid = {id}", conn))
                                    {
                                        using (var reader = cmd3.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                int userid = reader.GetInt32(0);
                                                var roleid2 = reader.GetValue(1);
                                                string firstname = reader.GetString(2);
                                                string lastname = reader.GetString(3);

                                                Console.WriteLine($"Userid: {userid}\nRoleid: {roleid2}\nFirstname: {firstname}\nLastname: {lastname}\n");
                                            }
                                        }
                                    }

                                    Console.WriteLine("\nSelect UserId");
                                    var userId = Console.ReadLine();

                                    using (var cmd3 = new NpgsqlCommand($"select * from roles", conn))
                                    {
                                        using (var reader = cmd3.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                int roleid = reader.GetInt32(0);
                                                string name = reader.GetString(1);

                                                Console.WriteLine($"Roleid: {roleid}\nName: {name}\n");
                                            }
                                        }
                                    }

                                    Console.WriteLine("RoleId: ");
                                    var RoleId = Console.ReadLine();
                                    Console.WriteLine("FirstName: ");
                                    var FirstName = Console.ReadLine();
                                    Console.WriteLine("LastName: ");
                                    var LastName = Console.ReadLine();
                                    Console.WriteLine("Email");
                                    var Email = Console.ReadLine();
                                    Console.WriteLine("Password");
                                    var Password = Console.ReadLine();

                                    using (var cmd3 = new NpgsqlCommand($"UPDATE Users SET FirstName = '{FirstName}', LastName = '{LastName}', RoleID = {RoleId}, Email = '{Email}', Password = '{Password}' WHERE UserID = {userId};", conn))
                                    {
                                        cmd3.ExecuteNonQuery();

                                        Console.WriteLine("User updated!");
                                        Console.WriteLine("Press any button to continue");
                                        Console.ReadKey();
                                    }
                                }
                                break;
                            case "3":
                                {
                                    using (var cmd3 = new NpgsqlCommand($"select * from roles", conn))
                                    {
                                        using (var reader = cmd3.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                int roleid = reader.GetInt32(0);
                                                string name = reader.GetString(1);

                                                Console.WriteLine($"Roleid: {roleid}\nName: {name}\n");
                                            }
                                        }
                                    }

                                    Console.WriteLine("RoleId: ");
                                    var RoleId = Console.ReadLine();
                                    Console.WriteLine("firstName: ");
                                    var FirstName = Console.ReadLine();
                                    Console.WriteLine("LastName: ");
                                    var LastName = Console.ReadLine();
                                    Console.WriteLine("Email");
                                    var Email = Console.ReadLine();
                                    Console.WriteLine("Password");
                                    var Password = Console.ReadLine();

                                    using (var cmd = new NpgsqlCommand($"INSERT INTO Users (RoleID,  FirstName, LastName, Email, Password) VALUES ({RoleId}, '{FirstName}', '{LastName}', '{Email}', '{Password}')", conn))
                                    {
                                        cmd.ExecuteNonQuery();
                                        Console.WriteLine("\nSuccess!");
                                        Console.WriteLine("Press any button to continue");
                                        Console.ReadKey();
                                    }
                                }
                                break;
                            case "4":
                                break;
                        }
                    }
                    break;

                case "2":
                    {
                        using (var cmd3 = new NpgsqlCommand($"select * from users where not userid = {id}", conn))
                        {
                            using (var reader = cmd3.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int userid = reader.GetInt32(0);
                                    var roleid2 = reader.GetValue(1);
                                    string firstname = reader.GetString(2);
                                    string lastname = reader.GetString(3);

                                    Console.WriteLine($"Userid: {userid}\nRoleid: {roleid2}\nFirstname: {firstname}\nLastname: {lastname}\n");
                                }
                            }
                        }

                        Console.WriteLine("\nSelect UserId");
                        var userId = Console.ReadLine();

                        using (var cmd3 = new NpgsqlCommand($"select * from roles", conn))
                        {
                            using (var reader = cmd3.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int roleid = reader.GetInt32(0);
                                    string name = reader.GetString(1);

                                    Console.WriteLine($"Roleid: {roleid}\nName: {name}\n");
                                }
                            }
                        }

                        Console.WriteLine("\nSelect roleid");
                        var uroleid = Console.ReadLine();

                        using (var cmd4 = new NpgsqlCommand($"UPDATE Users SET roleid = {uroleid} WHERE UserID = {userId}", conn))
                        {
                            cmd4.ExecuteNonQuery();

                            Console.WriteLine("You've updated the user role!");
                            Console.WriteLine("Press any button to continue");
                            Console.ReadKey();
                        }
                    }
                    break;

                case "3":
                    {
                        Console.Clear();
                        Console.WriteLine("Name: ");
                        var name1 = Console.ReadLine();
                        Console.WriteLine("Description: ");
                        var description1 = Console.ReadLine();
                        Console.WriteLine("StartDate (YYYY-MM-DD): ");
                        var startDate1 = Console.ReadLine();
                        Console.WriteLine("EndDate (YYYY-MM-DD): ");
                        var endDate1 = Console.ReadLine();

                        using (var cmd3 = new NpgsqlCommand($"select * from users where roleid = (select roleid from roles where name = 'Instructor')", conn))
                        {
                            using (var reader = cmd3.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int userid = reader.GetInt32(0);
                                    int roleid2 = reader.GetInt32(1);
                                    string firstname = reader.GetString(2);
                                    string lastname = reader.GetString(3);

                                    Console.WriteLine($"Userid: {userid}\nRoleid: {roleid2}\nFirstname: {firstname}\nLastname: {lastname}\n");
                                }
                            }
                        }

                        Console.WriteLine("\nSelect UserId to make it an instructor");
                        var userId = Console.ReadLine();

                        using (var cmd = new NpgsqlCommand($"INSERT INTO Courses (Name, Description, StartDate, EndDate) VALUES ('{name1}', '{description1}', '{startDate1}', '{endDate1}') RETURNING CourseID", conn))
                        {
                            var courseid1 = Convert.ToInt32(cmd.ExecuteScalar());

                            using (var cmd4 = new NpgsqlCommand($"INSERT INTO Instructors (UserID, CourseID, Description) VALUES ({userId}, {courseid1}, 'Инструктор для Курса {courseid1}')", conn))
                            {
                                cmd4.ExecuteNonQuery();

                                Console.WriteLine("You've created a course and a new instructor!");
                                Console.WriteLine("Press any button to continue");
                                Console.ReadKey();
                            }
                        }
                    }
                    break;

                case "4":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.Clear();
                                while (reader.Read())
                                {
                                    int courseid = reader.GetInt32(0);
                                    string name = reader.GetString(1);
                                    string description = reader.GetString(2);
                                    DateTime startDate = reader.GetDateTime(3);
                                    DateTime endDate = reader.GetDateTime(4);

                                    Console.WriteLine($"Courseid: {courseid}\nName: {name}\nDescription: {description}\nStartDate: {startDate}\nEndDate: {endDate}\n");
                                }
                            }
                        }

                        Console.WriteLine("\nSelect CourseId: ");
                        var course2 = Console.ReadLine();
                        Console.WriteLine("\nName: ");
                        var name2 = Console.ReadLine();
                        Console.WriteLine("\nContent: ");
                        var content = Console.ReadLine();
                        using (var cmd = new NpgsqlCommand($"INSERT INTO CourseMaterials (CourseID, Name, Content) VALUES ({course2}, '{name2}', '{content}')", conn))
                        {
                            cmd.ExecuteNonQuery();

                            Console.WriteLine("New material added!");
                            Console.WriteLine("Press any button to continue");
                            Console.ReadKey();
                        }
                    }
                    break;

                case "5":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.Clear();
                                while (reader.Read())
                                {
                                    int courseid = reader.GetInt32(0);
                                    string name = reader.GetString(1);
                                    string description = reader.GetString(2);
                                    DateTime startDate = reader.GetDateTime(3);
                                    DateTime endDate = reader.GetDateTime(4);

                                    Console.WriteLine($"Courseid: {courseid}\nName: {name}\nDescription: {description}\nStartDate: {startDate}\nEndDate: {endDate}\n");
                                }
                            }
                        }

                        Console.WriteLine("\nSelect CourseId: ");
                        var course33 = Console.ReadLine();

                        using (var cmd = new NpgsqlCommand($"SELECT * FROM CourseMaterials where CourseID = {course33}", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.Clear();
                                while (reader.Read())
                                {
                                    int materialid = reader.GetInt32(0);
                                    string name3 = reader.GetString(2);
                                    string content3 = reader.GetString(3);

                                    Console.WriteLine($"Naterialid: {materialid}\nCourseid: {course33}\nName: {name3}\nContent: {content3}\n");
                                }
                            }
                        }


                        Console.WriteLine("\nSelect Materialid: ");
                        var materialidsel = Console.ReadLine();

                        while (true)
                        {
                            Console.WriteLine("\n1 - Delete\n" +
                                "2 - Update\n" +
                                "3 - Back");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                    using (var cmd = new NpgsqlCommand($"DELETE FROM CourseMaterials where MaterialID = {materialidsel}", conn))
                                    {
                                        cmd.ExecuteNonQuery();

                                        Console.WriteLine("Material deleted!");
                                        Console.WriteLine("Press any button to continue");
                                        Console.ReadKey();
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("\nNew name: ");
                                    var newName = Console.ReadLine();
                                    Console.WriteLine("\nNew content: ");
                                    var newContent = Console.ReadLine();

                                    using (var cmd = new NpgsqlCommand($"UPDATE CourseMaterials SET Name = '{newName}', Content = '{newContent}' WHERE MaterialID = {materialidsel}", conn))
                                    {
                                        cmd.ExecuteNonQuery();

                                        Console.WriteLine("Material updated!");
                                        Console.WriteLine("Press any button to continue");
                                        Console.ReadKey();
                                    }
                                    break;
                                case "3":
                                    break;
                                default:
                                    continue;
                            }
                            break;
                        }
                    }
                    break;

                case "6":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.Clear();
                                while (reader.Read())
                                {
                                    int courseid = reader.GetInt32(0);
                                    string name = reader.GetString(1);
                                    string description = reader.GetString(2);
                                    DateTime startDate = reader.GetDateTime(3);
                                    DateTime endDate = reader.GetDateTime(4);

                                    Console.WriteLine($"Courseid: {courseid}\nName: {name}\nDescription: {description}\nStartDate: {startDate}\nEndDate: {endDate}\n");
                                }
                            }
                        }

                        Console.WriteLine("\nSelect CourseId: ");
                        var course5 = Console.ReadLine();

                        Console.WriteLine("Name: ");
                        var name2 = Console.ReadLine();
                        Console.WriteLine("Description: ");
                        var description2 = Console.ReadLine();

                        using (var cmd = new NpgsqlCommand($"INSERT INTO Assignments (CourseID, Name, Description) VALUES ({course5}, '{name2}', '{description2}')", conn))
                        {
                            cmd.ExecuteNonQuery();

                            Console.WriteLine("Assignment added!");
                            Console.WriteLine("Press any button to continue");
                            Console.ReadKey();
                        }
                    }
                    break;

                case "7":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.Clear();
                                while (reader.Read())
                                {
                                    int courseid = reader.GetInt32(0);
                                    string name = reader.GetString(1);
                                    string description = reader.GetString(2);
                                    DateTime startDate = reader.GetDateTime(3);
                                    DateTime endDate = reader.GetDateTime(4);

                                    Console.WriteLine($"Courseid: {courseid}\nName: {name}\nDescription: {description}\nStartDate: {startDate}\nEndDate: {endDate}\n");
                                }
                            }
                        }

                        Console.WriteLine("\nSelect CourseId: ");
                        var course3 = Console.ReadLine();
                        using (var cmd = new NpgsqlCommand($"select * from CourseForums where courseid={course3}", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                bool isEmpty = true;
                                while (reader.Read())
                                {
                                    int forumId = reader.GetInt32(0);
                                    int courseId = reader.GetInt32(1);
                                    string name = reader.GetString(2);

                                    Console.WriteLine($"\nForumId: {forumId}\nCourseId: {courseId}\nName: {name}");
                                    isEmpty = false;
                                }

                                if (isEmpty)
                                {
                                    Console.WriteLine("\nNo forums found");

                                    Console.WriteLine("Press any button to continue");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                        }

                        Console.WriteLine("\nSelect ForumId: ");
                        var forum = Console.ReadLine();
                        Console.WriteLine("\nMessage: ");
                        var message = Console.ReadLine();

                        using (var cmd = new NpgsqlCommand($"INSERT INTO ForumMessages (ForumID, UserID, MessageText) VALUES ({forum}, {id}, '{message}')", conn))
                        {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("The message has been sent!");
                        }

                        Console.WriteLine("Press any button to continue");
                        Console.ReadKey();
                    }
                    break;

                case "8":

                    Console.Clear();

                    using (var cmd3 = new NpgsqlCommand($"SELECT Users.FirstName, Users.LastName, AVG(Grades.Grade) AS AverageGrade FROM Users JOIN Students ON Users.UserID = Students.UserID JOIN Grades ON Students.StudentID = Grades.StudentID WHERE Users.RoleID = (SELECT RoleID FROM Roles WHERE Name = 'Student') GROUP BY Users.UserID", conn))
                    {
                        using (var reader = cmd3.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string firstname = reader.GetString(0);
                                string lastname = reader.GetString(1);
                                int avg = reader.GetInt32(2);

                                Console.WriteLine($"Firstname: {firstname}\nLastname: {lastname}\nAVG: {avg}\n");
                            }

                            Console.WriteLine("Press any button to continue");
                            Console.ReadKey();
                        }
                    }
                    break;

                case "9":
                    return;
            }
        }
    }
}
