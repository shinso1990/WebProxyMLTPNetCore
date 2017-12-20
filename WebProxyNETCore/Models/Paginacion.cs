using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Models
{
    public class Paginacion<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int PageSize { get; set; }
        public int ItemsCount { get; set; }
        public int StartIndex { get; set; }

        public int PageCount { get; set; }
        public int PageNumber { get; set; }
    }
}
