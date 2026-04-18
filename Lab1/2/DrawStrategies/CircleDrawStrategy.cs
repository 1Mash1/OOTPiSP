namespace MyShape
{
    class CircleDrawStrategy : IDrawStrategy // Concrete strategy for circle logic
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color, 2))
            {
                Rectangle visualBounds = GetBounds(shape); // Drawing using the bounds provided by the strategy
                graphics.DrawEllipse(pen, visualBounds);
            }
        }

        public Rectangle GetBounds(Shape shape)
        {
            int width, height, size, left, top;
            // Calculation logic
            width = Math.Abs(shape.x - shape.x2);
            height = Math.Abs(shape.y - shape.y2);
            size = Math.Min(width, height);
            left = Math.Min(shape.x, shape.x2);
            top = Math.Min(shape.y, shape.y2);
            return new Rectangle(left, top, size, size);
        }

        public bool ContainsPoint(Shape shape, int pointX, int pointY)
        {
            Rectangle bounds = GetBounds(shape); // Get the same bounds used for drawing
            bounds.Inflate(5, 5); // Margin for selection
            return bounds.Contains(pointX, pointY);
        }
    }
}