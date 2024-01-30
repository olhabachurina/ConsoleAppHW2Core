using ConsoleAppHW2Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
namespace ConsoleAppHW2Core;
class Program
{
    static void Main()
    {

        using (var db = new ApplicationContext())
        {
            db.Database.EnsureCreated();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Добавить курс");
                Console.WriteLine("2. Просмотреть курсы");
                Console.WriteLine("3. Добавить лекцию");
                Console.WriteLine("4. Просмотреть лекции");
                Console.WriteLine("5. Добавить мастер-класс");
                Console.WriteLine("6. Просмотреть мастер-классы");
                Console.WriteLine("7. Добавить студента");
                Console.WriteLine("8. Просмотреть студентов");
                Console.WriteLine("9. Регистрация на курс");
                Console.WriteLine("10. Отметить прогресс лекции");
                Console.WriteLine("11. Отметить прогресс мастер-класса");
                Console.WriteLine("0. Выход");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddCourse(db);
                        break;
                    case 2:
                        ViewCourses(db);
                        break;
                    case 3:
                        AddLecture(db);
                        break;
                    case 4:
                        ViewLectures(db);
                        break;
                    case 5:
                        AddWorkshop(db);
                        break;
                    case 6:
                        ViewWorkshops(db);
                        break;
                    case 7:
                        AddStudent(db);
                        break;
                    case 8:
                        ViewStudents(db);
                        break;
                    case 9:
                        RegisterForCourse(db);
                        break;
                    case 10:
                        MarkLectureProgress(db);
                        break;
                    case 11:
                        MarkWorkshopProgress(db);
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void AddCourse(ApplicationContext db)
        {
            Console.WriteLine("Введите название курса:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите описание курса:");
            string description = Console.ReadLine();

            var course = new Course { Title = title, Description = description };
            db.Courses.Add(course);
            db.SaveChanges();

            Console.WriteLine("Курс добавлен.");
        }
        static void ViewCourses(ApplicationContext db)
        {

            var courses = db.Courses.ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.Id}, Название: {course.Title}, Описание: {course.Description}");
            }
        }
        static void AddLecture(ApplicationContext db)
        {
            Console.WriteLine("Введите название лекции:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите описание лекции:");
            string description = Console.ReadLine();

            Console.WriteLine("Введите дату проведения (формат: гггг-мм-дд):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Неверный формат даты.");
                return;
            }

            Console.WriteLine("Введите ID курса:");
            if (!int.TryParse(Console.ReadLine(), out int courseId) || !db.Courses.Any(c => c.Id == courseId))
            {
                Console.WriteLine("Неверный ID курса.");
                return;
            }

            var lecture = new Lecture { Title = title, Description = description, Date = date, CourseId = courseId };
            db.Lectures.Add(lecture);

