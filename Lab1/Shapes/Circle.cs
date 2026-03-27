namespace MyShape
{
    class Circle : Shape
    {
        public int radius;
        public override void Draw(Graphics g) //Переопределение для рисования
        {
            using (Pen pen = new Pen(color))
            {
                g.DrawEllipse(pen, x, y, radius, radius);
            }
        }
    }
}
