namespace MyShape
{
    public interface IDrawStrategy
    {
        void Draw(Graphics graphics, Shape shape); // Method to draw a shape on the graphics surface
        Rectangle GetBounds(Shape shape); // Calculates the bounding rectangle of the shape
        bool ContainsPoint(Shape shape, int pointX, int pointY); // Checks if the given coordinates are inside the shape's boundaries
    }
}