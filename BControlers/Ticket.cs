using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Drawing;
using System.Drawing.Printing;

using System.Data;

// se agrega la siguiente referencia para enviar texto a impresora.
using System.Runtime.InteropServices;

namespace BControlers
{
    class Ticket
    {
        public static System.Drawing.Font printFont;
        public static StreamReader streamToPrint;

        public class CreaTicket
        {
            public static StringBuilder line = new StringBuilder();

            string ticket = "";
            string parte1, parte2;

            public static int max = 40;
            int cort;
            private string[] cadenaserver;

            public byte[] Serverbyte { get; private set; }

            public static string LineasGuion()
            {
                string LineaGuion = "----------------------------------------";   // agrega lineas separadoras -

                return line.AppendLine(LineaGuion).ToString();
            }


            public static void EncabezadoVenta()
            {
                string LineEncavesado = "REFERENCIA      FECHA/HORA       IMPORTE";   // 40 caracteres agrega lineas de  encabezados
                line.AppendLine(LineEncavesado);
            }
            public void TextoIzquierda(string par1)                          // agrega texto a la izquierda
            {
                max = par1.Length;
                if (max > 40)                                 // **********
                {
                    cort = max - 40;
                    parte1 = par1.Remove(40, cort);        // si es mayor que 40 caracteres, lo corta
                }
                else { parte1 = par1; }                      // **********
                line.AppendLine(ticket = parte1);

            }
            public void TextoDerecha(string par1)
            {
                ticket = "";
                max = par1.Length;
                if (max > 40)                                 // **********
                {
                    cort = max - 40;
                    parte1 = par1.Remove(40, cort);           // si es mayor que 40 caracteres, lo corta
                }
                else { parte1 = par1; }                      // **********
                max = 40 - par1.Length;                     // obtiene la cantidad de espacios para llegar a 40
                for (int i = 0; i < max; i++)
                {
                    ticket += " ";                          // agrega espacios para alinear a la derecha
                }
                line.AppendLine(ticket += parte1 + "\n");                //Agrega el texto

            }
            public void AgregaDetalle(string referencia, string date, string importe)
            {
                ticket = "";
                cort = (date.Length) - 16 ;

                date = date.Remove(16, cort);

                int c1 = (referencia.Length + date.Length + importe.Length);
                max = (40 - c1) / 2;
                for (int i = 0; i < max; i++)
                {
                    ticket += " ";                          
                }
                string cadenaFinal = referencia + ticket + date + ticket + importe;
                line.AppendLine(cadenaFinal + "\n");

            }
            public void TextoCentro(string par1)
            {
                ticket = "";
                max = par1.Length;
                if (max > 40)                                 // **********
                {
                    cort = max - 40;
                    parte1 = par1.Remove(40, cort);          // si es mayor que 40 caracteres, lo corta
                }
                else { parte1 = par1; }                      // **********
                max = (int)(40 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios antes del texto a centrar
                }                                            // **********
                line.AppendLine(ticket += parte1 + "\n");

            }
            
