using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vives.BLL
{
    public class VivesTask<T>
    {
        public static async Task<T> Try(Func<Task<T>> operation)
        {
            try
            {
                var result = await operation.Invoke();
                return await Task.FromResult<T>(result);
            }
            catch (Exception e)
            {
                return await Task.FromException<T>(e);
            }

        }
    }
}
