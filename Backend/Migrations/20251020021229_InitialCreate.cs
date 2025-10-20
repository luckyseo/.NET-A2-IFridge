using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Steps = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientList = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    preferredName = table.Column<string>(type: "TEXT", nullable: false),
                    loginId = table.Column<string>(type: "TEXT", nullable: false),
                    pw = table.Column<string>(type: "TEXT", nullable: false),
                    Allergies = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    OpenedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeName = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientName = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShoppingListId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "IngredientList", "Name", "Steps" },
                values: new object[,]
                {
                    { 1, "Soup", "Classic tomato soup recipe", "tomatoSoup.png", "Tomato,Carrot,Onion", "Tomato Soup", "Boil tomatoes, add some carrot and onion, blend, add spices." },
                    { 2, "Side", "Tasty tomato with fried egg", "tomatoAndEgg.png", "Tomato,Egg,SoySauce", "Tomato and Egg", "Cut tomator, fried scramble egg then mix together and add ketchup also seasoning." },
                    { 3, "Side", "A famous Chinese sweet chicken dish", "ChickenWithCoke.png", "Chicken,Coke,SoySauce", "Chicken and Coke", "Cut chicken, season with salt and pepper then pan-fry chicken until golden, put Coke and Soy sauce to braise until all cooked." },
                    { 4, "Main", "A simple lemon salmon with butter", "BakedLemonSalmon.png", "Salmon,Lemon,Butter,Garlic", "Baked Lemon Salmon", "Season salmon, put to oevn or pan fry until turn golden, add butter and saute garlic, finish with lemon juice." },
                    { 5, "Main", "An easy and hearty salmon with tomato", "SalmonAndTomato.png", "Tomato,Salmon,Onion", "Salmon with Tomato", "Cut tomato in slices, season with salt, pan-fry tomato until soft then add salmon, saute onion, cook until ready, add herbs." },
                    { 6, "Main", "A Japanese style beef eat with udon", "TeriyakiAndUdon.png", "Beef,Teriyaki,Udon,Garlic", "Teriyaki beef with udon", "Stir fry sliced beef with teryaki sauce and boil some udon to go with" },
                    { 7, "Main", "Classic main course", "SteakAndMaskedPotato.png", "Beef,Butter,Potato,Gravy", "Steak with mashed potato", "Season steak with salt and pepper, pan fry steak with olive oil and butter, prepare mashed potato and gravy sauce" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Allergies", "firstName", "lastName", "loginId", "preferredName", "pw" },
                values: new object[] { 1, "", "Joe", "Lian", "joelian1", "Joe", "joelian123" });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Category", "ExpiredDate", "Name", "OpenedDate", "Quantity", "UserId" },
                values: new object[,]
                {
                    { 1, "VegetableAndFruit", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tomato", new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1 },
                    { 2, "Dairy", new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milk", new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 3, "Meat", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salmon", new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1 },
                    { 4, "Meat", new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egg", new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 1 },
                    { 5, "Beverage", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Coke", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 6, "Meat", new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chicken", new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "Quantity", "ShoppingListId", "UserId" },
                values: new object[,]
                {
                    { 1, "Eggs", 12, null, 1 },
                    { 2, "Bread", 1, null, 1 },
                    { 3, "Milk", 2, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ShoppingListId",
                table: "Items",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
