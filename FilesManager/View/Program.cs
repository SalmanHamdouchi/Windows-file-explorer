using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FilesManager.Model;
using FilesManager.Controller;
using FilesManager.View;

namespace FilesManager.View
{
    class ConsoleApp
    {
        private static List<Users> registredUsers;
        private static int idPrivilege = 2;
        private static Users currentUser;
        private static Folder currentFolder;
        private static List<Folder> usersFolders; 
        private static String path;
        private static int loginStatus = 0;
        private static int PrivilegesSet = 0;

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

        public static void setUsersPrivileges()
         {
             foreach(Users user in registredUsers)
             {
                /*
                 if (user.getName().ToLower() == "admin")
                 {
                     continue;
                 }
                 int r, w, e;
                 Console.WriteLine("Set Privileges For " + user.getName());

                 Console.Write("Read = ");
                 r = int.Parse(Console.ReadLine());
                 Console.Write("Write = ");
                 w = int.Parse(Console.ReadLine());
                 Console.Write("Execute = ");
                 e = int.Parse(Console.ReadLine());

                 Privileges.privileges.Add(new Privileges(idPrivilege, Convert.ToBoolean(r), Convert.ToBoolean(e), Convert.ToBoolean(w)));
               
                 user.setPrivileges(idPrivilege);
                 PrivilegesController.insertPrivileges(idPrivilege, r, w, e);
                 UserController.addPrivilegeToUser(user.getName(), idPrivilege);
                 idPrivilege++;
                 */
            }
         }

        public static bool checkUser(String Username, String Password)
        {
            foreach(Users user in registredUsers)
            {
                if (user.checkPassword(Username,Password))
                {
                    System.Console.WriteLine(user.getName() + " has been logged in\n");
                    currentUser = user;
                    return true;
                }
            }
            return false;
        }

        public static void Login()
        {
            loginStatus = 0;
            String Username;
            String Password;
            while (loginStatus == 0)
            {
                System.Console.Write("Enter Username : ");
                Username = System.Console.ReadLine();
                System.Console.Write("Enter Password : ");
                Password = System.Console.ReadLine();
            
                if(!checkUser(Username, Password))
                {
                    System.Console.WriteLine("Username and/or Password is incorrect");
                }
                else loginStatus = 1;
            }
        }

