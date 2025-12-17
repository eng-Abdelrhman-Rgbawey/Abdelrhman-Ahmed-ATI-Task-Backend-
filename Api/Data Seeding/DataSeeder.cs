using Api.Models;

namespace Api.Data_Seeding
{
    public class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course { Name = "Angular" },
                    new Course { Name = ".NET" },
                    new Course { Name = "SQL" },
                    new Course { Name = "JavaScript" }
                );

                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                var student1 = new Student
                {
                    FullName = "Abdelrhman Ahmed",
                    Email = "abdo@example.com",
                    Phone = "0123456789"
                };

                var student2 = new Student
                {
                    FullName = "Mohamed Ali",
                    Email = "Mohamed@example.com",
                    Phone = "0112865307"
                };

                context.Students.AddRange(student1, student2);
                context.SaveChanges();

                var angularCourse = context.Courses.FirstOrDefault(c => c.Name == "Angular");
                var dotNetCourse = context.Courses.FirstOrDefault(c => c.Name == ".NET");
                var sqlCourse = context.Courses.FirstOrDefault(c => c.Name == "SQL");

                context.StudentCourses.AddRange(
                    new StudentCourse { StudentId = student1.Id, CourseId = angularCourse.Id },
                    new StudentCourse { StudentId = student1.Id, CourseId = sqlCourse.Id },
                    new StudentCourse { StudentId = student2.Id, CourseId = dotNetCourse.Id },
                    new StudentCourse { StudentId = student2.Id, CourseId = sqlCourse.Id }
                );

                context.SaveChanges();
            }
        }
    }
}
