using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Data;
using Backend.Dtos;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.Security.Cryptography;

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

    public async Task<List<Recipe>> GetRecipesByAvailableIngredient(int userId)
    {
        //get user ingredients bane
        var userIngredientNames = await _context.Ingredients
            .Where(i => i.UserId == userId)
            .Select(i => i.Name.ToLower())
            .ToListAsync();


        var matchedRecipes = await (
         from recipe in _context.Recipes
         where recipe.Ingredients.All(ri => userIngredientNames.Contains(ri.Ingredient.Name.ToLower()))
         select recipe
         ).Include(r => r.Ingredients)
         .ThenInclude(ri => ri.Ingredient)
         .ToListAsync();

        return matchedRecipes;

    }
}

//GeeksforGeeks. (2019, March 14). HashSet in C#. GeeksforGeeks. https://www.geeksforgeeks.org/c-sharp/hashset-in-c-sharp/