        public static void startSession()
        {
            Login();
           // setUsersPrivileges();

            if (currentUser.getName().ToLower() == "admin")
            {
                currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("admin"));
                PrivilegesSet = 1;
            }
            else if (currentUser.getName().ToLower() == "salman")
            {
                currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("salman"));
            }
            else if (currentUser.getName().ToLower() == "youssef")
            {
                currentFolder = usersFolders.Find(x => x.getFolderName().ToLower().Contains("youssef"));
            }
            path = currentFolder.getFolderName() + @"\";
            addSubFolders(currentFolder);
            
        }

        public static void addSubFolders(Folder f)
        {
            f.setSubFolders(FolderController.getFolderByParentId(f.getId()));
            foreach (Folder folder in f.getSubFolders())
            {
                addSubFolders(folder);
            }

        }

        public static void addSubFiles(Folder f)
        {
            f.setSubFiles(FilesController.getFilesById(currentFolder.getId()));
        }

        public static void shareFolder(Folder folderToBeShared, Folder parentFolder)
        {
            folderToBeShared.setShared(true);

            String pFolder = parentFolder.getFolderName();
            foreach (Folder folder in usersFolders)
            {
                if(folder.getFolderName().ToLower() == "admin")
                {
                    continue;
                }
                if (parentFolder.getFolderName().ToLower() == "admin")
                {
                    folderToBeShared.setParentFolder(folder);
                    folder.addFolder(folderToBeShared);
                }
                else
                {
                    addSubFolders(folder);
                    folderToBeShared.setParentFolder(folder.getAnySubFolder(pFolder));
                }
                Users currentFolderUser = registredUsers.Find(x => x.getName().ToLower() == folder.getFolderName().ToLower());
                FolderController.insertFolder(folderToBeShared, currentFolderUser);

            }
        }

        public static void shareFile(String name, String ext)
        {
            foreach (Folder folder in usersFolders)
            {
                if (folder.getFolderName().ToLower() == "admin")
                {
                    continue;
                }
                folder.addFile(name, ext);
            }
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

        public static void Command_Line()
        {
            Boolean exit = false;

            while (!exit)
            {
                addSubFolders(currentFolder);
                addSubFiles(currentFolder);

                System.Console.Write(path);
                string command = System.Console.ReadLine();
                string[] cmd = command.Split(' ');

                switch (cmd[0])
                {
                    
                    case "mkdir":
                        Folder tempFolder = new Folder(cmd[1],currentFolder);
                        tempFolder.setShared(true);
                        currentFolder.addFolder(tempFolder);

                        if (currentUser.getName().ToLower() == "admin")
                        {
                            System.Console.WriteLine("Would you like this folder to be accessible by other users Y/N");
                            if (System.Console.ReadLine().ToLower() == "y")
                            {
                                shareFolder(currentFolder.getSubFolderByName(cmd[1]), currentFolder);
                                tempFolder.setParentFolder(currentFolder);
                                FolderController.insertFolder(tempFolder, currentUser);
                                break;

                            }
                        }
                        FolderController.insertFolder(tempFolder, currentUser);

                        break;

                    case "cd":
                        if (cmd[1] == "..")
                        {
                            if(currentFolder.getFolderName() == currentUser.getName())
                            {
                                break;
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
                            if (currentFolder.FolderExists(cmd[1]))
                            {
                                Folder temp = currentFolder;
                                path = path + cmd[1] + @"\";
                                currentFolder = currentFolder.getSubFolders().ElementAt(currentFolder.getFolderIndex(cmd[1]));
                                currentFolder.setParentFolder(temp);
                            }
                            else
                            {
                                System.Console.WriteLine("Folder Not Found");
                            }
                        }
                        break;

                    case "ls":
                        try
                        {
                            List<Folder> SubFolders = currentFolder.getSubFolders();
                            foreach (Folder subfolder in SubFolders)
                            {
                                System.Console.WriteLine(subfolder.getFolderName());
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
                        break;

                    case "mkfile":
                        try
                        {
                            String[] fileName = cmd[1].Split('.');
                            Files temp = new Files(fileName[0], fileName[1]);
                            FilesController.insertFile(temp,currentFolder);

                            currentFolder.setSubFiles(FilesController.getFilesById(currentFolder.getId()));
                            if (currentUser.getName().ToLower() == "admin")
                            {
                                System.Console.WriteLine("Would you like this file to be seen by other users Y/N");
                                if (System.Console.ReadLine().ToLower() == "y") shareFile(fileName[0], fileName[1]);
                            }
                        }
                        catch(Exception e)
                        {
                            System.Console.WriteLine("Unknown File Format " + e);
                        }
                        
                        break;

                    case "rmdir":
                        Folder temp2 = currentFolder.getSubFolders().ElementAt(currentFolder.getFolderIndex(cmd[1]));
                        if (currentFolder.FolderExists(cmd[1]))
                        {
                            if(temp2.getShared() == true)
                            {
                                if(currentUser.getPrivilege().getExecutePrivilege() == true)
                                {
                                    foreach (Folder folder in usersFolders)
                                    {
                                        try
                                        {
                                            Folder temp3 = folder.getAnySubFolder(cmd[1]);
                                            deleteSubFolders(temp3);
                                        }
                                        catch (Exception)
                                        {
                                            throw;
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine("You dont have the rights to delete this folder");
                                }
                            }
                            else
                            {
                                deleteSubFolders(currentFolder.getSubFolderByName(cmd[1]));
                                
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Folder Not Found");
                        }
                        break;

                    case "rmfile":
                        if (currentFolder.FileExists(cmd[1]))
                        {
                            FilesController.deleteFileById(currentFolder.getSubFile(cmd[1]).getId());
                            //currentFolder.delFile(currentFolder.getSubFileIndex(cmd[1]));
                        }
                        else
                        {
                            System.Console.WriteLine("File Not Found");
                        }
                        break;

                    case "chuser":
                        startSession();
                        break;

                    case "exit":
                        exit = true;
                        break;
                    
                    default:
                        System.Console.WriteLine("Unknown Command");
                        break;
                }
            }
        }

        /*static void Main(string[] args)
        {
            setUsers();
            startSession();
            Command_Line();
        }*/
    }
}
