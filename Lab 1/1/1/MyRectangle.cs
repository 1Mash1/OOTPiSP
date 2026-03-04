using System.Drawing;

namespace MyShape
{
    class MyRectangle : Shape
    {
        public int width, height;
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Blue, x, y, width, height);
        }
    }
}