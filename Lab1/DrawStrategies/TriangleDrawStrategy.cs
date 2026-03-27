namespace MyShape
{
    public class TriangleDrawStrategy : IDrawStrategy // Concrete strategy class that implements drawing logic for a triangle
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color))
            {
                int topX, topY, bottomY;
                topX = (shape.x + shape.x2) / 2;
                topY = Math.Min(shape.y, shape.y2);
                bottomY = Math.Max(shape.y, shape.y2);
                Point[] points = {new Point(topX, topY), new Point(shape.x, bottomY), new Point(shape.x2, bottomY) };
                graphics.DrawPolygon(pen, points);
            }
        }
    }
}