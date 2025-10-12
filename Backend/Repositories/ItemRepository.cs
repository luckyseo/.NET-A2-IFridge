// namespace Backend.Repositories;

// using Backend.AppData;
// using Backend.Domain.Entities;
// using Microsoft.EntityFrameworkCore;

// public class ItemRepository
// {
//     private readonly AppDbContext _context;

    // public ItemRepository(AppDbContext context)
    // {
    //     _context = context;
    // }

    // public async Task<List<Item>> GetAllAsync()
    // {
    //     return await _context.Items.ToListAsync();
    // }

    // public async Task<Item?> GetByIdAsync(int id)
    // {
    //     return await _context.Items.FindAsync(id);
    // }

    // public async Task<Item> AddAsync(Item item)
    // {
    //     _context.Items.Add(item);
    //     await _context.SaveChangesAsync();
    //     return item;
    // }

    // public async Task UpdateAsync(Item item)
    // {
    //     _context.Items.Update(item);
    //     await _context.SaveChangesAsync();
    // }

    // public async Task DeleteAsync(int id)
    // {
    //     var item = await _context.Items.FindAsync(id);
    //     if (item != null)
    //     {
    //         _context.Items.Remove(item);
    //         await _context.SaveChangesAsync();
    //     }
    // }
