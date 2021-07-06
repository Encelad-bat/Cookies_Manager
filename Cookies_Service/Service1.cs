using Dapper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Cookies_Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanShutdown = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string main_cookies_path = "C:\\Users\\Kneven\\AppData\\Roaming\\Cookies_Service";
            //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-

            try
            {
                Directory.CreateDirectory(main_cookies_path);
                if (!File.Exists(main_cookies_path + "\\Cookies.sqlite"))
                {
                    SQLiteConnection.CreateFile(main_cookies_path + "\\Cookies.sqlite");
                }
                using (SQLiteConnection conn = new SQLiteConnection("Data Source =" + main_cookies_path + "\\Cookies.sqlite"))
                {
                    conn.Query(@"CREATE TABLE IF NOT EXISTS chrome_cookies (
    creation_utc    INTEGER NOT NULL,
    host_key        TEXT    NOT NULL,
    name            TEXT    NOT NULL,
    value           TEXT    NOT NULL,
    path            TEXT    NOT NULL,
    expires_utc     INTEGER NOT NULL,
    is_secure       INTEGER NOT NULL,
    is_httponly     INTEGER NOT NULL,
    last_access_utc INTEGER NOT NULL,
    has_expires     INTEGER NOT NULL
                            DEFAULT 1,
    is_persistent   INTEGER NOT NULL
                            DEFAULT 1,
    priority        INTEGER NOT NULL
                            DEFAULT 1,
    encrypted_value BLOB    DEFAULT '',
    samesite        INTEGER NOT NULL
                            DEFAULT - 1,
    source_scheme   INTEGER NOT NULL
                            DEFAULT 0,
    source_port     INTEGER NOT NULL
                            DEFAULT - 1,
    is_same_party   INTEGER NOT NULL
                            DEFAULT 0
); ");
                    conn.Query(@"CREATE TABLE IF NOT EXISTS opera_cookies (
    creation_utc    INTEGER NOT NULL,
    host_key        TEXT    NOT NULL,
    name            TEXT    NOT NULL,
    value           TEXT    NOT NULL,
    path            TEXT    NOT NULL,
    expires_utc     INTEGER NOT NULL,
    is_secure       INTEGER NOT NULL,
    is_httponly     INTEGER NOT NULL,
    last_access_utc INTEGER NOT NULL,
    has_expires     INTEGER NOT NULL
                            DEFAULT 1,
    is_persistent   INTEGER NOT NULL
                            DEFAULT 1,
    priority        INTEGER NOT NULL
                            DEFAULT 1,
    encrypted_value BLOB    DEFAULT '',
    samesite        INTEGER NOT NULL
                            DEFAULT -1,
    source_scheme   INTEGER NOT NULL
                            DEFAULT 0,
    source_port     INTEGER NOT NULL
                            DEFAULT -1,
    is_same_party   INTEGER NOT NULL
                            DEFAULT 0
);");
                    conn.Query(@"CREATE TABLE IF NOT EXISTS edge_cookies (
    creation_utc        INTEGER NOT NULL,
    host_key            TEXT    NOT NULL,
    name                TEXT    NOT NULL,
    value               TEXT    NOT NULL,
    path                TEXT    NOT NULL,
    expires_utc         INTEGER NOT NULL,
    is_secure           INTEGER NOT NULL,
    is_httponly         INTEGER NOT NULL,
    last_access_utc     INTEGER NOT NULL,
    has_expires         INTEGER NOT NULL
                                DEFAULT 1,
    is_persistent       INTEGER NOT NULL
                                DEFAULT 1,
    priority            INTEGER NOT NULL
                                DEFAULT 1,
    encrypted_value     BLOB    DEFAULT '',
    samesite            INTEGER NOT NULL
                                DEFAULT -1,
    source_scheme       INTEGER NOT NULL
                                DEFAULT 0,
    source_port         INTEGER NOT NULL
                                DEFAULT -1,
    is_same_party       INTEGER NOT NULL
                                DEFAULT 0,
    is_edgelegacycookie INTEGER DEFAULT 0,
    browser_provenance  INTEGER DEFAULT 0
);");

                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
            } // SQLite database and tables create

            //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
            List<Chrome_Cookie> chrome_cookies = Model.Read_Cookies<Chrome_Cookie>(new Chrome_Cookie().Path);
            try {
                Model.Create_Cookie<Chrome_Cookie>(main_cookies_path, chrome_cookies);
            }
            catch
            {
                Model.Delete_Cookie<Chrome_Cookie>(main_cookies_path, chrome_cookies);
            }
            

            List<Opera_Cookie> opera_cookies = Model.Read_Cookies<Opera_Cookie>(new Opera_Cookie().Path);
            try
            {
                Model.Create_Cookie<Opera_Cookie>(main_cookies_path, opera_cookies);
            }
            catch
            {
                Model.Delete_Cookie<Opera_Cookie>(main_cookies_path, opera_cookies);
            }

            List<Edge_Cookie> edge_cookies = Model.Read_Cookies<Edge_Cookie>(new Edge_Cookie().Path);
            try
            {
                Model.Create_Cookie<Edge_Cookie>(main_cookies_path, edge_cookies);
            }
            catch
            {
                Model.Delete_Cookie<Edge_Cookie>(main_cookies_path, edge_cookies);
            }

            //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
        }

        protected override void OnStop()
        {
        }
    }
}
