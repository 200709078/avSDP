using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sdp_03
{
    public partial class MainWindow : Window
    {
        static bool activemenu = true;
        static imageWindow imgw = new imageWindow();
        public MainWindow()
        {
            InitializeComponent();
            this.Height= 50;
        }

        private void btn_kapat_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btn_closee_Click(object sender, RoutedEventArgs e)
        {
            if (activemenu)
            {
                this.Height = 300;
                btn_active_menu.Content = "Aktif";
                imgw.Show();
                imgw.WindowState = WindowState.Maximized;
                activemenu = false;
            }
            else
            {
                this.Height = 50;
                btn_active_menu.Content = "Aktif Değil";
                imgw.WindowState = WindowState.Minimized;
                activemenu = true;
            }
        }
    }
}
