# Климкович Николай, 153504
# Онлайн-образовательная платформа

## Функциональные требования:**
0. **Авторизация и аутентификация пользователей.**

1. **Пользователи могут:**   
   - Регистрироваться с использованием электронной почты и пароля.
   - Входить в систему, указывая свою электронную почту и пароль.
   - Роли пользователей могут быть "Студент", "Преподаватель", "Администратор".

2. **Студенты могут:**
   - Записываться на курсы.
   - Отправлять решения заданий.
   - Cоздавать сообщения на форумах курсов.

3. **Преподаватели могут:**
   - Создавать новые курсы.
   - Добавлять и управлять материалами для курсов.
   - Оценивать студентов по их успеваемости на курсах.
   - Создавать задания на курсах.
   - Cоздавать сообщения на форумах курсов.

4. **Администраторы могут:**
   - Управлять пользователями и назначать роли.
   - Создавать новые курсы.
   - Добавлять и управлять материалами для курсов.
   - Создавать задания на курсах.
   - Cоздавать сообщения на форумах курсов.

5. **Форумы и общение:**
   - Курсы имеют форумы, на которых все роли могут создавать сообщения.

6. **Действия пользователей:**
   - Система регистрирует действия пользователей (например, вход в систему, создание курсов, отправка сообщений на форумах).

## Сущности БД:

1. **Пользователи (Users)**
   - - UserID (Primary Key, INT, AUTOINCREMENT)
     - FirstName (VARCHAR(255), UNIQUE)
     - LastName (VARCHAR(255))
     - Email (VARCHAR(255), UNIQUE)
     - Password (VARCHAR(255), HASH)
     - Role (Foreign Key, связь с Roles.RoleID)
   - Виды связей: Один к одному (One-to-One) с сущностью "Роли".

2. **Роли (Roles)**
   - - RoleID (Primary Key, INT, AUTOINCREMENT)
     - Name (VARCHAR(255))

3. **Курсы (Courses)**
   - - CourseID (Primary Key, INT, AUTOINCREMENT)
     - Name (VARCHAR(255))
     - Description (TEXT)
     - StartDate (DATE)
     - EndDate (DATE)

4. **Материалы курсов (CourseMaterials)**
   - - MaterialID (Primary Key, INT, AUTOINCREMENT)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Name (VARCHAR(255))
     - Content (TEXT)
   - Виды связей: Многие к одному (Many-to-One). Один курс может иметь много материалов.

5. **Оценки (Grades)**
   - - GradeID (Primary Key, INT, AUTOINCREMENT)
     - InstructorID (Foreign Key, связь с Instructors.InstructorID)
     - StudentID (Foreign Key, связь с Students.StudentID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Grade (INT)
     - Description (TEXT)
     - Date (DATE)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может иметь много оценок в рамках множества курсов.

6. **Действия пользователя (UserActions)**
   - - ActionID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - DateAndTime (DATETIME)
     - Description (TEXT)
   - Виды связей: Многие к одному (Many-to-One). Один пользователь может совершать много действий.

7. **Преподаватели курсов (Instructors)**
   - - InstructorID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Description (TEXT)
     - Date (DATE)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может быть преподавателями множества курсов.

8. **Учащиеся курсов (Students)**
   - - StudentID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Description (TEXT)
     - Date (DATE)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может быть учащимися множества курсов.

9. **Форумы курсов (CourseForums)**
   - - ForumID (Primary Key, INT, AUTOINCREMENT)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Name (VARCHAR(255))
   - Виды связей: Один ко многим (One-to-Many). Один курс может иметь много форумов.

10. **Сообщения на форуме (ForumMessages)**
    - - MessageID (Primary Key, INT, AUTOINCREMENT)
      - ForumID (Foreign Key, связь с CourseForums.ForumID)
      - UserID (Foreign Key, связь с Users.UserID)
      - MessageText (TEXT)
      - Date (DATE)
    - Виды связей: Многие к одному (Many-to-One). Один пользователь может отправлять много сообщений на форуме.

11. **Задания (Assignments)**
    - - AssignmentID (Primary Key, INT, AUTOINCREMENT)
      - CourseID (Foreign Key, связь с Courses.CourseID)
      - Name (VARCHAR(255))
      - Description (TEXT)
    - Виды связей: Многие ко многим (Many-to-Many). Один курс может иметь много заданий.

12. **Решения (Submission)**
    - - SubmissionID (Primary Key, INT, AUTOINCREMENT)
      - StudentID (Foreign Key, связь с Student.StudentID)      
      - AssignmentID (Foreign Key, связь с Assignment.AssignmentID)
      - Date (DATE)
      - SolutionText (TEXT)
    - Виды связей: Многие к одному (Many-to-One). Одно задание может иметь много решений.
