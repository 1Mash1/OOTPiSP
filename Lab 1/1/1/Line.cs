using System.Drawing;

namespace MyShape
{
    class Line : Shape
    {
        public int x2, y2;
        public override void Draw(Graphics g)
        {
            g.DrawLine(Pens.Black, x, y, x2, y2);
        }
    }
}
