namespace MyShape
{
    public interface IDrawStrategy
    {
        void Draw(Graphics graphics, Shape shape); // Method to draw a shape on the graphics surface
    }
}