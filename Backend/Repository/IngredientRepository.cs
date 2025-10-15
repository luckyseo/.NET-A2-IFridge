using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using System.Diagnostics.Contracts;

namespace Backend.Repository;

public class IngredientRepository : IIngredientRepository
{

    //instead of access data directly to db
    //access through repository
    private readonly AppDbContext _context;
    public IngredientRepository(AppDbContext context)
    {
        _context = context;
    }

    //signature methods

    public async Task<List<Ingredient>> GetAll()
    {
        return await _context.Ingredients.ToListAsync();
    }

    public async Task<Ingredient?> GetById(int id)
    {
        return await _context.Ingredients.FindAsync(id);

    }

    public async Task Add(Ingredient ingredient)
    {
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Ingredient ingredient)
    {
        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync();
    }

// get expiring ingedient
    public async Task<List<Ingredient>> GetExpired(DateTime startDate, DateTime endDate)
    {
        return await _context.Ingredients.
        Where(e => e.ExpiredDate.HasValue && e.ExpiredDate >= startDate && e.ExpiredDate <= endDate)
        .ToListAsync();
    }
}