using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using FilesManager.Model;

namespace FilesManager.Controller
{
    class PrivilegesController
    {

        public static List<Privileges> getPrivileges()
        {
            List<Privileges> privileges = new List<Privileges>();
            MySqlConnection con = DBConnection.getConnection();
            try
            {
                MySqlCommand com = con.CreateCommand();
                com.CommandText = "Select * from privileges";
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Privileges privilege = new Privileges(int.Parse(reader["IdPrivilege"].ToString()),
                    Convert.ToBoolean(int.Parse(reader["Read"].ToString())), Convert.ToBoolean(int.Parse(reader["Execute"].ToString())),
                    Convert.ToBoolean(int.Parse(reader["Write"].ToString())));

                    privileges.Add(privilege);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            con.Close();
            return privileges;

        }
        public static void insertPrivileges(int id, int read, int write, int execute)
        {
            MySqlConnection con = DBConnection.getConnection();
            try
            {
                MySqlCommand com = con.CreateCommand();
                com.CommandText = "Insert Into privileges (`IdPrivilege`, `Read`, `Execute`, `Write`) values (?id, ?read, ?execute, ?write);";
                com.Parameters.Add("?id", id);
                com.Parameters.Add("?read", read);
                com.Parameters.Add("?execute", execute);
                com.Parameters.Add("?write", write);

                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("inserting privilege " + e);
            }
            con.Close();

        }
    }
}
