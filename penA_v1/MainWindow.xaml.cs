using Microsoft.Win32;
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

namespace penA
{
    public partial class MainWindow : Window
    {
        public static w_Menu wm = new w_Menu();
        public static InkCanvas ink = new InkCanvas();
        //public static Brush penBrush = Brushes.Black;
        public static int penWidth = 5;
        public static bool isPaint = true;
        //public static bool isErase = false;
        public static int choice = 1;
        //public static Point currentPoint = new Point();

        public enum penSize
        {
            small = 5,
            medium = 10,
            large = 15
        }

        public MainWindow()
        {
            InitializeComponent();
            this.AddChild(ink);
            wm.Top = 70;
            wm.Left = 10;
            wm.Show();
        }
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            //MessageBox.Show("Basıldı...");
        }
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            //MessageBox.Show("Bırakıldı...");
        }

        private void drawEllipse(Brush eColor, Point ePoint)
        {
            //Ellipse ellipse = new Ellipse();
            //ellipse.Fill = eColor;
            //ellipse.Width = penWidth;
            //ellipse.Height = penWidth;
            //Canvas.SetLeft(ellipse, ePoint.X);
            //Canvas.SetTop(ellipse, ePoint.Y);
            //imgCanvas.Children.Add(ellipse);
        }

        private void w_penA_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void imgCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (isPaint)
            //{
            //    currentPoint = e.GetPosition(this);
            //}
        }
        private void imgCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //if (isPaint)
            //{
            //    if (choice == 1)
            //    {
            //        Line line = new Line();
            //        line.Stroke = penBrush;
            //        line.Width = penWidth;
            //        line.Height = penWidth;
            //        line.X1 = currentPoint.X;
            //        line.Y1 = currentPoint.Y;
            //        line.X2 = e.GetPosition(this).X;
            //        line.Y2 = e.GetPosition(this).Y;
            //        currentPoint = e.GetPosition(this);

            //        imgCanvas.Children.Add(line);


            //        //Point mousePos = e.GetPosition(imgCanvas);
            //        //drawEllipse(penBrush, mousePos);
            //    }
            //}
        }

        private void imgCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        //    isPaint = true;
        }

        private void imgCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //isPaint = false;
        }

        /*


        namespace sdp_02
            {
                public partial class Form1 : Form
                {
                    public Form1()
                    {
                        InitializeComponent();
                        this.Width = 950;
                        this.Height = 700;
                        bm = new Bitmap(pic.Width, pic.Height);
                        g = Graphics.FromImage(bm);
                        g.Clear(Color.White);
                        pic.Image = bm;

                    }
                    Bitmap bm;
                    Graphics g;
                    bool paint = false;
                    Point px, py;
                    Pen p = new Pen(Color.Black, 1);
                    Pen er = new Pen(Color.White, 10);
                    int index, x, y, sx, sy, cx, cy;

                    ColorDialog cd = new ColorDialog();
                    Color new_color;
                    private void pic_MouseDown(object sender, MouseEventArgs e)
                    {
                        paint = true;
                        py = e.Location;

                        cx = e.X;
                        cy = e.Y;

                    }

                    private void pic_Paint(object sender, PaintEventArgs e)
                    {
                        Graphics g = e.Graphics;

                        if (paint)
                        {
                            if (index == 3)
                            {
                                g.DrawEllipse(p, cx, cy, sx, sy);
                            }
                            if (index == 4)
                            {
                                g.DrawRectangle(p, cx, cy, sx, sy);
                            }
                            if (index == 5)
                            {
                                g.DrawLine(p, cx, cy, x, y);
                            }
                        }
                    }

                    private void btn_clear_Click(object sender, EventArgs e)
                    {
                        g.Clear(Color.White);
                        pic.Image = bm;
                        index = 0;
                    }

                    private void btn_color_Click(object sender, EventArgs e)
                    {
                        cd.ShowDialog();
                        new_color = cd.Color;
                        pic_color.BackColor = cd.Color;
                        p.Color = cd.Color;
                    }

                    private void pic_MouseMove(object sender, MouseEventArgs e)
                    {
                        if (paint)
                        {
                            if (index == 1)
                            {
                                px = e.Location;
                                g.DrawLine(p, px, py);
                                py = px;
                            }
                            if (index == 2)
                            {
                                px = e.Location;
                                g.DrawLine(er, px, py);
                                py = px;
                            }
                        }
                        pic.Refresh();

                        x = e.X;
                        y = e.Y;
                        sx = e.X - cx;
                        sy = e.Y - cy;

                    }

                    private void pencil_Click(object sender, EventArgs e)
                    {
                        index = 1;
                    }

                    private void btn_eraser_Click(object sender, EventArgs e)
                    {
                        index = 2;
                    }

                    private void btn_elips_Click(object sender, EventArgs e)
                    {
                        index = 3;
                    }

                    private void btn_rectangle_Click(object sender, EventArgs e)
                    {
                        index = 4;
                    }

                    private void btn_line_Click(object sender, EventArgs e)
                    {
                        index = 5;
                    }

                    private void btn_fill_Click(object sender, EventArgs e)
                    {
                        index = 6;
                    }

                    private void pic_MouseUp(object sender, MouseEventArgs e)
                    {
                        paint = false;

                        sx = x - cx;
                        sy = y - cy;

                        if (index == 3)
                        {
                            g.DrawEllipse(p, cx, cy, sx, sy);
                        }
                        if (index == 4)
                        {
                            g.DrawRectangle(p, cx, cy, sx, sy);
                        }
                        if (index == 5)
                        {
                            g.DrawLine(p, cx, cy, x, y);
                        }
                    }
                    static Point set_point(PictureBox pb, Point pt)
                    {
                        float px = 1f * pb.Image.Width / pb.Width;
                        float py = 1f * pb.Image.Height / pb.Height;
                        return new Point((int)(pt.X * px), (int)(pt.Y * py));
                    }
                    private void color_picker_MouseClick(object sender, MouseEventArgs e)
                    {
                        Point point = set_point(color_picker, e.Location);
                        pic_color.BackColor = ((Bitmap)color_picker.Image).GetPixel(point.X, point.Y);
                        new_color = pic_color.BackColor;
                        p.Color = pic_color.BackColor;
                    }

                    private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color old_color, Color new_color)
                    {
                        Color cx = bm.GetPixel(x, y);
                        if (cx == old_color)
                        {
                            sp.Push(new Point(x, y));
                            bm.SetPixel(x, y, new_color);
                        }
                    }

                    public void Fill(Bitmap bm, int x, int y, Color new_clr)
                    {
                        Color old_color = bm.GetPixel(x, y);
                        Stack<Point> pixel = new Stack<Point>();
                        pixel.Push(new Point(x, y));
                        bm.SetPixel(x, y, new_clr);
                        if (old_color == new_clr) return;

                        while (pixel.Count > 0)
                        {
                            Point pt = (Point)pixel.Pop();
                            if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                            {
                                validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_clr);
                                validate(bm, pixel, pt.X, pt.Y - 1, old_color, new_clr);
                                validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_clr);
                                validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_clr);
                            }
                        }

                    }
                    private void pic_MouseClick(object sender, MouseEventArgs e)
                    {
                        if (index == 6)
                        {
                            Point point = set_point(pic, e.Location);
                            Fill(bm, point.X, point.Y, new_color);
                        }
                    }
                    private void btn_save_Click(object sender, EventArgs e)
                    {
                        var sfd = new SaveFileDialog();
                        sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*)";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap btm = bm.Clone(new Rectangle(0, 0, pic.Width, pic.Height), bm.PixelFormat);
                            btm.Save(sfd.FileName, ImageFormat.Jpeg);
                            MessageBox.Show("Image saved succesfully");
                        }
                    }
                }
            }





        */
    }
}
