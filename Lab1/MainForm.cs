using System;

namespace MyShape
{
    public partial class MainForm : Form
    {
        private ShapeFactory selectedFactory; // The factory for the selected shape type
        private Shape currentShape; // The shape currently being drawn
        private ShapeList myShapes = new ShapeList(); // List of all shapes
        private Color currentColor = Color.Black; // Current color

        // Map shape types to their drawing strategies
        private Dictionary<Type, IDrawStrategy> strategies = new Dictionary<Type, IDrawStrategy>
        {
            { typeof(Line), new LineDrawStrategy() },
            { typeof(Circle), new CircleDrawStrategy() },
            { typeof(MyRectangle), new MyRectangleDrawStrategy() },
            { typeof(Square), new SquareDrawStrategy() },
            { typeof(Elipse), new ElipseDrawStrategy() },
            { typeof(Triangle), new TriangleDrawStrategy() }
        };

        public MainForm()
        {
            InitializeComponent();
            // Assign concrete factory objects to button Tags
            btnLine.Tag = new LineFactory();
            btnRectangle.Tag = new RectFactory();
            btnCircle.Tag = new CircleFactory();
            btnSquare.Tag = new SquareFactory();
            btnElipse.Tag = new ElipseFactory();
            btnTriangle.Tag = new TriangleFactory();
        }

        private void OnShapeButtonClick(object sender, EventArgs args)
        {
            // Update the active factory when a shape button is clicked
            if (sender is Button button)
                selectedFactory = (ShapeFactory)button.Tag;
        }

        private void canvas_MouseDown(object sender, MouseEventArgs args)
        {
            if (selectedFactory == null)
                return;
            currentShape = selectedFactory.Create(); // Factory generates a new shape
            // Link the shape with its corresponding drawing logic from the dictionary
            if (strategies.ContainsKey(currentShape.GetType()))
                currentShape.DrawStrategy = strategies[currentShape.GetType()];
            // Initialize shape properties
            currentShape.x = args.X;
            currentShape.y = args.Y;
            currentShape.x2 = args.X;
            currentShape.y2 = args.Y;
            currentShape.color = currentColor;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs args)
        {
            if (currentShape != null)
            {
                currentShape.x2 = args.X; // Update endpoint during dragging
                currentShape.y2 = args.Y;
                canvas.Invalidate(); // Trigger a redraw to show the preview
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs args)
        {
            if (currentShape != null)
            {
                myShapes.Add(currentShape); // Add the finished shape to the storage
                currentShape = null; // Clear the temporary reference
                canvas.Invalidate(); // Final refresh
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs args)
        {
            // Enable anti-aliasing for better visual quality
            //args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var shape in myShapes.GetList())
                shape.DrawStrategy.Draw(args.Graphics, shape); // Draw all stored shapes
            if (currentShape != null)
                currentShape.DrawStrategy.Draw(args.Graphics, currentShape); // Draw the shape that is currently being stretched
        }

        private void btnClearCanvas_Click(object sender, EventArgs args)
        {
            myShapes.GetList().Clear(); // Empty the shape list
            canvas.Invalidate(); // Clear the drawing area
        }

        private void btnSelectColor_Click(object sender, EventArgs args)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) // Open color dialog and update color
            {
                currentColor = colorDialog.Color;
                btnSelectColor.BackColor = currentColor;
            }
        }
    }
}