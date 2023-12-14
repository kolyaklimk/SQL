using Npgsql;

static public class Student
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
                    Console.WriteLine("Student:");
                    Console.WriteLine($"UserID: {userId},\nRoleID: {role},\nFirstName: {firstName},\nLastName: {lastName},\nEmail: {email},\nPassword: {password}\n");
                }
            }
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine(
                "1 - Enroll in the course\n" +
                "2 - Post assignment solutions\n" +
                "3 - Create a post in the course forum\n" +
                "4 - Back\n");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID not in (select courseid from students where userid = {id})", conn))
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

                    Console.WriteLine("Enter the courseid for which you wish to enroll: ");
                    var course = Console.ReadLine();
                    using (var cmd = new NpgsqlCommand($"INSERT INTO Students (UserID, CourseID) VALUES ({id}, {course})", conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("You're enrolled in the course!");
                    }

                    Console.WriteLine("Press any button to continue");
                    Console.ReadKey();
                    break;

                case "2":
                    using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID in (select courseid from students where userid = {id})", conn))
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
                    using (var cmd = new NpgsqlCommand($"select * from Assignments where courseid={course2}", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            bool isEmpty = true;
                            while (reader.Read())
                            {
                                int assignmentsId = reader.GetInt32(0);
                                int courseId = reader.GetInt32(1);
                                string name = reader.GetString(2);
                                string description = reader.GetString(3);

                                Console.WriteLine($"\nAssignmentId: {assignmentsId}\nCourseId: {courseId}\nName: {name}\nDescription: {description}");
                                isEmpty = false;
                            }

                            if (isEmpty)
                            {
                                Console.WriteLine("\nNo assignments found");

                                Console.WriteLine("Press any button to continue");
                                Console.ReadKey();
                                break;
                            }
                        }
                    }

                    Console.WriteLine("\nSelect AssignmentId: ");
                    var assignment = Console.ReadLine();
                    Console.WriteLine("\nSolutionText: ");
                    var solutionText = Console.ReadLine();
                    int studentid = -1;

                    using (var cmd = new NpgsqlCommand($"select studentid from students where UserID = {id} and courseId = {course2}", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                studentid = reader.GetInt32(0);
                            }
                        }
                    }

                    using (var cmd = new NpgsqlCommand($"INSERT INTO Submission (StudentID, AssignmentID, SolutionText) VALUES ( {studentid}, {assignment}, '{solutionText}')", conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("The solution has been sent!");
                    }

                    Console.WriteLine("Press any button to continue");
                    Console.ReadKey();
                    break;

                case "3":
                    using (var cmd = new NpgsqlCommand($"SELECT * FROM Courses where Courses.CourseID in (select courseid from students where userid = {id})", conn))
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
                    break;

                case "4":
                    return;
            }
        }
    }
}
