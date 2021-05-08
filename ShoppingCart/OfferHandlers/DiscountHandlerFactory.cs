using ShoppingCartHandler.SetupReader;

namespace ShoppingCartHandler.OfferHandlers
{
    public static class OfferHandlerFactory
    {
        public static BaseDiscountHandler CreateOfferHandler(OfferItem offerItem, PriceList priceList)
        {
            return new ConcreteDiscountHandler(offerItem, priceList);
        }
    }
}
