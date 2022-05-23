using FStoreLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStoreLibrary.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();
        public Product GetProductByID(int id);
        public List<Product> GetProductsByName(string name);
        public List<Product> GetProductsByPrice(decimal price);
        public List<Product> GetProductsByUnitInStock(int quantity);
        public bool InsertProduct(Product pro);
        public bool UpdateProduct(Product pro); 
        public bool DeleteProduct(int id);
        public String CheckQuantity(List<Product> list);
        public int SubQuantity(List<Product> list);
    }
}
