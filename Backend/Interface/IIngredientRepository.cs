using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;


namespace Backend.Interface;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetAll();
    Task<Ingredient?> GetById(int id);
    Task<List<Ingredient>> GetExpired(DateTime startDate, DateTime endDate);
    Task Add(Ingredient ingredient);
    Task Update(Ingredient ingredient);
    Task Delete(Ingredient ingredient);
}