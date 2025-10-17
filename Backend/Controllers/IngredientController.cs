using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Data;
using Backend.Services;
using Backend.Dtos;
using Backend.Interface; 

//Ingredient- IIngredientService -> view all ingredient and expiring ingredient
// Recipe -> IRecipeService -> inherit available ingredient recipe and random browsing all ingredient 
// Dtos ..

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class IngredientController : ControllerBase
{

    private readonly IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    //List all ingredients
    //GET / api / ingredient
    [HttpGet("all")]
    public async Task<ActionResult<List<Ingredient>>> GetAllIngredient()
    {
        var ingredients = await _ingredientService.GetAllIngredient();
        return Ok(ingredients);
    }

    //Get an ingredient based on id
    //GET /api/ingredient/{id} 
    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredient>> GetIngredientById(int id)
    {
        var ingredient = await _ingredientService.GetIngredientById(id);
        if (ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient);
    }

    //Convert entity to DTO IngredientDto -> for expiration notificaiton
    [HttpGet("expired")]
    public async Task<ActionResult<List<IngredientDto>>> GetExpiredIngredients()
    {
        var expired = await _ingredientService.GetExpiredIngredient();
        return Ok(expired);
    }

    //add new ingredient when entering the add form
    //POST /api/ingredient 
    [HttpPost]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient createdIngredient)
    {
        var result = await _ingredientService.AddIngredient(createdIngredient);
        return CreatedAtAction(nameof(GetIngredientById), new { id = result.Id }, result);
    }

    //PUT / api / ingredient /{ id}
    //update ingredient [name, category, quantity, opened date, expired date] 
    [HttpPut("{id}")]
    public async Task<ActionResult<Ingredient>> Update(int id, [FromBody] Ingredient updatedIngredient)
    {
        if (id != updatedIngredient.Id)
        {
            return BadRequest("Id not found");
        }

        var result = await _ingredientService.UpdateIngredient(id, updatedIngredient);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    //remove ingredient 
    //DELETE /api/ingredient/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<Ingredient>> Delete(int id)
    {
        var result = await _ingredientService.DeleteIngredient(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}


//References:
//dotnet-bot. (2025). ControllerBase Class (Microsoft.AspNetCore.Mvc). Microsoft.com. https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-9.0F