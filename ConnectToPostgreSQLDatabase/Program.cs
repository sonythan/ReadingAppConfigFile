using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ConnectToPostgreSQLDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ReadDBConnectionString());
 

            var DBConnection = new NpgsqlConnection(ReadDBConnectionString());
            DBConnection.Open();

            var StringSQLCommand = "select version()";

            var SQLCommand = new NpgsqlCommand(StringSQLCommand, DBConnection);
            var PostgreSQLVersion = SQLCommand.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {PostgreSQLVersion}");
            StringSQLCommand = @"SELECT * FROM public.""Item"";";
            SQLCommand.CommandText = StringSQLCommand;
            SQLCommand.Connection = DBConnection;
            Npgsql.NpgsqlDataReader ItemList = SQLCommand.ExecuteReader();

            while (ItemList.Read())
            {
                Console.WriteLine("{0} , {1} ", ItemList.GetString(0), ItemList.GetString(1));
            }
            Console.ReadKey();
            DBConnection.Close();

            string ReadDBConnectionString()
            {
                string appSettings = ConfigurationManager.ConnectionStrings["Database1"].ToString();
                return appSettings;
             }
        }
    }
}
