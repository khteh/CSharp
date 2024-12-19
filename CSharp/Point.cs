namespace CSharp;
public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public Point(double x, double y) => (X, Y) = (x, y);
    public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
}
