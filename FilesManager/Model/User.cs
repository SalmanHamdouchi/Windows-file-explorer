using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManager.Model
{
    class Users
    {
        private String nom;
        private int idUser;
        private Privileges privilege;
        private String password;

        public Users(String name, int Id, String Pw)
        {
            nom = name;
            idUser = Id;
            password = Pw;

        }
        public String getName()
        {
            return nom;
        }
        public String getPassword()
        {
            return password;
        }
        public Privileges getPrivilege()
        {
            return privilege;
        }
        public void setPrivileges(Privileges pr)
        {
            privilege = pr;
        }
        public bool checkPassword(String username, String pw)
        {
            if(username == nom.ToLower() && pw == password)
            {
                return true;
            }
            return false;
        }
        public int getId()
        {
            return idUser;
        }
    }
}
