namespace BAIS3150FinalProject.Domain
{
    public class SaleItem
    {
        public string ItemCode { get; set; } = string.Empty;
        private int _quantity { get; set; }
        private decimal _itemTotal { get; set; }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (int.TryParse(value.ToString(), out int result))
                {
                    _quantity = result;
                }
                else
                {
                    _quantity = 0;
                }
            }
        }

        public decimal ItemTotal
        {
            get
            {
                return _itemTotal;
            }
            set
            {
                if (decimal.TryParse(value.ToString(), out decimal result))
                {
                    _itemTotal = result;
                }
                else
                {
                    _itemTotal = 0.00m;
                }
            }
        }
    }
}
