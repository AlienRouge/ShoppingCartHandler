namespace ShoppingCartHandler.BillHandlers
{
    public class BillDiscount
    {
        private readonly string _name;

        private readonly double _discountPercent;

        private readonly double _discountValue;

        public BillDiscount(string name, double discountPercent, double discountValue)
        {
            this._name = name;
            this._discountPercent = discountPercent;
            this._discountValue = discountValue;
        }

        public double GetDiscountValue()
        {
            return _discountValue;
        }
        public double GetDiscountPercent()
        {
            return _discountPercent;
        }

        public string GetDiscountName()
        {
            return _name;
        }
    }
}