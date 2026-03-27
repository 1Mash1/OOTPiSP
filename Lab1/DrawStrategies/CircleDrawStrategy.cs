namespace MyShape
{
    class CircleDrawStrategy : IDrawStrategy // Concrete strategy class that implements drawing logic for a circle
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color))
            {
                int width, height, size, left, top;
                width = Math.Abs(shape.x - shape.x2);
                height = Math.Abs(shape.y - shape.y2);
                size = Math.Min(width, height);
                left = Math.Min(shape.x, shape.x2);
                top = Math.Min(shape.y, shape.y2);
                graphics.DrawEllipse(pen, left, top, size, size);
            }
        }
    }
}