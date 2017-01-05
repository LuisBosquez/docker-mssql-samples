using System;
using System.Text;
using System.Data.SqlClient;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "db";   // update me
            builder.UserID = "sa";              // update me
            builder.Password = "Luis9000";      // update me
            builder.InitialCatalog = "master";

            bool connected = false;
            while (!connected)
            {
                try
                {
                    // Connect to SQL
                    Console.Write("Connecting to SQL Server ... ");
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Done.");
                        connected = true;
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            
            Console.WriteLine("Successfully connected to SQL Server in Docker.");
        }
    }
}