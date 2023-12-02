using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Square square = new Square("Red", 5.0);
        Rectangle rectangle = new Rectangle("Blue", 4.0, 6.0);
        Circle circle = new Circle("Green", 3.0);

        DisplayShapeInfo(square);
        DisplayShapeInfo(rectangle);
        DisplayShapeInfo(circle);
    }

    static void DisplayShapeInfo(Shape shape)
    {
        Console.WriteLine($"Shape Color: {shape.GetColor()}");
        Console.WriteLine($"Shape Area: {shape.GetArea()}");
        Console.WriteLine();
    }
}