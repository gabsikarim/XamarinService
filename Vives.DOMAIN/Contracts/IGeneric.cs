using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vives.DOMAIN.Contracts
{
    public interface IGeneric<T>
    {
        Task<T> GetByIdAsync(int id);
        //Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAsync(int skip, int take);
        Task<int> GetTotalCountAsync();
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateRangeAsync(List<T> entities);
        //Task<IEnumerable<T>> CreateBulkAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        //Task<IEnumerable<T>> UpdateBulkAsync(List<T> entities);
        Task<T> DeleteAsync(T entity);
        //Task<IEnumerable<T>> DeleteBulkAsync(List<T> entities);
    }
}
