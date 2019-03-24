using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static System.Console;

namespace AutoLotDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine($"Fun With Data Readers");
            //Предположим, что значение connectionString на самом деле получено из файла *.config
            string connectionString = @"Data Source = (localDb)\mssqllocaldb; Initial Catalog = AutoLot; Connect Timeout = 30; Integrated Security = true";
            //Создать строку с помощью объекта построителя
            var cnStringBuilder = new SqlConnectionStringBuilder(connectionString)
            {
                //InitialCatalog = "AutoLot",
                //DataSource = @"(localDb)\mssqllocaldb",
                //ConnectTimeout = 30,
                //IntegratedSecurity = true
            };
            //Создать и открыть подключение
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = cnStringBuilder.ConnectionString;
                connection.Open();
                ShowConnectionProperty(connection);
                //Создать объект команды SQL
                string sqlCommand = "Select * From Inventory";
                SqlCommand myCommand = new SqlCommand(sqlCommand, connection);
                //Получить объект чтения данных с помощью ExecuteReader
                using (SqlDataReader dbReader = myCommand.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        for (int i = 0; i < dbReader.FieldCount; i++)
                        {
                            WriteLine($"{dbReader.GetName(i)} = {dbReader.GetValue(i)}");
                        }
                        WriteLine();
                    }
                }
            }
            ReadLine();
        }

        static void ShowConnectionProperty(SqlConnection connection)
        {
            WriteLine("Info About connection");
            WriteLine($"Database location: {connection.DataSource}");
            WriteLine($"Database name: {connection.Database}");
            WriteLine($"Timeout: {connection.ConnectionTimeout}");
            WriteLine($"Connection state: {connection.State}");
        }
    }
}

