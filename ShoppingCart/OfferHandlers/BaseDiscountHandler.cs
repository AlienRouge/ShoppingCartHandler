using System.Collections.Generic;
using ShoppingCartHandler.BillHandlers;

namespace ShoppingCartHandler.OfferHandlers
{
    public abstract class BaseDiscountHandler
    {
        protected BaseDiscountHandler nextHandler;

        public void AddNextHandler(BaseDiscountHandler handler)
        {
            this.nextHandler = handler;
        }

        protected bool IsHasNextHandler()
        {
            return this.nextHandler != null;
        }

        public abstract void ApplyOffer(ShoppingCart.ShoppingCart cart, List<BillDiscount> billDiscountResult);
    }
}