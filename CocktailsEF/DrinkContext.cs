using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class DrinkContext : DbContext
    {
        public DrinkContext() : base()
        { 
        }

        public DbSet<Drink> Drinks { get;set; }
        public DbSet<Liquid> Liquid { get;set; }

        public DbSet<XtraIngredient> XtraIngredients { get;set; }

        public DbSet<Amount> Amounts { get;set; }

        public DbSet<AmountIngredient> AmountIngredients { get;set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;User ID=root;Password=Muffin123;Database=Drink");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>(entity =>
            {
                entity.HasIndex(e => e.ID).IsUnique();
            });

            modelBuilder.Entity<Liquid>(entity =>
            {
                entity.HasIndex(e => e.ID).IsUnique();
            });

            modelBuilder.Entity<XtraIngredient>(entity =>
            {
                entity.HasIndex(e => e.ID).IsUnique();
            });

            modelBuilder.Entity<Amount>(entity =>
            {
                entity.HasIndex(e => e.ID).IsUnique();
            });


            modelBuilder.Entity<AmountIngredient>().HasKey(sc => new { sc.IngredientID, sc.AmountID });
        }
    }
}
