using ConnectionString;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExecuteRawSqlDataAdapter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // define connection 
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetSection("constr").Value;

            // define query only the text 
            var sqlQuery = "Select * from wallets";


            // open connection
            //con.Open();

            // retrieve data from db using (DataAdapter)
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, con);

            // filling data in DataTable (DataSet)
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            // we don't need to open and close the connection because Fill() method do this automatically 

            // close connection 
            //con.Close();


            // creating object of the data in-memory app and treate with it 
            var wallet = new Wallet();
            foreach (DataRow dr in dt.Rows)
            {
                wallet.Id = (int)dr["id"];
                wallet.Holder = dr["Holder"].ToString();
                wallet.Balance = (decimal)dr["Balance"];
                Console.WriteLine(wallet);
            }


        }
    }
}