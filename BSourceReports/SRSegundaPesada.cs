using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using BModelo;
namespace BSourceReports
{
    public class SRSegundaPesada
    {
        public int ticket { get; set; }
        public List<BinSegundaPesada> listBinSegundaPesada { get; private set; }


        public SRSegundaPesada(int cod)
        {
            this.ticket = cod;
            this.listBinSegundaPesada = new List<BinSegundaPesada>();
        }

        public void getSegundapesada()
        {
            Venta oVenta = new Venta();
            String[] columnas = { "VEN.ID_TKT", "VEN.PLACA", "VEN.FEC_SALIDA"
                                    , "PREC.MONTO"
                                    , "VEN.PESO_ENTRADA"
                                    , "VEN.PESO_SALIDA"};

            DataTable oDataTable = oVenta.select(columnas)
                                        .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                        .where("VEN.ID_TKT", "=", this.ticket).get();

            BinSegundaPesada oBinSegundaPesada;

            foreach (DataRow row in oDataTable.Rows)
            {
                oBinSegundaPesada = new BinSegundaPesada();
                oBinSegundaPesada.ticketExt = (string)row["ID_TKT"].ToString();
                oBinSegundaPesada.ticketExt = oBinSegundaPesada.ticketExt.PadLeft(8, '0');
                oBinSegundaPesada.placa = (string)row["PLACA"];

                oBinSegundaPesada.fechaSal = (DateTime)row["FEC_SALIDA"];

                oBinSegundaPesada.importe = (int)row["MONTO"];
                oBinSegundaPesada.pesoEnt = (int)row["PESO_ENTRADA"];
                oBinSegundaPesada.pesoSal = (int)row["PESO_SALIDA"];


                this.listBinSegundaPesada.Add(oBinSegundaPesada);
            }
        }

    }
}
