using ElsherbinyTask05.API.Constants;
using ElsherbinyTask05.API.Models;

namespace ElsherbinyTask05.API.Repositories;

public interface IUserRepository
{
    public void Add(User user);

    public User GetById(Guid id);

    public void ChangeRole(Guid userId, string role);

    public User GetByUsername(string Username);
    void Update(User user);
}
