using MyShape;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

class Program
{
    static void Main()
    {
        Application.EnableVisualStyles(); 
        Application.SetCompatibleTextRenderingDefault(false); 
        MainForm mainForm = new MainForm();
        ShapeList myShapes = new ShapeList();
        myShapes.Add(new Circle { x = 50, y = 50, radius = 40 });
        myShapes.Add(new MyRectangle { x = 200, y = 50, width = 100, height = 50 });
        myShapes.Add(new Line { x = 50, y = 150, x2 = 300, y2 = 200 });
        myShapes.Add(new Square { x = 400, y = 50, width = 60 });
        myShapes.Add(new Triangle { x = 100, y = 300, x2 = 150, y2 = 250, x3 = 200, y3 = 300 });
        myShapes.Add(new Elipse { x = 300, y = 300, width = 120, height = 60 });
        mainForm.Paint += (sender, e) => {
            myShapes.DrawAll(e.Graphics);
        };
        Application.Run(mainForm);
    }
}