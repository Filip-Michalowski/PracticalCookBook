using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PracticalCookBook.Database
{
    //In the future, I should probably research user-friendly global error handling for WPF.
    public class DatabaseUpdater : IDisposable
    {
        //TODO move database file from project's folder to user's documents/settings/appdata
        //TODO test if constant opening and closing db file is bad for performance

        /*
         In documentation:
         - "Optimizing SQL" - basics
        */

        public readonly int CurrentVersion = 1;

        private SQLiteConnection conn;
        private DateTime updateStartTime;

        public DatabaseUpdater()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLiteDatabase"].ConnectionString;

            //For lack of better options, I extract the path to file from connection string.
            Regex pathRegex = new Regex("Data Source=(.*);");

            //If this doesn't WAI, something has gone HORRIBLY WRONG.
            string path = pathRegex.Match(connectionString).Groups[1].Value;

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(new FileInfo(path).Directory.FullName);
                SQLiteConnection.CreateFile(path);
            }

            conn = new SQLiteConnection(connectionString);
            conn.Open();
        }

        static public void Debug_SelfDestruct()//HACK delete this
        {
            string path = @"databases\database.db";

            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.WriteLine($"\tUsunięto: {path}");
            }
            else
            {
                Debug.WriteLine($"\tNie można znaleźć: {path}");
            }
        }

        public void Update()
        {
            updateStartTime = DateTime.Now;

            int versionAtStartup;
            bool isDbEmpty = false;

            using (SQLiteCommand checkCmd = new SQLiteCommand(conn))
            {
                checkCmd.CommandText = 
                    "SELECT name"
                    + " FROM sqlite_master"
                    + " WHERE type='table' AND name='meta'";

                isDbEmpty = checkCmd.ExecuteScalar() == null;
            }

            if (isDbEmpty)
            {
                createMetaDatabase();
                versionAtStartup = 0;
            }
            else
            {
                versionAtStartup = readDatabaseVersion();
            }

            if (versionAtStartup < CurrentVersion) 
            {
                //The following transaction creates and/or updates the database to the newest version, as needed.
                using (SQLiteTransaction tran = conn.BeginTransaction())
                {
                    switch (versionAtStartup)
                    {
                        case 0:
                            using (SQLiteCommand cmd = new SQLiteCommand(conn))
                            {
                                cmd.CommandText =
                                    "CREATE TABLE categories ("
                                    + "id INTEGER PRIMARY KEY"
                                    + ",name TEXT NOT NULL"
                                    + ",description TEXT"//probably not need at the moment
                                    + ")";

                                cmd.ExecuteNonQuery();

                                cmd.CommandText =
                                    "CREATE TABLE recipes ("
                                    + "id INTEGER PRIMARY KEY"
                                    + ",name TEXT NOT NULL"
                                    + ",preparation TEXT"
                                    + ")";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText =
                                    "CREATE TABLE tags ("
                                    + "id INTEGER PRIMARY KEY"
                                    + ",name TEXT NOT NULL"
                                    + ")";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText =
                                    "CREATE TABLE recipeTag ("
                                    + "recipeId INTEGER NOT NULL"
                                    + ",tagId INTEGER NOT NULL"
                                    + ",PRIMARY KEY(recipeId, tagId)"
                                    + ",FOREIGN KEY(recipeId) REFERENCES recipes(id)"
                                    + ",FOREIGN KEY(tagId) REFERENCES tags(id)"
                                    + ")";
                                cmd.ExecuteNonQuery();
                                
                                cmd.CommandText =
                                    "CREATE TABLE ingredients ("
                                    + "id INTEGER PRIMARY KEY"
                                    + ",recipeId INTEGER NOT NULL"
                                    + ",body TEXT"
                                    + ",ordering INTEGER"
                                    + ",FOREIGN KEY(recipeId) REFERENCES recipes(id)"
                                    + ")";
                                cmd.ExecuteNonQuery();
                                
                                /*cmd.CommandText = "CREATE INDEX ingredientsByRecipeId ON ingredients (recipedId)";
                                cmd.ExecuteNonQuery();*/
                            }
                            goto case 1;
                        case 1:
                            //for future use: update to v2
                            break;
                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText =
                            "INSERT INTO meta (version, lastUpdate) VALUES (?,?)";
                        cmd.Parameters.Add(new SQLiteParameter(System.Data.DbType.Int32, CurrentVersion));
                        cmd.Parameters.Add(new SQLiteParameter(System.Data.DbType.DateTime, updateStartTime));

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    //TODO actual database creation, version update insert
                }
            }
            else if (versionAtStartup > CurrentVersion)
            {
                throw new Exception($"Wersja bazy danych ({versionAtStartup}) jest nowsza niż najnowsza wersja obsługiwana przez aplikację ({CurrentVersion})!");
            }
        }

        private void createMetaDatabase()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = 
                    "CREATE TABLE meta ("
                    + " version INTEGER"
                    + ",lastUpdate TEXT"
                    + ")";

                cmd.ExecuteNonQuery();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText =
                    "INSERT INTO meta (version, lastUpdate)"
                    + " VALUES (0, ?)";
                cmd.Parameters.Add(new SQLiteParameter(System.Data.DbType.DateTime, updateStartTime));

                cmd.ExecuteNonQuery();
            }
        }

        private int readDatabaseVersion()
        {
            int version = 0;

            using (SQLiteCommand versionCmd = new SQLiteCommand(conn))
            {
                versionCmd.CommandText = "SELECT MAX(version) FROM meta";

                version = (int)(versionCmd.ExecuteScalar() ?? 0);
            }

            return version;
        }

        public void Dispose()
        {
            conn?.Dispose();
        }
    }
}
