using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cookies_Manager.ViewModel;
using System.Windows.Threading;

namespace Cookies_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Cookie_ViewModel();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.data_GridView.ItemsSource == (this.DataContext as Cookie_ViewModel).Chrome_cookies)
            {
                this.DataContext = new Cookie_ViewModel();
                this.data_GridView.ItemsSource = (this.DataContext as Cookie_ViewModel).Chrome_cookies;
            }
            else if(this.data_GridView.ItemsSource == (this.DataContext as Cookie_ViewModel).Opera_cookies)
            {
                this.DataContext = new Cookie_ViewModel();
                this.data_GridView.ItemsSource = (this.DataContext as Cookie_ViewModel).Opera_cookies;
            }
            else if(this.data_GridView.ItemsSource == (this.DataContext as Cookie_ViewModel).Edge_cookies)
            {
                this.DataContext = new Cookie_ViewModel();
                this.data_GridView.ItemsSource = (this.DataContext as Cookie_ViewModel).Edge_cookies;
            }
        }

        private void Chrome_Click(object sender, RoutedEventArgs e)
        {
            this.data_GridView.ItemsSource = (this.DataContext as Cookie_ViewModel).Chrome_cookies;
        }

        private void Opera_Click(object sender, RoutedEventArgs e)
        {
            this.data_GridView.ItemsSource = (this.DataContext as Cookie_ViewModel).Opera_cookies;
        }

        private void Edge_Click(object sender, RoutedEventArgs e)
        {
            this.data_GridView.ItemsSource = (this.DataContext as Cookie_ViewModel).Edge_cookies;
        }
    }
}
