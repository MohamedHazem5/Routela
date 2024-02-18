using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Routela.Models;

namespace Routela.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppRole>().ToTable("Roles");
            builder.Entity<User>().ToTable("Users");
            builder.Entity<AppUserRole>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            #region Renaming
            builder.Entity<User>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", "security");
            #endregion

            builder.Entity<User>()
             .HasMany(ur => ur.UserRoles)
             .WithOne(u => u.User)
             .HasForeignKey(ur => ur.UserId)
             .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<Comment>()
               .HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserReview>()
             .HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserCourse>()
             .HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Order>()
            .HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Blog>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Course>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserReview> Reviews { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCourse> TagsCourses { get; set; }
        public DbSet<TagBlog> TagsBlogs { get; set; }
        public DbSet<UserCourse> UsersCourses { get; set; }
    }
}