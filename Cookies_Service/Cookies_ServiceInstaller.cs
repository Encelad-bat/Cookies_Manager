using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies_Service
{
    [RunInstaller(true)]
    public partial class Cookies_ServiceInstaller : System.Configuration.Install.Installer
    {
        public Cookies_ServiceInstaller()
        {
            InitializeComponent();
            this.AfterInstall += Cookies_ServiceInstaller_AfterInstall;
            this.AfterRollback += Cookies_ServiceInstaller_AfterRollback;
        }

        private void Cookies_ServiceInstaller_AfterRollback(object sender, InstallEventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed!");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private void Cookies_ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
