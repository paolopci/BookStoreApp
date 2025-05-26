using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<City> Cities { get; set; } = null!;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customize the ASP.NET Identity model and override the defaults if needed.
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Authors_Id");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Books_Id");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA7C84AE11").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(250);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_Authors");
        });


        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "62676900-72A8-407E-8E9F-250E7AAC1113"
            },
            new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                Id = "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124"
            }
        );


        var hasher = new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser
            {
                Id = "29EC137D-88AF-4437-A719-28B3A65F287D",
                Email = "admin@bookstore.com",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                UserName = "admin@bookstore.com",
                NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                FirstName = "Paolo",
                LastName = "Paci",
                PasswordHash = hasher.HashPassword(null, "Micene@65"),
            },
            new ApiUser
            {
                Id = "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69",
                Email = "user@bookstore.com",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                UserName = "user@bookstore.com",
                NormalizedUserName = "USER@BOOKSTORE.COM",
                FirstName = "User1",
                LastName = "User2",
                PasswordHash = hasher.HashPassword(null, "Micene@65"),
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "62676900-72A8-407E-8E9F-250E7AAC1113", // User
                UserId = "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69" // user@bookstore.com
            },
            new IdentityUserRole<string>
            {
                RoleId = "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", // Administrator
                UserId = "29EC137D-88AF-4437-A719-28B3A65F287D" // admin@bookstore.com
            }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
