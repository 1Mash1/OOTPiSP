namespace MyShape
{
    class ShapeList
    {
        private List<Shape> list = new List<Shape>(); // Internal storage for shapes

        public void Add(Shape shape) // Adds a new shape to the collection
        {
            list.Add(shape);
        }

        public List<Shape> GetList() // Returns the full list of shapes
        {
            return list;
        }

        public void DrawAll(Graphics graphics)
        {
            foreach (var shape in list)
            {
                shape.DrawStrategy.Draw(graphics, shape); // Polymorphic call: how to draw the shape
            }
        }
    }
}