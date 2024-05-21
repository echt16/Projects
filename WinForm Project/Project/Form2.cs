using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cw
{
    public partial class Form2 : Form
    {
        private Color outline;
        private Color fill;
        private Font font;
        private System.Drawing.Drawing2D.DashStyle style;
        private string text;
        private int depth;
        public Form2(Color back, Color color, Font font, System.Drawing.Drawing2D.DashStyle style, string txt, int depth)
        {
            InitializeComponent();
            fill = back;
            outline = color;
            this.font = font;
            this.style = style;
            textBox1.Text = txt;
            this.depth = depth;
            maskedTextBox1.Text = depth.ToString();
        }
        internal void SetFigur(ref Figure f, ref Graphics g)
        {
            f.Outline = outline;
            f.Fill = fill;
            if (maskedTextBox1.Text.Length != 0)
                f.Depth = int.Parse(maskedTextBox1.Text);
            if (f is nRectangle)
            {
                nRectangle nRectangle = f as nRectangle;
                Pen pen = new Pen(outline, nRectangle.Depth);
                pen.DashStyle = style;
                SolidBrush brush = new SolidBrush(fill);
                nRectangle.X += int.Parse(numericUpDown1.Value.ToString());
                nRectangle.Y -= int.Parse(numericUpDown2.Value.ToString());
                g.FillRectangle(brush, nRectangle.GetRectangle());
                g.DrawRectangle(pen, nRectangle.GetRectangle());
                f = nRectangle;
            }
            else if (f is Ellipse)
            {
                Ellipse ellipse = (Ellipse)f;
                Pen pen = new Pen(outline, ellipse.Depth);
                pen.DashStyle = style;
                SolidBrush brush = new SolidBrush(fill);
                ellipse.X += int.Parse(numericUpDown1.Value.ToString());
                ellipse.Y -= int.Parse(numericUpDown2.Value.ToString());
                g.FillEllipse(brush, ellipse.GetRectangle());
                g.DrawEllipse(pen, ellipse.GetRectangle());
                f = ellipse;
            }
            else if (f is Triangle)
            {
                Triangle triangle = (Triangle)f;
                Pen pen = new Pen(outline, triangle.Depth);
                pen.DashStyle = style;
                SolidBrush brush = new SolidBrush(fill);
                for (int i = 0; i < triangle.Points.Count; i++)
                {
                    triangle.Points[i] = new Point(triangle.Points[i].X + int.Parse(numericUpDown1.Value.ToString()), triangle.Points[i].Y - int.Parse(numericUpDown2.Value.ToString()));
                }
                g.FillPolygon(brush, triangle.Points.ToArray());
                g.DrawPolygon(pen, triangle.Points.ToArray());
                f = triangle;

            }
            else if (f is Rhombus)
            {
                Rhombus rhombus = (Rhombus)f;
                Pen pen = new Pen(outline, rhombus.Depth);
                pen.DashStyle = style;
                SolidBrush brush = new SolidBrush(fill);
                for (int i = 0; i < rhombus.Points.Count; i++)
                {
                    rhombus.Points[i] = new Point(rhombus.Points[i].X + int.Parse(numericUpDown1.Value.ToString()), rhombus.Points[i].Y - int.Parse(numericUpDown2.Value.ToString()));
                }
                g.FillPolygon(brush, rhombus.Points.ToArray());
                g.DrawPolygon(pen, rhombus.Points.ToArray());
                f = rhombus;
            }
            else if (f is Hourglass)
            {
                Hourglass hourglass = (Hourglass)f;
                Pen pen = new Pen(outline, hourglass.Depth);
                pen.DashStyle = style;
                for (int i = 0; i < hourglass.Points.Count; i++)
                {
                    hourglass.Points[i] = new Point(hourglass.Points[i].X + int.Parse(numericUpDown1.Value.ToString()), hourglass.Points[i].Y - int.Parse(numericUpDown2.Value.ToString()));
                }
                SolidBrush brush = new SolidBrush(fill);
                g.FillPolygon(brush, hourglass.Points.ToArray());
                g.DrawPolygon(pen, hourglass.Points.ToArray());
                f = hourglass;
            }
            else if (f is Line)
            {
                Line l = (Line)f;
                Pen pen = new Pen(outline, l.Depth);
                pen.DashStyle = style;
                pen.StartCap = l.startlinestyle;
                pen.EndCap = l.endlinestyle;
                pen.DashCap = l.linedashcup;
                l.end = new Point(l.end.X + int.Parse(numericUpDown1.Value.ToString()), l.end.Y - int.Parse(numericUpDown2.Value.ToString()));
                l.start = new Point(l.start.X + int.Parse(numericUpDown1.Value.ToString()), l.start.Y - int.Parse(numericUpDown2.Value.ToString()));
                g.DrawLine(pen, l.start, l.end);
                f = l;
            }
            else if (f is VectorLine)
            {
                VectorLine l = (VectorLine)f;
                Pen pen = new Pen(outline, l.Depth);
                pen.DashStyle = style;
                pen.StartCap = l.startlinestyle;
                pen.EndCap = l.endlinestyle;
                pen.DashCap = l.linedashcup;
                for (int i = 0; i < l.Points.Count; i++)
                {
                    l.Points[i][0] = new Point(l.Points[i][0].X + int.Parse(numericUpDown1.Value.ToString()), l.Points[i][0].Y - int.Parse(numericUpDown2.Value.ToString()));
                    l.Points[i][1] = new Point(l.Points[i][1].X + int.Parse(numericUpDown1.Value.ToString()), l.Points[i][1].Y - int.Parse(numericUpDown2.Value.ToString()));
                }
                foreach (var item in l.Points)
                {
                    g.DrawLine(pen, item[0], item[1]);
                }
                f = l;
            }
            else if (f is Pencil)
            {
                Pencil pencil = (Pencil)f;
                Pen pen = new Pen(outline, pencil.Depth);
                for (int i = 0; i < pencil.Points.Count; i++)
                {
                    pencil.Points[i] = new Point(pencil.Points[i].X + int.Parse(numericUpDown1.Value.ToString()), pencil.Points[i].Y - int.Parse(numericUpDown2.Value.ToString()));
                }
                pen.DashStyle = style;
                g.DrawCurve(pen, pencil.Points.ToArray());
                f = pencil;
            }
            else if (f is Text)
            {
                Text t = (Text)f;
                Brush brush = new SolidBrush(fill);
                if (textBox1.Text.Length != 0)
                {
                    t.text = text;
                }
                t.X += int.Parse(numericUpDown1.Value.ToString());
                t.Y -= int.Parse(numericUpDown2.Value.ToString());
                t.font = font;
                g.DrawString(t.text, t.font, brush, t.X, t.Y);
                f = t;
            }
            else if (f is Pentagon)
            {
                Pentagon p = (Pentagon)f;
                Brush brush = new SolidBrush(fill);
                Pen pen = new Pen(outline, p.Depth);
                pen.DashStyle = style;
                for (int i = 0; i < p.Points.Count; i++)
                {
                    p.Points[i] = new Point(p.Points[i].X + int.Parse(numericUpDown1.Value.ToString()), p.Points[i].Y - int.Parse(numericUpDown2.Value.ToString()));
                }
                g.FillPolygon(brush, p.Points.ToArray());
                pen.Width = p.Depth;
                g.DrawPolygon(pen, p.Points.ToArray());
                f = p;
            }
            else if (f is Hexagon)
            {
                Hexagon h = (Hexagon)f;
                for (int i = 0; i < h.Points.Count; i++)
                {
                    h.Points[i] = new Point(h.Points[i].X + int.Parse(numericUpDown1.Value.ToString()), h.Points[i].Y - int.Parse(numericUpDown2.Value.ToString()));
                }
                Brush brush = new SolidBrush(fill);
                Pen pen = new Pen(outline, h.Depth);
                pen.DashStyle = style;
                g.FillPolygon(brush, h.Points.ToArray());
                pen.Width = h.Depth;
                g.DrawPolygon(pen, h.Points.ToArray());
                f = h;
            }
            else if (f is SemiEllipse)
            {
                SemiEllipse semi = (SemiEllipse)f;
                Pen pen = new Pen(outline, semi.Depth);
                pen.DashStyle = style;
                semi.rectangle = new Rectangle(semi.rectangle.X + int.Parse(numericUpDown1.Value.ToString()), semi.rectangle.Y - int.Parse(numericUpDown2.Value.ToString()), semi.rectangle.Width, semi.rectangle.Height);
                pen.Width = semi.Depth;
                g.DrawArc(pen, semi.rectangle, 180, 180);
                f = semi;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if(color.ShowDialog() == DialogResult.OK)
            {
                outline = color.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                fill = color.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if(font.ShowDialog() == DialogResult.OK)
            {
                this.font = font.Font;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
