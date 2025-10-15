using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Data;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

//inherit from the ControllerBase class and its methods
public class IngredientController : ControllerBase
{

    //instead directly through db context 
    // private readonly AppDbContext _context;

    // public IngredientController(AppDbContext context)
    // {
    //     _context = context;
    // }

    //Through service -> repo 
    private readonly IngredientService _service; 

    public IngredientController(IngredientService service)
    {
        _service = service; 
    }

    //List all ingredients
    //GET / api / ingredient
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetAll()
    {
        var ingredients = await _service.GetAll(); 
        return Ok(ingredients);
    }

    //Get an ingredient based on id
    //GET /api/ingredient/{id} 
    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredient>> GetIngredientById(int id)
    {
        var ingredient = await _service.GetById(id);
        if (ingredient == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(ingredient);
        }
    }

    //Convert entity to DTO IngredientDto -> for expiration notificaiton
    [HttpGet("expired")]
    public async Task<ActionResult<List<IngredientDto>>> GetExpiredIngredients()
    {
        var expired = await _service.GetExpiredIngredient();
        //use ingredient dto - to expose short detail 
        return Ok(expired);
    }

    //add new ingredient when entering the add form
    //POST /api/ingredient 
    [HttpPost]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient createdIngredient)
    {
        var result = await _service.Add(createdIngredient); 
        return CreatedAtAction(nameof(GetIngredientById), new { id = result.Id }, result);
    }

    //for an ingredient it can be edited and deleted

    //PUT / api / ingredient /{ id}
    //update ingredient [name, category, quantity, opened date, expired date] 
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Ingredient updatedIngredient)
    {
        if (id != updatedIngredient.Id)
        {
            return BadRequest("Id not found");
        }

        var success = await _service.Update(id, updatedIngredient);

        if (!success)
        {
            return NotFound();
        } else
        {
            return Ok(updatedIngredient);
        }
 
    }

    //remove ingredient 
    //DELETE /api/ingredient/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.Delete(id);
        if (!success) return NotFound(); 
        return NoContent();
    }

}


//References:
//dotnet-bot. (2025). ControllerBase Class (Microsoft.AspNetCore.Mvc). Microsoft.com. https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-9.0F