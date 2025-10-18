using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Data;
using Backend.Dtos;
using System.Data.SqlTypes;
using System.ComponentModel;


//ensure dto mapping only in recipe 

namespace Backend.Services
{

    public class RecipeService : IRecipeService
    {

        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _context.Recipes.ToListAsync();

        }
        public async Task<Recipe?> GetRecipeById(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task<Recipe?> AddRecipe(RecipeCreatedDto recipeDto)
        {
            var recipe = new Recipe
            {
                Name = recipeDto.Name,
                Category = recipeDto.Category,
                Description = recipeDto.Description,
                ImageUrl = recipeDto.ImageUrl,
                Steps = recipeDto.Steps,
                RecipeIngredients= new List<RecipeIngredient>()
            };
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe?> UpdateRecipe(int id, RecipeUpdatedDto updatedRecipeDto)
        {
            var existingRecipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRecipe == null)
            {
                return null;
            }

            existingRecipe.Name = updatedRecipeDto.Name;
            existingRecipe.Category = updatedRecipeDto.Category;
            existingRecipe.Description = updatedRecipeDto.Description;
            existingRecipe.ImageUrl = updatedRecipeDto.ImageUrl;
            existingRecipe.Steps = updatedRecipeDto.Steps;

            await _context.SaveChangesAsync();
            return existingRecipe;
        }

        public async Task<Recipe?> DeleteRecipe(int id)
        {
            var existingRecipe = await _context.Recipes.FindAsync(id);
            if (existingRecipe == null)
            {
                return null;
            }

            _context.Recipes.Remove(existingRecipe);
            await _context.SaveChangesAsync();
            return existingRecipe;

        }

        public async Task<List<Recipe>> GetRecipesByCategory(RecipeCategory category)
        {
            return await _context.Recipes.Where(r => r.Category == category)
            .ToListAsync();
        }
        public async Task<List<RecipeSuggestionDto>> getRecipesByAvailableIngredient(int userId)
        {
            //get user's ingredient - return the ingredient name
            var userIngredientName = await _context.Ingredients
            .Where(i => i.UserId == userId)
            .Select(i => i.Name.ToLower())
            .ToListAsync();

            //get all recipes with ingredients
            var allRecipes = await _context.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .ToListAsync();

            var suggestions = new List<RecipeSuggestionDto>();
            //match ingredient name
            foreach (var recipe in allRecipes)
            {
                //required ingredients for the recipe
                var requiredIngredientNames = recipe.RecipeIngredients
                .Select(ri => ri.Ingredient.Name.ToLower())
                .ToList();

                //to find what missing 
                var missingIngredients = requiredIngredientNames
                .Where(name => !userIngredientName.Contains(name))
                .ToList();

                //make a suggestion dto for display 
                suggestions.Add(new RecipeSuggestionDto
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Category = recipe.Category,
                    ImageUrl = recipe.ImageUrl,
                    Steps = recipe.Steps,
                    TotalIngredients = requiredIngredientNames.Count,
                    MissingIngredientCounts = missingIngredients.Count,
                    MissingIngredients = missingIngredients
                });
            }
            return suggestions.OrderBy(s => s.MissingIngredientCounts).ToList();
        }
    }
}

//GeeksforGeeks. (2019, March 14). HashSet in C#. GeeksforGeeks. https://www.geeksforgeeks.org/c-sharp/hashset-in-c-sharp/