            try
            {
                db.SaveChangesAsync();
                Console.WriteLine("Лекция добавлена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при добавлении лекции: {ex.Message}");
            }
        }
        static void ViewLectures(ApplicationContext db)
        {
            var lectures = db.Lectures.Include(l => l.Course).ToList();
            foreach (var lecture in lectures)
            {
                Console.WriteLine($"ID: {lecture.Id}, Название: {lecture.Title}, Описание: {lecture.Description}, Дата: {lecture.Date.ToShortDateString()}, Курс: {lecture.Course.Title}");
            }
        }
        static void AddWorkshop(ApplicationContext db)
        {
            Console.WriteLine("Введите название мастер-класса:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите описание мастер-класса:");
            string description = Console.ReadLine();

            Console.WriteLine("Введите дату проведения (формат: гггг-мм-дд):");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введите максимальное количество участников:");
            int maxParticipants = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите ID курса:");
            int courseId = Convert.ToInt32(Console.ReadLine());

            var workshop = new Workshop { Title = title, Description = description, Date = date, MaxParticipants = maxParticipants, CourseId = courseId };
            db.Workshops.Add(workshop);
            db.SaveChanges();

            Console.WriteLine("Мастер-класс добавлен.");
        }
        static void ViewWorkshops(ApplicationContext db)
        {
            var workshops = db.Workshops.Include(w => w.Course).ToList();
            foreach (var workshop in workshops)
            {
                Console.WriteLine($"ID: {workshop.Id}, Название: {workshop.Title}, Описание: {workshop.Description}, Дата: {workshop.Date.ToShortDateString()}, Макс. участников: {workshop.MaxParticipants}, Курс: {workshop.Course.Title}");
            }
        }
        static void AddStudent(ApplicationContext db)
        {
            Console.WriteLine("Введите имя студента:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию студента:");
            string lastName = Console.ReadLine();

            var student = new Student { FirstName = firstName, LastName = lastName };
            db.Students.Add(student);
            db.SaveChanges();

            Console.WriteLine("Студент добавлен.");
        }
        static void ViewStudents(ApplicationContext db)
        {
            var students = db.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Имя: {student.FirstName}, Фамилия: {student.LastName}");
            }
        }
        static void RegisterForCourse(ApplicationContext db)
        {
            Console.WriteLine("Введите ID студента:");
            int studentId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите ID курса:");
            int courseId = Convert.ToInt32(Console.ReadLine());

            var registration = new CourseRegistration { StudentId = studentId, CourseId = courseId };
            db.CourseRegistrations.Add(registration);
            db.SaveChanges();

            Console.WriteLine("Студент зарегистрирован на курс.");
        }
        static void MarkLectureProgress(ApplicationContext db)
        {
            Console.WriteLine("Введите ID студента:");
            int studentId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите ID лекции:");
            int lectureId = Convert.ToInt32(Console.ReadLine());

            var lectureProgress = new LectureProgress { StudentId = studentId, LectureId = lectureId };
            var lectureExists = db.Lectures.Any(l => l.Id == lectureId);
            if (!lectureExists)
            {
                Console.WriteLine("Лекция с таким ID не найдена.");
                return;
            }
            db.LectureProgresses.Add(lectureProgress);
            db.SaveChanges();

            Console.WriteLine("Прогресс лекции отмечен.");
        }
        static void MarkWorkshopProgress(ApplicationContext context)
        {
            Console.WriteLine("Введите ID студента:");
            if (!int.TryParse(Console.ReadLine(), out int studentId) || !context.Students.Any(s => s.Id == studentId))
            {
                Console.WriteLine("Студент с таким ID не найден.");
                return;
            }

            Console.WriteLine("Введите ID мастер-класса:");
            if (!int.TryParse(Console.ReadLine(), out int workshopId) || !context.Workshops.Any(w => w.Id == workshopId))
            {
                Console.WriteLine("Мастер-класс с таким ID не найден.");
                return;
            }

            var workshopProgress = new WorkshopProgress { StudentId = studentId, WorkshopId = workshopId };

            try
            {
                context.WorkshopProgresses.Add(workshopProgress);
                context.SaveChanges();
                Console.WriteLine("Прогресс мастер-класса отмечен.");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                Console.WriteLine($"Произошла ошибка при сохранении данных: {ex.InnerException?.Message}");
            }
        }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public DbSet<LectureProgress> LectureProgresses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<WorkshopProgress> WorkshopProgresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-4PCU5RA\\SQLEXPRESS;Database=Education;Trusted_Connection=True;TrustServerCertificate=True;");
                optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // для Course
            modelBuilder.Entity<Course>().HasKey(c => c.Id); 
            modelBuilder.Entity<Course>()
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200); 

            //  для Lecture
            modelBuilder.Entity<Lecture>().HasKey(l => l.Id);
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Course) 
                .WithMany(c => c.Lectures)
                .HasForeignKey(l => l.CourseId);

            // для Workshop
            modelBuilder.Entity<Workshop>().HasKey(w => w.Id);
            modelBuilder.Entity<Workshop>()
                .HasOne(w => w.Course)
                .WithMany(c => c.Workshops)
                .HasForeignKey(w => w.CourseId);

            //  для Student
            modelBuilder.Entity<Student>().HasKey(s => s.Id);

            //  для CourseRegistration
            modelBuilder.Entity<CourseRegistration>().HasKey(cr => cr.Id);
            modelBuilder.Entity<CourseRegistration>()
                .HasOne(cr => cr.Student)
                .WithMany(s => s.CourseRegistrations)
                .HasForeignKey(cr => cr.StudentId);
            modelBuilder.Entity<CourseRegistration>()
                .HasOne(cr => cr.Course)
                .WithMany(c => c.CourseRegistrations)
                .HasForeignKey(cr => cr.CourseId);

            // для LectureProgress
            modelBuilder.Entity<LectureProgress>().HasKey(lp => lp.Id);
            modelBuilder.Entity<LectureProgress>()
                .HasOne(lp => lp.Student)
                .WithMany(s => s.LectureProgresses)
                .HasForeignKey(lp => lp.StudentId);
            modelBuilder.Entity<LectureProgress>()
                .HasOne(lp => lp.Lecture)
                .WithMany(l => l.LectureProgresses)
                .HasForeignKey(lp => lp.LectureId);
            // для WorkshopProgress
            modelBuilder.Entity<WorkshopProgress>().HasKey(wp => wp.Id);
            modelBuilder.Entity<WorkshopProgress>()
                .HasOne(wp => wp.Student)
                .WithMany(s => s.WorkshopProgresses)
                .HasForeignKey(wp => wp.StudentId);
            modelBuilder.Entity<WorkshopProgress>().HasOne(wp => wp.Workshop).WithMany(w => w.WorkshopProgresses).HasForeignKey(wp => wp.WorkshopId);
        }

    }
    
}





    





