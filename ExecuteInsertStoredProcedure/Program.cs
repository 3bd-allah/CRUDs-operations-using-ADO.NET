using ConnectionString;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExecuteInsertStoredProcedure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            SqlConnection con = new SqlConnection(config.GetSection("constr").Value);


            Wallet walletToInsert = new Wallet();

            Console.Write("Holder name: ");
            walletToInsert.Holder = Console.ReadLine();

            decimal result;

            do
            {
                Console.Write("Balance: $ ");
            }
            while (!decimal.TryParse(Console.ReadLine(), out result));
            walletToInsert.Balance = result;



            //var sql = "CALL AddWallet (holder, balance);";

            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@holder",
                Value = walletToInsert.Holder,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
            };

            SqlParameter balanceParameter = new SqlParameter
            {
                ParameterName = "@balance",
                Value = walletToInsert.Balance,
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
            };



            SqlCommand query = new SqlCommand("AddWallet", con);
            query.CommandType = CommandType.StoredProcedure;

            // adding parameters to the 
            query.Parameters.Add(holderParameter);
            query.Parameters.Add(balanceParameter);

            con.Open();

            if (query.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"wallet for {walletToInsert.Holder} added Successfully ");
            }
            else
            {
                Console.WriteLine("Error! : Failed to add a new Wallet");
            }

            con.Close();
        }
    }
}