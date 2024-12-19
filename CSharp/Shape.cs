using System;
namespace CSharp;
class Shape
{
}
class Circle : Shape
{
    public double Radius { get; init; }
    public Circle(double radius) => Radius = radius;
}
class Rectangle : Shape
{
    public double Length { get; init; }
    public double Height { get; init; }
    public Rectangle(double length, double height) => (Length, Height) = (length, height);
    public override bool Equals(Object obj)
    {
        // Perform an equality check on two rectangles (Point object pairs).
        if (obj == null || GetType() != obj.GetType())
            return false;
        Rectangle r = (Rectangle)obj;
        return Length.Equals(r.Length) && Height.Equals(r.Height);
    }
    public override int GetHashCode() => Tuple.Create(Length, Height).GetHashCode();
}
