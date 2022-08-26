using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace BDatos
{
    class Connection
    {
        private OleDbConnection oOleDbConnection = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = |DataDirectory|\\bdMorrup.accdb");

        public OleDbConnection getConnection()
        {

            if (oOleDbConnection.State == ConnectionState.Closed)
            {
                try
                {
                    oOleDbConnection.Open();
                }
                catch (OleDbException e)
                {
                    throw new Exception("Error: No se pudo conectar a la base de datos." + e.Message);
                }

            }

            return this.oOleDbConnection;
        }

        public OleDbConnection closeConnection()
        {
            if (oOleDbConnection.State == ConnectionState.Open)
            {
                oOleDbConnection.Close();
            }

            return this.oOleDbConnection;
        }
    }
}
