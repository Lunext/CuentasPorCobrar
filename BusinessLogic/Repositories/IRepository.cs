using CuentasPorCobrar.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public interface IRepository <T>
    {
        Task<T?> CreateAsync(T entity);
        Task<IEnumerable<T>> RetrieveAllAsync();
        Task<T?> RetrieveAsync(int id);
        Task<T?> UpdateAsync(int id, T entity);

        Task<bool?> DeleteAsync(int id);



     
    }
}
