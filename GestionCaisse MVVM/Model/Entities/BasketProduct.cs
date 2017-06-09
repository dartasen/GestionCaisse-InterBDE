namespace GestionCaisse_MVVM.Model.Entities
{
    public class BasketProduct
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        
        public BasketProduct(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
