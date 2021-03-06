﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace WindowsForms_SQLite_Authentication
{
    

    public class Authentication
    {
        public string connectionString { get; set; }//privat
        string connection;
        Stream myStream;//создать файл в одтельном потоке иначе не будет к нему доступа при первом создании

        public void GetConnection()
        {
            connection = @"Data Source = Account.db; Version=3";
            connectionString = connection;
        }

        public void CreateDataBase()// проверка на наличие файла
        {
            if (!File.Exists("Account.db"))
            {
               myStream= File.Create("Account.db");
                myStream.Close();//обязательно закрыть поток что б освободить доступ без этого при первом запуске ничего не получется
            }
            byte[] v = File.ReadAllBytes("Account.db");
            if (v.Length==0) { CreateTable(); }//так я проверяю на наличие таблицы в бд что б не писать длинный код
            else return; //CreateTable();если такой файл 

        }
        private void CreateTable()
        {
            GetConnection();
            using (SQLiteConnection connect = new SQLiteConnection(connection))
            {
                connect.Open();
                SQLiteCommand cmd = new SQLiteCommand();

                string query = @"CREATE TABLE Users (ID INTEGER PRIMARY KEY AUTOINCREMENT,Username Text(25),Password Text(25), Email Text (35))";
                cmd.CommandText = query;
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
