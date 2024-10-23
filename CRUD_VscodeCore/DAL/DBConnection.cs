using System;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public class DBConnection : DbContext
    {
        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {
        }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Login> Logins { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>().HasNoKey();
            modelBuilder.Entity<Login>().HasNoKey();
           
        }
    }
}