using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BDatos;
using System.Data.OleDb;

namespace BModelo
{
    public class Model
    {
        protected string table;                              //Esta propiedad debe ser redefinida obligatoriamente
        protected string tableAlias = "";                    //Esta propiedad puede ser redefiida para formar JOINS con alias
        protected List<string> fields;                       //Campos de tabla. El valor de esta propiedad se asigna automaticamente
        protected List<string> protectedFields;              //Esta propiedad puede ser redefinida, para impedir que se muestren dichos campos
        protected DataBase oDataBase;                        //Objeto de la clase 'DataBase'

        // Propiedades auxiliares
        protected string sql_statement = "";
        protected string sql_restriction = "";
        protected string sql_join = "";
        protected List<Object> values_to_link = new List<Object>();



        public Model(String table, String tableAlias)
        {
            this.table = table;
            this.tableAlias = tableAlias;

            this.oDataBase = new DataBase();
            this.fields = this.getFieldsDatabase();
        }

        public Model(String table, String tableAlias, List<String> protectedFields)
        {
            this.table = table;
            this.tableAlias = tableAlias;
            this.protectedFields = protectedFields;

            this.oDataBase = new DataBase();
            this.fields = this.getFieldsDatabase();
        }

        /**
        * Obtener el nombre de la tabla
        * 
        * @return string nombre de tabla asociada
        */
        public string getTable()
        {
            return this.table;
        }

        /**
        * Obtener los campos de la tabla
        * 
        * @return List<string> campos de tabla asociada
        */
        public List<string> getFields()
        {
            return this.fields;
        }

        /**
        * Este método puede ser usado para limpiar los campos auxiliares
        */
        private void cleanAuxiliaryProperties()
        {
            this.sql_statement = "";
            this.sql_restriction = "";
            this.sql_join = "";
            this.values_to_link = new List<Object>();
        }



        /**
         * Este método puede ser usado para ejecutar una sentencia SELECT y obtener los registros
         * 
         * @return DataSet registros obtenidos de la base de datos
         */
        public DataTable get()
        {
            if (!String.IsNullOrEmpty(this.sql_join))
            {
                this.sql_statement += $" {this.tableAlias} {this.sql_join} ";
            }
            if (!String.IsNullOrEmpty(this.sql_restriction))
            {
                this.sql_statement += $" {this.sql_restriction} ";
            }

            this.oDataBase.prepareStatement(this.sql_statement);

            // Vincular valores

            if (this.values_to_link.Count > 0)
            {
                this.oDataBase.bindValues(this.values_to_link);
            }

            DataTable oDataTable = this.oDataBase.executeWithDataReturn();

            // Cerrar Conexion
            this.oDataBase.releaseResource();

            //Limpiar campos auxiliares
            this.cleanAuxiliaryProperties();

            return oDataTable;
        }


        /**
         * Este método puede ser usado para ejecutar una sentencia SELECT
         * y obtener el primer registro que coincida con el criterio de búsqueda
         * 
         * @return List<Object> primer registro encontrado en la consulta
         */
        public List<Object> first()
        {
            List<Object> listResult = new List<Object>();
            DataTable oDataTable = this.get();
            if (oDataTable.Rows.Count != 0)
            {
                DataRow oDataRow = oDataTable.Rows[0];
                Object[] oObject = oDataRow.ItemArray;
                listResult = new List<Object>(oObject);
            }
            return listResult;
        }

        /**
         * Este método puede ser usado para ejecutar una sentencia INSERT, UPDATE, DELETE y obtener los registros afectados
         * 
         * @return int cantidad de registros afectados
         */
        public int run()
        {
            //Agregar restricciones
            if (!String.IsNullOrEmpty(this.sql_restriction))
            {
                this.sql_statement += $" {this.sql_restriction} ";
            }

            this.oDataBase.prepareStatement(this.sql_statement);

            // Vincular valores
            if (this.values_to_link.Count > 0)
            {
                this.oDataBase.bindValues(this.values_to_link);
            }

            int rowsAffected = this.oDataBase.executeWithoutDataReturn();

            // Cerrar Conexion
            this.oDataBase.releaseResource();

            //Limpiar campos auxiliares
            this.cleanAuxiliaryProperties();

            return rowsAffected;
        }

