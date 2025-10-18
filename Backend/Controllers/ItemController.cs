//Using this as a psedue code to for controller.cs
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Backend.Dtos;
using Microsoft.VisualBasic;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    //List all items
    //GET / api / item
    [HttpGet("all/{userId}")]
    public async Task<ActionResult<List<Item>>> GetAllItems(int userId)
    {
        var items = await _itemService.GetAllItems(userId);
        return Ok(items); //placeholder
    }

     [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItemById(int id)
    {
        var item = await _itemService.GetItemById(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }


    //add new item when entering the add form
    [HttpPost]
    public async Task<ActionResult<Item>> CreateItem([FromBody] ItemCreateDto dto)
    {

        var item = new Item
        {
            Name = dto.Name,
            Quantity = dto.Quantity,
            UserId = dto.UserId
        };
        //save to db

        var newItem = await _itemService.AddItem(item);
        return CreatedAtAction(nameof(GetItemById), new { id = newItem.Id }, newItem);
    }

    //update item when editing the item
    [HttpPut("{id}")] //api/item/{id}
    public async Task<ActionResult<Item>> UpdateItem(int id, [FromBody] ItemUpdateDto dto)
    {

        //find the id first 
        var item = await _itemService.GetItemById(id);
        //then edit 
        if (item == null)
        {
            return NotFound();
        }

        //Update
        item.Name = dto.Name;
        item.Quantity = dto.Quantity;
    

        await _itemService.UpdateItem(id, item);
        return Ok(item);
    
    }

    //delete item when clicking delete button
    [HttpDelete("{id}")]
    public async Task<ActionResult<Item>> DeleteItem(int id)
    {
        var deletedItem = await _itemService.DeleteItem(id);
        if (deletedItem == null)
        {
            return NotFound();
        }
       return Ok(deletedItem);
    }
}