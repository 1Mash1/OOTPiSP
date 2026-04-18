using MyShape;

public abstract class Shape //Abstract base class for all geometric shapes
{
    public int x, y, x2, y2; // Coordinates: where the mouse was pressed and released

    public Color color; //Color
    public bool IsSelected { get; set; } // Flag to track if the shape is currently selected for editing
    public IDrawStrategy DrawStrategy { get; set; } //Strategy for drawing the specific shape

}