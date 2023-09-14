# Климкович Николай, 153504

**Функциональные требования:**

1. **Функции аутентификации и авторизации:**
   - Пользователи могут регистрироваться с использованием электронной почты и пароля.
   - Пользователи могут входить в систему, указывая свою электронную почту и пароль.
   - Роли пользователей могут быть "Студент", "Преподаватель", "Администратор".
   - Администраторы могут управлять пользователями и назначать роли.

2. **Управление курсами:**
   - Преподаватели могут создавать новые курсы и указывать их параметры (название, описание, даты).
   - Преподаватели могут добавлять и управлять материалами для курсов.
   - Студенты могут записываться на курсы.
   - Преподаватели могут оценивать студентов по их успеваемости на курсах.

3. **Форумы и общение:**
   - Курсы имеют форумы, на которых студенты и преподаватели могут создавать и отвечать на сообщения.
   - Пользователи могут оценивать и комментировать сообщения на форумах.

4. **Задания и оценки:**
   - Преподаватели могут создавать задания для студентов на курсах.
   - Студенты могут отправлять решения заданий.
   - Преподаватели могут оценивать решения и выставлять оценки.

5. **Действия пользователей:**
   - Система должна регистрировать действия пользователей (например, вход в систему, создание курсов, отправка сообщений на форумах).

**Сущности БД:**

1. **Пользователи (Users)**
   - UserID (Primary Key, INT, AUTOINCREMENT)
   - Имя (VARCHAR(255))
   - Фамилия (VARCHAR(255))
   - Email (VARCHAR(255), Уникальный)
   - Пароль (VARCHAR(255))
   - Роль (Foreign Key, связь с Roles.RoleID)

2. **Роли (Roles)**
   - RoleID (Primary Key, INT, AUTOINCREMENT)
   - Название роли (VARCHAR(255))

3. **Курсы (Courses)**
   - CourseID (Primary Key, INT, AUTOINCREMENT)
   - Название курса (VARCHAR(255))
   - Описание (TEXT)
   - Дата начала (DATE)
   - Дата окончания (DATE)

4. **Материалы курсов (CourseMaterials)**
   - - MaterialID (Primary Key, INT, AUTOINCREMENT)
     - Название материала (VARCHAR(255))
     - Содержание (TEXT)
     - CourseID (Foreign Key, связь с Courses.CourseID)
   - Виды связей: Один ко многим (One-to-Many). Один курс может иметь много материалов.

5. **Оценки (Grades)**
   - - GradeID (Primary Key, INT, AUTOINCREMENT)
     - Оценка (DECIMAL(5, 2))
     - UserID (Foreign Key, связь с Users.UserID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может иметь много оценок в рамках множества курсов.

6. **Действия пользователя (UserActions)**
   - - ActionID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - Дата и время действия (DATETIME)
     - Описание действия (TEXT)
   - Виды связей: Один ко многим (One-to-Many). Один пользователь может совершать много действий.

7. **Преподаватели курсов (Instructors)**
   - - InstructorID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может быть преподавателями множества курсов.

8. **Учащиеся курсов (Students)**
   - - StudentID (Primary Key, INT, AUTOINCREMENT)
     - UserID (Foreign Key, связь с Users.UserID)
     - CourseID (Foreign Key, связь с Courses.CourseID)
   - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может быть учащимися множества курсов.

9. **Форумы курсов (CourseForums)**
   - - ForumID (Primary Key, INT, AUTOINCREMENT)
     - Название форума (VARCHAR(255))
     - CourseID (Foreign Key, связь с Courses.CourseID)
   - Виды связей: Один ко многим (One-to-Many). Один курс может иметь много форумов.

10. **Сообщения на форуме (ForumMessages)**
    - - MessageID (Primary Key, INT, AUTOINCREMENT)
      - Текст сообщения (TEXT)
      - UserID (Foreign Key, связь с Users.UserID)
      - ForumID (Foreign Key, связь с CourseForums.ForumID)
    - Виды связей: Один ко многим (One-to-Many). Один пользователь может отправлять много сообщений на форуме.

11. **Оценки за задания (AssignmentGrades)**
    - - AssignmentGradeID (Primary Key, INT, AUTOINCREMENT)
      - Оценка за задание (DECIMAL(5, 2))
      - UserID (Foreign Key, связь с Users.UserID)
      - AssignmentID (Foreign Key, связь с Assignments.AssignmentID)
    - Виды связей: Множество-ко-множеству (Many-to-Many). Много пользователей может иметь много оценок за разные задания.

12. **Задания (Assignments)**
    - - AssignmentID (Primary Key, INT, AUTOINCREMENT)
      - Название задания (VARCHAR(255))
      - Описание задания (TEXT)
      - CourseID (Foreign Key, связь с Courses.CourseID)
    - Виды связей: Один ко многим (One-to-Many). Один курс может иметь много заданий.
