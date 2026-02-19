using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Count()
        {
            var count =  _context.Set<T>().CountAsync(); 
            return count;
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
        }

        public async Task<T?> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = _context.Set<T>();

            foreach (var include in includes)
                result = result.Include(include);

            return await result.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> Search(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = _context.Set<T>();

            if (filter != null) 
                result = result.Where(filter);

            foreach(var include in includes)
                result = result.Include(include);

            return await result.ToListAsync();


        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
