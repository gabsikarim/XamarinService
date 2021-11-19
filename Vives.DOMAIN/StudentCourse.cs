using Vives.DOMAIN.Helpers;

namespace Vives.DOMAIN
{
    public class StudentCourse : GObject
    {
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
