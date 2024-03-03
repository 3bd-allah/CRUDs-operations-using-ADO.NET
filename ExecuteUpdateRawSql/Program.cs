using ConnectionString;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ExecuteUpdateRawSql
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            SqlConnection con = new SqlConnection(config.GetSection("constr").Value);





            var sql = "UPDATE Wallets SET Holder = @holder , Balance = @balance " +
                "Where Id= @id";


            SqlParameter idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Value = 1,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
            };

            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@holder",
                Value = "Body",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
            };

            SqlParameter balanceParameter = new SqlParameter
            {
                ParameterName = "@balance",
                Value = 9000,
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
            };



            SqlCommand query = new SqlCommand(sql, con) ;
            query.CommandType = CommandType.Text;

            // adding parameters to the 
            query.Parameters.Add(idParameter);
            query.Parameters.Add(holderParameter);
            query.Parameters.Add(balanceParameter);

            con.Open();

            if (query.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"wallet updated Successfully ");
            }
            

            con.Close();
        }


    }
}
