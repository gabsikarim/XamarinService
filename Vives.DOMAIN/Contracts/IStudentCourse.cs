using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vives.DOMAIN.Contracts
{
    public interface IStudentCourse : IGeneric<StudentCourse>
    {
        Task<StudentCourse> GetByIdAsync(int studentid, int courseid);
    }
}
