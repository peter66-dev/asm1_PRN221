using FStoreLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStoreLibrary.Repository
{
    public class ProductRepository : IProductRepository
    {
        public bool DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);

        public List<Product> GetAllProducts() => ProductDAO.Instance.GetAllProducts();

        public Product GetProductByID(int id) => ProductDAO.Instance.GetProductsByID(id);

        public List<Product> GetProductsByName(string name) => ProductDAO.Instance.GetProductsByName(name);

        public List<Product> GetProductsByPrice(decimal min, decimal max) => ProductDAO.Instance.GetProductsByPrice(min, max);

        public List<Product> GetProductsByUnitInStock(int quantity) => ProductDAO.Instance.GetProductsByUnitInStock(quantity);

        public bool InsertProduct(Product pro) => ProductDAO.Instance.InsertProduct(pro);

        public bool UpdateProduct(Product pro) => ProductDAO.Instance.UpdateProduct(pro);
    }
}
