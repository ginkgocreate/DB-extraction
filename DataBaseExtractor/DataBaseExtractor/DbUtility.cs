using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DataBaseExtractor
{
    internal class ConstantsInfo
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
    internal class DbUtility
    {
        public string GetUmlString() {
            var connectionString = ConstantsInfo.ConnectionString;
            var schemaName = ConfigurationManager.AppSettings["SchemaName"];

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                    SELECT 
                        table_name, 
                        column_name, 
                        data_type 
                    FROM 
                        user_tab_columns  
                    ORDER BY 
                        table_name, column_id";

                using (OracleCommand command = new OracleCommand(query, connection))
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    StringBuilder plantUml = new StringBuilder("@startuml\n");

                    string? currentTable = string.Empty;
                    while (reader.Read())
                    {
                        string? tableName = reader["TABLE_NAME"].ToString();
                        string? columnName = reader["COLUMN_NAME"].ToString();
                        string? dataType = reader["DATA_TYPE"].ToString();

                        if (currentTable != tableName)
                        {
                            if (!string.IsNullOrEmpty(currentTable))
                                plantUml.AppendLine("}\n");

                            currentTable = tableName;
                            plantUml.AppendLine($"entity \"{tableName}\" as {tableName} {{");
                        }

                        plantUml.AppendLine($"  {columnName} : {dataType}");
                    }
                    plantUml.AppendLine("}\n@enduml");

                    return plantUml.ToString();
                }
            }
        }
    }
}
