using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public ICustomerRepository Customers { get; }
        public ISaleRepository Sales { get; }
        public IDashboardRepository Dashboard { get; }
        public IAuthRepository Auths { get; }
        public IAccountRepository Accounts {  get; }
        public ICalendarEventRepository CalendarEvents { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            ISaleRepository sales,
            IProductRepository products,
            ICustomerRepository customers,
            ICategoryRepository categories,
            IDashboardRepository dashboard,
            IAccountRepository accounts,
            ICalendarEventRepository calendarEvents,
            IAuthRepository admins)
        {
            Sales = sales;
            Categories = categories;
            Products = products;
            Customers = customers;
            _context = context;
            Dashboard = dashboard;
            Accounts = accounts;
            CalendarEvents = calendarEvents;
            Auths = admins;
        }
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
