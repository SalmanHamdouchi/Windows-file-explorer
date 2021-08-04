using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;

namespace FilesManager.Controller
{
    class DBConnection
    {
        public static MySqlConnection getConnection()
        {
            
            string myConnectionString = "server=localhost;database=filesmanager;uid=root;pwd=;charset=utf8;";
            MySqlConnection con = new MySqlConnection(myConnectionString);

            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);

            }
            return con;
        }
    }
}
