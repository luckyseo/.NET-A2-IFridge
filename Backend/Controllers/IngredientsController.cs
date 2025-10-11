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
    public Task<ActionResult<IEnumerable<Ingredients>>> GetAll()
    {
        var ingredients = _context.Ingredients.ToList();
        return Ok(ingredients);
    }

    //Get an ingredient based on id
    //GET /api/ingredient/{id} 
    [HttpGet("{id}")]
    public Task<ActionResult<Ingredients>> GetIngredientById(int id)
    {
        var ingredient = _context.Ingredients.Find(id);
        if (ingredient is not null)
            return NotFound();
        else
            return Ok(ingredient);
    }

    //add new ingredient when entering the add form
    //POST /api/ingredient 
    [HttpPost]
    public Task<ActionResult<Ingredient>> Create(IngredientController ingredient)
    {
        _context.Ingredients.Add(ingredient);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetIngredientById), new { id = ingredient.Id }, ingredient);
    }


    //for an ingredient it can be edited and deleted

    //PUT / api / ingredient /{ id}
    //update ingredient [name, category, quantity, opened date, expired date] 
    [HttpPut("{id}")]
    public Task<IActionResult> Update(int id, IngredientController updatedIngredient)
    {

        if (id != updatedIngredient.id)
            return NotFound();
        var existingIngredient = _context.Ingredients.Find(id);
        if (existingIngredient is null)
            return NotFound();

        existingIngredient.Name = updatedIngredient.Name;
        existingIngredient.Quantity = updatedIngredient.Quantity;
        existingIngredient.Category = updatedIngredient.Category; 
        existingIngredient.OpenedDate = updateIngrdient.OpenedDate; 
        existingIngredient.ExpiredDate = updatedIngredient.ExpiredDate;

        _context.SaveChanges();
    }


    //remove ingredient 
    //DELETE /api/ingredient/{id}
    [HttpDelete("{id}")]
    public Task<IActionResult> Delete(int id)
    {
        var ingredient = _context.Ingredients.Find(id);
        if (ingredient == null)
            return NotFound();

        _context.Ingredients.Remove(ingredient);
        _context.SaveChanges();
    }

}


//References:
//dotnet-bot. (2025). ControllerBase Class (Microsoft.AspNetCore.Mvc). Microsoft.com. https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-9.0