        /**
        * Este método puede ser usado para construir una consulta SQL con la cláusula SELECT
        * 
        * Para obtener los registros ejecutar el método 'get()'
        * Para agregar restricciones de consulta ejecutar el metodo WHERE u otros según se requiera
        * NOTA:    Cuando este método es usado en conbinación con el método JOIN, se debe especificar
        *          los campos a selecionar con el alias de la tabla. ['ALIAS.FIELD_1', 'ALIAS.FIELD_2']
        * 
        * @param array Campos a mostrar, sino se especifica se obtendran todos los campos (a excepción de los protegidos)
        * @param boolean Este parametro indica si se mostraran los campos protegidos
        * @return Model
        */
        public Model select(string[] fieldsToShow = null, Boolean showProtectedFields = false)
        {
            //Filtrar campos protegidos
            string fieldsStringToShow = this.getSqlFieldsString(fieldsToShow, showProtectedFields);

            this.sql_statement = $"SELECT {fieldsStringToShow} FROM {this.table} ";
            return this;
        }

        /**
         * Este método puede ser usado para construir una consulta SQL con la cláusula INSERT
         * Para ejecutar la consulta ejecutar el método 'run()'
         * 
         * @param Dictionary Datos a guardar (campo : valor)
         * @return Model
         */
        public Model insert(Dictionary<String, Object> data)
        {
            this.sql_statement = $"INSERT INTO {this.table} ";

            string fields = "";
            string linkedFields = "";
            foreach (KeyValuePair<String, Object> kvp in data)
            {
                fields += $", {kvp.Key}";

                linkedFields += ", ?";
                this.values_to_link.Add(kvp.Value);
            }

            //Eliminar la coma inicial
            fields = fields.Substring(1);
            linkedFields = linkedFields.Substring(1);

            this.sql_statement += $" ({fields}) VALUES ({linkedFields})";

            return this;
        }


        /**
         * Este método puede ser usado para construir una consulta SQL con la cláusula UPDATE
         * Puede restringir la consulta UPDATE con el método 'where'
         * Para ejecutar la consulta ejecutar el método 'run()'
         * 
         * @param Dictionary datos a actualizar (campo : valor)
         * @return Model
         */
        public Model update(Dictionary<String, Object> data)
        {
            this.sql_statement = $"UPDATE {this.table} SET ";

            //Agregando campos
            string fields = "";
            foreach (KeyValuePair<String, Object> kvp in data)
            {
                fields += $", {kvp.Key} = ?";
                this.values_to_link.Add(kvp.Value);
            }

            //Eliminar la primera coma
            fields = fields.Substring(1);

            this.sql_statement += $" {fields} ";

            return this;
        }

        /**
         * Este método puede ser usado para construir una consulta SQL con la cláusula DELETE
         * Puede restringir la consulta DELETE con el método 'where'
         * Para ejecutar la consulta ejecutar el método 'run()'
         * 
         * @return Model
         */
        public Model delete()
        {
            this.sql_statement = $"DELETE FROM {this.table} ";

            return this;
        }


        /**
         * Este método puede ser usado para unir tablas con la cláusula INNER JOIN
         * 
         * @param string nombre de tabla a unir más una alias de tabla. ejem. =>  "usuario usu"
         * @param string nombre de campo de union de la primera tabla con alias de tabla. ejem. => "usu.personaId"
         * @param string operador de union. '='
         * @param string nombre de campo de union de la segunda tabla con alias de tabal. ejem. = > "per.personaId"
         * @return Model
         */
        public Model join(string table, string unionField_1, string operador, string unionField_2)
        {
            try
            {
                int index = this.sql_statement.IndexOf("FROM");
                if (index != -1)
                {
                    this.sql_statement = this.sql_statement.Insert(index + 4 , " ( ");
                }
                else
                {
                    throw new Exception("Deve primero agregar la clausula select");
                }

                this.sql_join += $" INNER JOIN {table} ON {unionField_1} {operador} {unionField_2} )";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            return this;
        }



        // ----------------------- CONDICIONES DE SELECCIÓN-----------------------


        /**
         * Este método puede ser usado para añadir una condición con la cláusula WHERE a la consulta
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre del campo
         * @param string operador condicional. =, >, >=, <, <=, <>
         * @param Object (string o numeric). Valor a evaluar contra la columna
         * @return Model
         */
        public Model where(string field, string operador, Object value)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field} {operador} ? ";
            this.values_to_link.Add(value);
            return this;
        }


