using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace BoxInformation
{
    public class SqlDataAccess
    {
        public string ConnectionString { get; set; }

        public SqlDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }


        public DataSet FillDataSet(string storedProcedure, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecuteDataAdapterFill(storedProcedure, commandType, parameters);
        }

        public int ExecuteNonQuery(string storedProcedure, params  SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                if(parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
      
        }

        private DataSet ExecuteDataAdapterFill(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            DataSet results = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(commandText,connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                if(parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.CommandType = commandType;
                try
                {
                    connection.Open();
                    results = new DataSet();
                    adapter.Fill(results);
                }
                finally
                {
                    connection.Close();
                }
            }

            return results;
        }


    }
}