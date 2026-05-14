using SQLite;

namespace StudentEnrollmentApp.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int StudentId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Email { get; set; }
    }
}