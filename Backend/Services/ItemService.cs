namespace Backend.Services;

using Backend.Domain.Entities;
using Backend.Repositories;

public class ItemService : IItemService
{
	private readonly IItemRepository _repository;

	public ItemService(IItemRepository repository)
	{
		_repository = repository;
	}

	public Task<List<Item>> GetAllItemsAsync() => _repository.GetAllAsync();

	public Task<Item?> GetItemByIdAsync(int id) => _repository.GetByIdAsync(id);

	public Task<Item> CreateItemAsync(Item item) => _repository.AddAsync(item);

	public Task UpdateItemAsync(Item item) => _repository.UpdateAsync(item);

	public Task DeleteItemAsync(int id) => _repository.DeleteAsync(id);
}