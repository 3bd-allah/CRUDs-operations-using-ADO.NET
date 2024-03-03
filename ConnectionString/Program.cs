using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ConnectionString
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();




        }
    }
}