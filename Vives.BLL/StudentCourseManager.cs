using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vives.DAL;
using Vives.DOMAIN;

namespace Vives.BLL
{
    public class StudentCourseManager
    {
        private readonly StudentCourseDB studentcourseDB = new StudentCourseDB();

        public async Task<StudentCourse> CreateAsync(StudentCourse entity)
        {
            return await studentcourseDB.CreateAsync(entity);
        }

        public async Task<IEnumerable<StudentCourse>> CreateRangeAsync(List<StudentCourse> entities)
        {
            return await studentcourseDB.CreateRangeAsync(entities);
        }

        public async Task<StudentCourse> DeleteAsync(StudentCourse entity)
        {
            return await studentcourseDB.DeleteAsync(entity);
        }

        public async Task<IEnumerable<StudentCourse>> GetAsync(int skip, int take)
        {
            return await studentcourseDB.GetAsync(skip, take);
        }

        public async Task<StudentCourse> GetByIdAsync(int id)
        {
            return await studentcourseDB.GetByIdAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await studentcourseDB.GetTotalCountAsync();
        }

        public async Task<StudentCourse> UpdateAsync(StudentCourse entity)
        {
            return await studentcourseDB.UpdateAsync(entity);
        }
    }
}
