namespace MyShape
{
    class Line : Shape
    {
        public int x2, y2;
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(color))
            {
                g.DrawLine(pen, x, y, x2, y2);
            }
        }
    }
}
