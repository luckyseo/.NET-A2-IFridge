using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Data;
using Backend.Dtos;
using System.Data.SqlTypes;
using System.ComponentModel;

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
        existingRecipe.Description = updatedRecipe.Description;
        existingRecipe.ImageUrl = updatedRecipe.ImageUrl;
        existingRecipe.Steps = updatedRecipe.Steps;
        existingRecipe.Category = updatedRecipe.Category;

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
    //make a available ingredient dto 

    public async Task<List<RecipeSuggestionDto>> GetRecipesByAvailableIngredient(int userId)
    {
        var userIngredients = await _context.Ingredients.Where(u => u.UserId == userId).ToListAsync();

        var availableIngredient = userIngredients.Select(u => u.Name.ToLower()).ToHashSet();
        //using hash set for faster lookup 

        var recipes = await _context.Recipes
        .Include(i => i.Name.ToLower())
        .Select(r => new RecipeSuggestionDto
        {
            Id = r.Id,
            Name = r.Name,
            TotalRequired = r.Ingredients.Count,
            MatchCount = r.Ingredients.Count(ri => availableIngredient.Contains(ri.IngredientName.ToLower()))
        })
        .OrderByDescending(r => r.MatchCount)
        .ToListAsync();

        return recipes;
    }

}

//GeeksforGeeks. (2019, March 14). HashSet in C#. GeeksforGeeks. https://www.geeksforgeeks.org/c-sharp/hashset-in-c-sharp/