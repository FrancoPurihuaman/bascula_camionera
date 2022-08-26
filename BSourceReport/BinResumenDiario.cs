using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSourceReport
{
    class BinResumenDiario
    {
        public DateTime fechaEntrada { get; set; }
        public String nombres { get; set; }
        public DateTime fechaSalida { get; set; }
        public String placa { get; set; }
        public int pesoEntrada { get; set; } 
        public int pesoSalida { get; set; }
        public int monto { get; set; }
    }
}
