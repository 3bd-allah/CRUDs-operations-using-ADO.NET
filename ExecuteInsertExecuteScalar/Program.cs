using ConnectionString;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExecuteInsertExecuteScalar
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
                
            

            var sql = "INSERT INTO WALLETS (Holder, Balance) VALUES(@holder, @balance);" +
                        "SELECT CAST(scope_identity() AS int) ";

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
            


            SqlCommand query = new SqlCommand(sql, con);
            query.CommandType = CommandType.Text;

            // adding parameters to the 
            query.Parameters.Add(holderParameter);
            query.Parameters.Add(balanceParameter);

            con.Open();

            walletToInsert.Id =(int) query.ExecuteScalar();

            Console.WriteLine($"wallet {walletToInsert} added successfully");

            con.Close();
            
        
        }
    }
}