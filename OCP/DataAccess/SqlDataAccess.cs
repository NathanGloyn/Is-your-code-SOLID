using System.Data.SqlClient;
using System.Data;
using BoxInformation.Interfaces;

namespace BoxInformation.DataAccess
{
    public class SqlDataAccess : IDataAccess
    {
        public string ConnectionString { get; set; }

        public SqlDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DataSet FillDataSet(string storedProcedure, CommandType commandType, params IDbDataParameter[] parameters)
        {
            return ExecuteDataAdapterFill(storedProcedure, commandType, parameters);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, params  IDbDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                if(parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.CommandType = commandType;
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

        public IDbDataParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        private DataSet ExecuteDataAdapterFill(string commandText, CommandType commandType, params IDbDataParameter[] parameters)
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