using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BModelo;

namespace BSourceReport
{
    class SRResumenDiario
    {
        public DateTime fechaInicio { get; private set; }
        public DateTime fechaFin { get; private set; }
        public String usuario { get; private set; }
        public List<BinResumenDiario> listBinResumenDiario { get; private set; }

        public SRResumenDiario(DateTime fechaInicio, DateTime fechaFin, String usuario)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.usuario = usuario;
        }

        public void getReportResumenDiario()
        {
            String[] columns = { "VEN.FEC_ENTRADA", "CLI.NOMBRE", "VEN.FEC_SALIDA", "VEN.PLACA"
                            , "VEN.PESO_ENTRADA", "VEN.PESO_SALIDA", "PREC.MONTO" };
            Venta oVenta = new Venta();
            DataTable oDataTable = oVenta.select(columns)
                                    .join("CLIENTE CLI", "VEN.ID_CLIENTE", "=", "CLI.ID_CLI")
                                    .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                    .where("VEN.FEC_ENTRADA", ">=", this.fechaInicio)
                                    .where("VEN.FEC_ENTRADA", "<=", this.fechaFin)
                                    .get();

            BinResumenDiario oBinResumenDiario; 

            foreach (DataRow row in oDataTable.Rows)
            {
                oBinResumenDiario = new BinResumenDiario();
                oBinResumenDiario.fechaEntrada = (DateTime)row["FEC_ENTRADA"];
                oBinResumenDiario.nombres = (String)row["FEC_ENTRADA"];
                oBinResumenDiario.fechaSalida = (DateTime)row["FEC_SALIDA"];
                oBinResumenDiario.placa = (String)row["PLACA"];
                oBinResumenDiario.pesoEntrada = (int)row["PESO_ENTRADA"];
                oBinResumenDiario.pesoSalida = (int)row["PESO_SALIDA"];
                oBinResumenDiario.monto = (int)row["MONTO"];

                this.listBinResumenDiario.Add(oBinResumenDiario);
            }
        }
    }
}
