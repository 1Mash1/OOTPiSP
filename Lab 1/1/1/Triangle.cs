namespace MyShape
{
    class Triangle : Shape
    {
        public int x2, y2;
        public int x3, y3;

        public override void Draw(Graphics g)
        {
            Point[] pts = {
                new Point(x, y),
                new Point(x2, y2),
                new Point(x3, y3)
            };
            g.DrawPolygon(Pens.Green, pts);
        }
    }
}