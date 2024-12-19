namespace CSharp;
public enum Role
{
    Admin,
    Member,
    Guest
}
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public Role Role { get; set; }
}