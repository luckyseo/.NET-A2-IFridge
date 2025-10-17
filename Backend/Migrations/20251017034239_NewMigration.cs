using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
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
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Steps = table.Column<string>(type: "TEXT", nullable: false)
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
                    status = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShoppingListId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Steps" },
                values: new object[,]
                {
                    { 1, "Classic tomato soup recipe", "https://example.com/soup.jpg", "Tomato Soup", "Boil tomatoes, blend, add spices." },
                    { 2, "Tasty tomato with fried egg", "https://example.com/egg.jpg", "Tomato and Egg", "Cut tomator, fried scramble egg then mix together and add ketchup also seasoning." },
                    { 3, "A famous Chinese sweet chicken dish", "https://example.com/chicken.jpg", "Chicken and Coke", "Cut chicken, season with salt and pepper then pan-fry chicken until golden, put Coke and Chinese spices to braise until all cooked." },
                    { 4, "A simple lemon salmon with butter", "https://example.com/salmon.jpg", "Baked Lemon Salmon", "Season salmon, put to oevn or pan fry until turn golden, add butter and saute garlic, finish with lemon juice." },
                    { 5, "An easy and hearty salmon with tomato", "https://example.com/tomatoSalmon.jpg", "Salmon with Tomato", "Cut tomato in slices, season with salt, pan-fry tomato until soft then add salmon, cook until ready, add herbs." }
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
                table: "RecipeIngredients",
                columns: new[] { "IngredientId", "RecipeId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 1, 2, 2 },
                    { 4, 2, 2 },
                    { 5, 3, 1 },
                    { 6, 3, 1 },
                    { 3, 4, 1 },
                    { 1, 5, 1 },
                    { 3, 5, 1 }
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
