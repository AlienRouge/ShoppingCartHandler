using System;
using System.IO;
using System.Linq;
using System.Text;
using ShoppingCartHandler.SetupReader;

namespace ShoppingCartHandler.BillHandlers
{
    internal class BillPrinter

    {
        public void print(string writePath, Bill bill, ShoppingCart.ShoppingCart cart, PriceList priceList)
        {
            var header = "\tSHOPPING BILL";
            var line = "--------------------------------";
            var discountsLine = "DISCOUNTS:";
            var totalLine = "TOTAL:";
            var subtotal = $"{"Subtotal:",15} {Math.Round(bill.Subtotal, 2),5}$";
            var total = $"{"Total:",15} {Math.Round(bill.Total, 2),5}$";
            var items = cart.GetItems();
            var discountItems = bill.BillDiscounts;

            var s = $"{"[Item]",5} {"[Qty]",8} {"[Rate]",7} {"[Amount]",7}\n";
            foreach (var t in items)
                s +=
                    $"{t,6} {cart.GetQuantity(t),6} {Math.Round(priceList.GetPriceOf(t), 2),8}$ {Math.Round(priceList.GetPriceOf(t) * cart.GetQuantity(t), 2),5}$\n";

            string str = null;
            str = discountItems.Count > 0
                ? discountItems.Aggregate(str,
                    (current, t) =>
                        current +
                        $"{t.GetDiscountName()} - {t.GetDiscountPercent(),3}% off:{t.GetDiscountValue().ToString(),4}$\n")
                : "No Offers";
            try
            {
                using (var sw = new StreamWriter(writePath, false, Encoding.Default))
                {
                    sw.WriteLine(line);
                    sw.WriteLine(header);
                    sw.WriteLine(line);
                    sw.WriteLine(s);
                    sw.WriteLine(line);
                    sw.WriteLine(discountsLine);
                    sw.WriteLine(line);
                    sw.WriteLine(str);
                    sw.WriteLine(line);
                    sw.WriteLine(totalLine);
                    sw.WriteLine(line);
                    sw.WriteLine(subtotal);
                    sw.WriteLine(total);
                    sw.WriteLine(line);
                }

                Console.WriteLine("Bill has been printed.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}