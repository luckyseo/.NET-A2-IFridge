using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListController : ControllerBase
{
    private readonly AppDBContext _context;

    public ShoppingListController(AppDbContext context)
    {
        _context = context;
    }

    //Get all shopping list items return enum
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShoppingList>>> GetAll()
    {
        var allShoppingLists = await _context.ShoppingLists.ToListAsync();
        return Ok(allShoppingLists);
    }
    //Get list by id return enum
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ShoppingList>>> GetListById(int id)
    {
        var shoppingList = await _context.ShoppingLists.FindAsync(id); //find matching id
        if (shoppingList == null)
            return NotFound();
        return Ok(shoppingList);
    }

    //Add a new list: user will enter title and make some note in textarea
    //Create a new list
    [HttpPost]
    public async Task<ActionResult<ShoppingList>> Create([FromBody] ShoppingList newList)
    {
        //update model data
        newList.DateCreated = DateTime.Now;
        //add 
        _context.ShoppingLists.Add(newList);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetListById), new { id = newList.Id }, newList);
    }

    //Update list 
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ShoppingList updatedList)
    {
        if (id != updatedList.Id)
            return BadRequest("ID mismatch");

        var existingList = await _context.ShoppingLists.FindAsync(id);

        if (existingList == null)
            return NotFound();

        // Update data
        existing.Title = updatedList.Title;
        existing.ItemsText = updatedList.ItemsText;

        await _context.SaveChangesAsync();
    }

    //delete list 
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var shoppingList = await _context.ShoppingLists.FindAsync(id);
        if (shoppingList == null) return NotFound();
        //remove 
        _context.ShoppingLists.Remove(shoppinglist);
        await _context.SaveChangesAsync();
    }

}


//References: async
//mjrousos. (2024, September 27). ASP.NET Core Best Practices. Microsoft.com. https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-9.0