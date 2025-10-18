using System.Diagnostics.Contracts;
using Frontend.Models;

namespace Frontend.Service
{
    public class ItemService
    {
        private readonly HttpClient _httpClient;
        private readonly LoginService _loginService;

        public ItemService(HttpClient httpClient, LoginService loginService)
        {
            _httpClient = httpClient;
            _loginService = loginService;

        }

        //get all items
        public async Task<List<Item>> GetAllItems(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Item>>("http://localhost:5037/api/item/all/" + userId);
        }

        public async Task<bool> AddItem(Item item)
        {
            try
            {
                int userId = _loginService.GetUserId();

                if (userId == -1)
                {
                    throw new InvalidOperationException("User is not logged in.");
                }
          
                Console.WriteLine("Mssg from ItemService AddItem");
                Console.WriteLine($"Adding item for user {userId}: {item.Name}, Quantity: {item.Quantity}");
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5037/api/item", new
                {
                    Name = item.Name,
                    Quantity = item.Quantity?? 1,
                    UserId = userId
                });
                if(!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to add item. Status Code: {response.StatusCode}, Error: {errorContent}");
                }
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding item: {ex.Message}");
                return false;
            }
          
        }
        public async Task<bool> UpdateItem(Item item)
        {
            var dto= new{
                Name = item.Name,
                Quantity = item.Quantity
            };
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5037/api/item/{item.Id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5037/api/item/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}