namespace MyShape
{
    class SquareDrawStrategy : IDrawStrategy
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color, 2))
            {
                Rectangle bounds = GetBounds(shape);
                graphics.DrawRectangle(pen, bounds);
            }
        }

        public Rectangle GetBounds(Shape shape)
        {
            int left, top, width, height, size;
            width = Math.Abs(shape.x2 - shape.x);
            height = Math.Abs(shape.y2 - shape.y);
            size = Math.Min(width, height); 
            left = Math.Min(shape.x, shape.x2);
            top = Math.Min(shape.y, shape.y2);

            return new Rectangle(left, top, size, size);
        }

        public bool ContainsPoint(Shape shape, int pointX, int pointY)
        {
            return GetBounds(shape).Contains(pointX, pointY);
        }
    }
}