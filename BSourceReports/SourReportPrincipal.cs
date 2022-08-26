using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BModelo;

namespace BSourceReports
{
    public class SourReportPrincipal
    {
        public int ticket { get; set; }
        public string placa { get; set; }
        public DateTime fechaEnt { get; set; }
        public DateTime fechaSal { get; set; }
        public int importe { get; set; }
        public int pesoE { get; set; }
        public int pesoS { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }


        public SourReportPrincipal(int cod)
        {
            this.ticket = cod;
        }

        public void getPesoGeneral()
        {
            Venta oVenta = new Venta();
            String[] columnas = { "VEN.ID_TKT"
                                    , "VEN.PLACA"
                                    , "VEN.FEC_ENTRADA"
                                    , "VEN.FEC_SALIDA"
                                    , "PREC.MONTO"
                                    , "VEN.PESO_ENTRADA"
                                    , "VEN.PESO_SALIDA"
                                    , "VEN.OBSERVACION"
                                    , "USR.NOMBRE"};

            List<Object> oLista = oVenta.select(columnas)
                                        .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                        .join("USUARIO USR", "VEN.ID_USER", "=", "USR.ID_USER")
                                        .where("VEN.ID_TKT", "=", this.ticket)
                                        .first();


            this.ticket = (int)oLista[0];
            this.placa = (string)oLista[1];
            this.fechaEnt = (DateTime)oLista[2];

            // VALIDAR FECHA DE SALIDA PARA QUE NO SE MUESTREN LOS ERRORES
            try{
                this.fechaSal = (DateTime)oLista[3];
            }catch(Exception e)
            {
                //Fecha bandera
                DateTime oDataTimeAux = new DateTime(1500, 1, 1, 1, 1, 1);
                this.fechaSal = oDataTimeAux;
            }

            this.importe = (int)oLista[4];
            this.pesoE = (int)oLista[5];
            try
            {
                this.pesoS = (int)oLista[6];
            }
            catch (Exception e)
            {
                this.pesoS = 0;
            }
            
            this.observacion = (string)oLista[7];

            this.usuario = (string)oLista[8];

        }

       

    }
    
}
