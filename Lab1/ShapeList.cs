public class ShapeList
{
    private List<Shape> shapes = new List<Shape>(); // Internal storage for all shapes on the canvas

    public event Action OnChanged; // Event triggered to notify the UI to repaint

    public void Add(Shape shape)
    {
        shapes.Add(shape);     // Adds a shape to the collection
        OnChanged?.Invoke();   // Notifies listeners that the list has changed
    }

    public void Remove(Shape shape)
    {
        shapes.Remove(shape);  // Removes a specific shape from the collection
        OnChanged?.Invoke();   // Notifies listeners to refresh the view
    }

    public void Clear()
    {
        shapes.Clear();        // Removes all shapes from the list
        OnChanged?.Invoke();   // Notifies listeners that the canvas is now empty
    }

    public void NotifyUpdate()
    {
        OnChanged?.Invoke();   // Manually triggers a UI refresh (e.g., after color change)
    }

    public List<Shape> GetList() => shapes; // Returns the current list of shapes
}