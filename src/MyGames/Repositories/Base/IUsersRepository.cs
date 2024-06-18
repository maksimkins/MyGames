using MyGames.Models;

namespace MyGames.Repositories.Base;

public interface IUserRepository {
    // public IQueryable<User> GetAll();
    public User GetById(int id);
    public void Post(User user);
    public void Update(int id, User user);
    public void Delete(int id);
}