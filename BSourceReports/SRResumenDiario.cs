using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BModelo;

namespace BSourceReports
{
    public class SRResumenDiario
    {
        public DateTime fechaInicio { get; private set; }
        public DateTime fechaFin { get; private set; }
        public String usuario { get; private set; }
        public String placa { get; private set; }
        public List<BinResumenDiario> listBinResumenDiario { get; private set; }

        public SRResumenDiario(DateTime fechaInicio, DateTime fechaFin, String usuario)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.usuario = usuario;
            this.listBinResumenDiario = new List<BinResumenDiario>();
        }

        public SRResumenDiario(DateTime fechaInicio, DateTime fechaFin, String usuario, String placa)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.usuario = usuario;
            this.placa = placa;
            this.listBinResumenDiario = new List<BinResumenDiario>();
        }

        public void getReportResumenDiario()
        {
            String[] columns = { "FORMAT(VEN.FEC_ENTRADA, 'dd-mm-yyyy hh:mm AM/PM') AS FEC_ENTRADA"
                                , "CLI.NOMBRE"
                                , "FORMAT(VEN.FEC_SALIDA, 'dd-mm-yyyy hh:mm AM/PM') AS FEC_SALIDA"
                                , "VEN.PLACA"
                                ,"VEN.PESO_ENTRADA", "VEN.PESO_SALIDA", "PREC.MONTO" };

            Venta oVenta = new Venta();

            // Formatenado fechas para reporte
            String auxFechaInicio = this.fechaInicio.ToString("dd/MM/yyyy");
            String auxFechaFin = this.fechaFin.ToString("dd/MM/yyyy");
            auxFechaFin = $"{auxFechaFin} 23:59:59";

            DataTable oDataTable = oVenta.select(columns)
                                    .join("CLIENTE CLI", "VEN.ID_CLIENTE", "=", "CLI.ID_CLI")
                                    .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                    .where("VEN.ANULADO", "=", 0)
                                    .where("VEN.FEC_ENTRADA", ">=", auxFechaInicio)
                                    .where("VEN.FEC_ENTRADA", "<=", auxFechaFin)
                                    .get();

            BinResumenDiario oBinResumenDiario;

            foreach (DataRow row in oDataTable.Rows)
            {
                oBinResumenDiario = new BinResumenDiario();
                oBinResumenDiario.fechaEntrada = (String)row["FEC_ENTRADA"];
                oBinResumenDiario.nombres = (String)row["NOMBRE"];

                try
                {
                    oBinResumenDiario.fechaSalida = (String)row["FEC_SALIDA"];
                }
                catch (Exception e)
                {
                    oBinResumenDiario.fechaSalida = "";
                }
                
                oBinResumenDiario.placa = (String)row["PLACA"];
                oBinResumenDiario.pesoEntrada = (int)row["PESO_ENTRADA"];
                oBinResumenDiario.pesoSalida = (int)row["PESO_SALIDA"];
                oBinResumenDiario.monto = (int)row["MONTO"];

                this.listBinResumenDiario.Add(oBinResumenDiario);
            }
        }

        public void getReportResumenDiarioxMarca()
        {
            String[] columns = { "VEN.FEC_ENTRADA", "CLI.NOMBRE", "VEN.FEC_SALIDA", "VEN.PLACA",
                             "VEN.PESO_ENTRADA", "VEN.PESO_SALIDA", "PREC.MONTO" };

            Venta oVenta = new Venta();

            // Formatenado fechas para reporte
            String auxFechaInicio = this.fechaInicio.ToString("dd/MM/yyyy");
            String auxFechaFin = this.fechaFin.ToString("dd/MM/yyyy");
            auxFechaFin = $"{auxFechaFin} 23:59:59";

            DataTable oDataTable = oVenta.select(columns)
                                    .join("CLIENTE CLI", "VEN.ID_CLIENTE", "=", "CLI.ID_CLI")
                                    .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                    .where("VEN.ANULADO", "=", 0)
                                    .where("VEN.PLACA","=",this.placa)
                                    .where("VEN.FEC_ENTRADA", ">=", auxFechaInicio)
                                    .where("VEN.FEC_ENTRADA", "<=", auxFechaFin)
                                    .get();

            BinResumenDiario oBinResumenDiario;

            foreach (DataRow row in oDataTable.Rows)
            {
                oBinResumenDiario = new BinResumenDiario();
                oBinResumenDiario.fechaEntrada = (String)row["FEC_ENTRADA"];
                oBinResumenDiario.nombres = (String)row["NOMBRE"];

                try
                {
                    oBinResumenDiario.fechaSalida = (String)row["FEC_SALIDA"];
                }
                catch (Exception e)
                {
                    oBinResumenDiario.fechaSalida = "";
                }

                oBinResumenDiario.placa = (String)row["PLACA"];
                oBinResumenDiario.pesoEntrada = (int)row["PESO_ENTRADA"];
                oBinResumenDiario.pesoSalida = (int)row["PESO_SALIDA"];
                oBinResumenDiario.monto = (int)row["MONTO"];

                this.listBinResumenDiario.Add(oBinResumenDiario);
            }
        }

    }
}
