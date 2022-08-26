using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BModelo;
using System.Data;


namespace BSourceReports
{
    public class SRLiquidacionCaja
    {
        public DateTime fechaInicio { get;  set; }
        public DateTime fechaFin { get;  set; }
        public int idUsuario { get;  set; }
        public String usuario { get;  set; }
        public List<BinLiquidacionCaja> listBinLiquidacionCaja { get;  set; }

        
        public SRLiquidacionCaja(DateTime fechaInicio, DateTime fechaFin, int idUsuario, String usuario)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.idUsuario = idUsuario;
            this.usuario = usuario;
            this.listBinLiquidacionCaja = new List<BinLiquidacionCaja>();
        }

        public void getReportLiquidacionCaja()
        {
            String[] columns = { "FORMAT(VEN.FEC_ENTRADA, 'dd-mm-yyyy') AS FECHA", "COUNT(VEN.PESO_ENTRADA) AS C_PESADA1"
                                , "COUNT(VEN.PESO_SALIDA) AS C_PESADA2", "SUM(VEN.PESO_ENTRADA) AS T_BRUTO"
                                , "SUM(VEN.PESO_SALIDA) AS T_TARA", "SUM(PRE.MONTO) AS T_MONTO" };

            String[] columsGroupBy = { "FORMAT(VEN.FEC_ENTRADA, 'dd-mm-yyyy')" };
            String auxFechaFin = this.fechaFin.ToString("dd/MM/yyyy");
            auxFechaFin = $"{auxFechaFin} 23:59:59";

            Venta oVenta = new Venta();

            DataTable oDataTable = oVenta.select(columns)
                                    .join("PRECIO PRE", "VEN.ID_PRECIO", "=", "PRE.ID_PRECIO")
                                    .where("VEN.ANULADO", "=", false)
                                    //.where("VEN.ID_USER", "=", this.idUsuario)
                                    .where("VEN.FEC_ENTRADA", ">=", this.fechaInicio)
                                    .where("VEN.FEC_ENTRADA", "<=", auxFechaFin)
                                    .groupBy(columsGroupBy)
                                    .get();

            BinLiquidacionCaja oBinLiquidacionCaja;

            foreach (DataRow row in oDataTable.Rows)
            {
                oBinLiquidacionCaja = new BinLiquidacionCaja();
                oBinLiquidacionCaja.fecha = (String)row["FECHA"].ToString();
                oBinLiquidacionCaja.cantPesada1 = (int)row["C_PESADA1"];
                oBinLiquidacionCaja.cantPesada2 = (int)row["C_PESADA2"];
                oBinLiquidacionCaja.pesoBruto = Convert.ToInt32(row["T_BRUTO"]);
                oBinLiquidacionCaja.pesoTara = Convert.ToInt32(row["T_TARA"]);
                oBinLiquidacionCaja.totalCobrado = Convert.ToInt32(row["T_MONTO"]);

                this.listBinLiquidacionCaja.Add(oBinLiquidacionCaja);
            }
        }
    }
}
