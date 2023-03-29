using System;
//using System.Collections.Generic;
//using System.Diagnostics.Tracing;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
namespace penA
{
    public partial class w_Menu : Window
    {
        public w_Menu()
        {
            InitializeComponent();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void bntCL_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void txt_Black_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.ink.EditingMode = InkCanvasEditingMode.Ink;
            MainWindow.ink.DefaultDrawingAttributes.Color = System.Windows.Media.Colors.Black;
            txt_Black.BorderBrush = Brushes.White;
            txt_Red.BorderBrush = Brushes.Red;
            txt_Blue.BorderBrush = Brushes.Blue;
            txt_Green.BorderBrush = Brushes.Green;

            btn_pencil.Background = Brushes.Aqua;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
        }

        private void txt_Red_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.ink.EditingMode = InkCanvasEditingMode.Ink;
            MainWindow.ink.DefaultDrawingAttributes.Color = System.Windows.Media.Colors.Red;
            txt_Black.BorderBrush = Brushes.Black;
            txt_Red.BorderBrush = Brushes.White;
            txt_Blue.BorderBrush = Brushes.Blue;
            txt_Green.BorderBrush = Brushes.Green;

            btn_pencil.Background = Brushes.Aqua;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
        }
        private void txt_Blue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.ink.EditingMode = InkCanvasEditingMode.Ink;
            MainWindow.ink.DefaultDrawingAttributes.Color = System.Windows.Media.Colors.Blue;
            txt_Black.BorderBrush = Brushes.Black;
            txt_Red.BorderBrush = Brushes.Red;
            txt_Blue.BorderBrush = Brushes.White;
            txt_Green.BorderBrush = Brushes.Green;

            btn_pencil.Background = Brushes.Aqua;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
        }
        private void txt_Green_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.ink.EditingMode = InkCanvasEditingMode.Ink;
            MainWindow.ink.DefaultDrawingAttributes.Color = System.Windows.Media.Colors.Green;
            txt_Black.BorderBrush = Brushes.Black;
            txt_Red.BorderBrush = Brushes.Red;
            txt_Blue.BorderBrush = Brushes.Blue;
            txt_Green.BorderBrush = Brushes.White;

            btn_pencil.Background = Brushes.Aqua;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
        }
        private void img_size_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.penWidth += 5;
            if (MainWindow.penWidth == 20)
            {
                MainWindow.penWidth = 5;
            }
            img_size.Source = new BitmapImage(new Uri(@"assets/size" + MainWindow.penWidth.ToString() + ".png", UriKind.Relative));
            MainWindow.ink.DefaultDrawingAttributes.Width = MainWindow.penWidth;
        }
        private void img_pencil_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.ink.EditingMode = InkCanvasEditingMode.Ink;
            btn_pencil.Background = Brushes.Aqua;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
            MainWindow.ink.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void img_eraser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn_pencil.Background = Brushes.AliceBlue;
            btn_eraser.Background = Brushes.Aqua;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
            MainWindow.ink.EditingMode = InkCanvasEditingMode.EraseByStroke;
            //MainWindow.ink.EraserShape = new RectangleStylusShape(MainWindow.penWidth, MainWindow.penWidth);

        }

        private void img_line_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn_pencil.Background = Brushes.AliceBlue;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.Aqua;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
            MainWindow.ink.EditingMode = InkCanvasEditingMode.None;
        }

        private void img_rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn_pencil.Background = Brushes.AliceBlue;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.Aqua;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
            MainWindow.ink.EditingMode = InkCanvasEditingMode.None;
        }

        private void img_ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn_pencil.Background = Brushes.AliceBlue;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.Aqua;
            btn_selection.Background = Brushes.AliceBlue;
            MainWindow.ink.EditingMode = InkCanvasEditingMode.None;
        }

        private void img_clearall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btn_pencil.Background = Brushes.AliceBlue;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.AliceBlue;
            MainWindow.ink.Strokes.Clear();
        }

        private void img_selection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btn_pencil.Background = Brushes.AliceBlue;
            btn_eraser.Background = Brushes.AliceBlue;
            btn_line.Background = Brushes.AliceBlue;
            btn_rectangle.Background = Brushes.AliceBlue;
            btn_ellipse.Background = Brushes.AliceBlue;
            btn_selection.Background = Brushes.Aqua;
            MainWindow.ink.EditingMode = InkCanvasEditingMode.Select;
        }
    }
}
