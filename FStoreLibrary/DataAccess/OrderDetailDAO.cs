using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<OrderDetail> GetAllOrderDetailsByOrderID(int orderID)
        {
            List<OrderDetail> list = null;
            try
            {
                var context = new FStoreDBContext();
                list = context.OrderDetails.Where(o => o.OrderId == orderID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetAllOrders function!");
            }
            return list;
        }
    }
}