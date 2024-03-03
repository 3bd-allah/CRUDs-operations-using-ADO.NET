using ConnectionString;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ExecuteInsertRawSql
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // connection information
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            SqlConnection con = new SqlConnection(config.GetSection("constr").Value);

            // data to be input to the query
            Wallet walletToInsert = new Wallet();
            Console.Write("Holder Name: ");
            walletToInsert.Holder = Console.ReadLine();

            decimal result;
            
            do
            {
                Console.Write("Balance: $ " );

            } while (!decimal.TryParse(Console.ReadLine(), out result));
            walletToInsert.Balance = result;

           


            // query information (text , type ,connection)
            SqlCommand query = new SqlCommand("INSERT INTO wallets(holder, balance) VALUES(@holder, @balance)", con);
            query.CommandType  = CommandType.Text;

            // using parameters to avoid SQL injection
            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@holder",
                Value = walletToInsert.Holder,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input
            };

            SqlParameter balanceParameter = new SqlParameter
            {
                ParameterName = "@balance",
                Value = walletToInsert.Balance,
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input
            };

            query.Parameters.Add(holderParameter);
            query.Parameters.Add(balanceParameter); 

            con.Open();


            if (query.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("Successfully added new wallet");
            }
            else
            {
                Console.WriteLine("Error! : Failed to add a new Wallet");
            }

            con.Close();

            //TODO:  ShowWallets();  => function to display all wallets on console

        }
    }
}