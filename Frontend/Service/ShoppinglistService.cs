namespace Frontend.Service
{

    public class ShoppinglistService()
    {

        private readonly List<Shoppinglist> _list = new Shoppinglist();
        public IEnumerable<ShoppingListModel> GetAll() => _lists;

        public void Add(Shoppinglist list)
        {
            if (list.Title is not null)
            {
                _list.Add(list);
            }

        }

    }

    public class ShoppinglistModel
    {
        public string Title { get; set; } = "";
        public List<string> Items { get; set; } = new();
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}