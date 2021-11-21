using Bogus;
using Bogus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vives.BLL;
using Vives.DOMAIN;

namespace Vives.Mock
{
    class Program
    {
        static void MockData()
        {
            StudentManager studentManager = new StudentManager();
            CourseManager courseManager = new CourseManager();
            StudentCourseManager studentcourseManager = new StudentCourseManager();

            Faker<Course> fake_courses = new Faker<Course>()
                .RuleFor(x => x.Name, f => f.Name.JobTitle())
                .RuleFor(x => x.SP, f => f.Random.Int(1, 20));

            Console.WriteLine("Creating Mock Courses...");
            IEnumerable<Course> myCourses = courseManager.CreateRangeAsync(fake_courses.GenerateBetween(5, 15).ToList()).Result;

            Faker<Student> fake_students = new Faker<Student>()
                .RuleFor(x => x.Firstname, f => f.Name.FirstName())
                .RuleFor(x => x.Lastname, f => f.Name.LastName())
                .RuleFor(x => x.Date_Of_Birth, f => f.Date.Between(new DateTime(1950, 1, 1), new DateTime(2003, 1, 1)))
                .RuleFor(x => x.Private_Email, (f, x) => f.Internet.Email(x.Firstname, x.Lastname));

            Console.WriteLine("Creating Mock Students...");
            IEnumerable<Student> myStudents = studentManager.CreateRangeAsync(fake_students.GenerateBetween(200, 500).ToList()).Result;


            Faker<StudentCourse> fake_enrollments = new Faker<StudentCourse>()
                .StrictMode(false)
                .UseSeed(1236)  //It's a good idea to set a specific seed to generate different result of each Faker
                .RuleFor(x => x.StudentID, f => f.PickRandom(myStudents).ID)  //lookup existing value in Grades
                .RuleFor(x => x.CourseID, f => f.PickRandom(myCourses).ID);

            IEnumerable<StudentCourse> fake_selective_enrollments = fake_enrollments.Generate(150).ToList()
                .GroupBy(x => (x.StudentID, x.CourseID))
                .Select(x => x.FirstOrDefault());  //remove duplicate

            Console.WriteLine("Creating Mock Enrollements...");
            IEnumerable<StudentCourse> myEnrollments = studentcourseManager.CreateRangeAsync(fake_selective_enrollments.ToList()).Result;

            Console.WriteLine("Done");
        }

        
        static void Main(string[] args)
        {
            MockData();
            //TestEmail();

        }

        private static void TestEmail()
        {
            StudentManager studentManager = new StudentManager();

            try
            {
                Task<Student> student_task = VivesTask<Student>.Try(() => studentManager.GetByIdAsync(654654));
                Student student = student_task.Result;

                if (!student.Successful)
                    throw student.Vex;

                Console.WriteLine("Email: " + student.Private_Email);
                student.Private_Email = "azerty blabla";

                student = studentManager.UpdateAsync(student).Result;
                if (!student.Successful)
                    throw student.Vex;

                Console.WriteLine("New email: " + student.Private_Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
