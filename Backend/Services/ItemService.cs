using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Data;
using Backend.Dtos;


namespace Backend.Services;
public class ItemService:IItemService
{
    //view available ingredient- IngredientService
    //view expired ingredient- ExpiredIngredientService
    //shared method 
    private readonly AppDbContext _context;

    //through db 
    public ItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetAllItems(int userId)
    {
        return await _context.Items.Where(i => i.UserId == userId).ToListAsync();
    }
     public async Task<Item?> GetItemById(int id)
    {
        return await _context.Items.FindAsync(id);
    }
    public async Task<Item> AddItem(Item item)
    {
        _context.Items.Add(item); //add to table
        await _context.SaveChangesAsync();
        return item;
    }
    public async Task<Item?> UpdateItem(int id, Item updatedItem)
    {
        var existingItem = await _context.Items.FindAsync(id);
        if (existingItem == null)
        {
            return null;
        }

        //Update data 
        existingItem.Name = updatedItem.Name;
        existingItem.Quantity = updatedItem.Quantity;
   

        await _context.SaveChangesAsync(); //save changes
        return existingItem;
    }
    public async Task<Item?> DeleteItem(int id)
    {
        var existingItem = await _context.Items.FindAsync(id);
        if(existingItem == null)
        {
            return null;
        }
        _context.Items.Remove(existingItem);
        await _context.SaveChangesAsync();
        return existingItem;
    }
   
}
