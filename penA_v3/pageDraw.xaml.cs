using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace penA_v3
{
    /// <summary>
    /// Interaction logic for pageDraw.xaml
    /// </summary>
    public partial class pageDraw : Page
    {
        public static InkCanvas inkCan;
        public static Stack<DoStroke> RedoStrokes { get; set; }
        public static Stack<DoStroke> UndoStrokes { get; set; }
        private bool handle = true;
        public pageDraw()
        {
            InitializeComponent();
            inkCan = inkBoard;
            inkBoard.DefaultDrawingAttributes.Color = Colors.Red;
            inkBoard.DefaultDrawingAttributes.Width = 2;
            inkBoard.UseCustomCursor = true;
            inkBoard.Cursor = Cursors.Pen;

            RedoStrokes = new Stack<DoStroke>();
            UndoStrokes = new Stack<DoStroke>();
            inkBoard.DefaultDrawingAttributes.FitToCurve = true;
            inkBoard.Strokes.StrokesChanged += Strokes_StrokesChanged;


            //Rectangle newRect = new Rectangle
            //{ 
            //Width=50,
            //Height=50,
            //Fill=Brushes.Red,
            //StrokeThickness=inkBoard.DefaultDrawingAttributes.Width
            //};

            //Canvas.SetLeft(newRect,120);
            //Canvas.SetTop(newRect,100);

            //inkBoard.Children.Add(newRect);

            //Image image = new Image
            //{
            //    Width = 200,
            //    Height = 200,
            //    Source = new BitmapImage(new Uri("C:/Users/mADEMatik/Desktop/aaa.jpg"))
            //};

            //inkBoard.Children.Add(image);


        }
        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            btnMode.Content = "Pen";
            inkBoard.UseCustomCursor = true;
            inkBoard.Cursor = Cursors.Pen;

            inkBoard.EditingMode = InkCanvasEditingMode.Ink;
            if (inkBoard.DefaultDrawingAttributes.Color == Colors.Red)
            {
                inkBoard.DefaultDrawingAttributes.Color = Colors.Blue;
                btnColor.Background = new SolidColorBrush(Colors.Blue);
            }
            else if (inkBoard.DefaultDrawingAttributes.Color == Colors.Blue)
            {
                inkBoard.DefaultDrawingAttributes.Color = Colors.Black;
                btnColor.Background = new SolidColorBrush(Colors.Black);
            }
            else
            {
                inkBoard.DefaultDrawingAttributes.Color = Colors.Red;
                btnColor.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void btnSize_Click(object sender, RoutedEventArgs e)
        {
            btnMode.Content = "Pen";
            inkBoard.UseCustomCursor = true;
            inkBoard.Cursor = Cursors.Pen;

            inkBoard.EditingMode = InkCanvasEditingMode.Ink;
            if (inkBoard.DefaultDrawingAttributes.Width == 2)
            {
                inkBoard.DefaultDrawingAttributes.Width = 4;
                btnSize.Content = "Size 4";
            }
            else if (inkBoard.DefaultDrawingAttributes.Width == 4)
            {
                inkBoard.DefaultDrawingAttributes.Width = 6;
                btnSize.Content = "Size 6";
            }
            else
            {
                inkBoard.DefaultDrawingAttributes.Width = 2;
                btnSize.Content = "Size 2";
            }
        }

        private void btnMode_Click(object sender, RoutedEventArgs e)
        {
            if (inkBoard.EditingMode == InkCanvasEditingMode.Ink)
            {
                inkBoard.EditingMode = InkCanvasEditingMode.EraseByStroke;
                btnMode.Content = "Eraser";
                inkBoard.UseCustomCursor = false;
            }
            else if (inkBoard.EditingMode == InkCanvasEditingMode.EraseByStroke)
            {
                inkBoard.EditingMode = InkCanvasEditingMode.Select;
                btnMode.Content = "Select";
                inkBoard.UseCustomCursor = false;
            }
            else
            {
                inkBoard.EditingMode = InkCanvasEditingMode.Ink;
                btnMode.Content = "Pen";
                inkBoard.UseCustomCursor = true;
                inkBoard.Cursor = Cursors.Pen;
            }
        }

        private void inkBoard_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            inkBoard.EditingMode = InkCanvasEditingMode.Ink;
            btnMode.Content = "Pen";
        }

        private void Strokes_StrokesChanged(object sender, System.Windows.Ink.StrokeCollectionChangedEventArgs e)
        {
            if (handle)
            {
                RedoStrokes.Push(new DoStroke
                {
                    ActionFlag = e.Added.Count > 0 ? "ADD" : "REMOVE",
                    Stroke = e.Added.Count > 0 ? e.Added[0] : e.Removed[0]
                });
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
            btnRedo.IsEnabled = true;
            btnRedo.Background = Brushes.Blue;

            if (RedoStrokes.Count > 0)
            {
                DoStroke @do = RedoStrokes.Pop();
                if (@do.ActionFlag.Equals("ADD"))
                {
                    inkBoard.Strokes.Remove(@do.Stroke);
                }
                else
                {
                    inkBoard.Strokes.Add(@do.Stroke);
                }

                UndoStrokes.Push(@do);
            }
            if (inkBoard.Strokes.Count == 0)
            {
                btnUndo.IsEnabled = false;
                btnUndo.Background = Brushes.LightGray;

                btnClear.IsEnabled = false;
                btnClear.Background = Brushes.LightGray;
            }
            handle = true;
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
            btnUndo.IsEnabled = true;
            btnUndo.Background = Brushes.Blue;

            btnClear.IsEnabled = true;
            btnClear.Background = Brushes.Blue;

            if (UndoStrokes.Count > 0)
            {
                DoStroke @do = UndoStrokes.Pop();
                RedoStrokes.Push(@do);

                if (@do.ActionFlag.Equals("ADD"))
                {
                    inkBoard.Strokes.Add(@do.Stroke);
                }
                else
                {
                    inkBoard.Strokes.Remove(@do.Stroke);
                }
            }
            if (UndoStrokes.Count == 0)
            {
                btnRedo.IsEnabled = false;
                btnRedo.Background = Brushes.LightGray;
            }
            handle = true;
        }

        private void inkBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnUndo.IsEnabled = true;
            btnUndo.Background = Brushes.Blue;

            if (inkBoard.EditingMode == InkCanvasEditingMode.Ink)
            {
                btnRedo.IsEnabled = false;
                btnRedo.Background = Brushes.LightGray;
            }

            btnClear.IsEnabled = true;
            btnClear.Background = Brushes.Blue;

            UndoStrokes.Clear();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            inkBoard.Strokes.Clear();
            UndoStrokes.Clear();
            RedoStrokes.Clear();

            btnUndo.IsEnabled = false;
            btnUndo.Background = Brushes.LightGray;

            btnClear.IsEnabled = false;
            btnClear.Background = Brushes.LightGray;

            btnRedo.IsEnabled = false;
            btnRedo.Background = Brushes.LightGray;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Image img = new Image();
            //img.Source = Clipboard.GetImage();

            //InkCanvas.SetTop(img,100);
            //InkCanvas.SetLeft(img,100);
            //inkBoard.Children.Add(img);
        }
    }
    public struct DoStroke
    {
        public string ActionFlag { get; set; }
        public System.Windows.Ink.Stroke Stroke { get; set; }
    }
}
