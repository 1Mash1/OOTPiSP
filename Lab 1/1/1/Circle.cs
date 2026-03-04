using System.Drawing;

namespace MyShape
{
    class Circle : Shape
    {
        public int radius;
        public override void Draw(Graphics g) // Переопределение для рисования круга
        {
            g.DrawEllipse(Pens.Red, x, y, radius * 2, radius * 2);
        }
    }
}
