using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace cw
{
    internal class Figure
    {
        public string Name { get; set; }
        public int Depth { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Color Outline { get; set; }
        public Color Fill { get; set; }
        public System.Drawing.Drawing2D.DashStyle style { get; set; }
    }
    internal class nRectangle : Figure
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public static int index;
        internal Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
        public nRectangle()
        {
            Name = $"rectangle{++index}";
        }
    }
    internal class Triangle : Figure
    {
        public List<Point> Points { get; set; }
        public static int index;
        public Triangle()
        {
            Name = $"triangle{++index}";
        }
    }
    internal class Ellipse : Figure
    {
        public int Width { get; set; }
        public static int index;
        public int Height { get; set; }
        internal Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
        public Ellipse()
        {
            Name = $"ellipse{++index}";
        }
    }
    internal class Line : Figure
    {
        public static int index;
        public Point start { get; set; }
        public Point end { get; set; }
        public System.Drawing.Drawing2D.LineCap startlinestyle;
        public System.Drawing.Drawing2D.LineCap endlinestyle;
        public System.Drawing.Drawing2D.DashCap linedashcup;
        public Line()
        {
            Name = $"line{++index}";
        }
    }
    internal class VectorLine : Figure
    {
        public static int index;
        public List<List<Point>> Points { get; set; }
        public System.Drawing.Drawing2D.LineCap startlinestyle;
        public System.Drawing.Drawing2D.LineCap endlinestyle;
        public System.Drawing.Drawing2D.DashCap linedashcup;
        public VectorLine()
        {
            Name = $"vectorline{++index}";
            Points = new List<List<Point>>();
        }
    }
    internal class Pencil : Figure
    {
        public static int index;
        public List<Point> Points { get; set; }
        public Pencil()
        {
            Name = $"pencil{++index}";
            Points = new List<Point>();
        }
    }

    internal class Text : Figure
    {
        public static int index;
        public Font font { get; set; }
        public string text { get; set; }
        public Text()
        {
            Name = $"text{++index}";
        }
    }
    internal class Rhombus : Figure
    {
        public static int index;
        public List<Point> Points { get; set; }
        public Rhombus()
        {
            Name = $"rhombus{++index}";
        }
    }
    internal class Pentagon : Figure
    {
        public static int index;
        public List<Point> Points { get; set; }
        public Pentagon()
        {
            Name = $"pentagon{++index}";
        }
    }
    internal class Hexagon : Figure
    {
        public static int index;
        public List<Point> Points { get; set; }
        public Hexagon()
        {
            Name = $"hexagon{++index}";
        }
    }
    internal class SemiEllipse : Figure
    {
        public static int index;
        public Rectangle rectangle { get; set; }
        public SemiEllipse()
        {
            Name = $"semi-ellipse{++index}";
        }
    }
    internal class Hourglass : Figure
    {
        public static int index;
        public List<Point> Points { get; set; }
        public Hourglass()
        {
            Name = $"hourglass{++index}";
        }
    }
}
