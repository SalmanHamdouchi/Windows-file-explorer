using FilesManager.Controller;
using FilesManager.Model;
using FilesMnage.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FilesMnage.View.LogIn;
namespace FilesManager.View
{
    class GUI
    {
        public static List<Users> registredUsers;
        public static int idPrivilege = 2;
        public static Users currentUser;
        public static Folder currentFolder;
        public static Folder rootFolder;
        public static List<Folder> usersFolders;
        public static String path;
        public static int loginStatus = 0;
        public static int PrivilegesSet = 0;

        public static void setFolders()
        {
            usersFolders = new List<Folder>();
            usersFolders = FolderController.getFolders();
        }

        public static void setUsers()
        {
            registredUsers = new List<Users>();
            registredUsers = UserController.getUsers();
            setFolders();
        }

        public static bool checkUser(String Username, String Password)
        {
            loginStatus = 0;
            foreach (Users user in registredUsers)
            {
                if (user.checkPassword(Username, Password))
                {
                    currentUser = user;
                    loginStatus = 1;
                    return true;
                }
            }
            if (loginStatus == 0) return false;
            else return true;
        }

        public static bool Login(String Username, String Password)
        {

                if (!checkUser(Username, Password))
                {
                    MessageBox.Show("Username and/or Password is incorrect");
                    return false;
                }
                else
                {
                    loginStatus = 1;
                    return true;
                }
        }

        public static bool startSession(String Username, String Password)
        {
            setUsers();
            if(!Login(Username, Password))
            {
                return false;
            }
            // setUsersPrivileges();

            if (currentUser.getName().ToLower() == "admin")
            {
                currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("admin"));
                rootFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("admin"));
                PrivilegesSet = 1;
            }
            else if (currentUser.getName().ToLower() == "salman")
            {
                currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("salman"));
                rootFolder = currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("salman"));
            }
            else if (currentUser.getName().ToLower() == "youssef")
            {
                currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("youssef"));
                rootFolder = currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("youssef"));
            }
            path = currentFolder.getFolderName() + @"\";

            addSubFolders(currentFolder);
            addSubFiles(currentFolder);
            return true;
        }

        public static void setPath(Label pathL)
        {
            pathL.Text = path;
        }
        public static void deleteSubFolders(Folder f)
        {
            FolderController.deleteFolderById(f.getId());
            foreach (Folder folder in f.getSubFolders())
            {
                deleteSubFolders(folder);
            }
            deleteSubFiles(f);
            currentFolder.delFolder(f);
        }

        public static void deleteSubFiles(Folder f)
        {
            FilesController.deleteFilesByParentId(f.getId());
            foreach (Files file in f.getSubFiles())
            {
                currentFolder.delFile(file.getName());
            }
        }

        public static void addSubFolders(Folder f)
        {
            f.setSubFolders(FolderController.getFolderByParentId(f.getId()));
           
            foreach (Folder folder in f.getSubFolders())
            {
                folder.setParentFolder(f);
                addSubFolders(folder);
            }
        }

        public static void copyFolder(Folder tobeCopied, Folder parentFolder)
        {
            Folder copiedFolder = tobeCopied.ShallowCopy();
            copiedFolder.setParentFolder(parentFolder);

            FolderController.insertFolder(copiedFolder, currentUser);

        }

        public static void addSubFiles(Folder f)
        {
            f.setSubFiles(FilesController.getFilesById(currentFolder.getId()));
        }

        public static void printItems(ListView list, ImageList imageList)
        {
            int i = 0;
            try
            {
                List<Folder> SubFolders = currentFolder.getSubFolders();
                foreach (Folder subfolder in SubFolders)
                {
                    imageList.Images.Add(Image.FromFile(@"F:\Study\S3\C#\FilesManagers\resources\folder.png"));

                }
                list.LargeImageList = imageList;

                for (int j = 0; j < imageList.Images.Count; j++)
                {
                    ListViewItem item = new ListViewItem("sd");
                    item.ImageIndex = j;
                    list.Items.Add(item);
                }

                List<Files> files = currentFolder.getSubFiles();
                foreach (Files file in files)
                {
                    System.Console.WriteLine(file.getName());
                }
            }
            catch
            {
                System.Console.WriteLine("No Files/Folders Found");
            }
        }
        
        public static void mkdir(String name)
        {
            Folder tempFolder = new Folder(name, currentFolder);
            tempFolder.setShared(false);
            currentFolder.addFolder(tempFolder);

            if (currentUser.getName().ToLower() == "admin")
            {
                System.Console.WriteLine("Would you like this folder to be accessible by other users Y/N");
                if (System.Console.ReadLine().ToLower() == "y")
                {
                    //shareFolder(currentFolder.getSubFolderByName(name), currentFolder);
                    tempFolder.setParentFolder(currentFolder);
                    FolderController.insertFolder(tempFolder, currentUser);

                }
            }
            FolderController.insertFolder(tempFolder, currentUser);
        }

       
       
        public static void cd(String action)
        {
            if (action == "..")
            {
                if (currentFolder.getFolderName() == currentUser.getName())
                {
                    return;
                }
                else if (currentFolder.getParentFolder().getFolderName() == currentUser.getName())
                {
                    path = currentUser.getName() + @"\";
                    currentFolder = currentFolder.getParentFolder();
                }
                else
                {
                    path = currentUser.getName() + @"\" + currentFolder.getParentFolder().getFolderName() + @"\";
                    currentFolder = currentFolder.getParentFolder();
                }
            }
            else
            {
                if (currentFolder.FolderExists(action))
                {
                    Folder temp = currentFolder;
                    path = path + action + @"\";
                    currentFolder = currentFolder.getSubFolders().ElementAt(currentFolder.getFolderIndex(action));
                    currentFolder.setParentFolder(temp);
                }
            }
        }

        public static void mkfile(String name)
        {
            try
            {
                String[] fileName = name.Split('.');
                Files temp = new Files(fileName[0], fileName[1]);
                FilesController.insertFile(temp, currentFolder);

                currentFolder.setSubFiles(FilesController.getFilesById(currentFolder.getId()));
                if (currentUser.getName().ToLower() == "admin")
                {
                    System.Console.WriteLine("Would you like this file to be seen by other users Y/N");
                    //if (System.Console.ReadLine().ToLower() == "y") shareFile(fileName[0], fileName[1]);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Unknown File Format " + e);
            }
        }

        public static void rename(String name)
        {
            currentFolder.rename(name);
            FolderController.renameFolder(currentFolder, name);
        }
        [STAThread]
        static void Main(string[] args)
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogIn());

        }
    }
}
