using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BModelo;
using System.Data;


namespace BControlers
{
    public class PrintTicket
    {
        String  Type, Placa, FechaE, FechaS, Observacion, Cliente, formatTicket = "";
        Decimal Importe;
        int PesoE, PesoS ;
        int Ticket;
        // string placa, string fechaE, string fechaS, int pesoE, int pesoS, int importe
        public PrintTicket(string type, int ticket)
        {
            this.Type = type;
            this.Ticket = ticket;
        }

        public void cargaDatosImprime()
        {
            if (this.Type == "E")
            { 
                Venta oVenta = new Venta();
                String[] columnas = { "VEN.ID_TKT", "VEN.PLACA", "VEN.PESO_ENTRADA", "VEN.FEC_ENTRADA", "VEN.OBSERVACION"
                                    , "PREC.MONTO"
                                    , "CLI.NOMBRE AS CLI_NOMBRE"};

                List<Object> oList = oVenta.select(columnas)
                                            .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                            .join("CLIENTE CLI", "VEN.ID_CLIENTE", "=", "CLI.ID_CLI")
                                            .where("VEN.ID_TKT", "=", this.Ticket).first();

                this.formatTicket = oList[0].ToString();
                this.Placa = oList[1].ToString();
                this.PesoE = Convert.ToInt32(oList[2]);
                this.FechaE = oList[3].ToString();
                this.Observacion = oList[4].ToString();
                this.Importe =Convert.ToDecimal(oList[5]);
                this.Cliente = oList[6].ToString();

                
                this.formatTicket = formatTicket.PadLeft(8, '0');


            }
            if (this.Type == "S")
            {
                Venta oVenta = new Venta();
                String[] columnas = { "VEN.ID_TKT", "VEN.PLACA","VEN.PESO_ENTRADA", "VEN.PESO_SALIDA", "VEN.FEC_SALIDA"
                                    , "PREC.MONTO"
                                    , "CLI.NOMBRE AS CLI_NOMBRE"};

                List<Object> oList = oVenta.select(columnas)
                                            .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                            .join("CLIENTE CLI", "VEN.ID_CLIENTE", "=", "CLI.ID_CLI")
                                            .where("VEN.ID_TKT", "=", this.Ticket).first();

                this.formatTicket = oList[0].ToString();
                this.Placa = oList[1].ToString();
                this.PesoE = Convert.ToInt32(oList[2]);
                this.PesoS = Convert.ToInt32(oList[3]);
                this.FechaS = oList[4].ToString();
                this.Importe = Convert.ToDecimal(oList[5]);
                this.Cliente = oList[6].ToString();


                this.formatTicket = formatTicket.PadLeft(8, '0');
            }

            imprimeTicket();


        }
        public void imprimeTicket()
        {
            // CREACION DEL TICKET IMPORTANTE
            Ticket.CreaTicket nTicket = new Ticket.CreaTicket();

            nTicket.TextoCentro("BALANZA ELECTRONICA"); //imprime una linea de descripcion
            nTicket.TextoIzquierda("MINERA MORRUP S.A.C.");
            nTicket.TextoIzquierda("PANAM. NORTE Km. 798");
            nTicket.TextoIzquierda("SERVICIO : 24 HORAS");
            nTicket.TextoDerecha("T / " + this.formatTicket);
            BControlers.Ticket.CreaTicket.LineasGuion(); //-------------------------

            BControlers.Ticket.CreaTicket.EncabezadoVenta();
            BControlers.Ticket.CreaTicket.LineasGuion(); //-------------------------

            //                     // Referencia           Fecha /  Hora        Importe 
            if (this.Type == "E")
            {
                nTicket.AgregaDetalle("ENTRADA", this.FechaE, String.Format("{0:N1}", this.Importe));
            }
            else if (this.Type == "S")
            {
                nTicket.AgregaDetalle("SALIDA", this.FechaS, String.Format("{0:N1}",this.Importe));
            }

            nTicket.TextoCentro("  ");
            
            // Imprime linea con Subtotal
            nTicket.TextoIzquierda("PLACA        : " + this.Placa);
            nTicket.TextoIzquierda("PESO BRUTO   : " + String.Format("{0,7:N0}",this.PesoE) + " kg.");

            if (this.Type == "S")
            {
                nTicket.TextoIzquierda("PESO TARA    : " + String.Format("{0,7:N0}", this.PesoS) + " kg."); // imprime linea con decuento total
                nTicket.TextoIzquierda("PESO NETO    : " + String.Format("{0,7:N0}",(int)(this.PesoE - this.PesoS)) + " kg."); // imprime linea con ITBis total
                
            }else if(this.Type == "E")
            {
                nTicket.TextoIzquierda("OBSERVACION  : " + this.Observacion); // imprime linea con ITBis total
                
            }
            nTicket.TextoIzquierda("CLIENTE      : " + this.Cliente);
            BControlers.Ticket.CreaTicket.LineasGuion();
            

            //nTicket.LineasTotales(); // imprime linea 
            nTicket.TextoIzquierda(" ");
            nTicket.TextoCentro("******************************************");
            nTicket.TextoCentro("CERTIFICADO DE CALIBRACION MM 3929-2017");
            nTicket.TextoCentro("QUALITY CERTIFICATE DEL PERU S.A.C");
            nTicket.TextoCentro(" DIC 2017 ");
            nTicket.TextoCentro("******************************************");
            nTicket.TextoIzquierda(" ");

            nTicket.CortaTicket();
            Printer oNamePrinter = new Printer();
            String[] columna = { "NOMBRE"};
            List<Object> oList = oNamePrinter.select(columna).where("PREDETERMINADO","=",true).first();
            string namePrinter = oList[0].ToString();
            
            nTicket.ImprimirTiket1(namePrinter);
            
        }
    }
}
