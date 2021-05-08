using System.Collections.Generic;
using ShoppingCartHandler.OfferHandlers;
using ShoppingCartHandler.SetupReader;

namespace ShoppingCartHandler.BillHandlers
{
    class BillGenerator
    {
        private readonly PriceList _priceList;
        private readonly MainOfferHandler _mainOfferHandler;

        public BillGenerator(PriceList priceList, MainOfferHandler mainOfferHandler)
        {
            this._priceList = priceList;
            this._mainOfferHandler = mainOfferHandler;
        }

        public Bill GenerateBill(ShoppingCart.ShoppingCart shoppingCart)
        {
            double subtotal = CalculateSubTotal(shoppingCart);
            List<BillDiscount> billDiscounts = _mainOfferHandler.ApplyOffer(shoppingCart);
            double discount = totalDiscount(billDiscounts);
            double total = subtotal - discount;
            return new Bill(subtotal, total, billDiscounts);
        }

        private double CalculateSubTotal(ShoppingCart.ShoppingCart shoppingCart)
        {
            double subtotal = 0;
            foreach (var item in shoppingCart.GetItems())
            {
                subtotal += (_priceList.GetPriceOf(item) * shoppingCart.GetQuantity(item));
            }

            return subtotal;
        }

        private double totalDiscount(List<BillDiscount> discounts)
        {
            double discount = 0;
            foreach (var item in discounts)
            {
                discount += item.GetDiscountValue();
            }

            return discount;
        }
    }
}
