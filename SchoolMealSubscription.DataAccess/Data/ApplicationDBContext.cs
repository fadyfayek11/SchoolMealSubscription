using SchoolMealSubscription.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolMealSubscription.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<UserClub> UserClubs { get; set; }
    public DbSet<BookComments> Comments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // User-Club many-to-many relationship
        modelBuilder.Entity<UserClub>()
            .HasKey(uc => new { uc.UserId, uc.ClubId }); // Composite key

        modelBuilder.Entity<BookComments>()
            .HasOne(bc => bc.Book)
            .WithMany(b => b.BookComments)
            .HasForeignKey(bc => bc.BookId);

        modelBuilder.Entity<Club>()
            .HasMany(c => c.Users)
            .WithMany(u => u.Clubs)
            .UsingEntity(j => j.ToTable("ClubUsers"));

        // One-to-Many relationship between Club and Owner (ApplicationUser)
        modelBuilder.Entity<Club>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.OwnedClubs)
            .HasForeignKey(c => c.OwnerId);

        // Configure User-BookComments relationship
        modelBuilder.Entity<BookComments>()
            .HasOne(bc => bc.User)
            .WithMany()
            .HasForeignKey(bc => bc.UserId);
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        );
        var hasher = new PasswordHasher<ApplicationUser>();
        modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "admin@Mostakel.com",
                    NormalizedUserName = "ADMIN@Mostakel.COM",
                    Email = "admin@Mostakel.com",
                    NormalizedEmail = "ADMIN@Mostakel.COM",
                    EmailConfirmed = true,
                    IsAdmin = true,
                    PasswordHash = hasher.HashPassword(null, "P@ssword123")
                });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            });
    }
}
