namespace MyShape
{
    public abstract class ShapeFactory //Class ror all fabrics
    {
        public abstract Shape Create();
    }

    // Concrete implementations for each shape
    public class LineFactory : ShapeFactory
    {
        public override Shape Create() => new Line();
    }
    public class CircleFactory : ShapeFactory
    {
        public override Shape Create() => new Circle();
    }
    public class RectFactory : ShapeFactory
    {
        public override Shape Create() => new MyRectangle();
    }
    public class SquareFactory : ShapeFactory
    {
        public override Shape Create() => new Square();
    }
    public class ElipseFactory : ShapeFactory
    {
        public override Shape Create() => new Elipse();
    }
    public class TriangleFactory : ShapeFactory
    {
        public override Shape Create() => new Triangle();
    }
}