using System.Drawing;

namespace MyShape
{
    class Square : Shape
    {
        public int width;
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Orange, x, y, width, width);
        }
    }
}
