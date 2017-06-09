namespace GestionCaisse_MVVM.Model.Entities
{
    public class BasketProduct
    {
        public BasketProduct(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}