using System.Reflection;
using ImaginaryRealEstate.Entities;
using MongoFramework;

namespace ImaginaryRealEstate;

public class DomainDbContext: MongoDbContext
{

    public MongoDbSet<User> Users { get; set; }
    public MongoDbSet<Offer> Offers { get; set; }
    public MongoDbSet<Image> Images { get; set; }


    public DomainDbContext(IMongoDbConnection connection): base(connection)
    {
        
    }

    protected override void OnConfigureMapping(MappingBuilder mappingBuilder)
    {
        
    }

    // protected override IMongoDbSet OnDbSetCreation(PropertyInfo property, IDbSetOptions dbSetOptions)
    // {
    //     return
    // }
}