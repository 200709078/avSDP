using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace penA_v5
{
    public partial class winDraw : Window
    {
        public static InkCanvas inkBoard = new InkCanvas();
        public static Stack<DoStroke> RedoStrokes { get; set; }
        public static Stack<DoStroke> UndoStrokes { get; set; }
        private bool handle = true;
        public static Boolean draw = false;
        public static Point strtPoint = new Point();
        public static Point endPoint = new Point();
        public static double shapeWidth;
        public static double shapeHeight;
        public static Rectangle rect;
        public static Ellipse elli;
        public static Line line;

        public static String myPath = Directory.GetCurrentDirectory();
        public static String drawPath = null;
        public winDraw()
        {
            InitializeComponent();
            drawCanGrid.Children.Add(inkBoard);
            inkBoard.MouseDown += inkBoard_MouseDown;
            inkBoard.MouseMove += inkBoard_MouseMove;
            inkBoard.MouseUp += inkBoard_MouseUp;
            inkBoard.DefaultDrawingAttributes.Color = Colors.Red;
            inkBoard.DefaultDrawingAttributes.Width = 2;
            inkBoard.UseCustomCursor = true;
            inkBoard.Cursor = Cursors.Pen;

            RedoStrokes = new Stack<DoStroke>();
            UndoStrokes = new Stack<DoStroke>();
            inkBoard.DefaultDrawingAttributes.FitToCurve = true;
            inkBoard.Strokes.StrokesChanged += Strokes_StrokesChanged;
            haveDrawPath();

        }

        private void haveDrawPath()
        {
            if (drawPath != null)
            {
                FileStream fs = File.Open(drawPath, FileMode.Open, FileAccess.Read);
                InkCanvas savedCanvas = XamlReader.Load(fs) as InkCanvas;
                fs.Close();
                inkBoard.Strokes = savedCanvas.Strokes;
                drawPath = null;
            }
        }

        //private void wDraw_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    MessageBox.Show(inkBoard.Strokes.Count.ToString());
        //    //if (inkBoard.Strokes.Count>0)
        //    //{
        //    //    String datetime = DateTime.Now.ToString("yyMMdd_HHmmss");
        //    //    String myDir = myPath + "\\drawings\\" + datetime + ".xaml";
        //    //    FileStream fs = File.Open(myDir, FileMode.Create);
        //    //    XamlWriter.Save(inkBoard, fs);
        //    //    fs.Close();
        //    //}
        //}

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            inkBoard.Strokes.Clear();
            drawCanGrid.Children.Remove(inkBoard);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
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
            if (btnMode.Content.Equals("Pen"))
            {
                btnMode.Content = "Eraser";
                inkBoard.EditingMode = InkCanvasEditingMode.EraseByStroke;
                inkBoard.UseCustomCursor = false;
                btnShape.IsEnabled = false;
                btnShape.Background = Brushes.LightGray;
                draw = false;
            }
            else if (btnMode.Content.Equals("Eraser"))
            {
                btnMode.Content = "Select";
                inkBoard.EditingMode = InkCanvasEditingMode.Select;
                inkBoard.UseCustomCursor = false;
                btnShape.IsEnabled = false;
                btnShape.Background = Brushes.LightGray;
                draw = false;
            }
            else if (btnMode.Content.Equals("Select"))
            {
                btnMode.Content = "Shape";
                inkBoard.EditingMode = InkCanvasEditingMode.None;
                inkBoard.UseCustomCursor = true;
                inkBoard.Cursor = Cursors.UpArrow;
                btnShape.IsEnabled = true;
                btnShape.Background = Brushes.Blue;
            }
            else
            {
                inkBoard.EditingMode = InkCanvasEditingMode.Ink;
                btnMode.Content = "Pen";
                inkBoard.UseCustomCursor = true;
                inkBoard.Cursor = Cursors.Pen;
                btnShape.IsEnabled = false;
                btnShape.Background = Brushes.LightGray;
                draw = false;
            }
        }
        private void inkBoard_MouseDown(object sender, MouseEventArgs e)
        {
            strtPoint = e.GetPosition(inkBoard);
            if (btnMode.Content.Equals("Shape"))
            {
                draw = true;
                if (btnShape.Content.Equals("Rect"))
                {
                    rect = new Rectangle();
                    inkBoard.Children.Add(rect);
                }
                if (btnShape.Content.Equals("Circle"))
                {
                    elli = new Ellipse();
                    inkBoard.Children.Add(elli);
                }
                if (btnShape.Content.Equals("Line"))
                {
                    line = new Line();
                    inkBoard.Children.Add(line);
                }
            }
        }
        private void inkBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                endPoint = e.GetPosition(inkBoard);
                shapeWidth = Math.Abs(endPoint.X - strtPoint.X);
                shapeHeight = Math.Abs(endPoint.Y - strtPoint.Y);

                if (btnShape.Content.Equals("Rect"))
                {
                    rect.Stroke = btnColor.Background;
                    rect.StrokeThickness = inkBoard.DefaultDrawingAttributes.Width;
                    rect.Width = shapeWidth;
                    rect.Height = shapeHeight;
                    if (strtPoint.X <= endPoint.X)
                    {
                        InkCanvas.SetLeft(rect, strtPoint.X);
                    }
                    else
                    {
                        InkCanvas.SetLeft(rect, endPoint.X);
                    }

                    if (strtPoint.Y <= endPoint.Y)
                    {
                        InkCanvas.SetTop(rect, endPoint.Y - shapeHeight);
                    }
                    else
                    {
                        InkCanvas.SetTop(rect, endPoint.Y);
                    }
                }

                if (btnShape.Content.Equals("Circle"))
                {
                    elli.Stroke = btnColor.Background;
                    elli.StrokeThickness = inkBoard.DefaultDrawingAttributes.Width;
                    elli.Width = shapeWidth;
                    elli.Height = shapeHeight;
                    if (strtPoint.X <= endPoint.X)
                    {
                        InkCanvas.SetLeft(elli, strtPoint.X);
                    }
                    else
                    {
                        InkCanvas.SetLeft(elli, endPoint.X);
                    }

                    if (strtPoint.Y <= endPoint.Y)
                    {
                        InkCanvas.SetTop(elli, endPoint.Y - shapeHeight);
                    }
                    else
                    {
                        InkCanvas.SetTop(elli, endPoint.Y);
                    }
                }

                if (btnShape.Content.Equals("Line"))
                {
                    line.Stroke = btnColor.Background;
                    line.StrokeThickness = inkBoard.DefaultDrawingAttributes.Width;
                    line.X1 = strtPoint.X;
                    line.Y1 = strtPoint.Y;
                    line.X2 = endPoint.X;
                    line.Y2 = endPoint.Y;
                }
            }
        }
        private void inkBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            draw = false;

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

        private void Strokes_StrokesChanged(object sender, System.Windows.Ink.StrokeCollectionChangedEventArgs e)
        {
            if (handle)
            {
                RedoStrokes.Push(new DoStroke
                {
                    ActionFlag = e.Added.Count > 0 ? "ADD" : "REMOVE",
                    Stroke = e.Added.Count > 0 ? e.Added[0] : e.Removed[0],
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

        private void btnSS_Click(object sender, RoutedEventArgs e)
        {

            //MessageBox.Show();
            //this.Hide();
            //Thread.Sleep(500);
            //stckBottom.Visibility = Visibility.Hidden;

            //pageScreenShot page = new pageScreenShot();
            //frameMain.Content = page;
            //System.Windows.Forms.SendKeys.SendWait("{PRTSC}");
            //page.imgSS.Source = Clipboard.GetImage();
            //Clipboard.Clear();
            //this.WindowStyle = WindowStyle.None;
            //this.Show();
        }

        private void btnShape_Click(object sender, RoutedEventArgs e)
        {
            if (btnShape.Content.Equals("Line"))
            {
                btnShape.Content = "Rect";
            }
            else if (btnShape.Content.Equals("Rect"))
            {
                btnShape.Content = "Circle";
            }
            else
            {
                btnShape.Content = "Line";
            }
        }
        private void wDraw_Deactivated(object sender, EventArgs e)
        {
            MessageBox.Show(inkBoard.Strokes.Count.ToString());
            if (inkBoard.Strokes.Count > 0)
            {
                String datetime = DateTime.Now.ToString("yyMMdd_HHmmss");
                String myDir = myPath + "\\drawings\\" + datetime + ".xaml";
                FileStream fs = File.Open(myDir, FileMode.Create);
                XamlWriter.Save(inkBoard, fs);
                fs.Close();
            }
        }
    }
    public struct DoStroke
    {
        public string ActionFlag { get; set; }
        public System.Windows.Ink.Stroke Stroke { get; set; }
    }
}
