using ConnectionString;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExecuteRawSql
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ShowWallets(configuration);

        }

        public static void ShowWallets(IConfigurationRoot configuration)
        {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = configuration.GetSection("constr").Value;

                var sqlQuery = "Select * from wallets";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.CommandType = CommandType.Text;
                //command.Connection = con;

                con.Open();

                SqlDataReader data = command.ExecuteReader();

                while (data.Read())
                {
                    Wallet wallet = new Wallet
                    {
                        Id = (int)data["id"],
                        Holder = data["holder"].ToString(),
                        Balance = data.GetDecimal("balance")
                    };
                    Console.WriteLine(wallet);
                }

                con.Close();
        }
    }
}