using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace BDatos
{
    public class DataBase
    {
        private Connection oConnection = new Connection();
        private OleDbCommand oOleDbCommand = null;
        private OleDbDataReader oOleDbDataReader = null;

        public OleDbDataReader executeSql(string sql)
        {
            OleDbConnection oOleDbConnection = oConnection.getConnection();

            this.oOleDbCommand = new OleDbCommand(sql, oOleDbConnection);

            try
            {
                this.oOleDbDataReader = this.oOleDbCommand.ExecuteReader();
            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Error: No se pudo ejecutar la operación." + e.Message);
            }

            return this.oOleDbDataReader;
        }

        public DataBase prepareStatement(string sql)
        {

            OleDbConnection oOleDbConnection = oConnection.getConnection();

            oOleDbCommand = new OleDbCommand();
            oOleDbCommand.Connection = oOleDbConnection;
            oOleDbCommand.CommandText = sql;

            return this;
        }

        public void bindValues(List<Object> data)
        {
            foreach (Object item in data)
            {
                OleDbParameter oOleDbParameter = new OleDbParameter();
                oOleDbParameter.Value = item;
                this.oOleDbCommand.Parameters.Add(oOleDbParameter);
            }

        }

        public DataTable executeWithDataReturn()
        {
            DataTable oDataTable;

            try
            {
                this.oOleDbDataReader = this.oOleDbCommand.ExecuteReader();
                oDataTable = this.oleDataReaderToDataTable();
            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Error: No se pudo ejecutar la operación." + e.Message);
            }

            return oDataTable;
        }

        public int executeWithoutDataReturn()
        {
            int numberRowsAffected;

            try
            {
                numberRowsAffected = this.oOleDbCommand.ExecuteNonQuery();
            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Error: No se pudo ejecutar la operación." + e.Message);
            }

            return numberRowsAffected;
        }

        public DataTable oleDataReaderToDataTable()
        {
            DataTable dtData;

            try
            {
                dtData = new DataTable("Data");
                DataTable dtSchema = new DataTable("Schema");

                dtSchema = this.oOleDbDataReader.GetSchemaTable();
                foreach (DataRow schemarow in dtSchema.Rows)
                {
                    dtData.Columns.Add(schemarow.ItemArray[0].ToString(), System.Type.GetType(schemarow.ItemArray[5].ToString()));
                }
                while (this.oOleDbDataReader.Read())
                {
                    object[] ColArray = new object[this.oOleDbDataReader.FieldCount];
                    for (int i = 0; i < this.oOleDbDataReader.FieldCount; i++)
                    {
                        if (this.oOleDbDataReader[i] != null) ColArray[i] = this.oOleDbDataReader[i];
                    }
                    dtData.LoadDataRow(ColArray, true);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return dtData;
        }

        public void releaseResource()
        {
            this.oOleDbDataReader.Close();
            this.closeConnection();
        }

        private void closeConnection()
        {
            this.oConnection.closeConnection();
        }

    }
}
