
//get all
//get by id
//create 
//update
//delete

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
    public Task<ActionResult<IEnumerable<ShoppingList>>> GetAll()
    {
        var allShoppingLists = _context.ShoppingLists
                .Include(sl => sl.Items)
                .ToList();
        return Ok(allShoppingLists);
    }

    // GET all shopping lists
    [HttpGet]
    public Task<ActionResult<IEnumerable<ShoppingList>>> GetAll()
    {
        return _context.ShoppingLists.ToList();
    }

    //Get list by id return enum
    [HttpGet("{id}")]
    public Task<ActionResult<IEnumerable<ShoppingList>>> GetListById(int id)
    {
        var shoppingList = _context.ShoppingLists.Find(id); //find matching id
        if (shoppingList == null)
            return NotFound();
        return Ok(shoppingList);
    }

    //Add a new list: user will enter title and make some note in textarea
    //Create a new list
    [HttpPost]
    public Task<ActionResult<ShoppingList>> Create([FromBody] ShoppingList newList) {
    newList.DateCreated = DateTime.Now;
    _context.ShoppingLists.Add(newList);
    _context.SaveChanges();
    return CreatedAtAction(nameof(GetListById), new { id = newList.Id }, newList);
    }

//Update list 
[HttpPut("{id}")]
public Task<IActionResult> Update(int id, [FromBody] ShoppingList updatedList)
{
    if (id != updatedList.Id)
        return BadRequest("ID mismatch");

    var existingList = _context.ShoppingLists.Find(id);

    if (existingList == null)
        return NotFound();
     
    // Update data
    existing.Title = updatedList.Title;
    existing.ItemsText = updatedList.ItemsText;
    _context.SaveChanges();
    }

    //delete list 
    [HttpDelete("{id}")]
    public Task<IActionResult> Delete(int id)
    {
        var shoppingList = _context.ShoppingLists.Find(id);
        if (shoppingList == null) return NotFound();
        _context.ShoppingLists.Remove(shoppinglist);
        _context.SaveChanges();
    }

}


//Make to be async to be cont
//mjrousos. (2024, September 27). ASP.NET Core Best Practices. Microsoft.com. https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-9.0