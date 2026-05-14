using StudentEnrollmentApp.Models;

namespace StudentEnrollmentApp.Services
{
    public class SeedDataService
    {
        private readonly DatabaseService _databaseService;

        public SeedDataService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task SeedData()
        {
            // STUDENTS

            var students = await _databaseService.GetStudents();

            if (students.Count == 0)
            {
                await _databaseService.AddStudent(new Student
                {
                    Name = "Alice",
                    Email = "alice@test.com"
                });

                await _databaseService.AddStudent(new Student
                {
                    Name = "Bob",
                    Email = "bob@test.com"
                });
            }

            // COURSES

            var courses = await _databaseService.GetCourses();

            if (courses.Count == 0)
            {
                await _databaseService.AddCourse(new Course
                {
                    CourseCode = "CS101",
                    CourseName = "Programming",
                    Credits = 15
                });

                await _databaseService.AddCourse(new Course
                {
                    CourseCode = "IT202",
                    CourseName = "Databases",
                    Credits = 20
                });
            }
        }
    }
}