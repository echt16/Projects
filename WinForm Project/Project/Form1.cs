using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cw
{
    public partial class Form1 : Form
    {
        List<PointF> eraser;
        bool a;
        bool b;
        bool c;
        bool save;
        BindingList<Figure> figures;
        Point nstart;
        short figure;
        int depth;
        string path;
        System.Drawing.Drawing2D.DashStyle style;
        System.Drawing.Drawing2D.LineCap startlinestyle;
        System.Drawing.Drawing2D.LineCap endlinestyle;
        System.Drawing.Drawing2D.DashCap linedashcup;
        Font font;
        Color color;
        public Form1()
        {
            InitializeComponent();
            save = true;
            eraser = new List<PointF>();
            a = false;
            b = false;
            c = false;
            nRectangle.index = 0;
            Triangle.index = 0;
            Ellipse.index = 0;
            cw.Text.index = 0;
            figures = new BindingList<Figure>();
            startlinestyle = System.Drawing.Drawing2D.LineCap.Round;
            endlinestyle = System.Drawing.Drawing2D.LineCap.Round;
            linedashcup = System.Drawing.Drawing2D.DashCap.Round;
            font = new Font("Verdana", 5);
            toolStripButton1.ToolTipText = "Save";
            toolStripButton2.ToolTipText = "Change color";
            toolStripDropDownButton2.ToolTipText = "Change style";
            toolStripButton4.ToolTipText = "Delete figure";
            toolStripTextBox1.ReadOnly = true;
            toolStripComboBox1.ToolTipText = "Width";
            toolStripComboBox1.Items.AddRange(new string[] { "5", "10", "15", "20", "25", "30", "35", "40", "45", "50" });
            toolStripComboBox3.Items.AddRange(new string[] { "5", "10", "15", "20", "25", "30", "35", "40", "45", "50" });
            color = Color.Black;
            path = "";
            figure = 1;
            style = System.Drawing.Drawing2D.DashStyle.Solid;
            depth = 5;
            toolStripTextBox1.Text = color.Name;
            toolStripTextBox1.ToolTipText = "Color";
            toolStripButton4.ToolTipText = "Paint over";
            toolStripButton3.ToolTipText = "Erase";
            toolStripDropDownButton1.ToolTipText = "Change figure";
            toolStripTextBox2.ToolTipText = "Enter text";
            toolStripButton5.ToolTipText = "Font type";
            toolStripButton6.ToolTipText = "Open";
            toolStripButton7.ToolTipText = "Exit";
            toolStripButton8.ToolTipText = "Customise figure";
            this.ContextMenuStrip = contextMenuStrip1;
            
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (path == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image Files (*.png;*.jpg;*.pdf)|*.png;*.jpeg;*pdf|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog.FileName;
                }
                else
                {
                    path = "D:\\New image.png";
                }
                Thread.Sleep(500);
            }
            save = true;
            Rectangle rect = RectangleToScreen(DesktopBounds);
            using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(rect.Location, new Point(0, 0), rect.Size);
                }
                bitmap.Save(path);
            }
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files (*.png;*.jpg;*.pdf)|*.png;*.jpeg;*pdf|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
            }
            else
            {
                path = "D:\\New image";
            }
            Thread.Sleep(500);
            save = true;
            Rectangle rect = RectangleToScreen(DesktopBounds);
            using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(rect.Location, new Point(0, 0), rect.Size);
                }
                bitmap.Save(path);
            }
        }

        private void Color_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                this.color = color.Color;
                toolStripTextBox1.Text = color.Color.Name;
                save = false;
            }
        }
        private void Solid_Click(object sender, EventArgs e)
        {
            style = System.Drawing.Drawing2D.DashStyle.Solid;
        }
        private void Dot_Click(object sender, EventArgs e)
        {
            style = System.Drawing.Drawing2D.DashStyle.Dot;
        }
        private void DashDot_Click(object sender, EventArgs e)
        {
            style = System.Drawing.Drawing2D.DashStyle.DashDot;
        }
        private void Dash_Click(object sender, EventArgs e)
        {
            style = System.Drawing.Drawing2D.DashStyle.Dash;
        }
        private void DashDotDot_Click(object sender, EventArgs e)
        {
            style = System.Drawing.Drawing2D.DashStyle.DashDotDot;
        }
        private void Rectangle_Click(object sender, EventArgs e)
        {
            
            figure = 1;
        }
        private void Triangle_Click(object sender, EventArgs e)
        {
            
            figure = 2;
        }
        private void Ellipse_Click(object sender, EventArgs e)
        {
            
            figure = 3;
        }
        private void Eraser_Click(object sender, EventArgs e)
        {
            figure = 10;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (figure != 6)
                {
                    nstart.X = e.X;
                    nstart.Y = e.Y;
                }
                if (figure == 7)
                {
                    c = true;
                    figures.Add(new Pencil() { Outline = color, Fill = BackColor, style = this.style, Depth = this.depth });
                }
                if (figure == 10)
                {
                    a = true;
                }
            }
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "")
            {
                depth = 5;
            }
            else
            {
                int g;
                if (int.TryParse(toolStripComboBox1.Text, out g))
                {
                    depth = g;
                    toolStripComboBox3.Text = g.ToString();
                }
                else
                {
                    depth = 5;
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (figure != 6)
                {
                    Graphics g = CreateGraphics();
                    Pen pen = new Pen(this.color, this.depth);
                    pen.DashStyle = this.style;
                    try
                    {
                        if (figure == 1)
                        {
                            if (e.Y > nstart.Y && e.X > nstart.X)
                            {
                                figures.Add(new nRectangle() { X = nstart.X, Y = nstart.Y, Depth = this.depth, Height = e.Y - nstart.Y, Width = e.X - nstart.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.Y < nstart.Y && e.X > nstart.X)
                            {
                                figures.Add(new nRectangle() { X = nstart.X, Y = e.Y, Depth = this.depth, Height = nstart.Y - e.Y, Width = e.X - nstart.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.Y > nstart.Y && e.X < nstart.X)
                            {
                                figures.Add(new nRectangle() { X = e.X, Y = nstart.Y, Depth = this.depth, Height = e.Y - nstart.Y, Width = nstart.X - e.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.Y < nstart.Y && e.X < nstart.X)
                            {
                                figures.Add(new nRectangle() { X = e.X, Y = e.Y, Depth = this.depth, Height = nstart.Y - e.Y, Width = nstart.X - e.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            nRectangle rectangle = (nRectangle)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(rectangle.Name);
                            g.DrawRectangle(pen, rectangle.GetRectangle());
                        }
                        else if (figure == 2)
                        {
                            if (e.X < nstart.X)
                            {
                                figures.Add(new Triangle() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, nstart.Y), new Point(nstart.X, e.Y), new Point(e.X, e.Y) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else
                            {
                                figures.Add(new Triangle() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X, e.Y), new Point(e.X, e.Y), new Point(nstart.X + (e.X - nstart.X) / 2, nstart.Y) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            Triangle triangle = (Triangle)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(triangle.Name);
                            g.DrawPolygon(pen, triangle.Points.ToArray());
                        }
                        else if (figure == 3)
                        {
                            if (e.Y > nstart.Y && e.X > nstart.X)
                            {
                                figures.Add(new Ellipse() { X = nstart.X, Y = nstart.Y, Depth = this.depth, Height = e.Y - nstart.Y, Width = e.X - nstart.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.Y < nstart.Y && e.X > nstart.X)
                            {
                                figures.Add(new Ellipse() { X = nstart.X, Y = e.Y, Depth = this.depth, Height = nstart.Y - e.Y, Width = e.X - nstart.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.Y > nstart.Y && e.X < nstart.X)
                            {
                                figures.Add(new Ellipse() { X = e.X, Y = nstart.Y, Depth = this.depth, Height = e.Y - nstart.Y, Width = nstart.X - e.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.Y < nstart.Y && e.X < nstart.X)
                            {
                                figures.Add(new Ellipse() { X = e.X, Y = e.Y, Depth = this.depth, Height = nstart.Y - e.Y, Width = nstart.X - e.X, Outline = color, Fill = BackColor, style = this.style });
                            }
                            Ellipse ellipse = (Ellipse)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(ellipse.Name);
                            g.DrawEllipse(pen, ellipse.GetRectangle());
                        }
                        else if (figure == 4)
                        {
                            if (e.X < nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Rhombus() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, nstart.Y), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(e.X + (nstart.X - e.X) / 2, e.Y), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Rhombus() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, nstart.Y), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(nstart.X + (e.X - nstart.X) / 2, e.Y), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Rhombus() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, e.Y), new Point(e.X, e.Y + (nstart.Y - e.Y) / 2), new Point(nstart.X + (e.X - nstart.X) / 2, nstart.Y), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X < nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Rhombus() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (e.X - nstart.X) / 2, e.Y), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 2), new Point(e.X + (e.X - nstart.X) / 2, nstart.Y), new Point(e.X, e.Y + (nstart.Y - e.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            Rhombus rhomb = (Rhombus)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(rhomb.Name);
                            g.DrawPolygon(pen, rhomb.Points.ToArray());
                        }
                        else if (figure == 5)
                        {
                            Line line = new Line() { Depth = this.depth, start = nstart, end = e.Location, Outline = color, Fill = BackColor, style = this.style, endlinestyle = this.endlinestyle, linedashcup = this.linedashcup, startlinestyle = this.startlinestyle };
                            figures.Add(line);
                            toolStripComboBox2.ComboBox.Items.Add(line.Name);
                            pen.DashCap = linedashcup;
                            pen.EndCap = endlinestyle;
                            pen.StartCap = startlinestyle;
                            g.DrawLine(pen, line.start, line.end);
                        }
                        else if (figure == 7)
                        {
                            Pencil pencil = figures[figures.Count - 1] as Pencil;
                            List<PointF> list = new List<PointF>();
                            foreach (var item in pencil.Points)
                            {
                                list.Add(new PointF(item.X, item.Y));
                            }
                            c = false;
                            toolStripComboBox2.ComboBox.Items.Add(pencil.Name);
                            g.DrawCurve(pen, list.ToArray());
                        }
                        else if (figure == 8)
                        {
                            if (e.X < nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Hourglass() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, nstart.Y), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(nstart.X + (nstart.X - e.X) / 2, e.Y) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Hourglass() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, e.Y), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(nstart.X + (e.X - nstart.X) / 2, nstart.Y) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Hourglass() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, e.Y), new Point(e.X, e.Y + (nstart.Y - e.Y) / 2), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 2), new Point(nstart.X + (e.X - nstart.X) / 2, nstart.Y) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X < nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Hourglass() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, e.Y), new Point(e.X, e.Y + (nstart.Y - e.Y) / 2), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 2), new Point(e.X + (nstart.X - e.X) / 2, nstart.Y) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            Hourglass hourglass = (Hourglass)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(hourglass.Name);
                            g.DrawPolygon(pen, hourglass.Points.ToArray());
                        }
                        else if (figure == 9)
                        {
                            if (toolStripTextBox2.Text != "")
                            {
                                Text text = new Text() { Depth = this.depth, font = this.font, text = toolStripTextBox2.Text, X = e.X, Y = e.Y, Outline = BackColor, Fill = color, style = this.style };
                                figures.Add(text);
                                toolStripComboBox2.Items.Add(text.Name);
                                SolidBrush brush = new SolidBrush(color);
                                g.DrawString(text.text, text.font, brush, e.X, e.Y);
                            }
                        }
                        else if (figure == 10)
                        {
                            a = false;
                            Pen pen1 = new Pen(BackColor, this.depth);
                            g.DrawCurve(pen1, eraser.ToArray());
                            eraser.Clear();
                        }
                        else if (figure == 11)
                        {
                            if (e.X < nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Pentagon() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, nstart.Y), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(nstart.X - (nstart.X - e.X) / 4, e.Y), new Point(e.X + (nstart.X - e.X) / 4, e.Y), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Pentagon() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, nstart.Y), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 2), new Point(e.X - (e.X - nstart.X) / 4, e.Y), new Point(nstart.X + (e.X - nstart.X) / 4, e.Y), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Pentagon() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, e.Y), new Point(e.X, e.Y + (nstart.Y - e.Y) / 2), new Point(e.X - (e.X - nstart.X) / 4, nstart.Y), new Point(nstart.X + (e.X - nstart.X) / 4, nstart.Y), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X < nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Pentagon() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, e.Y), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 2), new Point(nstart.X - (nstart.X - e.X) / 4, nstart.Y), new Point(e.X + (nstart.X - e.X) / 4, nstart.Y), new Point(e.X, e.Y + (nstart.Y - e.Y) / 2) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            Pentagon pentagon = (Pentagon)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(pentagon.Name);
                            g.DrawPolygon(pen, pentagon.Points.ToArray());
                        }
                        else if (figure == 12)
                        {
                            if (e.X < nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Hexagon() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, nstart.Y), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 3), new Point(nstart.X, e.Y - (e.Y - nstart.Y) / 3), new Point(nstart.X - (nstart.X - e.X) / 2, e.Y), new Point(e.X, e.Y - (e.Y - nstart.Y) / 3), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 3) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new Hexagon() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, nstart.Y), new Point(e.X, nstart.Y + (e.Y - nstart.Y) / 3), new Point(e.X, e.Y - (e.Y - nstart.Y) / 3), new Point(e.X - (e.X - nstart.X) / 2, e.Y), new Point(nstart.X, e.Y - (e.Y - nstart.Y) / 3), new Point(nstart.X, nstart.Y + (e.Y - nstart.Y) / 3) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Hexagon() { Depth = this.depth, Points = new List<Point>() { new Point(nstart.X + (e.X - nstart.X) / 2, e.Y), new Point(e.X, e.Y + (nstart.Y - e.Y) / 3), new Point(e.X, nstart.Y - (nstart.Y - e.Y) / 3), new Point(e.X - (e.X - nstart.X) / 2, nstart.Y), new Point(nstart.X, nstart.Y - (nstart.Y - e.Y) / 3), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 3) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X < nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new Hexagon() { Depth = this.depth, Points = new List<Point>() { new Point(e.X + (nstart.X - e.X) / 2, e.Y), new Point(nstart.X, e.Y + (nstart.Y - e.Y) / 3), new Point(nstart.X, nstart.Y - (nstart.Y - e.Y) / 3), new Point(nstart.X - (nstart.X - e.X) / 2, nstart.Y), new Point(e.X, nstart.Y - (nstart.Y - e.Y) / 3), new Point(e.X, e.Y + (nstart.Y - e.Y) / 3) }, Outline = color, Fill = BackColor, style = this.style });
                            }
                            Hexagon hexagon = (Hexagon)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(hexagon.Name);
                            g.DrawPolygon(pen, hexagon.Points.ToArray());
                        }
                        else if (figure == 13)
                        {
                            if (e.X < nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new SemiEllipse() { Depth = this.depth, rectangle = new Rectangle(e.X, nstart.Y, nstart.X - e.X, e.Y - nstart.Y), Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y > nstart.Y)
                            {
                                figures.Add(new SemiEllipse() { Depth = this.depth, rectangle = new Rectangle(nstart.X, nstart.Y, e.X - nstart.X, e.Y - nstart.Y), Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X > nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new SemiEllipse() { Depth = this.depth, rectangle = new Rectangle(nstart.X, e.Y, e.X - nstart.X, nstart.Y - e.Y), Outline = color, Fill = BackColor, style = this.style });
                            }
                            else if (e.X < nstart.X && e.Y < nstart.Y)
                            {
                                figures.Add(new SemiEllipse() { Depth = this.depth, rectangle = new Rectangle(e.X, e.Y, nstart.X - e.X, nstart.Y - e.Y), Outline = color, Fill = BackColor, style = this.style });
                            }
                            SemiEllipse semi = (SemiEllipse)figures[figures.Count - 1];
                            toolStripComboBox2.ComboBox.Items.Add(semi.Name);
                            g.DrawArc(pen, semi.rectangle, 180, 180);
                        }
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        MessageBox.Show("Что бы нарисовать фигуру нужно провести какое-то расстояние от одного ее угла до другого");

                    }
                    g.Dispose();
                }
                save = false;
            }

        }



        private void paintOverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            SolidBrush brush = new SolidBrush(color);
            if (toolStripComboBox2.Text != "")
            {
                if (figures.Where(x => x.Name == toolStripComboBox2.Text).Count() != 0)
                {
                    Figure f = figures.Where(x => x.Name == toolStripComboBox2.Text).First();
                    f.Fill = color;
                    if (f is nRectangle)
                    {
                        nRectangle rect = (nRectangle)f;
                        g.FillRectangle(brush, rect.GetRectangle());
                    }
                    else if (f is Triangle)
                    {
                        Triangle triangle = (Triangle)f;
                        g.FillPolygon(brush, triangle.Points.ToArray());
                    }
                    else if (f is Ellipse)
                    {
                        Ellipse ellipse = (Ellipse)f;
                        g.FillEllipse(brush, ellipse.GetRectangle());
                    }
                    else if (f is Rhombus)
                    {
                        Rhombus r = (Rhombus)f;
                        g.FillPolygon(brush, r.Points.ToArray());
                    }
                    else if (f is Hourglass)
                    {
                        Hourglass r = (Hourglass)f;
                        g.FillPolygon(brush, r.Points.ToArray());
                    }
                    else if (f is Pentagon)
                    {
                        Pentagon p = (Pentagon)f;
                        g.FillPolygon(brush, p.Points.ToArray());
                    }
                    else if (f is Hexagon)
                    {
                        Hexagon h = (Hexagon)f;
                        g.FillPolygon(brush, h.Points.ToArray());
                    }
                }
                save = false;
            }
            g.Dispose();
        }
        private void Erase_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            SolidBrush brush = new SolidBrush(this.BackColor);
            Pen pen = new Pen(this.BackColor);
            if (toolStripComboBox2.Text != "")
            {
                if (figures.Where(x => x.Name == toolStripComboBox2.Text).Count() != 0)
                {
                    Figure f = figures.Where(x => x.Name == toolStripComboBox2.Text).First();
                    if (f is nRectangle)
                    {
                        nRectangle rect = (nRectangle)f;
                        pen.Width = rect.Depth;
                        g.FillRectangle(brush, rect.GetRectangle());
                        g.DrawRectangle(pen, rect.GetRectangle());
                    }
                    else if (f is Triangle)
                    {
                        Triangle triangle = (Triangle)f;
                        g.FillPolygon(brush, triangle.Points.ToArray());
                        pen.Width = triangle.Depth;
                        g.DrawPolygon(pen, triangle.Points.ToArray());
                    }
                    else if (f is Ellipse)
                    {
                        Ellipse ellipse = (Ellipse)f;
                        g.FillEllipse(brush, ellipse.GetRectangle());
                        pen.Width = ellipse.Depth;
                        g.DrawEllipse(pen, ellipse.GetRectangle());
                    }
                    else if (f is Rhombus)
                    {
                        Rhombus rhombus = (Rhombus)f;
                        g.FillPolygon(brush, rhombus.Points.ToArray());
                        pen.Width = rhombus.Depth;
                        g.DrawPolygon(pen, rhombus.Points.ToArray());
                    }
                    else if (f is Hourglass)
                    {
                        Hourglass hourglass = (Hourglass)f;
                        g.FillPolygon(brush, hourglass.Points.ToArray());
                        pen.Width = hourglass.Depth;
                        g.DrawPolygon(pen, hourglass.Points.ToArray());
                    }
                    else if (f is Line)
                    {
                        Line l = (Line)f;
                        pen.Width = l.Depth + 3;
                        pen.StartCap = l.startlinestyle;
                        pen.EndCap = l.endlinestyle;
                        pen.DashCap = l.linedashcup;
                        g.DrawLine(pen, l.start, l.end);
                    }
                    else if (f is VectorLine)
                    {
                        VectorLine l = (VectorLine)f;
                        pen.Width = l.Depth;
                        pen.StartCap = l.startlinestyle;
                        pen.EndCap = l.endlinestyle;
                        pen.DashCap = l.linedashcup;
                        foreach (var item in l.Points)
                        {
                            g.DrawLine(pen, item[0], item[1]);
                        }
                    }
                    else if (f is Pencil)
                    {
                        Pencil pencil = (Pencil)f;
                        pen.Width = pencil.Depth;
                        g.DrawCurve(pen, pencil.Points.ToArray());
                    }
                    else if (f is Text)
                    {
                        Text t = (Text)f;
                        g.DrawString(t.text, t.font, brush, t.X, t.Y);
                    }
                    else if (f is Pentagon)
                    {
                        Pentagon p = (Pentagon)f;
                        g.FillPolygon(brush, p.Points.ToArray());
                        pen.Width = p.Depth;
                        g.DrawPolygon(pen, p.Points.ToArray());
                    }
                    else if (f is Hexagon)
                    {
                        Hexagon h = (Hexagon)f;
                        g.FillPolygon(brush, h.Points.ToArray());
                        pen.Width = h.Depth;
                        g.DrawPolygon(pen, h.Points.ToArray());
                    }
                    else if (f is SemiEllipse)
                    {
                        SemiEllipse semi = (SemiEllipse)f;
                        pen.Width = semi.Depth;
                        g.DrawArc(pen, semi.rectangle, 180, 180);
                    }
                    figures.Remove(f);
                    toolStripComboBox2.Items.Clear();
                    foreach (var item in figures)
                    {
                        toolStripComboBox2.Items.Add(item.Name);
                    }
                    toolStripComboBox2.Text = "";
                }
                save = false;
            }
            g.Dispose();

        }
        private void EraseAll_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            toolStripComboBox2.Items.Clear();
            figures.Clear();
            toolStripComboBox2.Text = "";
            g.Clear(this.BackColor);
            save = false;
            g.Dispose();
        }
        private void Castomise_Click(object sender, EventArgs e)
        {
            if (figures.Where(x => x.Name == toolStripComboBox2.Text).Count() != 0)
            {
                Figure f = figures.Where(x => x.Name == toolStripComboBox2.Text).First();
                string txt = "";
                if(f is Text)
                {
                    txt = (f as Text).text;
                }
                Form2 form2 = new Form2(f.Fill, f.Outline, this.font, f.style, txt, f.Depth);
                Graphics g = CreateGraphics();
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    SolidBrush brush = new SolidBrush(this.BackColor);
                    Pen pen = new Pen(this.BackColor);
                    if (toolStripComboBox2.Text != "")
                    {
                        if (f is nRectangle)
                        {
                            nRectangle rect = (nRectangle)f;
                            pen.Width = rect.Depth;
                            g.FillRectangle(brush, rect.GetRectangle());
                            g.DrawRectangle(pen, rect.GetRectangle());
                        }
                        else if (f is Triangle)
                        {
                            Triangle triangle = (Triangle)f;
                            g.FillPolygon(brush, triangle.Points.ToArray());
                            pen.Width = triangle.Depth;
                            g.DrawPolygon(pen, triangle.Points.ToArray());
                        }
                        else if (f is Ellipse)
                        {
                            Ellipse ellipse = (Ellipse)f;
                            g.FillEllipse(brush, ellipse.GetRectangle());
                            pen.Width = ellipse.Depth;
                            g.DrawEllipse(pen, ellipse.GetRectangle());
                        }
                        else if (f is Rhombus)
                        {
                            Rhombus rhombus = (Rhombus)f;
                            g.FillPolygon(brush, rhombus.Points.ToArray());
                            pen.Width = rhombus.Depth;
                            g.DrawPolygon(pen, rhombus.Points.ToArray());
                        }
                        else if (f is Hourglass)
                        {
                            Hourglass hourglass = (Hourglass)f;
                            g.FillPolygon(brush, hourglass.Points.ToArray());
                            pen.Width = hourglass.Depth;
                            g.DrawPolygon(pen, hourglass.Points.ToArray());
                        }
                        else if (f is Line)
                        {
                            Line l = (Line)f;
                            pen.Width = l.Depth;
                            pen.StartCap = l.startlinestyle;
                            pen.EndCap = l.endlinestyle;
                            pen.DashCap = l.linedashcup;
                            g.DrawLine(pen, l.start, l.end);
                        }
                        else if (f is VectorLine)
                        {
                            VectorLine line = (VectorLine)f;
                            pen.Width = line.Depth;
                            pen.StartCap = line.startlinestyle;
                            pen.EndCap = line.endlinestyle;
                            pen.DashCap = line.linedashcup;
                            foreach (var item in line.Points)
                            {
                                g.DrawLine(pen, item[0], item[1]);
                            }
                        }
                        else if (f is Pencil)
                        {
                            Pencil pencil = (Pencil)f;
                            pen.Width = pencil.Depth;
                            g.DrawCurve(pen, pencil.Points.ToArray());
                        }
                        else if (f is Text)
                        {
                            Text t = (Text)f;
                            g.DrawString(t.text, t.font, brush, t.X, t.Y);
                        }
                        else if (f is Pentagon)
                        {
                            Pentagon p = (Pentagon)f;
                            g.FillPolygon(brush, p.Points.ToArray());
                            pen.Width = p.Depth;
                            g.DrawPolygon(pen, p.Points.ToArray());
                        }
                        else if (f is Hexagon)
                        {
                            Hexagon h = (Hexagon)f;
                            g.FillPolygon(brush, h.Points.ToArray());
                            pen.Width = h.Depth;
                            g.DrawPolygon(pen, h.Points.ToArray());
                        }
                        else if (f is SemiEllipse)
                        {
                            SemiEllipse semi = (SemiEllipse)f;
                            pen.Width = semi.Depth;
                            g.DrawArc(pen, semi.rectangle, 180, 180);
                        }
                    }
                    figures.Remove(f);
                    form2.SetFigur(ref f, ref g);
                    figures.Add(f);
                    save = false;
                }
                g.Dispose();
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                Graphics g = CreateGraphics();
                this.BackgroundImage = Image.FromFile(open.FileName);
                this.BackgroundImageLayout = ImageLayout.Zoom;
                g.Dispose();
                save = false;
            }
        }
        private void Font_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                this.font = font.Font;
            }
        }
        private void Rhombus_Click(object sender, EventArgs e)
        {
            
            figure = 4;
        }
        private void Line_Click(object sender, EventArgs e)
        {
            
            figure = 5;
        }
        private void VectorLine_Click(object sender, EventArgs e)
        {
            
            figure = 6;
        }
        private void CurvedLine_Click(object sender, EventArgs e)
        {
            
            figure = 7;
        }
        private void Hourglass_Click(object sender, EventArgs e)
        {
            
            figure = 8;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            if (save)
            {
                this.Close();
            }
            else
            {
                Form3 form3 = new Form3();
                DialogResult result = form3.ShowDialog();
                if(result == DialogResult.Yes)
                {
                    if (path == "")
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Image Files (*.png;*.jpg;*.pdf)|*.png;*.jpeg;*pdf|All files(*.*)|*.*";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            path = saveFileDialog.FileName;
                        }
                        else
                        {
                            path = "D:\\New image.png";
                        }
                        Thread.Sleep(500);
                    }
                    Rectangle rect = RectangleToScreen(DesktopBounds);
                    using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.CopyFromScreen(rect.Location, new Point(0, 0), rect.Size);
                        }
                        bitmap.Save(path);
                    }
                    this.Close();
                }
                if(result == DialogResult.No)
                {
                    this.Close();
                }
            }
        }
        private void StartOrdinary_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.startlinestyle = System.Drawing.Drawing2D.LineCap.Flat;
        }
        private void StartArrow_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.startlinestyle = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }
        private void StartRhombus_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.startlinestyle = System.Drawing.Drawing2D.LineCap.DiamondAnchor;
        }
        private void StartBall_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.startlinestyle = System.Drawing.Drawing2D.LineCap.Round;
        }
        private void StartSquare_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.startlinestyle = System.Drawing.Drawing2D.LineCap.Square;
        }
        private void StartSquareAnchor_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.startlinestyle = System.Drawing.Drawing2D.LineCap.SquareAnchor;
        }
        private void StartTriangle_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.startlinestyle = System.Drawing.Drawing2D.LineCap.Triangle;
        }
        private void EndOrdinary_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.endlinestyle = System.Drawing.Drawing2D.LineCap.Flat;
        }
        private void EndArrow_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.endlinestyle = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }
        private void EndRhombus_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.endlinestyle = System.Drawing.Drawing2D.LineCap.DiamondAnchor;
        }
        private void EndBall_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.endlinestyle = System.Drawing.Drawing2D.LineCap.Round;
        }
        private void EndSquare_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.endlinestyle = System.Drawing.Drawing2D.LineCap.Square;
        }
        private void EndSquareAnchor_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.endlinestyle = System.Drawing.Drawing2D.LineCap.SquareAnchor;
        }
        private void EndTriangle_Click(object sender, EventArgs e)
        {
            
            figure = 5;
            this.endlinestyle = System.Drawing.Drawing2D.LineCap.Triangle;
        }
        private void Ordinary_Click(object sender, EventArgs e)
        {
            figure = 5;
            this.linedashcup = System.Drawing.Drawing2D.DashCap.Flat;
        }
        private void Ball_Click(object sender, EventArgs e)
        {
            figure = 5;
            this.linedashcup = System.Drawing.Drawing2D.DashCap.Round;
        }
        private void lTriangle_Click(object sender, EventArgs e)
        {
            figure = 5;
            this.linedashcup = System.Drawing.Drawing2D.DashCap.Triangle;
        }
        private void Text_Click(object sender, EventArgs e)
        {
            figure = 9;
        }
        private void Pentagon_Click(object sender, EventArgs e)
        {
            
            figure = 11;
        }
        private void Hexagon_Click(object sender, EventArgs e)
        {
            
            figure = 12;
        }
        private void SemiEllipse_Click(object sender, EventArgs e)
        {
            
            figure = 13;
        }

        private void backcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = color.Color;
                save = false;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (figure == 6 && !b)
                {
                    nstart = new Point(e.X, e.Y);
                    VectorLine vector = new VectorLine() { Depth = this.depth, linedashcup = this.linedashcup, startlinestyle = this.startlinestyle, endlinestyle = this.endlinestyle, Outline = color, Fill = BackColor, style = this.style };
                    figures.Add(vector);
                    b = true;
                }
                else if (figure == 6 && b)
                {
                    Graphics g = CreateGraphics();
                    Pen pen = new Pen(this.color, this.depth);
                    pen.DashStyle = this.style;
                    VectorLine vector = figures[figures.Count - 1] as VectorLine;
                    pen.StartCap = vector.startlinestyle;
                    pen.EndCap = vector.endlinestyle;
                    pen.DashCap = vector.linedashcup;
                    vector.Points.Add(new List<Point>() { nstart, new Point(e.X, e.Y) });
                    g.DrawLine(pen, nstart, new Point(e.X, e.Y));
                    nstart = new Point(e.X, e.Y);
                    g.Dispose();
                }
            }
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (figure == 6 && b)
                {
                    Graphics g = CreateGraphics();
                    Pen pen = new Pen(this.color, this.depth);
                    VectorLine vector = figures[figures.Count - 1] as VectorLine;
                    pen.DashStyle = this.style;
                    pen.StartCap = vector.startlinestyle;
                    pen.EndCap = vector.endlinestyle;
                    pen.DashCap = vector.linedashcup;
                    vector.Points.Add(new List<Point>() { nstart, new Point(e.X, e.Y) });
                    toolStripComboBox2.Items.Add(vector.Name);
                    g.DrawLine(pen, nstart, new Point(e.X, e.Y));
                    b = false;
                    g.Dispose();
                    save = false;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(c)
            {
                (figures[figures.Count - 1] as Pencil).Points.Add(new Point(e.X, e.Y));
            }
            if(a)
            {
                eraser.Add(new Point(e.X, e.Y));
            }
        }

        private void toolStripComboBox3_TextChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox3.Text == "")
            {
                depth = 5;
            }
            else
            {
                int g;
                if (int.TryParse(toolStripComboBox3.Text, out g))
                {
                    depth = g;
                    toolStripComboBox1.Text = g.ToString();
                }
                else
                {
                    depth = 5;
                }
            }
        }
    }
}
