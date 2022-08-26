using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using BModelo;
namespace BSourceReports
{
    public class SRPrimerPesada
    {
        public int ticket { get; set; }
        //public String tipoImp { get; private set; }

        public List<BinPrimerPesada> listBinPrimerPesada { get; private set; }

        public  SRPrimerPesada(int cod )
        {
            this.ticket = cod;
            this.listBinPrimerPesada = new List<BinPrimerPesada>();
        }

        public void getPrimerPesada()
        {
            Venta oVenta = new Venta();
            String[] columnas = { "VEN.ID_TKT", "VEN.PLACA", "VEN.FEC_ENTRADA"
                                    , "PREC.MONTO"
                                    , "VEN.PESO_ENTRADA"
                                    , "VEN.OBSERVACION"};

            DataTable oDataTable = oVenta.select(columnas)
                                        .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                        .where("VEN.ID_TKT", "=", this.ticket).get();

            BinPrimerPesada oBinPrimerPesada;

            foreach (DataRow row in oDataTable.Rows)
            {
                oBinPrimerPesada = new BinPrimerPesada();
                //oBinPrimerPesada.ticket = (int)row["ID_TKT"];
                oBinPrimerPesada.ticketExt = (string)row["ID_TKT"].ToString();
                oBinPrimerPesada.ticketExt = oBinPrimerPesada.ticketExt.PadLeft(8,'0');
                oBinPrimerPesada.placa = (string)row["PLACA"];

                oBinPrimerPesada.fechaEnt = (DateTime)row["FEC_ENTRADA"];

                oBinPrimerPesada.importe = (int)row["MONTO"];
                oBinPrimerPesada.pesoE = (int)row["PESO_ENTRADA"];
                oBinPrimerPesada.observacion = (string)row["OBSERVACION"];


                this.listBinPrimerPesada.Add(oBinPrimerPesada);
            }
        }


    }

}
