namespace MyShape
{
    class Square : Shape
    {
        public int width;
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(color))
            {
                g.DrawRectangle(pen, x, y, width, width);
            }
        }
    }
}
