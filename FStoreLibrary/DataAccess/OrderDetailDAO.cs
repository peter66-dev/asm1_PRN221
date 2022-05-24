using System;
using System.Collections.Generic;
using System.Linq;

namespace FStoreLibrary.DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public bool InsertOrderDetails(List<Product> cart, int orderID, float discount)
        {
            bool check = false;
            try
            {
                var context = new FStoreDBContext();
                foreach (var pro in cart)
                {
                    OrderDetail od = new OrderDetail()
                    {
                        OrderId = orderID,
                        ProductId = pro.ProductId,
                        UnitPrice = pro.UnitPrice,
                        Quantity = pro.UnitsInStock,
                        Discount = Math.Round(discount, 3)
                    };
                    context.OrderDetails.Add(od);
                    if (context.SaveChanges() != 0)
                    {
                        check = true;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Bug in InsertOrderDetails function");
            }
            return check;
        }

        public List<OrderDetail> GetAllOrderDetailsByOrderID(int orderID)
        {
            List<OrderDetail> list = null;
            try
            {
                var context = new FStoreDBContext();
                list = context.OrderDetails.Where(o => o.OrderId == orderID).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Bug in GetAllOrders function!");
            }
            return list;
        }

    }
}