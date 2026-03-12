using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<int>Count(); 
        Task<List<T>> Search(Expression<Func<T,bool>>? filter = null, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetByPagination<TKey>(Expression<Func<T, TKey>> orderBy, int pageIndex, int pageSize);



    }
}
