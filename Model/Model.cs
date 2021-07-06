using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookies_Manager.Model
{
    class Model
    {
        static public void Create_Cookie<T>(string path, List<T> items)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source =" + path + "\\Cookies"))
                {
                    foreach (T item in items)
                    {
                        if(typeof(T) == typeof(Chrome_Cookie)) 
                        {
                            conn.Query(@"INSERT INTO chrome_cookies (
                        creation_utc,
                        host_key,
                        name,
                        value,
                        path,
                        expires_utc,
                        is_secure,
                        is_httponly,
                        last_access_utc,
                        has_expires,
                        is_persistent,
                        priority,
                        encrypted_value,
                        samesite,
                        source_scheme,
                        source_port,
                        is_same_party
                    )
                    VALUES (
                        @creation_utc,
                        @host_key,
                        @name,
                        @value,
                        @path,
                        @expires_utc,
                        @is_secure,
                        @is_httponly,
                        @last_access_utc,
                        @has_expires,
                        @is_persistent,
                        @priority,
                        @encrypted_value,
                        @samesite,
                        @source_scheme,
                        @source_port,
                        @is_same_party
                    );", item);
                        } // Insert Chrome_Cookies

                        else if (typeof(T) == typeof(Opera_Cookie)) 
                        {
                            conn.Query(@"INSERT INTO opera_cookies (
                        creation_utc,
                        host_key,
                        name,
                        value,
                        path,
                        expires_utc,
                        is_secure,
                        is_httponly,
                        last_access_utc,
                        has_expires,
                        is_persistent,
                        priority,
                        encrypted_value,
                        samesite,
                        source_scheme,
                        source_port,
                        is_same_party
                    )
                    VALUES (
                        @creation_utc,
                        @host_key,
                        @name,
                        @value,
                        @path,
                        @expires_utc,
                        @is_secure,
                        @is_httponly,
                        @last_access_utc,
                        @has_expires,
                        @is_persistent,
                        @priority,
                        @encrypted_value,
                        @samesite,
                        @source_scheme,
                        @source_port,
                        @is_same_party
                    );", item);
                        } // Insert Opera_Cookies

                        if (typeof(T) == typeof(Edge_Cookie)) 
                        {
                            conn.Query(@"INSERT INTO edge_cookies (
                        creation_utc,
                        host_key,
                        name,
                        value,
                        path,
                        expires_utc,
                        is_secure,
                        is_httponly,
                        last_access_utc,
                        has_expires,
                        is_persistent,
                        priority,
                        encrypted_value,
                        samesite,
                        source_scheme,
                        source_port,
                        is_same_party,
                        is_edgelegacycookie,
                        browser_provenance
                    )
                    VALUES (
                        @creation_utc,
                        @host_key,
                        @name,
                        @value,
                        @path,
                        @expires_utc,
                        @is_secure,
                        @is_httponly,
                        @last_access_utc,
                        @has_expires,
                        @is_persistent,
                        @priority,
                        @encrypted_value,
                        @samesite,
                        @source_scheme,
                        @source_port,
                        @is_same_party,
                        @is_edgelegacycookie,
                        @browser_provenance
                    );", item);
                        } // Insert Edge_Cookies
                    }
                }
            }
            catch(Exception ex)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION_SQLITE.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
            } 
        }

        static public List<T> Read_Cookies<T>(string path)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + path + "\\Cookies"))
            {
                try
                {
                    if(typeof(T) == typeof(Chrome_Cookie) && path.Contains("Cookies_Service"))
                    {
                        var result = conn.Query<T>("SELECT * FROM chrome_cookies;").ToList();
                        return result;
                    }
                    else if (typeof(T) == typeof(Opera_Cookie) && path.Contains("Cookies_Service"))
                    {
                        var result = conn.Query<T>("SELECT * FROM opera_cookies;").ToList();
                        return result;
                    }
                    else if (typeof(T) == typeof(Edge_Cookie) && path.Contains("Cookies_Service"))
                    {
                        var result = conn.Query<T>("SELECT * FROM edge_cookies;").ToList();
                        return result;
                    }
                    else
                    {
                        var result = conn.Query<T>("SELECT * FROM cookies;").ToList();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION_SQLITE.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
                    return null;
                }
            }
        }

        static public void Update_Cookie<T>(string path, List<T> items)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + path + "\\Cookies"))
            {
                try
                {
                    foreach(T item in items){

                        if(typeof(T) == typeof(Chrome_Cookie))
                        {
                            conn.Query(@"UPDATE chrome_cookies SET creation_utc = @creation_utc,
       host_key = @host_key,
       name = @name,
       value = @value,
       path = @path,
       expires_utc = @expires_utc,
       is_secure = @is_secure,
       is_httponly = @is_httponly,
       last_access_utc = @last_access_utc,
       has_expires = @has_expires,
       is_persistent = @is_persistent,
       priority = @priority,
       encrypted_value = @encrypted_value,
       samesite = @samesite,
       source_scheme = @source_scheme,
       source_port = @source_port,
       is_same_party = @is_same_party
 WHERE host_key = @host_key",item);
                        } // Update Chrome_Cookies

                        else if(typeof(T) == typeof(Opera_Cookie))
                        {
                            conn.Query(@"UPDATE opera_cookies SET creation_utc = @creation_utc,
       host_key = @host_key,
       name = @name,
       value = @value,
       path = @path,
       expires_utc = @expires_utc,
       is_secure = @is_secure,
       is_httponly = @is_httponly,
       last_access_utc = @last_access_utc,
       has_expires = @has_expires,
       is_persistent = @is_persistent,
       priority = @priority,
       encrypted_value = @encrypted_value,
       samesite = @samesite,
       source_scheme = @source_scheme,
       source_port = @source_port,
       is_same_party = @is_same_party
 WHERE host_key = @host_key",item);
                        } // Update Opera_Cookies

                        else if (typeof(T) == typeof(Edge_Cookie)) 
                        {
                            conn.Query(@"UPDATE edge_cookies SET creation_utc = @creation_utc,
       host_key = @host_key,
       name = @name,
       value = @value,
       path = @path,
       expires_utc = @expires_utc,
       is_secure = @is_secure,
       is_httponly = @is_httponly,
       last_access_utc = @last_access_utc,
       has_expires = @has_expires,
       is_persistent = @is_persistent,
       priority = @priority,
       encrypted_value = @encrypted_value,
       samesite = @samesite,
       source_scheme = @source_scheme,
       source_port = @source_port,
       is_same_party = @is_same_party,
       is_edgelegacycookie = @is_edgelegacycookie,
       browser_provenance = @browser_provenance
 WHERE host_key = @host_key",item);
                        } // Update Edge_Cookies
                    }
                }
                catch (Exception ex)
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION_SQLITE.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
                }
            }
        }

        static public void Delete_Cookie<T>(string path, List<T> items)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + path + "\\Cookies"))
            {
                try
                {
                    foreach (T item in items)
                    {

                        if (typeof(T) == typeof(Chrome_Cookie))
                        {
                            conn.Query(@"DELETE FROM chrome_cookies WHERE host_key = @host_key", item);
                        } // Delete Chrome_Cookie

                        else if (typeof(T) == typeof(Opera_Cookie))
                        {
                            conn.Query(@"DELETE FROM opera_cookies WHERE host_key = @host_key", item);
                        } // Delete Opera_Cookie

                        else if (typeof(T) == typeof(Edge_Cookie))
                        {
                            conn.Query(@"DELETE FROM edge_cookies WHERE host_key = @host_key",item);
                        } // Delete Edge_Cookie
                    }
                }
                catch (Exception ex)
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION_SQLITE.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
                }
            }
        }
    }
}
