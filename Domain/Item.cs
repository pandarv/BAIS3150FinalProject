namespace BAIS3150FinalProject.Domain
{
    public class Item
    {
        public string ItemCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; } = decimal.Zero;
        public int StockQuantity { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
