using MyShape;
using System;
using System.Drawing;

namespace StarPlugin
{
    public class Star : Shape { public Star() { } }

    public class StarFactory : ShapeFactory
    {
        public override Shape Create() => new Star();
    }

    public class StarStrategy : IDrawStrategy
    {
        double rx, ry;
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color, 2))
            {
                Rectangle rect = GetBounds(shape);
                PointF[] points = new PointF[10];
                Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                rx = rect.Width / 2.0;
                ry = rect.Height / 2.0;
                for (int i = 0; i < 10; i++)
                {
                    double angle = Math.PI / 5 * i - Math.PI / 2;
                    double r = (i % 2 == 0) ? 1.0 : 0.5;
                    points[i] = new PointF(
                        (float)(center.X + rx * r * Math.Cos(angle)),
                        (float)(center.Y + ry * r * Math.Sin(angle))
                    );
                }
                graphics.DrawPolygon(pen, points);
            }
        }

        public Rectangle GetBounds(Shape shape)
        {
            return new Rectangle(Math.Min(shape.x, shape.x2), Math.Min(shape.y, shape.y2),
                                 Math.Abs(shape.x2 - shape.x), Math.Abs(shape.y2 - shape.y));
        }

        public bool ContainsPoint(Shape shape, int px, int py) => GetBounds(shape).Contains(px, py);
    }

    public class StarPluginMain : IPlugin
    {
        public string Name => "star";
        public ShapeFactory GetFactory() => new StarFactory();
        public IDrawStrategy GetStrategy() => new StarStrategy();
    }
}