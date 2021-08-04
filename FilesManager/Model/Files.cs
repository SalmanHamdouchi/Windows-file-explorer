using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManager.Model
{
    class Files
    {
        private int idFile;
        private String nom;
        private String Extension;
        private int idFolder;

        public Files(String name, String ext)
        {
            nom = name;
            Extension = ext;
        }        
        public void setId(int id)
        {
            idFile = id;
        }
        public void setFolderId(int id)
        {
            idFolder = id;
        }
        public String getName()
        {
            return nom;
        }
        public int getFolderId()
        {
            return idFolder;
        }
        public String getExtension()
        {
            return Extension;
        }
        public int getId()
        {
            return idFile;
        }
        public void setName(String name)
        {
            nom = name;
        }
    }
}
