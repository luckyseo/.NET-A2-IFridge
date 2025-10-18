using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Data;
using Backend.Dtos;

namespace Backend.Services;

public class IngredientService : IIngredientService
{
    private readonly AppDbContext _context;

    //through db 
    public IngredientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ingredient>> GetAllIngredient(int userId)
    {
        return await _context.Ingredients.Where(i=>i.UserId ==userId).OrderBy(i=>i.ExpiredDate).ToListAsync();
    }
    public async Task<Ingredient?> GetIngredientById(int id)
    {
        return await _context.Ingredients.FindAsync(id);
    }
    public async Task<Ingredient> AddIngredient(Ingredient ingredient)
    {
        _context.Ingredients.Add(ingredient); //add to table
        await _context.SaveChangesAsync();
        return ingredient;
    }
    public async Task<Ingredient?> UpdateIngredient(int id, Ingredient updatedIngredient)
    {
        //find id then update 
        var existingIngredient = await _context.Ingredients.FindAsync(id);
        if (existingIngredient == null)
        {
            return null;
        }

        //Update data 
        existingIngredient.Name = updatedIngredient.Name;
        existingIngredient.Quantity = updatedIngredient.Quantity;
        existingIngredient.Category = updatedIngredient.Category;
        existingIngredient.OpenedDate = updatedIngredient.OpenedDate;
        existingIngredient.ExpiredDate = updatedIngredient.ExpiredDate;

        await _context.SaveChangesAsync(); //save changes
        return existingIngredient;
    }

    public async Task<Ingredient?> DeleteIngredient(int id)
    {
        var existingIngredient = await _context.Ingredients.FindAsync(id);
        if(existingIngredient == null)
        {
            return null;
        }
        _context.Ingredients.Remove(existingIngredient);
        await _context.SaveChangesAsync();
        return existingIngredient;
    }
    
    public async Task<List<IngredientDto>> GetExpiredIngredient()
    {
        var threshold = DateTime.Today.AddDays(3);

        var expired = await _context.Ingredients
        .Where(i => i.ExpiredDate.HasValue && i.ExpiredDate < threshold)
        .ToListAsync();

        return expired.Select(e => new IngredientDto
        {
            Id = e.Id,
            Name = e.Name,
            ExpiredDate = e.ExpiredDate.Value
        }).ToList();
    }

}

