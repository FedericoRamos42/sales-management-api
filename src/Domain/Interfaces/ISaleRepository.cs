using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enitites;

namespace Domain.Interfaces
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        Task<List<Sale>> GetAllSalesByPagination(int pageIndex, int pageSize);
        Task<List<Sale>> GetAll();
    }
}
