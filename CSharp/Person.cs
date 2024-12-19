namespace CSharp;
public record Person
{
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public Person(string firstName, string lastName) => (FirstName, LastName) = (firstName, lastName);
}
public record Teacher : Person
{
    public string Subject { get; init; }
    public Teacher(string firstName, string lastName, string subject) : base(firstName, lastName) => Subject = subject;
}
