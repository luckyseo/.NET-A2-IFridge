using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using System.Diagnostics.Contracts;

namespace Backend.Repository;

public class IngredientService
{

    //from repo -> service -> controller 
    private readonly IIngredientRepository _repo;

    public IngredientService(IIngredientRepository repo)
    {
        _repo = repo;
    }


    public async Task<List<Ingredient>> GetAll()
        => await _repo.GetAll();

    public async Task<Ingredient> GetById(int id)
        => await _repo.GetById(id);

    public async Task<List<IngredientDto>> GetExpiredIngredient()
    {
        var today = DateTime.Today;
        var threshold = today.AddDays(3);

        var expired = await _repo.GetExpiredAsync(today, threshold);
        return expired
            .Select(e => new IngredientDto
            {
                Id = e.Id,
                Name = e.Name,
                ExpiredDate = e.ExpiredDate!.Value
            }).ToList();
    }

    public async Task<Ingredient> Add(Ingredient ingredient)
    {
        await _repo.Add(ingredient);
        return ingredient;

    }

    public async Task<bool> Update(int id, Ingredient updatedIngredient)
    {
        var existingIngredient = await _repo.GetById(id);
        if (existingIngredient == null)
        {
            return false;
        }

        existingIngredient.Name = updatedIngredient.Name;
        existingIngredient.Quantity = updatedIngredient.Quantity;
        existingIngredient.Category = updatedIngredient.Category;
        existingIngredient.OpenedDate = updatedIngredient.OpenedDate;
        existingIngredient.ExpiredDate = updatedIngredient.ExpiredDate;

        //update via repo
        await _repo.Update(existingIngredient);
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var existingIngredient = await _repo.GetById(id);
        if (existingIngredient == null) return false;

        await _repo.Delete(existingIngredient);
        return true;
    }


}