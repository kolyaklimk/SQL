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
     - Имя (VARCHAR(255), UNIQUE)
     - Фамилия (VARCHAR(255))
     - Email (VARCHAR(255), UNIQUE)
     - Пароль (VARCHAR(255), HASH)
     - Роль (Foreign Key, связь с Roles.RoleID)
   - Виды связей: Один к одному (One-to-One) с сущностью "Роли".

2. **Роли (Roles)**
   - - RoleID (Primary Key, INT, AUTOINCREMENT)
     - Название роли (VARCHAR(255))

3. **Курсы (Courses)**
   - - CourseID (Primary Key, INT, AUTOINCREMENT)
     - Название курса (VARCHAR(255))
     - Описание (TEXT)
     - Дата начала (DATE)
     - Дата окончания (DATE)

4. **Материалы курсов (CourseMaterials)**
   - - MaterialID (Primary Key, INT, AUTOINCREMENT)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Название материала (VARCHAR(255))
     - Содержание (TEXT)
   - Виды связей: Многие к одному (Many-to-One). Один курс может иметь много материалов.

5. **Оценки (Grades)**
   - - GradeID (Primary Key, INT, AUTOINCREMENT)
     - InstructorID (Foreign Key, связь с Instructors.InstructorID)
     - StudentID (Foreign Key, связь с Students.StudentID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Оценка (INT)
     - Описание (TEXT)
     - Дата (DATE)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может иметь много оценок в рамках множества курсов.

6. **Действия пользователя (UserActions)**
   - - ActionID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - Дата и время действия (DATETIME)
     - Описание действия (TEXT)
   - Виды связей: Многие к одному (Many-to-One). Один пользователь может совершать много действий.

7. **Преподаватели курсов (Instructors)**
   - - InstructorID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Описание (TEXT)
     - Дата (DATE)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может быть преподавателями множества курсов.

8. **Учащиеся курсов (Students)**
   - - StudentID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Описание (TEXT)
     - Дата (DATE)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может быть учащимися множества курсов.

9. **Форумы курсов (CourseForums)**
   - - ForumID (Primary Key, INT, AUTOINCREMENT)
     - CourseID (Foreign Key, связь с Courses.CourseID)
     - Название форума (VARCHAR(255))
   - Виды связей: Один ко многим (One-to-Many). Один курс может иметь много форумов.

10. **Сообщения на форуме (ForumMessages)**
    - - MessageID (Primary Key, INT, AUTOINCREMENT)
      - ForumID (Foreign Key, связь с CourseForums.ForumID)
      - UserID (Foreign Key, связь с Users.UserID)
      - Текст сообщения (TEXT)
      - Дата (DATE)
    - Виды связей: Многие к одному (Many-to-One). Один пользователь может отправлять много сообщений на форуме.

11. **Задания (Assignments)**
    - - AssignmentID (Primary Key, INT, AUTOINCREMENT)
      - CourseID (Foreign Key, связь с Courses.CourseID)
      - Название задания (VARCHAR(255))
      - Описание задания (TEXT)
    - Виды связей: Многие ко многим (Many-to-Many). Один курс может иметь много заданий.

12. **Решения (Submission)**
    - - SubmissionID (Primary Key, INT, AUTOINCREMENT)
      - StudentID (Foreign Key, связь с Student.StudentID)      
      - AssignmentID (Foreign Key, связь с Assignment.AssignmentID)
      - Дата (DATE)
      - Текст решения (TEXT)
    - Виды связей: Многие к одному (Many-to-One). Одно задание может иметь много решений.
