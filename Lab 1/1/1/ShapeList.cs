using System;
using System.Drawing;

namespace MyShape
{
    class ShapeList
    {
        private List<Shape> list = new List<Shape>(); ////Список объектов (фигур)
        public void Add(Shape shape) //Метод для добавления фигуры в список
        {
            list.Add(shape);
        }

        public void DrawAll(Graphics g) //Метод для отрисовки всех фигур
        {
            foreach (var shape in list)
            {
                shape.Draw(g);
            }
        }
    }
}