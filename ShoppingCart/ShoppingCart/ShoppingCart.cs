using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartHandler.ShoppingCart
{
    public class ShoppingCart
    {
        private readonly Dictionary<string, long> _itemCount = new Dictionary<string, long>();

        public void AddItems(List<string> items)
        {
            foreach (var item in items)
                AddItemToCart(item);
        }

        private void AddItemToCart(string item)
        {
            long count = _itemCount.ContainsKey(item) ? _itemCount[item] : 0;
            _itemCount[item] = count + 1;
        }

        public List<string> GetItems()
        {
            return _itemCount.Keys.ToList();
        }

        public long GetQuantity(string item)
        {
            return _itemCount.ContainsKey(item) ? _itemCount[item] : 0;
        }
    }
}