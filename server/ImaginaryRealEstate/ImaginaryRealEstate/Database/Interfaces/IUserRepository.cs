using ImaginaryRealEstate.Entities;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Database.Interfaces;

public interface IUserRepository
{
    Task<User> GetById(ObjectId id);

    Task<User> GetByEmail(string email);

    Task Insert(User user);

    Task Update(User user);
}