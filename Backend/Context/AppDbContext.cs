using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<User> Users{get; set;}
         public DbSet<Products> Products { get; set; } 
         public DbSet<Carts> Carts{get;set;}
         public DbSet<Feedback> Feedback{get;set;}
   
         public DbSet<Checkout> Checkout{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<User>().HasKey(Users=>Users.Id);
            modelBuilder.Entity<User>().HasMany(Users=>Users.carts);
            modelBuilder.Entity<Carts>().HasKey(Carts=>Carts.Id);          
            modelBuilder.Entity<Feedback>().HasKey(feedback=>feedback.Id);
          
            modelBuilder.Entity<Checkout>().HasKey(order=>order.Id);
            modelBuilder.Entity<Checkout>().HasMany(order=>order.cart);


        }
    }
}