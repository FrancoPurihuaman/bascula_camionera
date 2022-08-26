using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BModelo;
using System.Data;

namespace BSourceReports
{
    public class SoursTicketDirectory
    {
        public int ticket { get; set; }
        public string placa { get; set; }
        public DateTime fechaEnt { get; set; }
        public DateTime fechaSal { get; set; }
        public int importe { get; set; }
        public int pesoE { get; set; }
        public int pesoS { get; set; }
        public string observacion { get; set; }

        public SoursTicketDirectory(int cod)
        {
            this.ticket = cod;
        }

        public void getPrimerPesada()
        {
            Venta oVenta = new Venta();
            String[] columnas = { "VEN.ID_TKT"
                                    , "VEN.PLACA"
                                    , "VEN.FEC_ENTRADA"
                                    , "PREC.MONTO"
                                    , "VEN.PESO_ENTRADA"
                                    , "VEN.OBSERVACION"};

            List<Object> oLista = oVenta.select(columnas)
                                        .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                        .where("VEN.ID_TKT", "=", this.ticket)
                                        .first();

            
            this.ticket = (int)oLista[0];
            this.placa = (string)oLista[1];
            this.fechaEnt = (DateTime)oLista[2];
            this.importe = (int)oLista[3];
            this.pesoE = (int)oLista[4];
            this.observacion = (string)oLista[5];
            
        }

        public void getSegundaPesada()
        {
            Venta oVenta = new Venta();
            String[] columnas = { "VEN.ID_TKT"
                                    , "VEN.PLACA"
                                    , "VEN.FEC_SALIDA"
                                    , "PREC.MONTO"
                                    , "VEN.PESO_ENTRADA"
                                    , "VEN.PESO_SALIDA"};

            List<Object> oLista = oVenta.select(columnas)
                                        .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                        .where("VEN.ID_TKT", "=", this.ticket)
                                        .first();

            //string auxticket = (string)oLista[0];
            this.ticket = (int)oLista[0];
            this.placa = (string)oLista[1];
            this.fechaSal = (DateTime)oLista[2];
            this.importe = (int)oLista[3];
            this.pesoE = (int)oLista[4];
            this.pesoS = (int)oLista[5];

        }
    }
}
