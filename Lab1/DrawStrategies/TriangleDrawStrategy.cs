namespace MyShape
{
    class TriangleDrawStrategy : IDrawStrategy
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color, 2))
            {
                Rectangle rect = GetBounds(shape);
                Point[] points = {
                    new Point(rect.Left + rect.Width / 2, rect.Top), // Top center
                    new Point(rect.Left, rect.Bottom),               // Bottom left
                    new Point(rect.Right, rect.Bottom)               // Bottom right
                };
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

        public bool ContainsPoint(Shape shape, int pointX, int pointY)
        {
            return GetBounds(shape).Contains(pointX, pointY);
        }
    }
}