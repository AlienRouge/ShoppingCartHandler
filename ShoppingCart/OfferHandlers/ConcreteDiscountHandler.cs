using System;
using System.Collections.Generic;
using ShoppingCartHandler.BillHandlers;
using ShoppingCartHandler.SetupReader;

namespace ShoppingCartHandler.OfferHandlers
{
    class ConcreteDiscountHandler : BaseDiscountHandler
    {
        private readonly OfferItem _offerItem;
        private readonly PriceList _priceList;

        public ConcreteDiscountHandler(OfferItem offerItem, PriceList priceList)
        {
            _offerItem = offerItem;
            _priceList = priceList;
        }

        public override void ApplyOffer(ShoppingCart.ShoppingCart cart, List<BillDiscount> billDiscountResult)
        {
            var itemQuantity = cart.GetQuantity(_offerItem.name);
            var targetQuantity = cart.GetQuantity(_offerItem.discountItem.name);

            if (itemQuantity > 0 && targetQuantity > 0 && itemQuantity >= _offerItem.quantity)
            {
                BillDiscount billDiscount =
                    ProcessDiscount((long) Math.Truncate((double) itemQuantity / _offerItem.quantity)); 
                billDiscountResult.Add(billDiscount);
            }

            if (IsHasNextHandler())
            {
                nextHandler.ApplyOffer(cart, billDiscountResult);
            }
        }

        private BillDiscount ProcessDiscount(long itemQuantity)
        {
            DiscountItem discountItem = _offerItem.discountItem;
            double itemPrice = _priceList.GetPriceOf(discountItem.name);
            double discountValue = CalculateDiscount(itemQuantity, itemPrice, discountItem.discountPercent);
            return new BillDiscount(discountItem.name, discountItem.discountPercent, discountValue);
        }

        private static double CalculateDiscount(long itemQuantity, double price, double discountPercent)
        {
            return (itemQuantity * price * discountPercent) / 100;
        }
    }
}