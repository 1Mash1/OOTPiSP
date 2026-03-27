namespace MyShape
{
    class Elipse : Circle
    {
        public int height;
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(color))
            {
                g.DrawEllipse(pen, x, y, radius * 2, height);
            }
        }
    }
}
