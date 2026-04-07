using MyShape;
using System;
using System.Drawing;

namespace SmilePlugin
{
    public class Smile : Shape { public Smile() { } }

    public class SmileFactory : ShapeFactory
    {
        public override Shape Create() => new Smile();
    }

    public class SmileStrategy : IDrawStrategy
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            int eyeW, eyeH;
            using (Pen pen = new Pen(shape.color, 2))
            {
                Rectangle r = GetBounds(shape);
                if (r.Width < 5 || r.Height < 5)
                    return;
                graphics.DrawEllipse(pen, r);
                eyeW = r.Width / 6;
                eyeH = r.Height / 6;
                graphics.DrawEllipse(pen, r.X + r.Width / 4, r.Y + r.Height / 4, eyeW, eyeH);
                graphics.DrawEllipse(pen, r.X + 3 * r.Width / 4 - eyeW, r.Y + r.Height / 4, eyeW, eyeH);
                Rectangle mouthRect = new Rectangle(r.X + r.Width / 4, r.Y + r.Height / 2, r.Width / 2, r.Height / 3);
                graphics.DrawArc(pen, mouthRect, 0, 180);
            }
        }

        public Rectangle GetBounds(Shape shape)
        {
            return new Rectangle(Math.Min(shape.x, shape.x2), Math.Min(shape.y, shape.y2),
                                 Math.Abs(shape.x2 - shape.x), Math.Abs(shape.y2 - shape.y));
        }

        public bool ContainsPoint(Shape shape, int px, int py) => GetBounds(shape).Contains(px, py);
    }

    public class SmilePluginMain : IPlugin
    {
        public string Name => "smile";
        public ShapeFactory GetFactory() => new SmileFactory();
        public IDrawStrategy GetStrategy() => new SmileStrategy();
    }
}