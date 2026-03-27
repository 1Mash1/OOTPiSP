namespace MyShape
{
    class Triangle : Shape
    {
        public int x2, y2, x3, y3;

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(color))
            {
                Point[] pts = {
                new Point(x, y),
                new Point(x2, y2),
                new Point(x3, y3)
                };
                g.DrawPolygon(pen, pts);
            }
        }
    }
}