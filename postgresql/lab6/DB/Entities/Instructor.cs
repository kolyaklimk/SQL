using Npgsql;

static public class Instructor
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

            Console.WriteLine(
                "1 - Create a new course\n" +
                "2 - Add material for the course\n" +
                "3 - Manage course materials\n" +
                "4 - Grade a student on a course\n" +
                "5 - Create an assignment in the course\n" +
                "6 - Create a post in the course forum\n" +
                "7 - Back\n");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
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

                        int courseid1 = -1;

                        using (var cmd = new NpgsqlCommand($"INSERT INTO Courses (Name, Description, StartDate, EndDate) VALUES ('{name1}', '{description1}', '{startDate1}', '{endDate1}') RETURNING CourseID", conn))
                        {
                            courseid1 = Convert.ToInt32(cmd.ExecuteScalar());

                            using (var cmd2 = new NpgsqlCommand($"INSERT INTO Instructors (UserID, CourseID, Description) VALUES ({id}, {courseid1}, 'Инструктор для Курса {courseid1}')", conn))
                            {
                                cmd2.ExecuteNonQuery();

                                Console.WriteLine("You created a new course and became its instructor!");
                                Console.WriteLine("Press any button to continue");
                                Console.ReadKey();
                            }
                        }
                    }
                    break;

                case "2":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID in (select courseid from instructors where userid = {id})", conn))
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

                case "3":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID in (select courseid from instructors where userid = {id})", conn))
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

                case "4":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID in (select courseid from instructors where userid = {id})", conn))
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
                        var course4 = Console.ReadLine();
                        using (var cmd = new NpgsqlCommand($"select * from Students where courseid = {course4}", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.Clear();
                                bool isEmpty = true;
                                while (reader.Read())
                                {
                                    int studentid = reader.GetInt32(0);
                                    int userid = reader.GetInt32(1);
                                    int courseid = reader.GetInt32(2);
                                    var description = reader.GetValue(3);
                                    DateTime date = reader.GetDateTime(4);

                                    Console.WriteLine($"Studentid: {studentid}\nUserid: {userid}\nCourseid: {courseid}\nDescription: {description}\nDate: {date}\n");
                                    isEmpty = false;
                                }
                                if (isEmpty)
                                {
                                    Console.WriteLine("Student not found");
                                    Console.WriteLine("Press any button to continue");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                        }

                        Console.WriteLine("\nSelect Studentid: ");
                        var Studentid = Console.ReadLine();
                        Console.WriteLine("Grade: ");
                        var grade = Console.ReadLine();
                        Console.WriteLine("Description: ");
                        var description4 = Console.ReadLine();

                        using (var cmd = new NpgsqlCommand($"select instructorid from instructors where userid = {id}", conn))
                        {
                            var instructorid = Convert.ToInt32(cmd.ExecuteScalar());

                            using (var cmd2 = new NpgsqlCommand($"call assign_grade_to_student({Studentid}, {instructorid}, {course4}, {grade}, '{description4}')", conn))
                            {
                                cmd2.ExecuteNonQuery();

                                Console.WriteLine("You made the grade!");
                                Console.WriteLine("Press any button to continue");
                                Console.ReadKey();
                            }
                        }
                    }
                    break;

                case "5":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID in (select courseid from instructors where userid = {id})", conn))
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

                case "6":
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID in (select courseid from instructors where userid = {id})", conn))
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

                case "7":
                    return;
            }
        }
    }
}
