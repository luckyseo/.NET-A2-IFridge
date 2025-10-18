using Microsoft.AspNetCore.Mvc;
using Backend.Domain.Entities;
using Backend.Services;
using Backend.Dtos;
using Backend.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        // GET: api/recipe/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeById(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        // POST: api/recipe
        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe([FromBody] RecipeDto dto)
        {
            var newRecipe = await _recipeService.AddRecipe(dto);
            return CreatedAtAction(nameof(GetRecipeById), new { id = newRecipe.Id }, newRecipe);
        }

        // PUT: api/recipe/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> Update(int id, [FromBody] RecipeDto dto)
        {
            var updatedRecipe = await _recipeService.UpdateRecipe(id, dto);
            if (updatedRecipe == null)
            {
                return NotFound();
            }
            return Ok(updatedRecipe);
        }

        // DELETE: api/recipe/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipe>> Delete(int id)
        {
            var recipe = await _recipeService.DeleteRecipe(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        // GET: api/recipe/category/{category}
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<Recipe>>> GetByCategory(RecipeCategory category)
        {
            var recipes = await _recipeService.GetRecipesByCategory(category);
            return Ok(recipes);
        }
        
        // GET api/recipe/user/{userId}suggestion
        [HttpGet("user/{userId}/suggestion")]
        public async Task<ActionResult<List<RecipeSuggestionDto>>> GetRecipesByUserIngredients(int userId)
        {
            var suggestions = await _recipeService.getRecipesByAvailableIngredient(userId);
            return Ok(suggestions);

        }
    }
}