namespace BAIS3150FinalProject.Domain
{
    public class Sale
    {
        public int SaleNumber { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GST { get; set; }
        public decimal SaleTotal { get; set; }
        //public int AddSaleNumber {  get; set; }
        public List<SaleItem> SaleItemList = new List<SaleItem>();

        //public List<SaleItem> SaleItemList
        //{∏
        //    get
        //    {
        //        return _saleItemList;
        //    }
        //    set 
        //    { 
        //        _saleItemList = value; 
        //    }
        //}
    }
}
