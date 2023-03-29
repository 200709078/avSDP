using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace penA_v3
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

            if (pageDraw.inkCan.Strokes != null)
            {
                FileStream fs = File.Open(@"C:\aaa\canFile.xaml", FileMode.Create);
                XamlWriter.Save(pageDraw.inkCan, fs);
                fs.Close();
            }
        }
        private void btnSS_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Thread.Sleep(500);
            stckBottom.Visibility=Visibility.Hidden;

            pageScreenShot page = new pageScreenShot();
            frameMain.Content = page;
            System.Windows.Forms.SendKeys.SendWait("{PRTSC}");
            page.imgSS.Source = Clipboard.GetImage();
            Clipboard.Clear();
            this.WindowStyle = WindowStyle.None;
            this.Show();
        }

        private void frameMain_ContentRendered(object sender, EventArgs e)
        {
            if (frameMain.Content.ToString()+".xaml"== "penA_v3." + new Uri("pageDraw.xaml", UriKind.Relative).ToString())
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                stckBottom.Visibility = Visibility.Visible;
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
        //    //frameMain.Content = new pageDraw();
        //    //stckTop.Visibility = Visibility.Hidden;
        //    //stckBottom.Visibility = Visibility.Visible;

        //    //FileStream fs = File.Open(@"C:\aaa\fileName.xaml", FileMode.Open, FileAccess.Read);
        //    //Canvas savedCanvas = XamlReader.Load(fs) as Canvas;
        //    //fs.Close();
        //    //pageDraw.grd.Children.Add(savedCanvas);
        }
    }
}
