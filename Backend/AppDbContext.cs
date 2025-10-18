using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    //make connection
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
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
            entity.Property(e => e.loginId).IsRequired();
            entity.Property(e => e.pw).IsRequired();
            entity.Property(e => e.Allergies).HasConversion(
                Item => string.Join(',', Item),
                Item => Item.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        });

        //Configure Inrgedient and shopping list

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Name).IsRequired().HasMaxLength(50);
            entity.Property(i => i.Quantity);
            entity.Property(i => i.OpenedDate);
            entity.Property(i => i.ExpiredDate);
            entity.Property(i => i.Category).HasConversion<string>().IsRequired().HasMaxLength(50); //store enum as string

            entity.HasOne(i => i.User)
                        .WithMany(u => u.Ingredients)
                        .HasForeignKey(i => i.UserId);

        });


        // modelBuilder.Entity<ShoppingList>().HasForeignKey(i => i.ShoppingListId);

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
            entity.Property(r => r.Description).HasMaxLength(200);
            entity.Property(r => r.ImageUrl);
            entity.Property(r => r.Steps).HasColumnType("TEXT");
        });

        //Associative entity
        modelBuilder.Entity<RecipeIngredient>(entity =>
          {
              entity.HasKey(ri => new { ri.RecipeId, ri.IngredientId });  // composite key

              entity.HasOne(ri => ri.Recipe)
                    .WithMany(r => r.Ingredients)
                    .HasForeignKey(ri => ri.RecipeId);

              entity.HasOne(ri => ri.Ingredient)
                    .WithMany(i => i.RecipeIngredients)
                    .HasForeignKey(ri => ri.IngredientId);

          });


        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Title).IsRequired().HasMaxLength(50);
            entity.Property(s => s.DateCreated).IsRequired();
        });


        // item for shopping list - one to many relationship
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Quantity);
            

            entity.HasOne(e => e.User)
                  .WithMany(s => s.Items)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

        });

        //seed data
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                firstName = "Joe",
                lastName = "Lian",
                preferredName = "Joe",
                loginId = "joelian1",
                pw = "joelian123"
            }
        );

        modelBuilder.Entity<Ingredient>().HasData(
       new Ingredient
       {
           Id = 1,
           Name = "Tomato",
           Category = IngredientCategory.VegetableAndFruit,
           Quantity = 6,
           OpenedDate = new DateTime(2025, 10, 15),
           ExpiredDate = new DateTime(2025, 10, 27),
           UserId = 1
       },
        new Ingredient
        {
            Id = 2,
            Name = "Milk",
            Category = IngredientCategory.Dairy,
            Quantity = 1,
            OpenedDate = new DateTime(2025, 10, 13),
            ExpiredDate = new DateTime(2025, 10, 18),
            UserId = 1
        },

      new Ingredient
      {
          Id = 3,
          Name = "Salmon",
          Category = IngredientCategory.Meat,
          Quantity = 7,
          OpenedDate = new DateTime(2025, 10, 15),
          ExpiredDate = new DateTime(2025, 11, 2),
          UserId = 1
      },
        new Ingredient
        {
            Id = 4,
            Name = "Egg",
            Category = IngredientCategory.Meat,
            Quantity = 12,
            OpenedDate = new DateTime(2025, 10, 12),
            ExpiredDate = new DateTime(2025, 11, 5),
            UserId = 1
        },

      new Ingredient
      {
          Id = 5,
          Name = "Coke",
          Category = IngredientCategory.Beverage,
          Quantity = 1,
          OpenedDate = new DateTime(2025, 10, 1),
          ExpiredDate = new DateTime(2026, 6, 20),
          UserId = 1
      },

      new Ingredient
      {
          Id = 6,
          Name = "Chicken",
          Category = IngredientCategory.Meat,
          Quantity = 3,
          OpenedDate = new DateTime(2025, 10, 17),
          ExpiredDate = new DateTime(2025, 10, 19),
          UserId = 1
      }
      );

        // Seed recipe
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = 1,
                Name = "Tomato Soup",
                Description = "Classic tomato soup recipe",
                ImageUrl = "https://example.com/soup.jpg",
                Steps = "Boil tomatoes, blend, add spices."
            },

        new Recipe
        {
            Id = 2,
            Name = "Tomato and Egg",
            Description = "Tasty tomato with fried egg",
            ImageUrl = "https://example.com/egg.jpg",
            Steps = "Cut tomator, fried scramble egg then mix together and add ketchup also seasoning."
        },

        new Recipe
        {
            Id = 3,
            Name = "Chicken and Coke",
            Description = "A famous Chinese sweet chicken dish",
            ImageUrl = "https://example.com/chicken.jpg",
            Steps = "Cut chicken, season with salt and pepper then pan-fry chicken until golden, put Coke and Chinese spices to braise until all cooked."
        },

        new Recipe
        {
            Id = 4,
            Name = "Baked Lemon Salmon",
            Description = "A simple lemon salmon with butter",
            ImageUrl = "https://example.com/salmon.jpg",
            Steps = "Season salmon, put to oevn or pan fry until turn golden, add butter and saute garlic, finish with lemon juice."
        },

        new Recipe
        {
            Id = 5,
            Name = "Salmon with Tomato",
            Description = "An easy and hearty salmon with tomato",
            ImageUrl = "https://example.com/tomatoSalmon.jpg",
            Steps = "Cut tomato in slices, season with salt, pan-fry tomato until soft then add salmon, cook until ready, add herbs."
        }
        );

        modelBuilder.Entity<RecipeIngredient>().HasData(
    new RecipeIngredient
    {
        RecipeId = 1,
        IngredientId = 1, // tomato
        Quantity = 2
    },

    //tomato and egg - match 2
    new RecipeIngredient
    {
        RecipeId = 2,
        IngredientId = 1, // tomato
        Quantity = 2
    },

   new RecipeIngredient
   {
       RecipeId = 2,
       IngredientId = 4, // egg
       Quantity = 2
   },

   //chicken and coke 
   new RecipeIngredient
   {
       RecipeId = 3,
       IngredientId = 6, // Chicken
       Quantity = 1
   },
    new RecipeIngredient
    {
        RecipeId = 3,
        IngredientId = 5, // Coke
        Quantity = 1
    },


    // Baked Lemon Salmon
    new RecipeIngredient
    {
        RecipeId = 4,
        IngredientId = 3, // Salmon
        Quantity = 1
    },

    // Salmon with Tomato
    new RecipeIngredient
    {
        RecipeId = 5,
        IngredientId = 3, // Salmon
        Quantity = 1
    },
    new RecipeIngredient
    {
        RecipeId = 5,
        IngredientId = 1, // Tomato
        Quantity = 1
    }
);
        modelBuilder.Entity<Item>().HasData(
            new Item
            {
                Id = 1,
                Name = "Eggs",
                Quantity = 12,
                UserId = 1
            },
            new Item
            {
                Id = 2,
                Name = "Bread",
                Quantity = 1,
                UserId = 1
            },
            new Item
            {
                Id = 3,
                Name = "Milk",
                Quantity = 2,
                UserId = 1
            }
        );

    }
}