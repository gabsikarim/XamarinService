using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vives.DAL;
using Vives.DOMAIN;
using Vives.DOMAIN.Contracts;
using Vives.DOMAIN.Helpers;

namespace Vives.BLL
{
    public class StudentManager : IStudent
    {
        private readonly StudentDB studentDB = new StudentDB();

        public async Task<Student> CreateAsync(Student entity)
        {
            if (!IsValidEmail(entity.Private_Email))
            {
                entity.Vex = new VivesException("Invalid Email", ExceptionTypes.Warning);
                return entity;
            }
            return await studentDB.CreateAsync(entity);
        }

        public async Task<IEnumerable<Student>> CreateRangeAsync(List<Student> entities)
        {
            return await studentDB.CreateRangeAsync(entities);
        }

        public async Task<Student> DeleteAsync(Student entity)
        {
            return await studentDB.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Student>> GetAsync(int skip, int take)
        {
            return await studentDB.GetAsync(skip, take);
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            if (id <= 0)
                return new Student() { Vex = new VivesException("Id is lower than one", ExceptionTypes.Warning) };

            //int nul = 0;
            //int tst = 2 / nul;
            Student student = await studentDB.GetByIdAsync(id);
            return student ?? new Student() { Vex = new VivesException("No Student Found!", ExceptionTypes.Warning) };
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await studentDB.GetTotalCountAsync();
        }

        public async Task<Student> UpdateAsync(Student entity)
        {
            if (!IsValidEmail(entity.Private_Email))
            {
                entity.Vex = new VivesException("Invalid Email", ExceptionTypes.Warning);
                return entity;
            }

            return await studentDB.UpdateAsync(entity);
        }

        //https://stackoverflow.com/a/48476318/3701072
        public bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}
