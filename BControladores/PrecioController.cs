using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDatos;
using System.Data;
using System.Data.OleDb;

namespace BControladores
{
    public class PrecioController
    {
        public DataTable all()
        {
            DataBase obj = new DataBase();
            return obj.executeSql("SELECT * FROM PRECIO");
        }
    }
}
