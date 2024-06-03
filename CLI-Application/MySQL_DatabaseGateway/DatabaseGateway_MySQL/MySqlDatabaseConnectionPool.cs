using System;
using System.Collections.Generic;
using MySqlConnector;

namespace DatabaseGateway
{
    // SINGLETON CLASS WITH PRIVATE CONSTRUCTOR
    public class MySqlDatabaseConnectionPool
    {
        private static string DATABASE_USERNAME;
        private static string DATABASE_PASSWORD;
        private static string DATABASE_NAME;
        private static string DATABASE_SERVER;
        private static string DATABASE_PORT;

        // changed connection pool up from 1 to 5 to allow tasks to complete executing.
        private static MySqlDatabaseConnectionPool Instance = new MySqlDatabaseConnectionPool(10);

        private List<MySqlConnection> AvailableConnections;
        private List<MySqlConnection> BusyConnections;

        public static MySqlDatabaseConnectionPool GetInstance()
        {
            return Instance;
        }

        private MySqlDatabaseConnectionPool(int sizeOfPool)
        {
            DATABASE_USERNAME = "cccp_user";
            DATABASE_PASSWORD = "cccp_21022839";
            DATABASE_SERVER = "localhost";
            DATABASE_PORT = "3306";
            DATABASE_NAME = "cccp_a1";

            this.AvailableConnections = new List<MySqlConnection>(sizeOfPool);
            this.BusyConnections = new List<MySqlConnection>(sizeOfPool);

            /*
            if (DATABASE_USERNAME == null || DATABASE_USERNAME.Equals(""))
            {
                LoadDatabaseCredentialsFromFile();
            }
            */ 
            for (int i = 0; i < sizeOfPool; i++)
            {
                this.AvailableConnections.Add(CreateMySqlConnection());
            }
        }

        ~MySqlDatabaseConnectionPool()
        {
            foreach (MySqlConnection conn in this.AvailableConnections)
            {
                CloseMySqlConnection(conn);
            }
            this.AvailableConnections.Clear();

            foreach (MySqlConnection conn in this.BusyConnections)
            {
                CloseMySqlConnection(conn);
            }
            this.BusyConnections.Clear();
        }

        public MySqlConnection AcquireConnection()
        {
            if (this.AvailableConnections.Count > 0)
            {
                MySqlConnection conn = this.AvailableConnections[0];
                this.AvailableConnections.RemoveAt(0);
                this.BusyConnections.Add(conn);
                return conn;
            }

            return null;
        }

        private void CloseMySqlConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                try
                {
                    conn.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("ERROR: closure of database connection failed", e);
                }
            }
        }

        private MySqlConnection CreateMySqlConnection()
        {
            string DB_CONNECTION_STRING
                = "Server=" + DATABASE_SERVER + ";Port=" + DATABASE_PORT + ";Database= " + DATABASE_NAME
                    + ";Username=" + DATABASE_USERNAME + ";Password=" + DATABASE_PASSWORD;

            MySqlConnection connection;

            try
            {
                connection = new MySqlConnection(DB_CONNECTION_STRING);
                connection.Open();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: connection to database failed", e);
            }

            return connection;
        }

  
        public void ReleaseConnection(MySqlConnection conn)
        {
            if (this.BusyConnections.Contains(conn))
            {
                this.BusyConnections.Remove(conn);
                this.AvailableConnections.Add(conn);
            }
        }
    }
}
