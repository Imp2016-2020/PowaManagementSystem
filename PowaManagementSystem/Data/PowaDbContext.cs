using Microsoft.EntityFrameworkCore;
using PowaManagementSystem.Models;
using System.Drawing;

namespace PowaManagementSystem.Data
{
    public class PowaDbContext : DbContext
    {
        public PowaDbContext(DbContextOptions<PowaDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Woman> Women { get; set; }
        public DbSet<SocialWorker> SocialWorkers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BoardMember> BoardMembers { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}