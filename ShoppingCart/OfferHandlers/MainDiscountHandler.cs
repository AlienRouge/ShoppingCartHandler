using System.Collections.Generic;
using System.Linq;
using ShoppingCartHandler.BillHandlers;
using ShoppingCartHandler.SetupReader;

namespace ShoppingCartHandler.OfferHandlers
{
    public class MainOfferHandler
    {
        private BaseDiscountHandler _firstHandler;
        private BaseDiscountHandler _lastHandler;

        public MainOfferHandler(List<OfferItem> offerItems, PriceList priceList)
        {
            foreach (var baseDiscountHandler in offerItems.Select(item => OfferHandlerFactory.CreateOfferHandler(item, priceList)))
                AddHandler(baseDiscountHandler);
        }

        private void AddHandler(BaseDiscountHandler baseDiscountHandler)
        {
            if (_firstHandler == null)
            {
                _firstHandler = baseDiscountHandler;
            }
            else
            {
                _lastHandler.AddNextHandler(baseDiscountHandler);
            }
            _lastHandler = baseDiscountHandler;
        }

        public List<BillDiscount> ApplyOffer(ShoppingCart.ShoppingCart shoppingCart)
        {
            var discountsResult = new List<BillDiscount>();
            _firstHandler?.ApplyOffer(shoppingCart, discountsResult);

            return discountsResult;
        }

    }
}