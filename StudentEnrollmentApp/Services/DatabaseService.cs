using SQLite;
using StudentEnrollmentApp.Models;

namespace StudentEnrollmentApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task Init()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "Enrollment.db");

            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Student>();
            await _database.CreateTableAsync<Course>();
            await _database.CreateTableAsync<Enrollment>();
        }

        // =========================
        // STUDENT
        // =========================

        public async Task AddStudent(Student student)
        {
            await Init();

            await _database.InsertAsync(student);
        }

        public async Task<List<Student>> GetStudents()
        {
            await Init();

            return await _database.Table<Student>().ToListAsync();
        }

        // =========================
        // COURSE
        // =========================

        public async Task<string> AddCourse(Course course)
        {
            await Init();

            var existingCourse = await _database.Table<Course>()
                .Where(c => c.CourseCode == course.CourseCode)
                .FirstOrDefaultAsync();

            if (existingCourse != null)
            {
                return "Course code already exists.";
            }

            await _database.InsertAsync(course);

            return "Course added.";
        }

        public async Task<List<Course>> GetCourses()
        {
            await Init();

            return await _database.Table<Course>().ToListAsync();
        }

        // =========================
        // ENROLLMENT
        // =========================

        public async Task<string> AddEnrollment(Enrollment enrollment)
        {
            await Init();

            // check duplicate enrollment

            var existing = await _database.Table<Enrollment>()
                .Where(e =>
                    e.StudentId == enrollment.StudentId &&
                    e.CourseCode == enrollment.CourseCode)
                .FirstOrDefaultAsync();

            if (existing != null)
            {
                return "Student already enrolled.";
            }

            await _database.InsertAsync(enrollment);

            return "Enrollment successful.";
        }

        public async Task<List<Enrollment>> GetEnrollments()
        {
            await Init();

            return await _database.Table<Enrollment>().ToListAsync();
        }
    }
}