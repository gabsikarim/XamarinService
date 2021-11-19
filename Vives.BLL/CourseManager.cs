using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vives.DAL;
using Vives.DOMAIN;

namespace Vives.BLL
{
    public class CourseManager
    {
        private readonly CourseDB courseDB = new CourseDB();

        public async Task<Course> CreateAsync(Course entity)
        {
            return await courseDB.CreateAsync(entity);
        }

        public async Task<IEnumerable<Course>> CreateRangeAsync(List<Course> entities)
        {
            return await courseDB.CreateRangeAsync(entities);
        }

        public async Task<Course> DeleteAsync(Course entity)
        {
            return await courseDB.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Course>> GetAsync(int skip, int take)
        {
            return await courseDB.GetAsync(skip, take);
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await courseDB.GetByIdAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await courseDB.GetTotalCountAsync();
        }

        public async Task<Course> UpdateAsync(Course entity)
        {
            return await courseDB.UpdateAsync(entity);
        }
    }
}
