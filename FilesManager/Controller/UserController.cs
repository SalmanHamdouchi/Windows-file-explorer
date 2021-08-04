using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using FilesManager.Model;

namespace FilesManager.Controller
{
    class UserController
    {
        static MySqlConnection con = DBConnection.getConnection();

        public static List<Users> getUsers()
        {
            con = DBConnection.getConnection();
            List<Users> users = new List<Users>();
            List<Privileges> pr = PrivilegesController.getPrivileges();
            try
            {
                MySqlCommand com = con.CreateCommand();
                com.CommandText = "Select * from users";
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Users user = new Users(reader["Nom"].ToString(),int.Parse(reader["IdUser"].ToString()),reader["Password"].ToString());
                    foreach(Privileges privilege in pr)
                    {
                        if (int.Parse(reader["IdPrivilege"].ToString()) == privilege.getId())
                        {
                            user.setPrivileges(privilege);
                        }
                    }
                    users.Add(user);
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return users;
        }
        public static void addPrivilegeToUser(String Username, int IdPrivilege)
        {
            con = DBConnection.getConnection();
            try
            {
                MySqlCommand com = con.CreateCommand();
                com.CommandText = "Update users set IdPrivilege = " + IdPrivilege + " where users.Nom = " + "'" + Username + "'" + ";";
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           

        }
    }
}
