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
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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