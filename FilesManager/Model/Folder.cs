using System;
using System.Collections.Generic;

namespace FilesManager.Model
{
    class Folder
    {
        private Folder parentDirectory;
        private int IdFolder;
        private String nom;
        private List<Files> Files;
        private List<Folder> Folders;
        private bool shared;

        public Folder()
        {
            Files = new List<Files>();
            Folders = new List<Folder>();
            shared = false;

        }

        public Folder(String name)
        {
            nom = name;
            Files = new List<Files>();
            Folders = new List<Folder>();

        }

        public Folder(String name, Folder f)
        {
            nom = name;
            Files = new List<Files>();
            Folders = new List<Folder>();
            parentDirectory = f;
            shared = false;
        }

        public String getFolderName()
        {
            return nom;
        }

        public void setFoldername(String name)
        {
            nom = name;
        }

        public Folder getParentFolder()
        {
            return parentDirectory;
        }

        public void setParentFolder(Folder folder)
        {
            parentDirectory = folder;
        }

        public int getFolderIndex(String name)
        {
            int i = Folders.FindIndex(x => x.nom == name);
            return i;
        }

        public int getFileIndex(String name)
        {
            int i = Files.FindIndex(x => x.getName() == name);
            return i;
        }

        public void delFolder(Folder folder)
        {
            try
            {
                folder.getParentFolder().Folders.Remove(folder);
                foreach (Folder f in folder.Folders)
                {
                    if (f.getParentFolder().getId() == folder.getId())
                    {
                        folder.Folders.Remove(f);
                        delFolder(f);
                    }
                }
            }
            catch
            {

            }
        }

        public void delFile(String name)
        {
            int index = Files.FindIndex(x => x.getName() == name);
            Files.RemoveAt(index);
        }

        public List<Folder> getSubFolders()
        {
            return Folders;
        }

        public List<Files> getSubFiles()
        {
            return Files;
        }

        public Folder getSubFolderByName(String name)
        {
            Folder f = Folders.Find(item => item.getFolderName() == name);
            return f;

        }

        public Folder getSubFolderById(Folder f, int id)
        {
            Folder temp = null;
            foreach (Folder folder in f.Folders)
            {
                temp = folder.Folders.Find(item => item.getId() == id);
                
                if (temp == null)
                {
                    getSubFolderById(folder, id);
                    temp = f.Folders.Find(item => item.getId() == id);
                }
                else
                {
                    return temp;
                }
                
            }
            return temp;
        }

        public Files getSubFile(String name)
        {
            Files f = getSubFiles().Find(file => file.getName() == name);
            return f;
        }

        public bool FolderExists(String name)
        {
            if (Folders.Exists(x => x.nom == name))
                return true;
            return false;
        }

        public bool FileExists(String name)
        {
            if (Files.Exists(x => x.getName() == name))
                return true;
            return false;
        }

        public void addFile(String name, String Ext)
        {
            Files.Add(new Files(name, Ext));
        }

        public void setSubFiles(List<Files> f)
        {
            Files = f;
        }

        public void addFolder(String name)
        {
            Folders.Add(new Folder(name));
        }

        public void setSubFolders(List<Folder> f)
        {
            Folders = f;
        }

        public void addFolder(Folder f)
        {
            Folders.Add(f);
        }

        public void setShared(bool value)
        {
            shared = value;
        }

        public bool getShared()
        {
            return shared;
        }

        public int getId()
        {
            return IdFolder;
        }

        public void setId(int id)
        {
            IdFolder = id;
        }

        public void rename(String name)
        {
            nom = name;
        }

        public Folder getAnySubFolder(String name)
        {
            Folder f = Folders.Find(item => item.getFolderName() == name);
            if(f != null)
            {
                return f;
            }
            else
            {
                foreach(Folder folder in Folders)
                {
                    getAnySubFolder(folder.getFolderName());
                }
            }
            return null;
        }

        public void cutFolder(Folder parent)
        {
            parentDirectory = parent;
        }

        public Folder ShallowCopy()
        {
            return (Folder)this.MemberwiseClone();
        }

        public List<Folder> searchSubFolders(Folder currentFolder, String name)
        {
            List<Folder> subFolders = new List<Folder>();

            foreach (Folder subFolder in currentFolder.Folders)
            {
                if (subFolder.getFolderName().ToLower().Contains(name))
                {
                    subFolders.Add(subFolder);
                }
                searchSubFolders(subFolder, name);
            }
            return subFolders;
        }
    }
}