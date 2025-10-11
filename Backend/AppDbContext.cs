using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.AppData;

public class AppDbContext:DbContext {
    //make connection
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<ShoppingListItems> ShoppingListItems { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<User> Users { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        /*
        Entity Framework core! Code > table 
        Define table here
        hasKey : which property is the PK?
        Property: condfigure other properties

        */
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.firstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.lastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.preferredName);
            entity.Property(e => e.id).IsRequired();
            entity.Property(e => e.pw).IsRequired();
            entity.Property(e => e.Allergies).HasConversion(
                Item => string.Join(',', Item),
                Item => Item.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        });

        // Configure your entities here if needed
        // modelBuilder.Entity<Item>(entity =>
        // {
        //     entity.HasKey(e => e.Id);
        //     entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        //     entity.Property(e => e.Description).HasMaxLength(500);
        // });

       // modelBuilder.Entity<ShoppingList>().HasForeignKey(i => i.ShoppingListId);
        
    }
}