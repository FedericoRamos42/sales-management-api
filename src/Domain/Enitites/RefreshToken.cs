using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enitites
{
    public class RefreshToken
    {
        public int Id {  get; set; }
        public string Token { get; set; } = string.Empty;
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = default!;
        public DateTime ExpiresOnUtc { get; set; }
        
    }
}
