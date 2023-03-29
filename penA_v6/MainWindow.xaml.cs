using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace penA_v6
{
    public partial class MainWindow : Window
    {
        public static Stack<DoStroke> RedoStrokes { get; set; }
        public static Stack<DoStroke> UndoStrokes { get; set; }

        public static Boolean handle = true;
        public static Boolean draw = false;
        public static Point strtPoint = new Point();
        public static Point endPoint = new Point();
        public static double shapeWidth;
        public static double shapeHeight;
        public static Rectangle rect;
        public static Rectangle selectionRectangle;
        public static Ellipse elli;
        public static Line line;
        public static Boolean haveDrawing = false;

        public static String filesPath = Directory.GetCurrentDirectory() + "\\drawings\\";
        public static String activeFilePath = null;
        public static int filesCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            RedoStrokes = new Stack<DoStroke>();
            UndoStrokes = new Stack<DoStroke>();
            inkBoard.Strokes.StrokesChanged += Strokes_StrokesChanged;

            stckDraw.Visibility = Visibility.Hidden;
            stckFiles.Visibility = Visibility.Hidden;
            inkBoard.Visibility = Visibility.Hidden;
            inkBoard.DefaultDrawingAttributes.Color = Colors.Red;
            inkBoard.DefaultDrawingAttributes.Width = 2;
            if (!Directory.Exists("drawings"))
            {
                Directory.CreateDirectory("drawings");
            }
            refreshFiles();
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
        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            draw = false;
            stckDraw.Visibility = Visibility.Visible;
            stckMain.Visibility = Visibility.Hidden;
            inkBoard.Visibility = Visibility.Visible;
            inkBoard.Strokes.Clear();
            inkBoard.Children.Clear();
            inkBoard.UseCustomCursor = true;
            inkBoard.Cursor = Cursors.Pen;
            inkBoard.EditingMode = InkCanvasEditingMode.Ink;

            btnMode.Content = "Pen";
            btnShape.Content = "Line";
            btnShape.Style = (Style)FindResource("roundButtonGray");
            btnUndo.Style = (Style)FindResource("roundButtonGray");
            btnRedo.Style = (Style)FindResource("roundButtonGray");
            btnClear.Style = (Style)FindResource("roundButtonGray");
        }

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            saveDraw();
            refreshFiles();
            stckMain.Visibility = Visibility.Visible;
            stckDraw.Visibility = Visibility.Hidden;
            inkBoard.Visibility = Visibility.Hidden;

            UndoStrokes.Clear();
            RedoStrokes.Clear();
            activeFilePath = null;

            btnUndo.Style = (Style)FindResource("roundButtonGray");
            btnRedo.Style = (Style)FindResource("roundButtonGray");
            btnClear.Style = (Style)FindResource("roundButtonGray");
        }
        public void saveDraw()
        {
            if (haveDrawing)
            {
                if (activeFilePath != null)
                {
                    FileStream fs = File.Open(activeFilePath, FileMode.Truncate);
                    XamlWriter.Save(inkBoard, fs);
                    fs.Close();
                }
                else
                {
                    String datetime = DateTime.Now.ToString("yyMMdd_HHmmss");
                    String myFile = filesPath + datetime + ".xaml";
                    FileStream fs = File.Open(myFile, FileMode.Create);
                    XamlWriter.Save(inkBoard, fs);
                    fs.Close();
                }
                haveDrawing = false;
                activeFilePath = null;
            }
        }
        public void refreshFiles()
        {
            stckFiles.Children.Clear();
            filesCount = new DirectoryInfo(filesPath).GetFiles("*.xaml").Length;
            if (filesCount > 0)
            {
                stckFiles.Visibility = Visibility.Visible;
                String[] arrFiles = Directory.GetFiles(filesPath, "*.xaml");
                Array.Reverse(arrFiles);
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
                    img.Source = renderBitmap;
                    img.Width = 80;
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = new FileInfo(file).Name.Substring(0, 13);
                    textBlock.TextAlignment = TextAlignment.Center;
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Children.Add(img);
                    stackPanel.Children.Add(textBlock);

                    Button button = new Button();
                    button.Content = stackPanel;
                    button.Margin = new Thickness(5);
                    button.Click += button_Button_Click;
                    button.Width = 120;
                    button.Height = 80;
                    button.Style = (Style)FindResource("roundButtonBlue");
                    stckFiles.Children.Add(button);
                }
            }
        }

        private void button_Button_Click(object sender, RoutedEventArgs e)
        {
            stckDraw.Visibility = Visibility.Visible;
            stckMain.Visibility = Visibility.Hidden;
            inkBoard.Visibility = Visibility.Visible;
            inkBoard.UseCustomCursor = true;
            inkBoard.Cursor = Cursors.Pen;

            activeFilePath = filesPath + (((e.OriginalSource as Button).Content as StackPanel).Children[1] as TextBlock).Text + ".xaml";
            FileStream fs = File.Open(activeFilePath, FileMode.Open, FileAccess.Read);
            InkCanvas savedCanvas = XamlReader.Load(fs) as InkCanvas;
            fs.Close();
            inkBoard.Strokes = savedCanvas.Strokes;
            inkBoard.Strokes.StrokesChanged += Strokes_StrokesChanged;

            foreach (Stroke stroke in inkBoard.Strokes)
            {
                RedoStrokes.Push(new DoStroke
                {
                    ActionFlag = "ADD",
                    Stroke = stroke
                });
            }

            btnUndo.Style = (Style)FindResource("roundButtonBlue");
            btnRedo.Style = (Style)FindResource("roundButtonGray");
            btnClear.Style = (Style)FindResource("roundButtonGray");
        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            if (inkBoard.DefaultDrawingAttributes.Color == Colors.Red)
            {
                inkBoard.DefaultDrawingAttributes.Color = Colors.DarkBlue;
                btnColor.Style = (Style)FindResource("roundButtonBlue");
            }
            else if (inkBoard.DefaultDrawingAttributes.Color == Colors.DarkBlue)
            {
                inkBoard.DefaultDrawingAttributes.Color = Colors.Black;
                btnColor.Style = (Style)FindResource("roundButtonBlack");
            }
            else
            {
                inkBoard.DefaultDrawingAttributes.Color = Colors.Red;
                btnColor.Style = (Style)FindResource("roundButtonRed");
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
                btnShape.Style = (Style)FindResource("roundButtonGray");
                draw = false;
            }
            else if (btnMode.Content.Equals("Eraser"))
            {
                btnMode.Content = "Select";
                inkBoard.EditingMode = InkCanvasEditingMode.Select;
                inkBoard.UseCustomCursor = false;
                btnShape.Style = (Style)FindResource("roundButtonGray");
                draw = false;
            }
            else if (btnMode.Content.Equals("Select"))
            {
                btnMode.Content = "Shape";
                inkBoard.EditingMode = InkCanvasEditingMode.None;
                inkBoard.UseCustomCursor = true;
                inkBoard.Cursor = Cursors.UpArrow;
                btnShape.Style = (Style)FindResource("roundButton");
            }
            else
            {
                inkBoard.EditingMode = InkCanvasEditingMode.Ink;
                btnMode.Content = "Pen";
                inkBoard.UseCustomCursor = true;
                inkBoard.Cursor = Cursors.Pen;
                btnShape.Style = (Style)FindResource("roundButtonGray");
                draw = false;
            }
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

        private void btnSS_Click(object sender, RoutedEventArgs e)
        {
            if (imgSS.Visibility == Visibility.Hidden)
            {
                btnMainPage.Visibility = Visibility.Hidden;
                btnSize.Visibility = Visibility.Hidden;
                btnColor.Visibility = Visibility.Hidden;
                btnMode.Visibility = Visibility.Hidden;
                btnShape.Visibility = Visibility.Hidden;
                btnUndo.Visibility = Visibility.Hidden;
                btnRedo.Visibility = Visibility.Hidden;
                btnClear.Visibility = Visibility.Hidden;

                inkBoard.Visibility = Visibility.Hidden;
                this.WindowStyle = WindowStyle.None;
                this.Hide();
                Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{PRTSC}");
                imgSS.Source = Clipboard.GetImage();
                Clipboard.Clear();
                imgSS.Visibility = Visibility.Visible;

                this.Show();
            }
            else
            {
                btnMainPage.Visibility = Visibility.Visible;
                btnSize.Visibility = Visibility.Visible;
                btnColor.Visibility = Visibility.Visible;
                btnMode.Visibility = Visibility.Visible;
                btnShape.Visibility = Visibility.Visible;
                btnUndo.Visibility = Visibility.Visible;
                btnRedo.Visibility = Visibility.Visible;
                btnClear.Visibility = Visibility.Visible;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                imgSS.Visibility = Visibility.Hidden;
                imgSS.Source = null;
                inkBoard.Visibility = Visibility.Visible;
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
            btnRedo.Style = (Style)FindResource("roundButton");

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
                btnUndo.Style = (Style)FindResource("roundButtonGray");
                btnClear.Style = (Style)FindResource("roundButtonGray");
            }
            handle = true;
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
            btnUndo.Style = (Style)FindResource("roundButton");
            btnClear.Style = (Style)FindResource("roundButton");

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
                btnRedo.Style = (Style)FindResource("roundButtonGray");
            }
            handle = true;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            inkBoard.Strokes.Clear();
            inkBoard.Children.Clear();
            UndoStrokes.Clear();
            RedoStrokes.Clear();

            btnUndo.Style = (Style)FindResource("roundButtonGray");
            btnClear.Style = (Style)FindResource("roundButtonGray");
            btnRedo.Style = (Style)FindResource("roundButtonGray");
        }

        private void inkBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            draw = false;
            haveDrawing = true;
            endPoint = e.GetPosition(inkBoard);
            btnUndo.Style = (Style)FindResource("roundButton");

            if (inkBoard.EditingMode == InkCanvasEditingMode.Ink)
            {
                btnRedo.Style = (Style)FindResource("roundButtonGray");
            }
            btnClear.Style = (Style)FindResource("roundButton");
            UndoStrokes.Clear();

            if (btnMode.Content.Equals("Shape"))
            {
                if (btnShape.Content.Equals("Rect"))
                {
                    Stroke rectStroke = new Stroke(new StylusPointCollection(new List<Point>
                    {
                        new Point(InkCanvas.GetLeft(rect),InkCanvas.GetTop(rect)),
                        new Point(InkCanvas.GetLeft(rect)+rect.Width,InkCanvas.GetTop(rect)),
                        new Point(InkCanvas.GetLeft(rect)+rect.Width,InkCanvas.GetTop(rect)+rect.Height),
                        new Point(InkCanvas.GetLeft(rect),InkCanvas.GetTop(rect)+rect.Height),
                        new Point(InkCanvas.GetLeft(rect),InkCanvas.GetTop(rect))
                    }));
                    inkBoard.Children.Remove(rect);
                    rectStroke.DrawingAttributes.Color = ((SolidColorBrush)btnColor.Background).Color;
                    rectStroke.DrawingAttributes.Width = inkBoard.DefaultDrawingAttributes.Width;
                    rectStroke.DrawingAttributes.Height = inkBoard.DefaultDrawingAttributes.Width;
                    inkBoard.Strokes.Add(rectStroke);

                }
                if (btnShape.Content.Equals("Circle"))
                {
                    StylusPointCollection points = new StylusPointCollection();
                    Point center = new Point((strtPoint.X + endPoint.X) / 2, (strtPoint.Y + endPoint.Y) / 2);
                    double radiusX = shapeWidth / 2;
                    double radiusY = shapeHeight / 2;
                    for (int i = 0; i <= 360; i += 5)
                    {
                        double angle = i * Math.PI / 180;
                        double x = center.X + radiusX * Math.Cos(angle);
                        double y = center.Y + radiusY * Math.Sin(angle);
                        points.Add(new StylusPoint(x, y));
                    }
                    Stroke stroke = new Stroke(points);
                    inkBoard.Children.Remove(elli);
                    Stroke elliStroke = new Stroke(points);
                    elliStroke.DrawingAttributes.Color = ((SolidColorBrush)btnColor.Background).Color;
                    elliStroke.DrawingAttributes.Width = inkBoard.DefaultDrawingAttributes.Width;
                    elliStroke.DrawingAttributes.Height = inkBoard.DefaultDrawingAttributes.Height;
                    inkBoard.Strokes.Add(elliStroke);
                }
                if (btnShape.Content.Equals("Line"))
                {
                    Stroke lineStroke = new Stroke(new StylusPointCollection(new List<Point>
                    {
                        strtPoint,endPoint
                    }));
                    inkBoard.Children.Remove(line);
                    lineStroke.DrawingAttributes.Color = ((SolidColorBrush)btnColor.Background).Color;
                    lineStroke.DrawingAttributes.Width = inkBoard.DefaultDrawingAttributes.Width;
                    inkBoard.Strokes.Add(lineStroke);
                }
            }
        }
        private void inkBoard_MouseDown(object sender, MouseButtonEventArgs e)
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
        private void wMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveDraw();
        }
    }
    public struct DoStroke
    {
        public string ActionFlag { get; set; }
        public System.Windows.Ink.Stroke Stroke { get; set; }
    }
}
