﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VendorManagement2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // represent the gateway between our C# application and databasse
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get;set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        
        public DbSet<CategoryEvent> CategoryEvents { get; set; }

        public DbSet<Component> Components { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<BuildComponent> BuildComponents { get; set; }
        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }
    }
}