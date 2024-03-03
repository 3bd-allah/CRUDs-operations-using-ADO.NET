using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExecuteDeleteRawSql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            SqlConnection con = new SqlConnection(config.GetSection("constr").Value);


            var sql = "DELETE FROM Wallets " +
                "Where Id= @id";


            SqlParameter idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Value = 1,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
            };


            SqlCommand query = new SqlCommand(sql, con);
            query.CommandType = CommandType.Text;

            // adding parameters to the 
            query.Parameters.Add(idParameter);
            

            con.Open();

            if (query.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"wallet Deleted Successfully ");
            }
            else
            {
                Console.WriteLine("Error");
            }

            con.Close();
        }
    }
}