using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudMvc.Repository.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            Task<IEnumerable<T>> FindAllAsync();
            Task<T> FindByIdAsync(Guid id);
            Task InsertAsync(T item);
            Task UpdateAsync(T item);
            Task DeleteAsync(T item);
            Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        }
    }
}
