using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Data;
using Backend.Services;
using Backend.Dtos;
using Backend.Interface;

//Ingredient- IIngredientService -> view all ingredient and expiring ingredient
// Recipe -> IRecipeService -> inherit available ingredient recipe and random browsing all ingredient 
// Dtos simplfy entry for testing and execute

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
    
    [HttpGet("all/{userId}")]
    public async Task<ActionResult<List<Ingredient>>> GetAllIngredient(int userId)
    {
        var ingredients = await _ingredientService.GetAllIngredient(userId);
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
    //use dto instead on the to ignore entering whole object after testign not work
    [HttpPost]
    public async Task<ActionResult<Ingredient>> Create([FromBody] IngredientCreatedDto dto)
    {
        var ingredient = new Ingredient
        {
            Name = dto.Name,
            Category = dto.Category,  //debug cast the int to enum  
            Quantity = dto.Quantity,
            OpenedDate = dto.OpenedDate,
            ExpiredDate = dto.ExpiredDate,
            UserId = dto.UserId
        };

        //save to db
        var result = await _ingredientService.AddIngredient(ingredient);
        return CreatedAtAction(nameof(GetIngredientById), new { id = result.Id }, result);
    }

    //PUT / api / ingredient /{ id}
    //update ingredient [name, category, quantity, opened date, expired date] 
    [HttpPut("{id}")]
    public async Task<ActionResult<Ingredient>> Update(int id, [FromBody] IngredientUpdatedDto dto)
    {
        //find the id first 
        var ingredient = await _ingredientService.GetIngredientById(id);
        //then edit 
        if (ingredient == null)
        {
            return NotFound();
        }

        //Update
        ingredient.Name = dto.Name;
        ingredient.Category = dto.Category;
        ingredient.Quantity = dto.Quantity;
        ingredient.OpenedDate = dto.OpenedDate;
        ingredient.ExpiredDate = dto.ExpiredDate;

        await _ingredientService.UpdateIngredient(id, ingredient);
        return Ok(ingredient);
    }

    //remove ingredient 
    //DELETE /api/ingredient/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<Ingredient>> Delete(int id)
    {
        var ingredient = await _ingredientService.DeleteIngredient(id);
        if (ingredient == null)
        {
            return NotFound();
        }
        return NoContent();
        //return ingredient; - object 
    }
}


//References:
//dotnet-bot. (2025). ControllerBase Class (Microsoft.AspNetCore.Mvc). Microsoft.com. https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-9.0F
//Microsfot.AspNetCore.Mvc documentation main inspiration
//notes: status code NoContext-> 204, ok -> 200, notfound-> 404, 201 -> success created resource