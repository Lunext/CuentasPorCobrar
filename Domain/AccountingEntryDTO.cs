using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccountingEntryDTO
    {

        public int id_aux { get; set; }
        public string nombre_aux { get; set;}
        public int cuenta { get; set;}
        public string origen { get; set; }
        public decimal monto { get; set;}

    }
}
