using Microsoft.EntityFrameworkCore;
using NetCore.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Services.Data
{
    public class CodeFirstDbContext : DbContext
    {
        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserRolesByUser> UserRolesByUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<UserRolesByUser>().HasKey(c => new { c.UserId, c.RoleId });            

            modelBuilder.Entity<User>(e =>
            {
                e.Property(c => c.IsMembershipWithdrawn).HasDefaultValue(value: false);
                e.Property(c => c.JoinedUTCDate).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserRole>(e =>
            {
                e.Property(c => c.ModifiedUtcDate).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserRolesByUser>(e =>
            {
                e.Property(c => c.OwnedUtcDate).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>().HasIndex(c => new { c.UserEmail }).IsUnique(true);
        }
    }
}
