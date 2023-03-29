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
    public partial class pageScreenShot : Page
    {
        private bool isDragging = false;
        private Point anchorPoint = new Point();
        public pageScreenShot()
        {
            InitializeComponent();
            Gridimage1.MouseLeftButtonDown += new MouseButtonEventHandler(image1_MouseLeftButtonDown);
            Gridimage1.MouseMove += new MouseEventHandler(image1_MouseMove);
            Gridimage1.MouseLeftButtonUp += new MouseButtonEventHandler(image1_MouseLeftButtonUp);
            //Go.IsEnabled = false;
            //image2.Source = null;

            canVas.Width = Gridimage1.Width;
            canVas.Height = Gridimage1.Height;
        }

        //private void imgSS_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    Rectangle rect = new Rectangle();
        //    rect.Width = 150;
        //    rect.Height = 200;
        //    rect.Stroke = new SolidColorBrush(Colors.Black);
        //    rect.Fill = new SolidColorBrush(Colors.Black);
        //    rect.VerticalAlignment = VerticalAlignment.Center;
        //    rect.HorizontalAlignment = HorizontalAlignment.Center;

        //    image1.Cursor = Cursors.Cross;
        //    image1.Width = pageSS.ActualWidth;
        //    image1.Height = pageSS.ActualHeight;
        //    image1.Stretch = Stretch.Fill;

        //    Gridimage1.Children.Add(rect);
        //}

        // TRYING
        //private void Go_Click(object sender, RoutedEventArgs e)
        //{
        //    if (image1.Source != null)
        //    {
        //        Rect rect1 = new Rect(Canvas.GetLeft(selectionRectangle), Canvas.GetTop(selectionRectangle), selectionRectangle.Width, selectionRectangle.Height);
        //        System.Windows.Int32Rect rcFrom = new System.Windows.Int32Rect();
        //        rcFrom.X = (int)((rect1.X) * (image1.Source.Width) / (image1.Width));
        //        rcFrom.Y = (int)((rect1.Y) * (image1.Source.Height) / (image1.Height));
        //        rcFrom.Width = (int)((rect1.Width) * (image1.Source.Width) / (image1.Width));
        //        rcFrom.Height = (int)((rect1.Height) * (image1.Source.Height) / (image1.Height));
        //        BitmapSource bs = new CroppedBitmap(image1.Source as BitmapSource, rcFrom);
        //        //image2.Source = bs;
        //    }
        //}


        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDragging == false)
            {
                anchorPoint.X = e.GetPosition(imgSS).X;
                anchorPoint.Y = e.GetPosition(imgSS).Y;
                isDragging = true;
            }

        }

        private void image1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                double x = e.GetPosition(imgSS).X;
                double y = e.GetPosition(imgSS).Y;
                selectionRectangle.SetValue(Canvas.LeftProperty, Math.Min(x, anchorPoint.X));
                selectionRectangle.SetValue(Canvas.TopProperty, Math.Min(y, anchorPoint.Y));
                selectionRectangle.Width = Math.Abs(x - anchorPoint.X);
                selectionRectangle.Height = Math.Abs(y - anchorPoint.Y);

                if (selectionRectangle.Visibility != Visibility.Visible)
                {
                    selectionRectangle.Visibility = Visibility.Visible;
                }
                    
            }
        }

        private void image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                //if (selectionRectangle.Width > 0)
                //{
                //    //Go.Visibility = System.Windows.Visibility.Visible;
                //    //Go.IsEnabled = true;
                //}

                if (selectionRectangle.Visibility != Visibility.Visible)
                {
                    selectionRectangle.Visibility = Visibility.Visible;
                }
            }
            ///////////////////

            if (imgSS.Source != null)
            {
                Rect rect1 = new Rect(Canvas.GetLeft(selectionRectangle), Canvas.GetTop(selectionRectangle), selectionRectangle.Width, selectionRectangle.Height);
                System.Windows.Int32Rect rcFrom = new System.Windows.Int32Rect();
                rcFrom.X = (int)((rect1.X)); // * (image1.Source.Width) / (image1.Width));
                rcFrom.Y = (int)((rect1.Y)); // * (image1.Source.Height) / (image1.Height));
                rcFrom.Width = (int)((rect1.Width)); // * (image1.Source.Width) / (image1.Width));
                rcFrom.Height = (int)((rect1.Height)); // * (image1.Source.Height) / (image1.Height));
                BitmapSource bs = new CroppedBitmap(imgSS.Source as BitmapSource, rcFrom); // new Int32Rect((int)Canvas.GetLeft(selectionRectangle), (int)Canvas.GetTop(selectionRectangle), (int)selectionRectangle.Width, (int)selectionRectangle.Height));

                Clipboard.SetImage(bs);

                //image2.Source = bs;
            }

            //MessageBox.Show(Canvas.GetLeft(selectionRectangle).ToString());


            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();

                Image img = new Image();
                img.Source = Clipboard.GetImage();

                InkCanvas.SetTop(img, 100);
                InkCanvas.SetLeft(img, 100);

                pageDraw.inkCan.Children.Add(img);

                //pageDraw.inkCan.Strokes.Add(img);
                //inkBoard.Children.Add(img);


            }

            ///////////////
        }

        private void RestRect()
        {
            selectionRectangle.Visibility = Visibility.Collapsed;
            isDragging = false;
        }
        // END TRYING
    }
}
