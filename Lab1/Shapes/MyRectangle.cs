namespace MyShape
{
    class MyRectangle : Square
    {
        public int height;
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(color))
            {
                g.DrawRectangle(pen, x, y, width, height);
            }
        }
    }
}