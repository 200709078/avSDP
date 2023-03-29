using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace penA_v2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frameMain.Content = new pageMain();
            stckBottom.Visibility = Visibility.Hidden;
        }

        private void btnBoard_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Content = new pageDraw();
            stckTop.Visibility = Visibility.Hidden;
            stckBottom.Visibility = Visibility.Visible;
        }

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Content = new pageMain();
            stckBottom.Visibility = Visibility.Hidden;
            stckTop.Visibility = Visibility.Visible;
        }
        private void btnSS_Click(object sender, RoutedEventArgs e)
        {
            //frameMain.Visibility = Visibility.Hidden;
            //mainGrid.Visibility = Visibility.Hidden;

            //this.Hide();
            //Thread.Sleep(2000);

            RenderTargetBitmap ssbmp = new RenderTargetBitmap((int)SystemParameters.VirtualScreenWidth, (int)SystemParameters.VirtualScreenHeight, 100, 100, PixelFormats.Pbgra32);
            ssbmp.Render(this);


            //frameMain.Visibility = Visibility.Visible;
            //mainGrid.Visibility = Visibility.Visible;

            //this.Show();
            pageScreenShot page = new pageScreenShot();
            frameMain.Content = page;
            //this.Hide();
            //Thread.Sleep(2000);
            //System.Windows.Forms.SendKeys.Send("{PRTSC}");
            page.imgSS.Source = ssbmp; //Clipboard.GetImage();
            //this.Show();


        }
    }
}
