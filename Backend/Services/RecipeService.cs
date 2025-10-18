using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Data;
using Backend.Dtos;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.Security.Cryptography;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Data.Common;

namespace Backend.Services;

public class RecipeService : IRecipeService
{
    private readonly AppDbContext _context;

    //through db 
    public RecipeService(AppDbContext context)
    {
        _context = context;
    }

    //get all recipe - for browsing everything 
    public async Task<List<Recipe>> GetAllRecipes()
    {
        return await _context.Recipes.ToListAsync();
    }
    public async Task<Recipe?> GetRecipeById(int id)
    {
        return await _context.Recipes.FindAsync(id);
    }

    //let user filter all recipes by category just everything for browsing
    public async Task<List<Recipe>> GetRecipesByCategory(RecipeCategory Category)
    {
        return await _context.Recipes
             .Where(r => r.Category == Category)
             .ToListAsync();
    }

    public async Task<Recipe> AddRecipe(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe?> UpdateRecipe(int id, Recipe updatedRecipe)
    {
        //find id then update 
        var existingRecipe = await _context.Recipes.FindAsync(id);
        if (existingRecipe == null)
        {
            return null;
        }

        //Update data 
        existingRecipe.Name = updatedRecipe.Name;
        existingRecipe.Category = updatedRecipe.Category;
        existingRecipe.Description = updatedRecipe.Description;
        existingRecipe.ImageUrl = updatedRecipe.ImageUrl;
        existingRecipe.Steps = updatedRecipe.Steps;

        await _context.SaveChangesAsync(); //save changes
        return existingRecipe;
    }

    public async Task<Recipe> DeleteRecipe(int id)
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

    //get recipes that can be made using available ingredient
    //make a availale ingredient dto 
    //dto act as a termporary storage because the ingredients can always change 
    public async Task<List<RecipeSuggestionDto>> GetRecipesByAvailableIngredient(int userId)
    {
        //get user's ingredient name 
        var userIngredientNames = await _context.Ingredients
        .Where(i => i.UserId == userId)
        .Select(i => i.Name.ToLower())
        .ToListAsync();

        //get recipes with their ingredients
        var allRecipes = await _context.Recipes
      .Include(r => r.RecipeIngredients)
      .ThenInclude(ri => ri.Ingredient)
      .ToListAsync();

        //return suggested recipe
        var suggestions = new List<RecipeSuggestionDto>();

        foreach (var recipe in allRecipes)
        {
            var requiredIngredientNames = recipe.RecipeIngredients
                .Select(ri => ri.Ingredient.Name.ToLower())
                .ToList();

            var missingIngredients = requiredIngredientNames
            .Where(name => !userIngredientNames.Contains(name))
            .ToList();

            //Build dto 
            var suggestionDto = new RecipeSuggestionDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Category = recipe.Category,
                ImageUrl = recipe.ImageUrl,
                Steps = recipe.Steps,
                TotalIngredients = requiredIngredientNames.Count(),
                MissingIngredientsCount = missingIngredients.Count(),
                MissingIngredients = missingIngredients
            };
            suggestions.Add(suggestionDto);
        }
        return suggestions.OrderBy(s => s.MissingIngredientsCount).ToList();
    }



}

//GeeksforGeeks. (2019, March 14). HashSet in C#. GeeksforGeeks. https://www.geeksforgeeks.org/c-sharp/hashset-in-c-sharp/