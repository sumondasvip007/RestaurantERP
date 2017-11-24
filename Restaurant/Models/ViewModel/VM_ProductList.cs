namespace Restaurant.Models.ViewModel
{
    public class VM_ProductList
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public string ProductName { get; set; }
        public bool IsSelected { get; set; }
        public int ProductionHouseId { get; set; }
        public string Unit { get; set; }
        public double OpeningBalance { get; set; }
    }
}