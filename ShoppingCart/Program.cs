using System;
using System.Linq;
using ShoppingCartHandler.BillHandlers;
using ShoppingCartHandler.OfferHandlers;
using ShoppingCartHandler.SetupReader;

namespace ShoppingCartHandler
{
    static class Program
    {
        private static string CONFIG_FILE_NAME = @"Setup\setup.json";
        private static string OUTPUT_BILL_FILE_NAME = @"Bill\Bill.txt";

        /*private static readonly string[] input = {"Cake", "Cake", "Cake", "Cake", "Bread", "Bread", "Bread", "Bread", "Apple", "Apple", "Apple", "Apple", "Apple", "Apple"};*/

        static void Main(string[] args)
        {
            // TO MANUAL ENTER UNCOMMENT THIS
            /*ShoppingCart shoppingCart = GetShoppingCart(input);

            var config = GetConfig(CONFIG_FILE_NAME);
            var priceList = new PriceList(config.GetPrices());
            var bill = GenerateBill(config, priceList, shoppingCart);

            BillPrinter pr = new BillPrinter();
            pr.print(bill, shoppingCart, priceList);*/

            // AND COMMENT BELOW
            if (args == null) { Console.WriteLine("Args is null"); }
            else
            {
                ShoppingCart.ShoppingCart shoppingCart = GetShoppingCart(args);

                var config = GetConfig();
                var priceList = new PriceList(config.GetPrices());
                var bill = GenerateBill(config, priceList, shoppingCart);
                
                BillPrinter pr = new BillPrinter();
                pr.print(OUTPUT_BILL_FILE_NAME, bill, shoppingCart, priceList);
            }
        }

        private static Root GetConfig()
        {
            try
            {
                Root parser = new Root();
                return parser.GetConfig(CONFIG_FILE_NAME);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to read config." + e);
                throw;
            }
        }

        private static ShoppingCart.ShoppingCart GetShoppingCart(String[] args)
        {
            ShoppingCart.ShoppingCart shoppingCart = new ShoppingCart.ShoppingCart();
            shoppingCart.AddItems(args.ToList());
            return shoppingCart;
        }

        private static Bill GenerateBill(Root config, PriceList priceList, ShoppingCart.ShoppingCart shoppingCart)
        {
            MainOfferHandler mainOfferHandler = new MainOfferHandler(config.GetOffers(), priceList);
            BillGenerator billGenerator = new BillGenerator(priceList, mainOfferHandler);
            Bill bill = billGenerator.GenerateBill(shoppingCart);
            return bill;
        }
    }
}
