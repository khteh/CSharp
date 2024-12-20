using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using static System.Console;
//using static System.Diagnostics.Debug;
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9
namespace CSharp;
public enum Quadrant
{
    Unknown,
    Origin,
    One,
    Two,
    Three,
    Four,
    OnBorder
}
class Program
{
    static void Main(string[] args)
    {
        OutVariables("Hello World!!!");
        OutVariables("10");
        IsExpressionWithPatterns(null);
        long l = 10;
        IsExpressionWithPatterns(l);
        int i = 10;
        IsExpressionWithPatterns(i);
        Circle circle = new Circle(123.456);
        Rectangle rectangle = new Rectangle(123.456, 789.012);
        Rectangle square = new Rectangle(123.456, 123.456);
        SwitchWithPatterns(circle);
        SwitchWithPatterns(rectangle);
        SwitchWithPatterns(square);
        SwitchWithPatterns(null);
        var multipleReturnValues = MultipleReturnValuesInTuple();
        WriteLine($"long: {multipleReturnValues.longValue}, string: {multipleReturnValues.strValue}, double: {multipleReturnValues.doubleValue}");
        (long longValue, string strValue, double dValue) = MultipleReturnValuesInTuple();
        WriteLine($"long: {longValue}, string: {strValue}, double: {dValue}");
        long fib = Fibonacci(0);
        Debug.Assert(Fibonacci(0) == 0);
        Debug.Assert(Fibonacci(1) == 1);
        Debug.Assert(Fibonacci(2) == 1);
        Debug.Assert(Fibonacci(3) == 2);
        Debug.Assert(Fibonacci(4) == 3);
        Debug.Assert(Fibonacci(5) == 5);
        literals();
        long[] array = { 1, 15, -39, 0, 7, 14, -12 };
        ref long place = ref RefReturnsAndLocals(7, array); // aliases 7's place in the array
        place = 9; // replaces 7 with 9 in the array
        WriteLine(array[4]); // prints 9
        Point p = new Point(123.456, 456.789); // Demonstrate tuple deconstruction
        (double x, double y) = p;
        Debug.Assert(x == 123.456);
        Debug.Assert(y == 456.789);
        WriteLine($"x: {x}, y: {y}");
        p.X = 0; p.Y = 0;
        Debug.Assert(SwitchWithPositionalPattern(p) == Quadrant.Origin);
        p.X = -1; p.Y = 0;
        Debug.Assert(SwitchWithPositionalPattern(p) == Quadrant.OnBorder);
        p.X = 0; p.Y = -1;
        Debug.Assert(SwitchWithPositionalPattern(p) == Quadrant.OnBorder);
        p.X = 1; p.Y = 1;
        Debug.Assert(SwitchWithPositionalPattern(p) == Quadrant.One);
        p.X = -1; p.Y = 1;
        Debug.Assert(SwitchWithPositionalPattern(p) == Quadrant.Two);
        p.X = -1; p.Y = -1;
        Debug.Assert(SwitchWithPositionalPattern(p) == Quadrant.Three);
        p.X = 1; p.Y = -1;
        Debug.Assert(SwitchWithPositionalPattern(p) == Quadrant.Four);
        // Indices and Ranges
        string[] indicesAndRange = new string[]
        {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
        };              // 9 (or words.Length) ^0
        Debug.Assert(indicesAndRange[^1] == "dog");
        string[] range = indicesAndRange[1..4];
        Debug.Assert(range.Length == 3);
        Debug.Assert(range[0] == "quick");
        Debug.Assert(range[1] == "brown");
        Debug.Assert(range[2] == "fox");
        string[] rangeFromBack = indicesAndRange[^4..^1];
        Debug.Assert(rangeFromBack.Length == 3);
        Debug.Assert(rangeFromBack[0] == "over");
        Debug.Assert(rangeFromBack[1] == "the");
        Debug.Assert(rangeFromBack[2] == "lazy");
        string[] frontOpenRange = indicesAndRange[..5];
        string[] backOpenRange = indicesAndRange[5..];
        string[] allRange = indicesAndRange[..];
        Debug.Assert(frontOpenRange.Length == 5);
        Debug.Assert(frontOpenRange[0] == "The");
        Debug.Assert(frontOpenRange[4] == "jumped");
        Debug.Assert(backOpenRange.Length == 4);
        Debug.Assert(backOpenRange[0] == "over");
        Debug.Assert(backOpenRange[3] == "dog");
        Debug.Assert(allRange.Length == indicesAndRange.Length);
        Debug.Assert(allRange.SequenceEqual(indicesAndRange));
        TestRecords();
        // Test C# 9.0 Json SerDes with init-only setters
        string serializedRectangle = JsonSerializer.Serialize<Rectangle>(rectangle);
        Debug.Assert(!string.IsNullOrEmpty(serializedRectangle));
        Rectangle rectangle1 = JsonSerializer.Deserialize<Rectangle>(serializedRectangle);
        Debug.Assert(rectangle1 != null);
        Debug.Assert(rectangle.Equals(rectangle1));
        if (DateTimeOffset.TryParse("2023-04-06T10:10:32.167Z", out DateTimeOffset date))
            WriteLine($"date: {date}");
        else
            WriteLine($"Failed to parse date string!");
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        //WriteLine($@"{dictionary["hello"]}"); throw
        WriteLine($@"{(i >= 10 ? "i >= 10" : "i < 10")}");
        List<Order> orders = new List<Order>();
        // Instantiate date and time using years, months, days,
        // hours, minutes, and seconds
        DateTimeOffset dto = new DateTimeOffset(2021, 12, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        WriteLine($"dto: {dto}");
        orders.Add(new Order(dto, new PricingDetails(200.00m))); // 2022 sales
        // 2022
        dto = new DateTimeOffset(2022, 12, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(100.00m))); // 2022 sales
        // 2023
        dto = new DateTimeOffset(2023, 1, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(1.00m)));
        dto = new DateTimeOffset(2023, 2, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(2.00m)));
        dto = new DateTimeOffset(2023, 3, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(3.00m)));
        dto = new DateTimeOffset(2023, 4, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(4.00m)));
        dto = new DateTimeOffset(2023, 5, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(5.00m)));
        dto = new DateTimeOffset(2023, 6, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(6.00m)));
        dto = new DateTimeOffset(2023, 7, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(7.00m)));
        dto = new DateTimeOffset(2023, 8, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(8.00m)));
        dto = new DateTimeOffset(2023, 9, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(9.00m)));
        dto = new DateTimeOffset(2023, 10, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(10.00m)));
        dto = new DateTimeOffset(2023, 11, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(11.00m)));
        dto = new DateTimeOffset(2023, 12, 1, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(12.00m))); // Sum: 78
        dto = new DateTimeOffset(2023, 12, 2, 0, 0, 0, new TimeSpan(1, 0, 0));
        orders.Add(new Order(dto, new PricingDetails(13.00m))); // Sum: 78
        IEnumerable<IGrouping<int, Order>> orders2023 = orders.Where(i => i.Timestamp.Year == DateTimeOffset.Now.Year).GroupBy(i => i.Timestamp.Month);
        IEnumerable<MonthSales> sales2023 = orders.Where(i => i.Timestamp.Year == DateTimeOffset.Now.Year).GroupBy(i => i.Timestamp.Month).Select(group => new MonthSales { Month = group.Key, Sales = group.Sum(y => y.Price.Total) });

        IEnumerable<MonthSales> yearlySales = orders.GroupBy(i => i.Timestamp.Year).Select(group => new MonthSales { Month = group.Key, Sales = group.Sum(y => y.Price.Total) }).OrderByDescending(i => i.Month).Take(2);

        List<PricingDetails> pricingDetails = new List<PricingDetails>() {
            new PricingDetails(123.456m),
            new PricingDetails(234.567m),
            new PricingDetails(456.789m),
        };
        WriteLine($"{pricingDetails.Count()} pricing details");
        pricingDetails.ForEach(p => WriteLine($"price.Total: {p.Total}"));
        WriteLine();
        pricingDetails = SortPricingDetails(pricingDetails);
        pricingDetails.ForEach(p => WriteLine($"price.Total: {p.Total}"));
        WriteLine();
        ApplyPriceDecorator(pricingDetails);
        WriteLine($"{pricingDetails.Count()} pricing details");
        pricingDetails.ForEach(p => WriteLine($"price.Total: {p.Total}"));
        //decimal totalSales = sales2023.Sum(i => i.Total)
        EnumerableIndex();
        EnumerableCountBy();
        AggregateBy();
        UUID7();
        WriteLine("Press ENTER to exit:");
        ReadLine();
    }
    static void ApplyPriceDecorator(List<PricingDetails> pricingDetails)
    {
        pricingDetails.ForEach(price =>
        {
            price.Total = price.Total - price.Total * 1m;
        });
        pricingDetails.RemoveAll(p => p.Total <= 0m);
    }
    static List<PricingDetails> SortPricingDetails(List<PricingDetails> pricingDetails)
    {
        return pricingDetails.OrderByDescending(p => p.Total).ToList();
    }
    static void OutVariables(string s) => WriteLine(int.TryParse(s, out var i) ? new string('*', i) : "Cloudy - no stars tonight!");
    static void IsExpressionWithPatterns(object o)
    {
        if (o is null)
            WriteLine("o is null!");
        else if (o is int i)
            WriteLine(new string('*', i));
        else
            WriteLine("Unexpected parameter o!");
    }
    // The following method uses the positional pattern to extract the values of x and y. 
    // Then, it uses a when clause to determine the Quadrant of the point:
    static Quadrant SwitchWithPositionalPattern(Point point) => point switch
    {
        (0, 0) => Quadrant.Origin,
        var (x, y) when x > 0 && y > 0 => Quadrant.One,
        var (x, y) when x < 0 && y > 0 => Quadrant.Two,
        var (x, y) when x < 0 && y < 0 => Quadrant.Three,
        var (x, y) when x > 0 && y < 0 => Quadrant.Four,
        var (_, _) => Quadrant.OnBorder,
        _ => Quadrant.Unknown
    };
    static void SwitchWithPatterns(Shape shape)
    {
        switch (shape)
        {
            case Circle c:
                WriteLine($"circle with radius {c.Radius}");
                break;
            case Rectangle s when (s.Length == s.Height):
                WriteLine($"{s.Length} x {s.Height} square!");
                break;
            case Rectangle r:
                WriteLine($"{r.Length} x {r.Height} rectangle!");
                break;
            default: //The default clause is always evaluated last: Even though the null case above comes last, it will be checked before the default clause is picked. This is for compatibility with existing switch semantics. However, good practice would usually have you put the default clause at the end.
                WriteLine("Unknown shape!");
                break;
            case null:
                //throw new ArgumentNullException(nameof(shape));
                WriteLine("The null clause at the end is not unreachable: This is because type patterns follow the example of the current is expression and do not match null. This ensures that null values aren’t accidentally snapped up by whichever type pattern happens to come first; you have to be more explicit about how to handle them (or leave them for the default clause).");
                break;
        }
    }
    static (long longValue, string strValue, double doubleValue) MultipleReturnValuesInTuple() => (longValue: 123, strValue: "Hello World!!!", doubleValue: 123.456);
    static long Fibonacci(long i)
    {
        if (i < 0)
            throw new ArgumentOutOfRangeException($"Less nagativitiy please! {nameof(i)}");
        return FibonacciLocalFunction(i).current;
        (long current, long previous) FibonacciLocalFunction(long x)
        {
            if (x == 0) return (0, 0);
            if (x == 1) return (1, 0);
            var (c, p) = FibonacciLocalFunction(x - 1);
            return (c + p, c);
        }
    }
    static void literals()
    {
        long l = 123_456;
        long hex = 0xAB_CD_EF;
        long binary = 0b1010_1011_1100_1101_1110_1111;
        WriteLine($"l: {l}, hex: {hex}, binary: {binary}");
    }
    static ref long RefReturnsAndLocals(long number, long[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
            if (numbers[i].Equals(number))
                return ref numbers[i]; // Return the storage location. Not the value
        throw new IndexOutOfRangeException($"{nameof(number)} not found!");
    }
    static void TestRecords()
    {
        StringBuilder sb = new StringBuilder();
        Person person = new Person("Mickey", "Mouse");
        Person person1 = new Person("Mickey", "Mouse");
        Teacher teacher = new Teacher("Mickey", "Mouse", "Mathematics");
        Debug.Assert(person == person1);
        Debug.Assert(person != teacher);
        WriteLine($"teacher: {teacher.ToString()}");
        // Test Positional Records
        Colour myColour = new Colour("MyColour", 123, 456, 789);
        var (name, r, g, b) = myColour;
        WriteLine($"{name}: R: {r} G: {g} B: {b}");
        // Test "with expressions"
        Colour newColour = myColour with { Name = "New Colour", Red = 789, Green = 123, Blue = 456 };
        Debug.Assert(newColour.Name.Equals("New Colour"));
        Debug.Assert(newColour.Red == 789);
        Debug.Assert(newColour.Green == 123);
        Debug.Assert(newColour.Blue == 456);
        Colour clone = newColour with { };
        Debug.Assert(clone == newColour);
    }
    public static List<string> Months => DateTimeFormatInfo.CurrentInfo.MonthNames[..^1].ToList(); // First through last element
    // https://learn.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting#alignment-component
    static void EnumerableIndex()
    {
        foreach (var (month, index) in Months.Index())
            WriteLine($"{index,-5}: {month}");
    }
    public static List<User> Users() =>
    [
        new() { Id = 1, UserName = "John", Role = Role.Admin },
            new() { Id = 2, UserName = "Jane", Role = Role.Member },
            new() { Id = 3, UserName = "Joe", Role = Role.Guest },
            new() { Id = 4, UserName = "Alice", Role = Role.Admin },
            new() { Id = 5, UserName = "Bob", Role = Role.Member },
            new() { Id = 6, UserName = "Charlie", Role = Role.Guest },
            new() { Id = 7, UserName = "Dave", Role = Role.Admin },
            new() { Id = 8, UserName = "Eve", Role = Role.Member },
            new() { Id = 9, UserName = "Frank", Role = Role.Guest },
            new() { Id = 10, UserName = "Grace", Role = Role.Admin }
    ];
    public static (string name, string department, int vacationDaysLeft)[] Employees =
    [
        ("John Doe", "IT", 12),
        ("Eve Peterson", "Marketing", 18),
        ("John Smith", "IT", 28),
        ("Grace Johnson", "HR", 17),
        ("Nick Carson", "Marketing", 5),
        ("Grace Morgan", "HR", 9)
    ];
    static void EnumerableCountBy()
    {
        List<User> users = Users();
        foreach (KeyValuePair<Role, int> role in users.CountBy(u => u.Role))
            WriteLine($"Role {role.Key}: {role.Value}");
    }
    static void AggregateBy()
    {
        List<KeyValuePair<string, int>> vacations = Employees.AggregateBy(e => e.department, 0, (acc, e) => acc + e.vacationDaysLeft).ToList();
        foreach (KeyValuePair<string, int> v in vacations)
            WriteLine($"Department: {v.Key}, Vacations: {v.Value}");
    }
    // https://uuid7.com/
    static void UUID7()
    {
        //Creates a new Guid according to RFC 9562, following the Version 7 format.
        WriteLine($"UUID7: {Guid.CreateVersion7()}");
        //Creates a new Guid according to RFC 9562, following the Version 7 format with DateTimeOffset
        WriteLine($"UUID7 with DateTimeOffset: {Guid.CreateVersion7(TimeProvider.System.GetUtcNow())}");
    }
}
