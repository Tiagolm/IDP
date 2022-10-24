using Domain.Core;
using Domain.Models;
using Domain.Models.Auth;
using Infrastructure.Data.Mappings.Base;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneContact> PhoneContacts { get; set; }
        public DbSet<PhoneContactType> PhoneContactTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ApplyConfiguration(new EnumerationBaseMap<UserRole>());

            modelBuilder
                .Entity<PhoneContactType>()
                .HasData(Enumeration.GetAll<PhoneContactType>());

            modelBuilder
                .Entity<UserRole>()
                .HasData(Enumeration.GetAll<UserRole>());

            // seed do primeiro user admin
            modelBuilder
                .Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = PasswordHasher.Hash("Pass123$"),
                    Name = "Admin Root Application",
                    CreatedAt = DateTime.ParseExact("13/10/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRoleId = UserRole.Admin.Id
                });
        }
    }
}