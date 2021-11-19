using System;
using System.Collections.Generic;
using Vives.DOMAIN.Helpers;

namespace Vives.DOMAIN
{
    public class Student : GObject
    {
        public int ID { get; set; }
        public string RNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public DateTime Registration_Date { get; set; }
        public string Official_Email { get; set; }
        public string Private_Email { get; set; }

        //Student kan meerdere vakken hebben
        public ICollection<StudentCourse> Courses { get; set; }
    }
}
