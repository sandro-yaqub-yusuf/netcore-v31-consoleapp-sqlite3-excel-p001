namespace KITAB.Products.Domain.Models
{
    public class ImportedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Inventory { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
    }
}
