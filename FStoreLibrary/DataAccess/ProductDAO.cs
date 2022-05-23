using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FStoreLibrary.DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                }
                return instance;
            }
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = null;
            try
            {
                var context = new FStoreDBContext();
                products = context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetAllProducts function!");
            }
            return products;
        }

        public Product GetProductsByID(int id)
        {
            Product pro = null;
            try
            {
                var context = new FStoreDBContext();
                pro = context.Products.SingleOrDefault(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetProductsByID function!");
            }
            return pro;
        }

        public List<Product> GetProductsByName(string name)
        {
            List<Product> products = null;
            try
            {
                var context = new FStoreDBContext();
                products = context.Products.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetProductsByName function!");
            }
            return products;
        }

        public List<Product> GetProductsByPrice(decimal price)
        {
            List<Product> products = null;
            try
            {
                var context = new FStoreDBContext();
                products = context.Products.Where(p => p.UnitPrice <= price).OrderBy(p => p.UnitPrice).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetProductsByPrice function!");
            }
            return products;
        }

        public List<Product> GetProductsByUnitInStock(int quantity)
        {
            List<Product> products = null;
            try
            {
                var context = new FStoreDBContext();
                products = context.Products.Where(p => p.UnitsInStock <= quantity).OrderBy(p => p.UnitsInStock).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetProductsByPrice function!");
            }
            return products;
        }

        public bool InsertProduct(Product pro)
        {
            bool isInserted = false;
            try
            {
                var context = new FStoreDBContext();
                Product tmp = context.Products.SingleOrDefault(p => p.ProductId == pro.ProductId);
                if (tmp != null)
                {
                    return isInserted;
                }
                context.Products.Add(pro);
                if (context.SaveChanges() != 0)
                {
                    isInserted = !isInserted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in InsertProduct function!");
            }
            return isInserted;
        }

        public bool UpdateProduct(Product pro)
        {
            bool isUpdated = false;
            try
            {
                Product tmp = GetProductsByID(pro.ProductId);
                if (tmp != null)
                {
                    var context = new FStoreDBContext();
                    context.Entry<Product>(pro).State = EntityState.Modified;
                    //context.Entry(oldObject).CurrentValues.SetValues(newObject);
                    //context.Entry(tmp).CurrentValues.SetValues(pro);
                    if (context.SaveChanges() != 0)
                    {
                        isUpdated = !isUpdated;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in UpdateProduct function!");
            }
            return isUpdated;
        }

        public bool DeleteProduct(int id)
        {
            bool isDeleted = false;
            try
            {
                var context = new FStoreDBContext();
                Product pro = context.Products.SingleOrDefault(p => p.ProductId == id);
                if (pro != null)
                {
                    context.Products.Remove(pro);
                    isDeleted = !isDeleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in DeleteProduct function!");
            }
            return isDeleted;
        }

        public String CheckQuantity(List<Product> list)
        {
            String msg = new String("");
            foreach (var proBuy in list)
            {
                Product proInStock = GetProductsByID(proBuy.ProductId);
                if (proInStock.UnitsInStock < proBuy.UnitsInStock) // sl kho < sl mua
                {
                    msg += proBuy.ProductName + " | ";
                }
            }
            return msg;
        }

        public int SubQuantity(List<Product> list)
        {
            int count = 0;
            try
            {
                foreach (var pro in list)
                {
                    Product proInStock = GetProductsByID(pro.ProductId);
                    var context = new FStoreDBContext();
                    if (proInStock.UnitsInStock >= pro.UnitsInStock)
                    {
                        proInStock.UnitsInStock -= pro.UnitsInStock;
                        context.Entry<Product>(proInStock).State = EntityState.Modified;
                        if (context.SaveChanges() != 0)
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in SubQuantity function!");
            }
            return count;
        }
    }
}
           