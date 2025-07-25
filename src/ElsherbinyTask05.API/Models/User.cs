namespace ElsherbinyTask05.API.Models;

public class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;


    public string Role { get; set; } = string.Empty;


    public string Password { get; set; }
}
