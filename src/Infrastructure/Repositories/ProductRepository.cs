using Domain.Enitites;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllWithCategory()
        {
            var products = await _context.Products.Include(x => x.Category).ToListAsync();
            return products;
        }
        public async Task<Product> GetWithCategory(int Id)
        {
            var product = await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(p=>p.Id == Id);
            return product;
        }
    }
}
