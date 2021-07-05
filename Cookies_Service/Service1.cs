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
            try
            {
                Directory.CreateDirectory("C:\\Users\\Kneven\\AppData\\Roaming\\Cookies_Service");
                if (!File.Exists("C:\\Users\\Kneven\\AppData\\Roaming\\Cookies_Service\\Cookies.sqlite"))
                {
                    SQLiteConnection.CreateFile("C:\\Users\\Kneven\\AppData\\Roaming\\Cookies_Service\\Cookies.sqlite");
                }
                using (SQLiteConnection conn = new SQLiteConnection("Data Source = C:\\Users\\Kneven\\AppData\\Roaming\\Cookies_Service\\Cookies.sqlite"))
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
                            DEFAULT 0,
    UNIQUE(
        host_key,
        name,
        path
    )
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
                            DEFAULT 0,
    UNIQUE (
        host_key,
        name,
        path
    )
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
    browser_provenance  INTEGER DEFAULT 0,
    UNIQUE (
        host_key,
        name,
        path
    )
);");

                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
            }
            try {
                using(SQLiteConnection conn = new SQLiteConnection("Data Source = C:\\Users\\Kneven\\AppData\\Roaming\\Cookies_Service\\Cookies.sqlite"))
                {
                    //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-


                    Chrome_Cookies chrome_cookies = new Chrome_Cookies();
                    List<Chrome_Cookies> chrome_collection = chrome_cookies.Load<Chrome_Cookies>(chrome_cookies.Path);

                    foreach (Chrome_Cookies item in chrome_collection)
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
                    }


                    //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-


                    Opera_Cookies opera_cookies = new Opera_Cookies();
                    List<Opera_Cookies> opera_collection = opera_cookies.Load<Opera_Cookies>(opera_cookies.Path);

                    foreach (Opera_Cookies item in opera_collection)
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
                    }


                    //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-


                    Edge_Cookies edge_cookies = new Edge_Cookies();
                    List<Edge_Cookies> edge_collection = edge_cookies.Load<Edge_Cookies>(edge_cookies.Path);

                    foreach (Opera_Cookies item in opera_collection)
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
                    }


                    //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
                }
            }
            catch
            {

            }
        }

        protected override void OnStop()
        {
        }
    }
}
