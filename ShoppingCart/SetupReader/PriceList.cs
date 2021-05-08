using System.Collections.Generic;

namespace ShoppingCartHandler.SetupReader
{
    public class PriceList
    {
        private readonly Dictionary<string, double> _priceList;

        public PriceList(Dictionary<string, double> priceList)
        {
            this._priceList = priceList;
        }

        public double GetPriceOf(string item)
        {
            return _priceList.ContainsKey(item) ? _priceList[item] : 0.00;
        }
    }
}