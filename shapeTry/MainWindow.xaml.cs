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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shapeTry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Boolean drag = false;
        public static Point startPt = new Point();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MovShp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            drag = true;
            Cursor = Cursors.Hand;
            startPt = e.GetPosition(CanvasImplant);
            Mouse.Capture(CanvasImplant);
        }

        private void MovShp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            drag = false;
            Cursor = Cursors.Arrow;
            Mouse.Capture(null);
        }

        private void MovShp_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                var mp = e.GetPosition(CanvasImplant);
                double deltaX = mp.X - startPt.X;
                double deltaY = mp.Y - startPt.Y;

                var newX = deltaX + Canvas.GetLeft(MovableShape);
                var newY = deltaY + Canvas.GetTop(MovableShape);

                if (newX < 0)
                    newX = 0;
                else if (newX + MovableShape.ActualWidth > CanvasImplant.ActualWidth)
                    newX = CanvasImplant.ActualWidth - MovableShape.ActualWidth;

                if (newY < 0)
                    newY = 0;
                else if (newY + MovableShape.ActualHeight > CanvasImplant.ActualHeight)
                    newY = CanvasImplant.ActualHeight - MovableShape.ActualHeight;

                Canvas.SetLeft(MovableShape, newX);
                Canvas.SetTop(MovableShape, newY);

                startPt = mp;
            }

        }
    }
}
