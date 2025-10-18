using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Dtos;

using Backend.Services;


namespace Backend.Interface;
public interface IItemService
{
    //view available ingredient- IngredientService
    //view expired ingredient- ExpiredIngredientService
    //shared method 

    Task<List<Item>> GetAllItems(int userId);
     Task<Item?> GetItemById(int id);
    Task<Item> AddItem(Item Item);
    Task<Item?> UpdateItem(int id, Item Item);
    Task<Item?> DeleteItem(int id);
   
}

