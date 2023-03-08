using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImaginaryRealEstate.Database;

public class UserRepository: IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly IMongoCollection<User> _usersCollection;

    public UserRepository(IMongoClient mongoClient, ILogger<UserRepository> logger, MongoSettings settings)
    {
        _logger = logger;
        _usersCollection = mongoClient.GetDatabase(settings.DatabaseName)
            .GetCollection<User>(MongoConsts.UsersCollectionName);
    }

    public async Task<User> GetById(ObjectId id) =>
        await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();

    public async Task<User> GetByEmail(string email) =>
        await _usersCollection.Find(user => user.Email == email).FirstOrDefaultAsync();

    public async Task<IEnumerable<User>> GetManyByIds(IEnumerable<ObjectId> usersIds) =>
        await _usersCollection.Find(user => usersIds.Contains(user.Id)).ToListAsync();
    

    public async Task Insert(User user) =>
        await _usersCollection.InsertOneAsync(user);

    public async Task Update(User user)
    {
        await _usersCollection.ReplaceOneAsync((oldUser) => oldUser.Id == user.Id, user);
    }
}