// See https://aka.ms/new-console-template for more information
// Here’s another example of how to use interfaces and abstraction in C#:
// https://medium.com/@susithapb/clean-coding-skills-every-c-developer-should-follow-in-net-core-ce041d2647a2

//using System;

//class Program
//{
//    static void Main(string[] args)
//    {
//        try
//        {
//            // Try to perform some operation that may throw an exception
//            int num1 = 10;
//            int num2 = 0;
//            int result = num1 / num2;
//        }
//        catch (DivideByZeroException ex)
//        {
//            // Handle the exception
//            Console.WriteLine("Error: " + ex.Message);
//        }
//    }
//}


// Use Interfaces and Abstraction

using System;

// Define an interface for a shape
interface IShape
{
    double CalculateArea();
}

// Define an abstract class for a shape
abstract class Shape : IShape
{
    public abstract double CalculateArea();
}

// Define a concrete class for a circle
class Circle : Shape
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * _radius * _radius;
    }
}

// Define a concrete class for a rectangle
class Rectangle : Shape
{
    private double _width;
    private double _height;

    public Rectangle(double width, double height)
    {
        _width = width;
        _height = height;
    }

    public override double CalculateArea()
    {
        return _width * _height;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an array of shapes
        Shape[] shapes = new Shape[] {
            new Circle(2),
            new Rectangle(3, 4)
        };

        // Calculate and print the area of each shape
        foreach (Shape shape in shapes)
        {
            double area = shape.CalculateArea();
            Console.WriteLine("Area: " + area);
        }
    }
}