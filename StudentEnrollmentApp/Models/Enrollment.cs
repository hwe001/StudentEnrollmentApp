using SQLite;

namespace StudentEnrollmentApp.Models
{
    public class Enrollment
    {
        [PrimaryKey, AutoIncrement]
        public int EnrollmentId { get; set; }

        [Indexed]
        public int StudentId { get; set; }

        [Indexed]
        public string CourseCode { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}