        /**
         * Este método puede ser usado para añadir una condición con la cláusula WHERE a la consulta
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre del campo
         * @param string operador condicional. =, >, >=, <, <=, <>
         * @param Object (string o numeric). Valor a evaluar contra la columna
         * @return Model
         */
        public Model orWhere(string field, string operador, Object value)
        {
            this.sql_restriction += $" OR {field} {operador} ? ";
            this.values_to_link.Add(value);

            return this;
        }


        /**
         * Este método verifica que un valor de columna esté en un intervalo de valores
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre del campo
         * @param Object[] arreglo con dos valores. [value_1, value_2]
         * @return Model
         * 
         */
        public Model whereBetween(string field, Object[] values)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field} {this.between(values)} ";
            return this;
        }


        /**
         * Este método verifica que un valor de columna esté en un intervalo de valores
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre del campo
         * @param Object[] arreglo con dos valores. [value_1, value_2]
         * @return Model
         */
        public Model orWhereBetween(string field, Object[] values)
        {
            this.sql_restriction += $" OR {field} {this.between(values)} ";
            return this;
        }


        /**
         * Este método verifica que un valor de columna no esté en un intervalo de valores
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre del campo
         * @param Object[] arreglo con dos valores. [value_1, value_2]
         * @return Model
         */
        public Model whereNotBetween(string field, Object[] values)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field} NOT {this.between(values)} ";
            return this;
        }

        /**
         * Este método verifica que un valor de columna no esté en un intervalo de valores
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre del campo
         * @param Object[] arreglo con dos valores. [value_1, value_2]
         * @return Model
         */
        public Model orWhereNotBetween(String field, Object[] values)
        {
            this.sql_restriction += $" OR {field} NOT {this.between(values)} ";
            return this;
        }

        /**
         * Este método construye la declaración BETWEEN con los nombre de campos preparados
         * 
         * @param string nombre del campo
         * @param Object[] arreglo con dos valores. [value_1, value_2]
         * @return string condicional between. BETWEEN ? AND ?
         */
        private string between(Object[] values)
        {
            string sql = "";

            sql += " BETWEEN ? ";
            this.values_to_link.Add(values[0]);

            sql += " AND ? ";
            this.values_to_link.Add(values[1]);

            return sql;
        }

        /**
         * Este método verifica que un valor de columna dada esté contenido dentro del arreglo dado
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre de campo
         * @param Object[] arreglo de valores.
         * @return Model
         */
        public Model whereIn(string field, Object[] values)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field} {this.getIn(values)} ";
            return this;
        }


        /**
         * Este método verifica que un valor de columna dada no esté contenido dentro del arreglo dado
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre de campo
         * @param Object[] arreglo de valores.
         * @return Model
         */
        public Model whereNotIn(string field, Object[] values)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field} NOT {this.getIn(values)} ";
            return this;
        }

        /**
         * Este método verifica que un valor de columna dada esté contenido dentro del arreglo dado
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre de campo
         * @param Object[] arreglo de valores.
         * @return Model
         */
        public Model orWhereIn(string field, Object[] values)
        {
            this.sql_restriction += $" OR {field} {this.getIn(values)} ";
            return this;
        }

        /**
         * Este método verifica que un valor de columna dada no esté contenido dentro del arreglo dado
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre de campo
         * @param Object[] arreglo de valores.
         * @return Model
         */
        public Model orWhereNotIn(string field, Object[] values)
        {
            this.sql_restriction += $" OR {field} NOT {this.getIn(values)} ";
            return this;
        }

        /**
         * Este método construye la declaración IN con los nombre de campos preparados
         * 
         * @param string nombre del campo
         * @param Object[] arreglo de valores. [value_1, value_2, value_3]
         * @return string condicional in. IN (? , ?, ?)
         */
        private string getIn(Object[] values)
        {
            string sql = "";
            foreach (Object value in values)
            {
                sql += ", ? ";
                this.values_to_link.Add(value);
            }
            sql = sql.Substring(1);
            sql = $" IN ({sql}) ";

            return sql;
        }

        /**
         * Este método verifica que el valor de una columna dada sea igual a NULL
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre de campo
         * @return Model
         */
        public Model whereNull(string field)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field} IS NULL ";
            return this;
        }

        /**
         * Este método verifica que el valor de una columna dada no sea igual a NULL
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre de campo
         * @return Model
         */
        public Model whereNotNull(string field)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field} IS NOT NULL ";
            return this;
        }

        /**
         * Este método verifica que el valor de una columna dada sea igual a NULL
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre de campo
         * @return Model
         */
        public Model orWhereNull(string field)
        {
            this.sql_restriction += $" OR {field} IS NULL ";
            return this;
        }

        /**
         * Este método verifica que el valor de una columna dada no sea igual a NULL
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre de campo
         * @return Model
         */
        public Model orWhereNotNull(string field)
        {
            this.sql_restriction += $" OR {field} IS NOT NULL ";
            return this;
        }



        //---------------------- PROPIAS DE ACCESS -----------------------------

        /**
         * Este método puede ser usado para comparar el valor de una columna contra una fecha
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre de campo
         * @param string operador condicional. =, >, >=, <, <=, <>
         * @param string Fecha a comparar. formato "dd-mm-yyyy"
         * @return Model
         */
        public Model whereDate(string field, string operador, string date)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            DateTime oDataTime = Convert.ToDateTime(date);

            this.sql_restriction += $" {union} {field} {operador} ? ";
            this.values_to_link.Add(oDataTime);
            return this;
        }

        /**
         * Este método puede ser usado para comparar el valor de una columna contra un año especifico
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre de campo
         * @param int año a comparar. 2020
         * @return Model
         */
        public Model whereYear(string field, int year)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} YEAR({field}) = ? ";
            this.values_to_link.Add(year);
            return this;
        }


        //------------------- FIN FUNCIONES PROPIAS DE MYSQL---------------------


        /**
         * Este método puede ser usado para comparar el valor de dos columnas
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre del primer campo
         * @param string operador condicional. =, >, >=, <, <=, <>
         * @param string nombre del segundo campo
         * @return Model
         */
        public Model whereColumn(string field_1, string operador, string field_2)
        {
            string union = String.IsNullOrEmpty(this.sql_restriction) ? "WHERE" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {field_1} {operador} {field_2} ";
            return this;
        }


        /**
         * Este método puede ser usado para comparar el valor de dos columnas
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre del primer campo
         * @param string operador condicional. =, >, >=, <, <=, <>
         * @param string nombre del segundo campo
         * @return Model
         */
        public Model orWhereColumn(string field_1, string operador, string field_2)
        {
            this.sql_restriction += $" OR {field_1} {operador} {field_2} ";
            return this;
        }

        /**
         * Este método puede ser usado para iniciar un grupo de restricciones
         * con la cláusula AND
         * 
         * NOTA: Recuerda finalizar un grupo de restricciones con el método 'endWhereGroup()'
         * 
         * @return Model
         */
        public Model groupRestrictions()
        {
            this.sql_restriction = $" {this.sql_restriction} AND (";
            return this;
        }

        /**
         * Este método puede ser usado para iniciar un grupo de restricciones
         * con la cláusula OR
         * 
         * NOTA: Recuerda finalizar un grupo de restricciones con el método 'endWhereGroup()'
         * 
         * @return Model
         */
        public Model orGroupRestrictions()
        {
            this.sql_restriction = $" {this.sql_restriction} OR (";
            return this;
        }


        /**
         * Este método puede ser usado para finalizar un grupo de restricciones
         * 
         * @return Model
         */
        public Model endGroupRestrictions()
        {
            this.sql_restriction = $" {this.sql_restriction}) ";
            return this;
        }


        /**
         * Este método puede ser usado para verificar si una condición se concatenará
         * al inicio de un grupo de condiciones. AND (field_1 = value_1 ...
         * 
         * @return bolean
         */
        private bool checkStartRestrictionsGroup()
        {
            bool verified = false;
            string sql = this.sql_restriction.Trim();
            if (!String.IsNullOrEmpty(sql))
            {
                string finalCharacter = sql.Substring(sql.Length - 1, 1);
                if (finalCharacter.Equals("("))
                {
                    verified = true;
                }
            }

            return verified;
        }


        /**
         * Este método puede ser usado para agrupar los resultados de consulta.
         * 
         * @param String[] lista de nombres de campos
         * @return Model
         */
        public Model groupBy(String[] listFields)
        {
            string sqlFields = "";
            foreach (String field in listFields)
            {
                sqlFields += $", {field} ";
            }
            //Eliminar la primera coma
            sqlFields = sqlFields.Substring(1);

            sqlFields = $" GROUP BY {sqlFields} ";

            this.sql_restriction += sqlFields;
            return this;
        }


        /**
         * Este método puede ser usado para agregar una condición de grupo con
         * la cláusula HAVING a la consulta
         * 
         * Si ya existe la cláusula WHERE en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre del campo de la función de grupo. AVG(salary)
         * @param string operador de comparación. =, >, >=, <, <=, <>
         * @param Object (string o numeric). Valor a evaluar contra la columna
         * @return Model
         */
        public Model having(string groupFunction, string operador, Object value)
        {
            string union = !this.sql_restriction.Contains("HAVING") ? "HAVING" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {groupFunction} {operador} ? ";
            this.values_to_link.Add(value);

            return this;
        }


        /**
         * Este método puede ser usado para agregar una condición de grupo con
         * la cláusula HAVING a la consulta
         * 
         * Se concatenará a la sentencia SQL con la cláusula OR
         * @param string nombre del campo de la función de grupo. AVG(salary)
         * @param string operador de comparación. =, >, >=, <, <=, <>
         * @param Object (string o numeric). Valor a evaluar contra la columna
         * @return Model
         */
        public Model orHaving(string groupFunction, string operador, Object value)
        {
            this.sql_restriction += $" OR {groupFunction} {operador} ? ";
            this.values_to_link.Add(value);
            return this;
        }

        /**
         * Este método puede ser usado para comparar el valor de dos columnas de funciones de grupo
         * 
         * Si ya existe la cláusula HAVING en la consulta SQL se concatenará con la cláusula AND
         * @param string nombre del primer campo de función de grupo
         * @param string operador condicional. =, >, >=, <, <=, <>
         * @param string nombre del segundo campo de función de grupo
         * @return Model
         */
        public Model havingColumn(string groupFunction_1, string operador, string groupFunction_2)
        {
            string union = !this.sql_restriction.Contains("HAVING") ? "HAVING" : "AND";
            //si una condición va al inicio de un grupo de condiciones no se agrega la clausula de unión
            union = this.checkStartRestrictionsGroup() ? "" : union;

            this.sql_restriction += $" {union} {groupFunction_1} {operador} {groupFunction_2} ";
            return this;
        }


        /**
         * Este método puede ser usado para comparar el valor de dos columnas de funciones de grupo
         * 
         * Si ya existe la cláusula HAVING en la consulta SQL se concatenará con la cláusula OR
         * @param string nombre del primer campo de función de grupo
         * @param string operador condicional. =, >, >=, <, <=, <>
         * @param string nombre del segundo campo de función de grupo
         * @return Model
         */
        public Model orHavingColumn(string groupFunction_1, string operador, string groupFunction_2)
        {
            this.sql_restriction += $" OR {groupFunction_1} {operador} {groupFunction_2} ";
            return this;
        }


        /**
         * Este método puede ser usado para ordenar los resultados de la consulta 
         * por una columna dada
         * 
         * @param string nombre de campo
         * @param string dirección de ordenamiento. ASC o DESC
         * @return Model
         */
        public Model orderBy(String field, string direction = "ASC")
        {
            this.sql_restriction += $" ORDER BY {field} {direction} ";
            return this;
        }


        /**
         * Este método puede ser usado para
         * limitar la cantidad de registros a selecionar.
         * 
         * @param string desplazamiento
         * @param string cantidad de registros a seleccionar
         * @return Model
         */
        public Model limit(int amount)
        {

            try
            {
                int index = this.sql_statement.IndexOf("SELECT");
                if (index != -1)
                {
                    this.sql_statement = this.sql_statement.Insert(index + 6, $" TOP {amount} ");
                }
                else
                {
                    throw new Exception("Deve primero agregar la clausula select");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return this;
        }

        // --------------------- FIN CONDICIONES DE SELECCIÓN-----------------------



        //--------------------- FUNCIONES DE AYUDA --------------------------------

        /**
         * Filtrar campos protegidos
         * 
         * @param String[] arreglo de campos ['campo_1', 'campo_2', 'campo_3'], sino se asigna se obtendran todos los campos (excepto los protegidos)
         * @return List<String> lista de campos filtrados 
         */
        private List<String> filterFieldstoShow(String[] fields)
        {
            fields = (fields == null || fields.Length == 0) ? this.fields.ToArray() : fields;

            List<String> filteredFields;
            if ((this.protectedFields != null && this.protectedFields.Count > 0))
            {

                filteredFields = new List<string>();

                foreach (String field in fields)
                {
                    String fieldAux = field.Trim();
                    bool allowedField = true;
                    foreach (String protectedField in this.protectedFields)
                    {
                        String protectedFieldAux = protectedField.Trim();
                        if (field.Equals(protectedField))
                        {
                            allowedField = false;
                            break;
                        }
                    }

                    if (allowedField)
                    {
                        filteredFields.Add(field);
                    }
                }
            }
            else
            {
                filteredFields = new List<String>(fields);
            }

            return filteredFields;
        }

        /**
         * Obtener cadena de campos para una sentencia 'SELECT'
         * 
         * @param boolean TRUE, si los campos protegidos se mostraran de lo contrario FALSE
         * @param String[] arreglo de campos ['campo_1', 'campo_2', 'campo_3'], sino se asigna se obtendran todos los campos (excepto los protegidos)
         * @return string cadena de campos "campo_1, campo_2, campo_3"
         */
        private string getSqlFieldsString(String[] fields, Boolean showProtectedFields)
        {
            string sqlFields = "";
            fields = (fields == null || fields.Length == 0) ? this.fields.ToArray() : fields;

            if (showProtectedFields)
            {
                fields = this.filterFieldstoShow(fields).ToArray();
            }

            foreach (String field in fields)
            {
                sqlFields += $", {field}";
            }

            sqlFields = sqlFields.Substring(1);
            sqlFields = $" {sqlFields} ";

            return sqlFields;
        }


        /**
         * Obtener todos los campos de una tabla
         * 
         * Sino se pasa la tabla, se obtendra los campos de la tabla del modelo instanciado
         * 
         * @param string tabla de la cual se obtendran sus campos
         * @return List<String> lista de campos
         */
        public List<String> getFieldsDatabase(String table = "")
        {
            table = String.IsNullOrEmpty(table) ? this.table : table;
            List<String> fieldsDatabase = new List<String>();
            string sql = $"SELECT * FROM {table}";
            System.Data.OleDb.OleDbDataReader oOleDbDataReader = this.oDataBase.executeSql(sql);

            try
            {
                DataTable dtSchema = new DataTable("Schema");

                dtSchema = oOleDbDataReader.GetSchemaTable();
                foreach (DataRow schemarow in dtSchema.Rows)
                {
                    fieldsDatabase.Add(schemarow.ItemArray[0].ToString());
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return fieldsDatabase;
        }

        //--------------------- FIN FUNCIONES DE AYUDA ----------------------------


        public OleDbDataReader executeSql(String sql)
        {
            return oDataBase.executeSql(sql);
        }
    }

}
