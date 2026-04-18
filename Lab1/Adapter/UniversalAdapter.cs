using System;
using System.Drawing;
using System.Reflection;
using MyShape;

namespace Adapter
{
    // A shape wrapper that enables third-party objects to work within our system
    public class AdaptedShape : Shape
    {
        public AdaptedShape() { } // Empty constructor for compatibility with the base Shape class
    }

    // A factory responsible for producing adapted shape instances
    public class AdaptedFactory : ShapeFactory
    {
        public override Shape Create() => new AdaptedShape(); // Returns a new instance of an adapted shape
    }

    // Bridges our system with external libraries using reflection to call foreign methods
    public class UniversalAdapterStrategy : IDrawStrategy
    {
        private readonly object _foreignFactory;  // The external factory instance
        private readonly MethodInfo _createMethod; // The external method to create shapes
        private readonly object _foreignRenderer; // The external rendering engine instance
        private readonly MethodInfo _renderMethod; // The external method to draw shapes

        public UniversalAdapterStrategy(object factory, MethodInfo create, object renderer, MethodInfo render)
        {
            _foreignFactory = factory;
            _createMethod = create;
            _foreignRenderer = renderer;
            _renderMethod = render;
        }

        public void Draw(Graphics graphics, Shape shape)
        {
            float thickness = (float)2.0; // Default line thickness for external rendering
            int sides = 0;               // Default parameter for shape geometry

            var points = new List<Point>
            {
                new Point(shape.x, shape.y),
                new Point(shape.x2, shape.y2)
            };

            // Prepares arguments and invokes the foreign method to create an external shape object
            object[] factoryArgs = { points, Brushes.Black, Brushes.Transparent, thickness, sides };
            object foreignShape = _createMethod.Invoke(_foreignFactory, factoryArgs);

            // Invokes the foreign renderer to draw the created object onto our Graphics surface
            object[] renderArgs = { foreignShape, graphics };
            _renderMethod.Invoke(_foreignRenderer, renderArgs);
        }

        public Rectangle GetBounds(Shape shape)
        {
            // Calculates the bounding box based on coordinates
            return new Rectangle(
                Math.Min(shape.x, shape.x2),
                Math.Min(shape.y, shape.y2),
                Math.Abs(shape.x2 - shape.x),
                Math.Abs(shape.y2 - shape.y));
        }

        public bool ContainsPoint(Shape shape, int px, int py)
        {
            return GetBounds(shape).Contains(px, py); // Checks if a mouse click is inside the shape bounds
        }
    }
}