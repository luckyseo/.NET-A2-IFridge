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
        public async Task<ActionResult<List<Recipe>>> GetAll()
        {
            var recipes = await _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        // GET: api/recipe/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetById(int id)
        {
            var recipe = await _recipeService.GetRecipeById(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        // POST: api/recipe
        [HttpPost]
        public async Task<ActionResult<Recipe>> Add([FromBody] Recipe recipe)
        {
            var createdRecipe = await _recipeService.AddRecipe(recipe);
            return CreatedAtAction(nameof(GetById), new { id = createdRecipe.Id }, createdRecipe);
        }

        // PUT: api/recipe/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> Update(int id, [FromBody] Recipe updatedRecipe)
        {
            var recipe = await _recipeService.UpdateRecipe(id, updatedRecipe);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
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
        public async Task<ActionResult<List<Recipe>>> GetRecipesByUserIngredients(int userId)
        {
            var recipes = await _recipeService.GetRecipesByAvailableIngredient(userId);

            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }
    }
}

