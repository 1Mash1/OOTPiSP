using MyShape;
using System;
using System.Drawing;

namespace HexagonPlugin
{
    public class Hexagon : Shape
    {
        public Hexagon() { }
    }

    public class HexagonFactory : ShapeFactory
    {
        public override Shape Create() => new Hexagon();
    }

    public class HexagonStrategy : IDrawStrategy
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            double angle;
            using (Pen pen = new Pen(shape.color, 2))
            {
                Rectangle rect = GetBounds(shape);
                Point[] points = new Point[6];
                for (int i = 0; i < 6; i++)
                {
                    angle = 2 * Math.PI / 6 * i;
                    points[i] = new Point(
                        rect.X + rect.Width / 2 + (int)(rect.Width / 2 * Math.Cos(angle)),
                        rect.Y + rect.Height / 2 + (int)(rect.Height / 2 * Math.Sin(angle))
                    );
                }
                graphics.DrawPolygon(pen, points);
            }
        }

        public Rectangle GetBounds(Shape shape)
        {
            int left, top, width, height;
            left = Math.Min(shape.x, shape.x2);
            top = Math.Min(shape.y, shape.y2);
            width = Math.Abs(shape.x2 - shape.x);
            height = Math.Abs(shape.y2 - shape.y);
            return new Rectangle(left, top, width, height);
        }

        public bool ContainsPoint(Shape shape, int px, int py)
        {
            return GetBounds(shape).Contains(px, py);
        }
    }

    public class HexagonPluginMain : IPlugin
    {
        public string Name => "hexagon";

        public ShapeFactory GetFactory() => new HexagonFactory();

        public IDrawStrategy GetStrategy() => new HexagonStrategy();
    }
}