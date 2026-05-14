using SQLite;

namespace StudentEnrollmentApp.Models
{
    public class Course
    {
        [PrimaryKey]
        public string CourseCode { get; set; }

        [NotNull]
        public string CourseName { get; set; }

        public int Credits { get; set; }
    }
}