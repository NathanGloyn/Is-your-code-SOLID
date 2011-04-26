﻿using System.Data;

namespace BoxInformation.Interfaces
{
    public interface IDataAccess
    {
        DataSet FillDataSet(string storedProcedure, CommandType commandType, params IDbDataParameter[] parameters);
        int ExecuteNonQuery(string commandText, CommandType commandType, params  IDbDataParameter[] parameters);
        IDbDataParameter CreateParameter(string name, object value);
    }
}