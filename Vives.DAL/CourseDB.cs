using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vives.DOMAIN;
using Vives.DOMAIN.Contracts;

namespace Vives.DAL
{
    public class CourseDB : ICourse
    {
        private readonly VivesContext VC = new VivesContext();

        public async Task<Course> CreateAsync(Course entity)
        {
            VC.Courses.Add(entity);
            await VC.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Course>> CreateRangeAsync(List<Course> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i] = await CreateAsync(entities[i]);
            }
            return entities;
        }

        public async Task<Course> DeleteAsync(Course entity)
        {
            VC.Courses.Remove(VC.Courses.Single(x => x.ID == entity.ID));
            await VC.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Course>> GetAsync(int skip, int take)
        {
            return await VC.Courses.AsNoTracking().OrderBy(x => x.ID).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await VC.Courses.AsNoTracking()
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.ID == id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await VC.Courses.CountAsync();
        }

        public async Task<Course> UpdateAsync(Course entity)
        {
            VC.Courses.Attach(entity);
            VC.Entry<Course>(entity).State = EntityState.Modified;
            await VC.SaveChangesAsync();
            return entity;
        }
    }
}
