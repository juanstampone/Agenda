using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class PaginadoPropiedades
    {
        public int PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int RecordsCount { get; set; }
    }
}
