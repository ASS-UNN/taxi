using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace TestingSample.Base
{
    class DBLoader
    {
        private static DBLoader instance = null;
        public static DBLoader GetInstance()
        {
            if (instance == null)
                return instance = new DBLoader();
            else
                return instance;
        }

        private static string DB_NAME = "../../Base/db.sqlite";
        private SQLiteConnection connection;        

        private DBLoader()
        {
            connection = new SQLiteConnection("Data Source=" + DB_NAME + ";Version=3;");
            connection.Open();
        }

        ~DBLoader()
        {
            connection.Close();
        }

        public void ExecuteUpdate(String query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public int ExecuteInsert(String query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            query = "select seq from sqlite_sequence where name='orders'";
            SQLiteDataReader reader = ExecuteSelect(query);
            int key = Convert.ToInt32(reader["seq"]);
            return key;
        }

        public SQLiteDataReader ExecuteSelect(String query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return reader;
            else throw new Exception("Database returned an empty row.");
        }

    }
}
