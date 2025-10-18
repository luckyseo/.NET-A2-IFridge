using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Data;
using Backend.Dtos;

namespace Backend.Interface;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllRecipes();
    Task<Recipe?> GetRecipeById(int id);
    Task<Recipe?> AddRecipe(RecipeDto recipeDto);
    Task<Recipe?> UpdateRecipe(int id, RecipeDto updatedRecipeDto);
    Task<Recipe?> DeleteRecipe(int id);
    Task<List<Recipe>> GetRecipesByCategory(RecipeCategory category);
    Task<List<RecipeSuggestionDto>> getRecipesByAvailableIngredient(int userId);

}



