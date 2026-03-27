using System;
using System.Drawing;

namespace MyShape
{
    public class ElipseDrawStrategy : IDrawStrategy // Concrete strategy class that implements drawing logic for an elipse
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color))
            {
                int left, top, width, height;
                left = Math.Min(shape.x, shape.x2);
                top = Math.Min(shape.y, shape.y2);
                width = Math.Abs(shape.x - shape.x2);
                height = Math.Abs(shape.y - shape.y2);
                graphics.DrawEllipse(pen, left, top, width, height);
            }
        }
    }
}