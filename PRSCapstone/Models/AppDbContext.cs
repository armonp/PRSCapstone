using Microsoft.EntityFrameworkCore;
using System;

namespace PRSCapstone.Models {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLines> RequestLines { get; set; }

        protected override void OnModelCreating(ModelBuilder model) {
            model.Entity<User>(u => {
                u.HasIndex(x => x.Username).IsUnique();
            });
            model.Entity<Vendor>(v => {
                v.HasIndex(x => x.Code).IsUnique();
            });
            model.Entity<Product>(p => {
                p.HasIndex(x => x.PartNbr).IsUnique();
            });
        }
        //Check if a user is and Admin and has access to maintenance functions
        //public User _loggedinuser = new User { Id = 3, Firstname = "Testing", Lastname = "User", Username = "user2", Password = "user2", Phone = null, Email = null, IsReviewer = true, IsAdmin = true };

        //public void CheckAdmin(User loggedinuser) {
        //    if (!loggedinuser.IsAdmin) throw new Exception("User does not have permission to access this function");
        //}
    }

}
