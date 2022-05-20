using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStoreLibrary.DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = null;
            try
            {
                var context = new FStoreDBContext();
                orders = context.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetAllOrders function!");
            }
            return orders;
        }

        public Order GetOrderByID(int id)
        {
            Order o = null;
            try
            {
                var context = new FStoreDBContext();
                o = context.Orders.SingleOrDefault(ord => ord.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetOrderByID function!");
            }
            return o;
        }

        public List<Order> Statistic(DateTime start, DateTime end)
        {
            List<Order> list = null;
            try
            {
                var context = new FStoreDBContext();
                list = context.Orders.Where(o => o.OrderDate >= start && o.OrderDate <= end).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in Statistic function!");
            }
            return list;
        }

        public List<Order> SortOrdersBySalesAscending(List<Order> list)
        {
            return list.OrderBy(o => o.Freight).ToList();
        }

        public List<Order> SortOrdersBySalesDescending(List<Order> list)
        {
            return list.OrderByDescending(o => o.Freight).ToList();
        }
    }
}