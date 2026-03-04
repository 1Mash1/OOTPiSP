using System;
using System.Drawing;

namespace MyShape
{
    abstract class Shape //Абстрактный базовый класс для всех фигур
    {
        public int x, y; //Координаты положения на форме
        public abstract void Draw(Graphics g);
    }
}