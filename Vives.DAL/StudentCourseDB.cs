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
    public class StudentCourseDB : IStudentCourse
    {
        private readonly VivesContext VC = new VivesContext();

        public async Task<StudentCourse> CreateAsync(StudentCourse entity)
        {
            VC.StudentCourses.Add(entity);
            await VC.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<StudentCourse>> CreateRangeAsync(List<StudentCourse> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i] = await CreateAsync(entities[i]);
            }
            return entities;
        }

        public async Task<StudentCourse> DeleteAsync(StudentCourse entity)
        {
            VC.StudentCourses.Remove(VC.StudentCourses.Single(x => x.StudentID == entity.StudentID && x.CourseID == entity.CourseID));
            await VC.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<StudentCourse>> GetAsync(int skip, int take)
        {
            return await VC.StudentCourses.AsNoTracking().OrderBy(x => x.CourseID).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<StudentCourse> GetByIdAsync(int studentid, int courseid)
        {
            return await VC.StudentCourses.AsNoTracking().SingleOrDefaultAsync(x => x.StudentID == studentid && x.CourseID == courseid);
        }

        public Task<StudentCourse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await VC.StudentCourses.CountAsync();
        }

        public async Task<StudentCourse> UpdateAsync(StudentCourse entity)
        {
            VC.StudentCourses.Attach(entity);
            VC.Entry<StudentCourse>(entity).State = EntityState.Modified;
            await VC.SaveChangesAsync();
            return entity;
        }
    }
}
