namespace MyShape
{
    class LineDrawStrategy : IDrawStrategy
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color, 2))
            {
                graphics.DrawLine(pen, shape.x, shape.y, shape.x2, shape.y2);
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

        public bool ContainsPoint(Shape shape, int pointX, int pointY)
        {
            Rectangle bounds = GetBounds(shape);
            bounds.Inflate(5, 5); // Buffer for easier line selection
            return bounds.Contains(pointX, pointY);
        }
    }
}