            public void TextoExtremos(string par1, string par2)
            {
                max = par1.Length;
                if (max > 18)                                 // **********
                {
                    cort = max - 18;
                    parte1 = par1.Remove(18, cort);          // si par1 es mayor que 18 lo corta
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1;                             // agrega el primer parametro
                max = par2.Length;
                if (max > 18)                                 // **********
                {
                    cort = max - 18;
                    parte2 = par2.Remove(18, cort);          // si par2 es mayor que 18 lo corta
                }
                else { parte2 = par2; }
                max = 40 - (parte1.Length + parte2.Length);
                for (int i = 0; i < max; i++)                 // **********
                {
                    ticket += " ";                            // Agrega espacios para poner par2 al final
                }                                             // **********
                line.AppendLine(ticket += parte2 + "\n");                   // agrega el segundo parametro al final

            }
            public void AgregaTotales(string par1, double total)
            {
                max = par1.Length;
                if (max > 25)                                 // **********
                {
                    cort = max - 25;
                    parte1 = par1.Remove(25, cort);          // si es mayor que 25 lo corta
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1;
                parte2 = total.ToString() + "$";
                max = 40 - (parte1.Length + parte2.Length);
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios para poner el valor de moneda al final
                }                                            // **********
                line.AppendLine(ticket += parte2 + "\n");

            }

            // se le pasan los Aticulos  con sus detalles
            public void AgregaArticulo(string Articulo, double precio, int cant, double subtotal)
            {
                if (cant.ToString().Length <= 3 && precio.ToString("c").Length <= 10 && subtotal.ToString("c").Length <= 11) // valida que cant precio y total esten dentro de rango
                {
                    string elementos = "", espacios = "";
                    bool bandera = false;
                    int nroEspacios = 0;

                    if (Articulo.Length > 40)                                 // **********
                    {
                        //cort = max - 16;
                        //parte1 = Articulo.Remove(16, cort);          // corta a 16 la descripcion del articulo
                        nroEspacios = (3 - cant.ToString().Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elementos += espacios + cant.ToString();

                        // colocamos el precio a la derecha
                        nroEspacios = (10 - precio.ToString().Length);
                        espacios = "";

                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elementos += espacios + precio.ToString();

                        //colocar el subtotal a la dercha
                        nroEspacios = (11 - subtotal.ToString().Length);
                        espacios = "";

                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elementos += espacios + subtotal.ToString();

                        int CaracterActual = 0;// indica en que caracter se quedo
                        for (int Longtext = Articulo.Length; Longtext > 16; Longtext++)
                        {
                            if (bandera == false)
                            {
                                line.AppendLine(Articulo.Substring(CaracterActual, 16) + elementos);
                                bandera = true;
                            }
                            else
                            {
                                line.AppendLine(Articulo.Substring(CaracterActual, 16));

                            }
                            CaracterActual += 16;
                        }
                        line.AppendLine(Articulo.Substring(CaracterActual, Articulo.Length - CaracterActual));


                    }
                    else
                    {
                        for (int i = 0; i < (16 - Articulo.Length); i++)
                        {
                            espacios += " ";

                        }
                        elementos = Articulo + espacios;
                        nroEspacios = (3 - cant.ToString().Length);
                        espacios = "";
                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elementos += espacios + cant.ToString();

                        // colocamos el precio a la derecha
                        nroEspacios = (10 - precio.ToString().Length);
                        espacios = "";

                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elementos += espacios + precio.ToString();

                        //colocar el subtotal a la dercha
                        nroEspacios = (11 - subtotal.ToString().Length);
                        espacios = "";

                        for (int i = 0; i < nroEspacios; i++)
                        {
                            espacios += " ";
                        }
                        elementos += espacios + subtotal.ToString();
                        line.AppendLine(elementos);

                    }
                }
                else
                {
                    //  MessageBox.Show("Valores fuera de rango");

                }
            }
            // --------------------------------------------------------------
            
            // -----------------------------------------------------------

            public void ImprimirTiket1(string Impresora)
            {
                File.WriteAllText("Ticket.txt", line.ToString());

                line = new StringBuilder();

                try
                {
                    streamToPrint = new StreamReader("Ticket.txt");
                    try
                    {
                        printFont = new Font("Courier New", 10);
                        PrintDocument pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        
                        // pd.PrinterSettings.PrinterName = "EPSON L3110 SERIES";// Nombre de la impresora
                        // pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";// Nombre de la impresora
                        pd.PrinterSettings.PrinterName = Impresora;// Nombre de la impresora

                        // pd.DefaultPageSettings.PaperSize = new PaperSize("", 10, 800);
                        pd.DocumentName = "Imprimiendo...";

                        pd.Print();
                    }
                    finally
                    {
                        streamToPrint.Close();
                    }
                }
                catch (Exception e)
                {
                    String error = e.Message;
                    streamToPrint.Close();
                }


            }
            public void ImprimirTiket2(string Impresora)
            {
                //RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString());
            }

            public void pd_PrintPage(object sender, PrintPageEventArgs ev)
            {
                float linesPerPage = 0;
                float yPos = 0;
                int count = 0;
                //float leftMargin = ev.MarginBounds.Left;
                float leftMargin = 20;
                //float topMargin = ev.MarginBounds.Top;
                float topMargin = 50;
                string line = null;

                


                // Calculate the number of lines per page.
                linesPerPage = ev.MarginBounds.Height /
                   printFont.GetHeight(ev.Graphics);

                // Print each line of the file.
                while (count < linesPerPage &&
                   ((line = streamToPrint.ReadLine()) != null))
                {
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    count++;

                }

                // If more lines exist, print another page.
                if (line != null)
                    ev.HasMorePages = true;
                else
                    ev.HasMorePages = false;
            }

            public void CortaTicket()
            {
                line.AppendLine("\x1B" + "m"); //Caracteres de corte. Estos comando varian segun el tipo de impresora
                line.AppendLine("\x1B" + "d" + "\x00"); //Avanza 9 renglones, Tambien varian
            }

            //Para abrir el cajon
            public void AbreCajon()
            {
                //Estos tambien varian, tienen que ever el manual de la impresora para poner los correctos.
                line.AppendLine("\x1B" + "p" + "\x00" + "\x0F" + "\x96"); //Caracteres de apertura cajon 0
                                                                           //linea.AppendLine("\x1B" + "p" + "\x01" + "\x0F" + "\x96"); //Caracteres de apertura cajon 1
            }

            


        }

    }
}
