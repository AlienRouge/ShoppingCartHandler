using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ShoppingCartHandler.SetupReader
{
    public class PriceItem
    {
        public string name { get; set; }
        public double price { get; set; }
    }

    public class DiscountItem
    {
        public string name { get; set; }
        public int discountPercent { get; set; }
    }

    public class OfferItem
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public DiscountItem discountItem { get; set; }
    }

    public class Root
    {
        public List<PriceItem> priceItem { get; set; }
        public List<OfferItem> offerItem { get; set; }

        public Dictionary<string, double> GetPrices()
        {
            var prices = new Dictionary<string, double>();
            foreach (var item in priceItem)
            {
                prices[item.name] = item.price;
            }

            return prices;
        }

        public List<OfferItem> GetOffers()
        {
            return offerItem;
        }

        public Root GetConfig(string path)
        {
            Directory.CreateDirectory(path.Split(new char[] { '\\' })[0]);
            if (!File.Exists(path))
                File.Create(path);

            if (new FileInfo(path).Length == 0)
                throw new Exception("Error. Config file is empty!");


            string jsonString = File.ReadAllText(path);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonString);
            return myDeserializedClass;
        }
    }
}