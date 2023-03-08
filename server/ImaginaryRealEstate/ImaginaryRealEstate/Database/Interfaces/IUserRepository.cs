using ImaginaryRealEstate.Entities;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Database.Interfaces;

public interface IUserRepository
{
    Task<User> GetById(ObjectId id);

    Task<User> GetByEmail(string email);

    Task<IEnumerable<User>> GetManyByIds(IEnumerable<ObjectId> usersIds);

    Task Insert(User user);

    Task Update(User user);
}