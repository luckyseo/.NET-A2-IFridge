//Using this as a psedue code to for controller.cs

using Backend.Domain.Entities;
//using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class ItemsController : ControllerBase
//{
    // private readonly IItemService _itemService;

    // public ItemsController(IItemService itemService)
    // {
    //     _itemService = itemService;
    // }

    // [HttpGet]
    // public async Task<ActionResult<List<Item>>> GetAll()
    // {
    //     var items = await _itemService.GetAllItemsAsync();
    //     return Ok(items);
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Item>> GetById(int id)
    // {
    //     var item = await _itemService.GetItemByIdAsync(id);
    //     if (item == null)
    //         return NotFound();

    //     return Ok(item);
    // }

    // [HttpPost]
    // public async Task<ActionResult<Item>> Create(Item item)
    // {
    //     var createdItem = await _itemService.CreateItemAsync(item);
    //     return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
    // }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Update(int id, Item item)
    // {
    //     if (id != item.Id)
    //         return BadRequest();

    //     await _itemService.UpdateItemAsync(item);
    //     return NoContent();
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     await _itemService.DeleteItemAsync(id);
    //     return NoContent();
    // }
//}