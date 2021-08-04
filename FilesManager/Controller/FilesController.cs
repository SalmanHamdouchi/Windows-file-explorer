using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FilesManager.Model;

namespace FilesManager.Controller
{
    class FilesController
    {
        static MySqlConnection con = DBConnection.getConnection();
     
        public static List<Files> getFiles()
        {
            List<Files> Files = new List<Files>();
            try
            {
                MySqlCommand com = con.CreateCommand();
                com.CommandText = "Select * from files";
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Files file = new Files(reader["Nom"].ToString(), reader["Extension"].ToString());
                    file.setId(int.Parse(reader["IdFile"].ToString()));
                    Files.Add(file);
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Files;
        }
        public static void insertFile(Files file, Folder folder)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();
            com.CommandText = "Insert Into files (`Nom`, `Extension`,`IdFolder`) values (" + "'" + file.getName() + "'" + "," + "'" + file.getExtension() + "'" + "," + folder.getId() + ");";
            com.ExecuteNonQuery();
            con.Close();
        }
        public static List<Files> getFilesById(int id)
        {
            List<Files> files = new List<Files>();

            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "select * from files where idFolder = " + id;
            MySqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                Files file = new Files(reader["Nom"].ToString(), reader["Extension"].ToString());
                file.setId(int.Parse(reader["IdFile"].ToString()));
                files.Add(file);
            }
            return files;
        }
        public static void deleteFileById(int id )
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();
            com.CommandText = "delete from files where idFile = " + id;
            com.ExecuteNonQuery();
            con.Close();
        }
        public static void deleteFilesByParentId(int id)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();
            com.CommandText = "delete from files where idFolder = " + id;
            com.ExecuteNonQuery();
            con.Close();
        }
        public static void cutFile(int idFile, int idFolder)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();
            com.CommandText = "update  files set idFolder = " + idFolder + " where idFile = " + idFile;
            com.ExecuteNonQuery();
            con.Close();
        }
        public static void copyFile(Files tobeCopied, Folder newFolder)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();

            Files temp = new Files(tobeCopied.getName(), tobeCopied.getExtension());
            insertFile(temp, newFolder);
            con.Close();
        }
        public static void renameFile(Files toBeRenamed)
        {
            con = DBConnection.getConnection();
            MySqlCommand com = con.CreateCommand();
            com.CommandText = "update files set Nom = " + "'" + toBeRenamed.getName()+ "'" + " where idFile = " + toBeRenamed.getId();
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
