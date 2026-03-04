using System.Drawing;

namespace MyShape
{
    class Elipse : Shape
    {
        public int width, height;
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(Pens.Purple, x, y, width, height);
        }
    }
}
