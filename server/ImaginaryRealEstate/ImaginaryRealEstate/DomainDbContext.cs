using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImaginaryRealEstate;

// TODO: Add connection string into configuration file
public class DomainDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Offer> Offers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=imaginary-real-estate; Username=postgres; Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(ent =>
        {
            ent.Property(x => x.Email).IsRequired();
            ent.Property(x => x.Role).IsRequired().HasDefaultValue(Roles.User);
            ent.Property(x => x.FirstName).IsRequired();
            ent.Property(x => x.LastName).IsRequired();
            ent.Property(x => x.DateOfBirth).IsRequired();
        });

        modelBuilder.Entity<Offer>(ent =>
        {
            // Relations
            ent
                .HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(p => p.AuthorId);

            ent.HasMany(p => p.Images)
                .WithOne(i => i.Offer)
                .HasForeignKey(i => i.OfferId);

            // Properties
            ent.Property(x => x.Title).IsRequired();
            ent.Property(x => x.Address).IsRequired();
            ent.Property(x => x.Area).IsRequired();
            ent.Property(x => x.Bathrooms).IsRequired();
            ent.Property(x => x.Bedrooms).IsRequired();
            ent.Property(x => x.Description).IsRequired().HasMaxLength(10240);
            ent.Property(x => x.Price).IsRequired();
        });

        modelBuilder.Entity<Image>(ent =>
        {
            ent.Property(x => x.FileName).IsRequired();
            ent.Property(x => x.Url).IsRequired();
            ent.Property(x => x.IsFrontPhoto).IsRequired().HasDefaultValue(false);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}