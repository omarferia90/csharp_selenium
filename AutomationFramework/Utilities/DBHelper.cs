using AutomationFramework.Reports;
using AventStack.ExtentReports;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;


namespace AutomationLibrary
{
    public enum DatabaseType
    {
        Oracle,
        SqlServer
    }

    public class DBHelper : IDisposable
    {
        private DbConnection connection;
        private readonly DatabaseType dbType;

        public DBHelper(DatabaseType dbType, string connectionString)
        {
            this.dbType = dbType;
            connection = CreateConnection(dbType, connectionString);
        }

        private DbConnection CreateConnection(DatabaseType dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseType.Oracle:
                    return new OracleConnection(connectionString);
                case DatabaseType.SqlServer:
                    return new SqlConnection(connectionString);
                default:
                    throw new ArgumentException("Unsupported database type.");
            }
        }

        public void Open()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void Close()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public DataTable ExecuteQuery(string query)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                var dt = new DataTable();
                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
                return dt;
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                return command.ExecuteScalar();
            }
        }

        public void Dispose()
        {
            Close();
            connection.Dispose();
        }

        public static string BuildOracleConnectionString(string host, string port, string service, string user, string password)
        {
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={service})));User Id={user};Password={password};";
        }

        public static string BuildSqlServerConnectionString(string server, string database, string user, string password)
        {
            return $"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;";
        }


    }
}
