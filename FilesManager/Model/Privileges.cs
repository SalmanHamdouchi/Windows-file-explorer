using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManager.Model
{
    class Privileges
    {
        private int idPrivilege;
        private bool read;
        private bool execute;
        private bool write;

        public Privileges()
        {

        }
        public Privileges(int id,bool re, bool ex, bool wr)
        {
            idPrivilege = id;
            read = re;
            execute = ex;
            write = wr;
        }
        public void readPrivilege()
        {
            read = true;
        }
        public void executePrivilege()
        {
            execute = true;
        }
        public void writePrivilege()
        {
            write = true;
        }
        public void delPrivilege(String name)
        {
            if(name == "read")
            {
                read = false;
            }
            else if(name == "execute")
            {
                execute = false;
            }
            else if(name == "write")
            {
                write = false;
            }
        }
        public String getPrivileges()
        {
           return "Privileges" + "\n" + "Read = " + read + "\n" + "Write = " + write + "\n" + "Execute = " + execute;
        }
        public bool getExecutePrivilege()
        {
            return execute;
        }
        public int getId()
        {
            return idPrivilege;
        }
    }
}
