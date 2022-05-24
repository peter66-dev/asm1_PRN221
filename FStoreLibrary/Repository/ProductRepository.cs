using FStoreLibrary.DataAccess;
using System.Collections.Generic;

namespace FStoreLibrary.Repository
{
    public class ProductRepository : IProductRepository
    {
        public string CheckQuantity(List<Product> list) => ProductDAO.Instance.CheckQuantity(list);

        public bool DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);

        public List<Product> GetAllProducts() => ProductDAO.Instance.GetAllProducts();

        public Product GetProductByID(int id) => ProductDAO.Instance.GetProductsByID(id);

        public List<Product> GetProductsByName(string name) => ProductDAO.Instance.GetProductsByName(name);

        public List<Product> GetProductsByPrice(decimal price) => ProductDAO.Instance.GetProductsByPrice(price);

        public List<Product> GetProductsByUnitInStock(int quantity) => ProductDAO.Instance.GetProductsByUnitInStock(quantity);

        public bool InsertProduct(Product pro) => ProductDAO.Instance.InsertProduct(pro);

        public int SubQuantity(List<Product> list) => ProductDAO.Instance.SubQuantity(list);

        public bool UpdateProduct(Product pro) => ProductDAO.Instance.UpdateProduct(pro);
    }
}
