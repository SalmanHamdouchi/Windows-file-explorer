using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FilesManager.Model;
using FilesManager.View;

namespace FilesManager.Controller
{
    class FolderController
    {
        static MySqlConnection con;

        public static List<Folder> getFolders()
        {
            List<Folder> Folders = new List<Folder>();
            con = DBConnection.getConnection();
            try
            {
                MySqlCommand com = con.CreateCommand();
                com.CommandText = "Select * from folders";
                MySqlDataReader reader = com.ExecuteReader();

                while (reader.Read())   
                {
                    Folder folder = new Folder(reader["Nom"].ToString());
                    folder.setId(int.Parse(reader["IdFolder"].ToString()));
                    Folders.Add(folder);
                }
                con.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Folders;
        }

        public static void insertFolder(Folder folder, Users user)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "insert into folders (Nom, ParentFolder, IdUser, Shared) values(?Nom,?ParentFolder, ?idUser, ?Shared);";

            com.Parameters.Add("?Nom", folder.getFolderName());
            com.Parameters.Add("?ParentFolder", folder.getParentFolder().getId());
            com.Parameters.Add("?idUser", user.getId());
            com.Parameters.Add("?Shared", folder.getShared());
            com.ExecuteNonQuery();

            List<Folder> subFolders = folder.getSubFolders();
            foreach (Folder f in subFolders)
            {
                f.setParentFolder(getFolderById(folder.getFolderName(),folder.getParentFolder()));
                insertFolder(f,user);
            }

        }
        public static Folder getFolderById(String name, Folder parentFolder)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "select * from folders where Nom = " + "'" + name + "'" + "and ParentFolder = " + parentFolder.getId();
            MySqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                Folder folder = new Folder(reader["Nom"].ToString());
                folder.setId(int.Parse(reader["IdFolder"].ToString()));
                String shared = reader["Shared"].ToString();
                folder.setShared(Convert.ToBoolean(int.Parse(shared)));
                return folder;
            }
            return null;
        }
        public static void deleteFolderById(int id)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "delete from folders where IdFolder = " + id;

            com.ExecuteNonQuery();
            con.Close();
        }

        public static List<Folder> getFolderByParentId(int id)
        {
            List<Folder> Folders = new List<Folder>();

            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();
            com.CommandText = "select * from folders where ParentFolder = " + id;
            MySqlDataReader reader = com.ExecuteReader();
            Folder parentFolder = GUI.rootFolder.getSubFolders().Find(x => x.getId() == id);

            while (reader.Read())
            {
                Folder folder = new Folder(reader["Nom"].ToString());
                folder.setId(int.Parse(reader["IdFolder"].ToString()));
                String shared = reader["Shared"].ToString();
                folder.setShared(Convert.ToBoolean(int.Parse(shared)));
                
                Folders.Add(folder);
            }
            con.Close();
            return Folders;
        }

        public static void setShared(Folder folder, Folder parentFolder)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "update folders set Shared = 1 where Nom = " + "'" + folder.getFolderName() + "'" + " and ParentFolder = " + parentFolder.getId() + "; ";
            com.ExecuteNonQuery();
            con.Close();
        }

        public static void renameFolder(Folder f, String name)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "update folders set Nom = " + "'" + name + "'" + "where IdFolder = " + f.getId() + ";";
            com.ExecuteNonQuery();
            con.Close();
        }

        public static void cutFolder(Folder current, Folder parent)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "update folders set ParentFolder = " + parent.getId() + " where IdFolder = " + current.getId() + ";";
            com.ExecuteNonQuery();
            con.Close();
        }
        public static void copyFolder(Folder tobeCopied, Folder parentFolder, Users user)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "insert into folders (Nom, ParentFolder, IdUser, Shared) values(?Nom,?ParentFolder, ?idUser, ?Shared);";

            com.Parameters.Add("?Nom", tobeCopied.getFolderName());
            com.Parameters.Add("?ParentFolder", parentFolder.getId());
            com.Parameters.Add("?idUser", user.getId());
            com.Parameters.Add("?Shared", tobeCopied.getShared());
            com.ExecuteNonQuery();
        }
       
    }
}