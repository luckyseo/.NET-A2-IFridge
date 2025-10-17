using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Dtos;
using Backend.Services;

namespace Backend.Interface;
public interface IIngredientService
{
    //view available ingredient- IngredientService
    //view expired ingredient- ExpiredIngredientService
    //shared method 

    Task<List<Ingredient>> GetAllIngredient(int userId);
    Task<Ingredient?> GetIngredientById(int id);
    Task<Ingredient> AddIngredient(Ingredient ingredient);
    Task<Ingredient?> UpdateIngredient(int id, Ingredient updatedIngredient);
    Task<Ingredient?> DeleteIngredient(int id);
    Task<List<IngredientDto>> GetExpiredIngredient();

    //Other method
    //GetExpiredIngredient()
    // Task<List<Ingredient>> GetExpiredIngredient(DateTime startDate, DateTime endDate);
}


