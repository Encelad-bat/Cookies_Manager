using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cookies_Manager.Model;
using Microsoft.Win32;

namespace Cookies_Manager.ViewModel
{
    class Cookie_ViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Chrome_Cookie> chrome_cookies = new ObservableCollection<Chrome_Cookie>(Model.Model.Read_Cookies<Chrome_Cookie>($"C:\\Users\\{Registry.CurrentUser.OpenSubKey("Volatile Environment").GetValue("USERNAME")}\\AppData\\Roaming\\Cookies_Service"));

        public ObservableCollection<Chrome_Cookie> Chrome_cookies
        {
            get { return chrome_cookies; }
            set { chrome_cookies = value; OnPropertyChanged("Chrome_cookies"); }
        }

        private ObservableCollection<Opera_Cookie> opera_cookies = new ObservableCollection<Opera_Cookie>(Model.Model.Read_Cookies<Opera_Cookie>($"C:\\Users\\{Registry.CurrentUser.OpenSubKey("Volatile Environment").GetValue("USERNAME")}\\AppData\\Roaming\\Cookies_Service"));

        public ObservableCollection<Opera_Cookie> Opera_cookies
        {
            get { return opera_cookies; }
            set { opera_cookies = value; OnPropertyChanged("Opera_cookies"); }
        }

        private ObservableCollection<Edge_Cookie> edge_cookies = new ObservableCollection<Edge_Cookie>(Model.Model.Read_Cookies<Edge_Cookie>($"C:\\Users\\{Registry.CurrentUser.OpenSubKey("Volatile Environment").GetValue("USERNAME")}\\AppData\\Roaming\\Cookies_Service"));

        public ObservableCollection<Edge_Cookie> Edge_cookies
        {
            get { return edge_cookies; }
            set { edge_cookies = value; OnPropertyChanged("Edge_cookies"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
