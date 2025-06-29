using ElsherbinyTask05.API.Constants;
using ElsherbinyTask05.API.Data;
using ElsherbinyTask05.API.Models;

namespace ElsherbinyTask05.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void ChangeRole(Guid userId, string role)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new InvalidOperationException($"User with ID {userId} not found.");

        user.Role = role.ToString();
        _context.SaveChanges();
    }



    public User GetById(Guid id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            throw new InvalidOperationException($"User with ID {id} not found.");

        return user;
    }

    public User GetByUsername(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username) ?? throw new InvalidOperationException($"User with username '{username}' not found.");
        return user;
    }

    public void Update(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
            throw new InvalidOperationException($"User with ID {user.Id} not found.");

        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        existingUser.Role = user.Role;
        // Add other properties as needed

        _context.SaveChanges();
    }
}
