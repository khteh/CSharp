namespace CSharp;
public record Colour(string Name, int Red, int Green, int Blue);
public record Red() : Colour("Red", 255, 0, 0);
public record Green() : Colour("Green", 0, 255, 0);
public record Blue() : Colour("Blue", 0, 0, 255);
