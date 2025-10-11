using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities; 

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

//inherit from the ControllerBase class and its methods
public class IngredientController : ControllerBase
{
    private readonly AppDBContext _context;

    public IngredientController(AppDbContext context)
    {
        _context = context;
    }

    //List all ingredients
    //GET / api / ingredient
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredients>>> GetAll()
    {
        var ingredients = await _context.Ingredients.ToListAsync();
        return Ok(ingredients);
    }

    //Get an ingredient based on id
    //GET /api/ingredient/{id} 
    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredients>> GetIngredientById(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null)
            return NotFound();
        else
            return Ok(ingredient);
    }

    //add new ingredient when entering the add form
    //POST /api/ingredient 
    [HttpPost]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient createdIngredient)
    {
        _context.Ingredients.Add(createdIngredient);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.Id }, createdIngredient);
    }


    //for an ingredient it can be edited and deleted

    //PUT / api / ingredient /{ id}
    //update ingredient [name, category, quantity, opened date, expired date] 
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Ingredient updatedIngredient)
    {
        if (id != updatedIngredient.Id)
            return BadRequest("Id mismatch");

        var existingIngredient = await _context.Ingredients.FindAsync(id);
        if (existingIngredient == null)
            return NotFound();

            //Update data
        existingIngredient.Name = updatedIngredient.Name;
        existingIngredient.Quantity = updatedIngredient.Quantity;
        existingIngredient.Category = updatedIngredient.Category; 
        existingIngredient.OpenedDate = updateIngrdient.OpenedDate; 
        existingIngredient.ExpiredDate = updatedIngredient.ExpiredDate;

        await _context.SaveChangesAsync();
    }

    //remove ingredient 
    //DELETE /api/ingredient/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null)
            return NotFound();

        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync();
    }

}


//References:
//dotnet-bot. (2025). ControllerBase Class (Microsoft.AspNetCore.Mvc). Microsoft.com. https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-9.0