using ConnectionString;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace ExecuteTransaction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            // define connection 
            using (SqlConnection connection = new SqlConnection(configuration.GetSection("constr").Value))
            {
                // open the connection
                connection.Open();


                // create transcation object
                SqlTransaction transaction = connection.BeginTransaction();

                // create command object that cary command information 
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.Transaction = transaction;

                try
                {

                    command.CommandText = "update wallets set balance = balance -1000 where id = 1004";
                    command.ExecuteNonQuery();

                    command.CommandText = "update wallets set balance = balance +1000 where id = 1003"; 
                    command.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Transaction of transfer is completed successfully");

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Transaction of transfer is failed");
                }
                finally
                {
                    connection.Close();
                }



            }




        }
    }
}