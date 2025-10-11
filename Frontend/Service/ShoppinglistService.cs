using Frontend.Models;
using System.Net.Http.Json;

namespace Frontend.Service
{

    public class ShoppingListService
    {

        private readonly List<ShoppingListModel> _list = new List<ShoppingListModel>();
        public IEnumerable<ShoppingListModel> GetAll() => _list;

        public void Add(ShoppingListModel list)
        {
            if (list.Title is not null)
            {
                _list.Add(list);
            }

        }

    }

    // public class ShoppinglistModel
    // {
    //     public string Title { get; set; } = "";
    //     public List<string> Items { get; set; } = new();
    //     public DateTime DateCreated { get; set; } = DateTime.Now;

    // }
}