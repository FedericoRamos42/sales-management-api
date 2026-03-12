using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils.Pagination
{
   public record PaginationParams(
       int PageIndex = 1,
       int PageSize = 5
       );
    
}
