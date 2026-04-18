namespace MyShape
{
    public class AddShapeCommand : ICommand // Command for adding a new shape to the canvas
    {
        private ShapeList _list;
        private Shape _shape;

        public AddShapeCommand(ShapeList list, Shape shape)
        {
            _list = list;
            _shape = shape;
        }

        public void Execute() => _list.Add(_shape); // Adds the shape to the list
        public void Undo() => _list.Remove(_shape); // Removes the shape from the list
    }

    public class RemoveShapeCommand : ICommand // Command for removing one or all shapes
    {
        private ShapeList _list;
        private List<Shape> _removedShapes; // Stores references to restored shapes after undo

        public RemoveShapeCommand(ShapeList list, Shape singleShape = null)
        {
            _list = list;
            _removedShapes = new List<Shape>();

            if (singleShape != null)
                _removedShapes.Add(singleShape); // Target a specific shape
            else
                _removedShapes.AddRange(_list.GetList()); // Target all shapes for clearing
        }

        public void Execute()
        {
            foreach (var s in _removedShapes)
                _list.Remove(s); // Deletes all stored shapes from the canvas
        }

        public void Undo()
        {
            foreach (var s in _removedShapes)
                _list.Add(s); // Restores all previously deleted shapes
        }
    }

    public class ChangeColorCommand : ICommand // Command for modifying the color of an existing shape
    {
        private Shape _shape;
        private Color _oldColor;
        private Color _newColor;
        private ShapeList _list;

        public ChangeColorCommand(ShapeList list, Shape shape, Color newColor)
        {
            _list = list;
            _shape = shape;
            _newColor = newColor;
            _oldColor = shape.color; // Backs up the current color before changing

            if (_oldColor.ToArgb() == _newColor.ToArgb())
            { } // Logic check for identical colors
        }

        public void Execute()
        {
            if (_shape.color.ToArgb() == _newColor.ToArgb())
            {
                return; // Skips execution if no actual color change is needed
            }
            _shape.color = _newColor; // Applies the new color
            _list.NotifyUpdate();     // Refreshes the UI
        }

        public void Undo()
        {
            _shape.color = _oldColor; // Reverts to the original color
            _list.NotifyUpdate();     // Refreshes the UI
        }
    }
}