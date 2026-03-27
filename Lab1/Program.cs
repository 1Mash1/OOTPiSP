using MyShape;
using System;

class Program
{
    static void Main()
    {
        MainForm mainForm = new MainForm();
        ShapeList myShapes = new ShapeList();
        myShapes.Add(new Circle { x = 50, y = 50, radius = 40, color = Color.Red });
        myShapes.Add(new MyRectangle { x = 200, y = 50, width = 100, height = 50, color = Color.Blue });
        myShapes.Add(new Line { x = 50, y = 150, x2 = 300, y2 = 200, color = Color.Black });
        myShapes.Add(new Square { x = 400, y = 50, width = 60, color = Color.Orange });
        myShapes.Add(new Triangle { x = 100, y = 300, x2 = 150, y2 = 250, x3 = 200, y3 = 300, color = Color.Purple });
        myShapes.Add(new Elipse { x = 300, y = 300, radius = 120, height = 60, color = Color.Green });
        mainForm.Paint += (sender, paintArgs) => {
            myShapes.DrawAll(paintArgs.Graphics);
        };
        Application.Run(mainForm);
    }
}