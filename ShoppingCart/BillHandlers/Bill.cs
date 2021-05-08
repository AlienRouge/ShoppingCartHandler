using System.Collections.Generic;

namespace ShoppingCartHandler.BillHandlers
{
    public class Bill
    {
        public readonly double Subtotal;
        public readonly double Total;
        public readonly List<BillDiscount> BillDiscounts;

        public Bill(double subtotal, double total, List<BillDiscount> billDiscounts)
        {
            this.Subtotal = subtotal;
            this.Total = total;
            this.BillDiscounts = billDiscounts;
        }
    }
}