namespace MyShape
{
    public class LineDrawStrategy : IDrawStrategy // Concrete strategy class that implements drawing logic for a line
    {
        public void Draw(Graphics graphics, Shape shape)
        {
            using (Pen pen = new Pen(shape.color))
            {
                graphics.DrawLine(pen, shape.x, shape.y, shape.x2, shape.y2);
            }
        }
    }
}