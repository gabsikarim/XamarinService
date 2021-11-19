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
    public class StudentDB : IStudent
    {
        private readonly VivesContext VC = new VivesContext();

        public async Task<Student> CreateAsync(Student entity)
        {
            VC.Students.Add(entity);
            await VC.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Student>> CreateRangeAsync(List<Student> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i] = await CreateAsync(entities[i]);
            }
            return entities;
        }

        public async Task<Student> DeleteAsync(Student entity)
        {
            VC.Students.Remove(VC.Students.Single(x => x.ID == entity.ID));
            await VC.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Student>> GetAsync(int skip, int take)
        {
            return await VC.Students.AsNoTracking().OrderBy(x => x.ID).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await VC.Students.AsNoTracking()
                .Include(x => x.Courses)
                .SingleOrDefaultAsync(x => x.ID == id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await VC.Students.CountAsync();
        }

        public async Task<Student> UpdateAsync(Student entity)
        {
            VC.Students.Attach(entity);
            VC.Entry<Student>(entity).State = EntityState.Modified;
            await VC.SaveChangesAsync();
            return entity;
        }
    }
}
