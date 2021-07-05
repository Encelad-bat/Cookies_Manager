using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SQLite;
using System.IO;

namespace Cookies_Service
{
    abstract class Browser_Cookies
    {
        abstract public string Path { get; }


        public List<T> Load<T>(string path) {
            using(SQLiteConnection conn = new SQLiteConnection("Data Source=" + path + "\\Cookies"))
            {
                try
                {
                    var result = conn.Query<T>("SELECT * FROM cookies;").ToList();
                    return result;
                }
                catch(Exception ex)
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION_SQLITE.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
                    return null;
                }
            }
        }
    }
}
