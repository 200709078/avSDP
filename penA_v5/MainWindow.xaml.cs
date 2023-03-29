using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace penA_v5
{
    public partial class MainWindow : Window
    {
        class Drawing
        {
            public int ID { get; set; }
            public string name { get; set; }
            public string imagePath { get; set; }
            public ImageSource imageSource { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            haveDirectory();
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            winDraw winDraw = new winDraw();
            winDraw.Show();
            this.Close();
        }

        private void haveDirectory()
        {
            bool existsDir = Directory.Exists("drawings");

            if (!existsDir)
            {
                Directory.CreateDirectory("drawings");
            }
            else
            {
                String[] arrFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\drawings\\");
                int id = 1;
                List<Drawing> drawings = new List<Drawing>();
                foreach (String file in arrFiles)
                {
                    FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read);
                    InkCanvas savedCanvas = XamlReader.Load(fs) as InkCanvas;
                    fs.Close();

                    Transform transform = savedCanvas.LayoutTransform;
                    savedCanvas.LayoutTransform = null;
                    Size size = new Size(this.Width, this.Height);
                    savedCanvas.Measure(size);
                    savedCanvas.Arrange(new Rect(size));
                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96d, 96d, PixelFormats.Pbgra32);
                    renderBitmap.Render(savedCanvas);

                    Image img = new Image();
                    //img.Source = new BitmapImage(new Uri("/penA.ico", UriKind.Relative));
                    img.Source = renderBitmap;
                    img.Height = 80;
                    //img.HorizontalAlignment = HorizontalAlignment.Left;
                    //img.VerticalAlignment = VerticalAlignment.Top;
                    //img.Margin = new Thickness(10);
                    //img.Cursor = Cursors.Hand;
                    //img.MouseLeftButtonDown += img_MouseLeftButtonDown;
                    //stckFiles.Children.Add(img);
;
                    FileInfo fi = new FileInfo(file);
                    drawings.Add(new Drawing()
                    {
                        ID = id,
                        name = fi.Name.Substring(0,13),
                        imagePath = file,
                        imageSource = img.Source
                    });
                    id++;
                }
                lbDrawings.ItemsSource = drawings;
            }
        }
        private void lbDrawings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drawing activeDrawing = lbDrawings.Items[lbDrawings.SelectedIndex] as Drawing;
            //MessageBox.Show(activeDrawing.imagePath.ToString());
            winDraw winDraw = new winDraw();
            winDraw.drawPath = activeDrawing.imagePath;
            winDraw.Show();
            this.Close();

        }
        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    if (inkBoard.Strokes != null)
        //    {
        //        String datetime = DateTime.Now.ToString("yyMMdd_HHmmss");
        //        String myDir = myPath + "\\drawings\\" + datetime + ".xaml";
        //        FileStream fs = File.Open(myDir, FileMode.Create);
        //        XamlWriter.Save(inkBoard, fs);
        //        fs.Close();
        //    }
        //}

        //private void btnLoad_Click(object sender, RoutedEventArgs e)
        //{
        //    //String datetime =DateTime.Now.ToString("yy mm dd hh:mm:ss");
        //    //FileStream fs = File.Open(@Directory.GetCurrentDirectory() + "\\drawings\\"+datetime+".xaml", FileMode.Open, FileAccess.Read);
        //    //InkCanvas savedCanvas = XamlReader.Load(fs) as InkCanvas;
        //    //fs.Close();
        //    //inkBoard.Strokes = savedCanvas.Strokes;
        //}
    }
}
