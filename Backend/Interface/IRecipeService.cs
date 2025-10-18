using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Data;
using Backend.Dtos;

namespace Backend.Interface;

public interface IRecipeService
{

    //view recipe that are based on available ingredient 
    //view recipe ->  based on category 
    //can edit the recipe espcially delete recipe they don't like

    Task<List<Recipe>> GetAllRecipes();
    Task<Recipe?> GetRecipeById(int id);

    Task<Recipe> AddRecipe(Recipe recipe);
    Task<Recipe> UpdateRecipe(int id, Recipe updatedRecipe);
    Task<Recipe> DeleteRecipe(int id);

    //get recipes that can be made using available ingredient
    Task<List<Recipe>> GetRecipesByCategory(RecipeCategory category);
    Task<List<Recipe?>> GetRecipesByAvailableIngredient(int userId);

}



