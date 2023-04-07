using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace penA_v7
{
    public partial class MainWindow : Window
    {
        public static Stack<Object> stackUndo = new Stack<Object>();
        public static Stack<Object> stackRedo = new Stack<Object>();


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
            inkBoard.Strokes.StrokesChanged += Strokes_StrokesChanged;

            stckDraw.Visibility = Visibility.Hidden;
            inkBoard.Visibility = Visibility.Hidden;
            inkBoard.DefaultDrawingAttributes.Color = Colors.Red;
            inkBoard.DefaultDrawingAttributes.Width = 2;
            if (!Directory.Exists("drawings"))
            {
                Directory.CreateDirectory("drawings");
                Directory.CreateDirectory("drawings/tempimg");

            }
            else
            {
                if (!Directory.Exists("drawings/tempimg"))
                {
                    Directory.CreateDirectory("drawings/tempimg");
                }
            }
            refreshFiles();
        }
        private void Strokes_StrokesChanged(object sender, System.Windows.Ink.StrokeCollectionChangedEventArgs e)
        {
            if (handle && inkBoard.EditingMode == InkCanvasEditingMode.Ink)
            {
                stackUndo.Push(e.Added[0]);
            }
        }
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            handle = false;

            btnRedo.Style = (Style)FindResource("roundButton");
            if (stackUndo.Count == 1)
            {
                btnUndo.Style = (Style)FindResource("roundButtonGray");
            }

            if (stackUndo.Count > 0)
            {
                Object obj = new object();
                obj = stackUndo.Pop();
                if (obj.GetType().ToString() == "System.Windows.Ink.Stroke")
                {
                    Stroke stroke = obj as Stroke;
                    stackRedo.Push(stroke);
                    inkBoard.Strokes.Remove(stroke);
                }
                else if (obj.GetType().ToString() == "System.Windows.Shapes.Line")
                {
                    Line line = obj as Line;
                    stackRedo.Push(line);
                    inkBoard.Children.Remove(line);
                }
                else if (obj.GetType().ToString() == "System.Windows.Shapes.Rectangle")
                {
                    Rectangle rect = obj as Rectangle;
                    stackRedo.Push(rect);
                    inkBoard.Children.Remove(rect);
                }
                else if (obj.GetType().ToString() == "System.Windows.Shapes.Ellipse")
                {
                    Ellipse elli = obj as Ellipse;
                    stackRedo.Push(elli);
                    inkBoard.Children.Remove(elli);
                }
                else
                {
                    System.Windows.Controls.Image img = obj as System.Windows.Controls.Image;
                    stackRedo.Push(img);
                    inkBoard.Children.Remove(img);
                }
            }
            handle = true;
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            handle = false;

            btnUndo.Style = (Style)FindResource("roundButton");
            if (stackRedo.Count == 1)
            {
                btnRedo.Style = (Style)FindResource("roundButtonGray");
            }

            if (stackRedo.Count > 0)
            {
                Object obj = new object();
                obj = stackRedo.Pop();
                if (obj.GetType().ToString() == "System.Windows.Ink.Stroke")
                {
                    Stroke stroke = obj as Stroke;
                    stackUndo.Push(stroke);
                    inkBoard.Strokes.Add(stroke);
                }
                else if (obj.GetType().ToString() == "System.Windows.Shapes.Line")
                {
                    Line line = obj as Line;
                    stackUndo.Push(line);
                    inkBoard.Children.Add(line);
                }
                else if (obj.GetType().ToString() == "System.Windows.Shapes.Rectangle")
                {
                    Rectangle rect = obj as Rectangle;
                    stackUndo.Push(rect);
                    inkBoard.Children.Add(rect);
                }
                else if (obj.GetType().ToString() == "System.Windows.Shapes.Ellipse")
                {
                    Ellipse elli = obj as Ellipse;
                    stackUndo.Push(elli);
                    inkBoard.Children.Add(elli);
                }
                else
                {
                    System.Windows.Controls.Image img = obj as System.Windows.Controls.Image;
                    stackUndo.Push(img);
                    inkBoard.Children.Add(img);
                }
            }
            handle = true;
        }

        private void btnNewDrawing_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
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
            handle = true;
        }

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
            saveDraw();
            refreshFiles();
            stckMain.Visibility = Visibility.Visible;
            stckDraw.Visibility = Visibility.Hidden;
            inkBoard.Visibility = Visibility.Hidden;

            stackUndo.Clear();
            stackRedo.Clear();
            activeFilePath = null;

            btnUndo.Style = (Style)FindResource("roundButtonGray");
            btnRedo.Style = (Style)FindResource("roundButtonGray");
            btnClear.Style = (Style)FindResource("roundButtonGray");
            handle = true;
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
            handle = false;
            stckFiles.Children.Clear();

            Button btnNew = new Button();
            btnNew.Content = "NEW DRAWING";
            btnNew.Width = 120;
            btnNew.Height = 80;
            btnNew.Style = (Style)FindResource("roundButtonBlue");
            btnNew.Click += btnNewDrawing_Click;
            stckFiles.Children.Add(btnNew);

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

                    System.Windows.Controls.Image img = new System.Windows.Controls.Image();
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
                    //button.ContextMenu = (ContextMenu)FindResource("MyContextMenu");
                    stckFiles.Children.Add(button);
                }
            }
            handle = true;
        }

        private void button_Button_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
            stckDraw.Visibility = Visibility.Visible;
            stckMain.Visibility = Visibility.Hidden;
            inkBoard.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            inkBoard.UseCustomCursor = true;
            inkBoard.Cursor = Cursors.Pen;
            btnMode.Content = "Pen";
            btnShape.Content = "Line";
            btnShape.Style = (Style)FindResource("roundButtonGray");

            inkBoard.EditingMode = InkCanvasEditingMode.Ink;
            inkBoard.DefaultDrawingAttributes.Color = Colors.Red;
            inkBoard.DefaultDrawingAttributes.Width = 2;
            btnColor.Style = (Style)FindResource("roundButtonRed");
            btnSize.Content = "Size 2";


            activeFilePath = filesPath + (((e.OriginalSource as Button).Content as StackPanel).Children[1] as TextBlock).Text + ".xaml";
            FileStream fs = File.Open(activeFilePath, FileMode.Open, FileAccess.Read);
            InkCanvas savedCanvas = XamlReader.Load(fs) as InkCanvas;
            fs.Close();
            inkBoard.Strokes = savedCanvas.Strokes;

            var childrenList = savedCanvas.Children.Cast<UIElement>().ToArray();
            foreach (var c in childrenList)
            {
                savedCanvas.Children.Remove(c);
                inkBoard.Children.Add(c);
            }

            inkBoard.Strokes.StrokesChanged += Strokes_StrokesChanged;

            btnUndo.Style = (Style)FindResource("roundButtonGray");
            btnRedo.Style = (Style)FindResource("roundButtonGray");
            btnClear.Style = (Style)FindResource("roundButtonGray");

            handle = true;
        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
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
            handle = true;
        }

        private void btnSize_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
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
            handle = true;
        }
        private void btnMode_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
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
            handle = true;
        }
        private void btnShape_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
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
            handle = true;
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
                btnSS.Visibility = Visibility.Hidden;
                btnPicture.Visibility = Visibility.Hidden;
                btnUndo.Visibility = Visibility.Hidden;
                btnRedo.Visibility = Visibility.Hidden;
                btnClear.Visibility = Visibility.Hidden;

                canSS.Visibility = Visibility.Visible;
                inkBoard.Visibility = Visibility.Hidden;
                this.WindowStyle = WindowStyle.None;
                this.Hide();
                Thread.Sleep(100);
                System.Windows.Forms.SendKeys.SendWait("{PRTSC}");

                imgSS.Source = Clipboard.GetImage();
                imgSS.Visibility = Visibility.Visible;
                this.Show();
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            handle = false;
            inkBoard.Strokes.Clear();
            inkBoard.Children.Clear();
            stackUndo.Clear();
            stackRedo.Clear();

            btnUndo.Style = (Style)FindResource("roundButtonGray");
            btnClear.Style = (Style)FindResource("roundButtonGray");
            btnRedo.Style = (Style)FindResource("roundButtonGray");
            handle = true;
        }
        private void inkBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            draw = false;
            haveDrawing = true;
            endPoint = e.GetPosition(inkBoard);
            btnUndo.Style = (Style)FindResource("roundButton");
            btnClear.Style = (Style)FindResource("roundButton");
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
                    stackUndo.Push(rect);
                }
                if (btnShape.Content.Equals("Circle"))
                {
                    elli = new Ellipse();
                    inkBoard.Children.Add(elli);
                    stackUndo.Push(elli);
                }
                if (btnShape.Content.Equals("Line"))
                {
                    line = new Line();
                    inkBoard.Children.Add(line);
                    stackUndo.Push(line);
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

        private void canSS_MouseUp(object sender, MouseButtonEventArgs e)
        {
            endPoint = e.GetPosition(canSS);
            draw = false;

            btnMainPage.Visibility = Visibility.Visible;
            btnSize.Visibility = Visibility.Visible;
            btnColor.Visibility = Visibility.Visible;
            btnMode.Visibility = Visibility.Visible;
            btnShape.Visibility = Visibility.Visible;
            btnSS.Visibility = Visibility.Visible;
            btnPicture.Visibility = Visibility.Visible;
            btnUndo.Visibility = Visibility.Visible;
            btnRedo.Visibility = Visibility.Visible;
            btnClear.Visibility = Visibility.Visible;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            imgSS.Visibility = Visibility.Hidden;
            imgSS.Source = null;
            inkBoard.Visibility = Visibility.Visible;
            canSS.Visibility = Visibility.Hidden;

            System.Windows.Controls.Image cimg = new System.Windows.Controls.Image();
            CroppedBitmap cBitMap = new CroppedBitmap((BitmapSource)Clipboard.GetImage(), new Int32Rect((int)Canvas.GetLeft(rect), (int)Canvas.GetTop(rect), (int)rect.Width, (int)rect.Height));
            Clipboard.Clear();

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(cBitMap));

            String datetime = DateTime.Now.ToString("yyMMdd_HHmmss");
            String addFilesPath = filesPath + "/tempimg/" + datetime + ".bmp";

            using (var fileStream = new System.IO.FileStream(addFilesPath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
                fileStream.Close();
            }

            System.Windows.Controls.Image cimG = new System.Windows.Controls.Image();
            cimg.Source = new BitmapImage(new Uri(addFilesPath, UriKind.Absolute));
            cimg.Margin = new Thickness(10);
            inkBoard.Children.Add(cimg);
            haveDrawing = true;
            stackUndo.Push(cimg);
            btnUndo.Style = (Style)FindResource("roundButton");
            btnClear.Style = (Style)FindResource("roundButton");
            canSS.Children.Remove(rect);
        }
        private void canSS_MouseMove(object sender, MouseEventArgs e)
        {
            endPoint = e.GetPosition(canSS);
            shapeWidth = Math.Abs(endPoint.X - strtPoint.X);
            shapeHeight = Math.Abs(endPoint.Y - strtPoint.Y);

            if (draw)
            {
                rect.Stroke = Brushes.Red;
                rect.StrokeThickness = 2;
                rect.Width = shapeWidth;
                rect.Height = shapeHeight;
                if (strtPoint.X <= endPoint.X)
                {
                    Canvas.SetLeft(rect, strtPoint.X);
                }
                else
                {
                    Canvas.SetLeft(rect, endPoint.X);
                }

                if (strtPoint.Y <= endPoint.Y)
                {
                    Canvas.SetTop(rect, endPoint.Y - shapeHeight);
                }
                else
                {
                    Canvas.SetTop(rect, endPoint.Y);
                }
            }
        }
        private void canSS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            draw = true;
            strtPoint = e.GetPosition(canSS);
            rect = new Rectangle();
            canSS.Children.Add(rect);
        }

        private void btnPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                System.Windows.Controls.Image addImage = new System.Windows.Controls.Image();
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                String datetime = DateTime.Now.ToString("yyMMdd_HHmmss");
                String ssFilesPath = filesPath + "/tempimg/" + datetime + ".bmp";

                using (var fileStream = new System.IO.FileStream(ssFilesPath, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                    fileStream.Close();
                }

                addImage.Source = new BitmapImage(new Uri(ssFilesPath));
                if (bitmap.PixelHeight > ActualHeight)
                {
                    addImage.Height = ActualHeight - stckDraw.Height * 2;
                }

                inkBoard.Children.Add(addImage);
                stackUndo.Push(addImage);
                btnUndo.Style = (Style)FindResource("roundButton");
                btnClear.Style = (Style)FindResource("roundButton");
                haveDrawing = true;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete file? " + filesPath, "FILE DELETE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                handle = false;
                FileStream fs = File.Open(activeFilePath, FileMode.Open, FileAccess.Read);
                InkCanvas savedCanvas = XamlReader.Load(fs) as InkCanvas;
                fs.Close();
                var childrenList = savedCanvas.Children.Cast<UIElement>().ToArray();
                File.Delete(System.IO.Path.Combine(filesPath, activeFilePath.Substring(activeFilePath.Length - 18, 18)));

                inkBoard.Children.Clear();
                savedCanvas.Children.Clear();
                stackUndo.Clear();
                stackRedo.Clear();
                refreshFiles();

                //foreach (var c in childrenList)
                //{
                //    if (c.GetType().ToString().Equals("System.Windows.Controls.Image"))
                //    {
                //        System.Windows.Controls.Image img = c as System.Windows.Controls.Image;
                //        File.Delete(System.IO.Path.Combine(filesPath + "tempimg\\", img.Source.ToString().Substring(img.Source.ToString().Length - 17, 17)));
                //    }
                //}

                stckMain.Visibility = Visibility.Visible;
                stckDraw.Visibility = Visibility.Hidden;
                inkBoard.Visibility = Visibility.Hidden;
                activeFilePath = null;
                btnUndo.Style = (Style)FindResource("roundButtonGray");
                btnRedo.Style = (Style)FindResource("roundButtonGray");
                btnClear.Style = (Style)FindResource("roundButtonGray");
                handle = true;
            }
        }
    }
}
