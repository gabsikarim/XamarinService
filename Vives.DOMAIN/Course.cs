using System.Collections.Generic;
using Vives.DOMAIN.Helpers;

namespace Vives.DOMAIN
{
    public class Course : GObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SP { get; set; }
        public string Course_Code { get; set; }

        // Vak kan meerdere studenten hebben
        public ICollection<StudentCourse> Students { get; set; }

    }
}
