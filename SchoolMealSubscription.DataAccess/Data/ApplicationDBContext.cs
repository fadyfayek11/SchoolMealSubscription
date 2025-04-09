using SchoolMealSubscription.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolMealSubscription.Models.Entities;

namespace SchoolMealSubscription.DataAccess.Data;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = true;
    }
  
    public new DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<FoodType> FoodTypes { get; set; }
    public DbSet<StudentFoodPreference> StudentFoodPreferences { get; set; }
    public DbSet<Orders> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Parent",
                NormalizedName = "PARENT"
            }
        );
        var hasher = new PasswordHasher<ApplicationUser>();
        modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "admin@schoool.com",
                    FullName = "Admin",
                    NormalizedUserName = "ADMIN@SCHOOL.COM",
                    Email = "admin@schoool.com",
                    NormalizedEmail = "ADMIN@SCHOOL.COM",
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


    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Grades
        modelBuilder.Entity<Grade>().HasData(
            new Grade { GradeId = 1, Name = "الصف الأول" },
            new Grade { GradeId = 2, Name = "الصف الثاني" },
            new Grade { GradeId = 3, Name = "الصف الثالث" },
            new Grade { GradeId = 4, Name = "الصف الرابع" },
            new Grade { GradeId = 5, Name = "الصف الخامس" },
            new Grade { GradeId = 6, Name = "الصف السادس" }
        );

        // Seed Schools
        modelBuilder.Entity<School>().HasData(
            new School { SchoolId = 1, Name = "مدرسة الأمل" },
            new School { SchoolId = 2, Name = "مدرسة المستقبل" },
            new School { SchoolId = 3, Name = "مدرسة النور" },
            new School { SchoolId = 4, Name = "مدرسة الفرسان" }
        );

        // Seed FoodTypes
        modelBuilder.Entity<FoodType>().HasData(
            new FoodType { FoodTypeId = 1, Name = "سلطات", Price = 10},
            new FoodType { FoodTypeId = 2, Name = "فواكه", Price = 20},
            new FoodType { FoodTypeId = 3, Name = "منتجات الألبان", Price = 25},
            new FoodType { FoodTypeId = 4, Name = "حبوب كاملة", Price = 15},
            new FoodType { FoodTypeId = 5, Name = "عصائر طبيعية", Price = 15 }
        );
    }
}
