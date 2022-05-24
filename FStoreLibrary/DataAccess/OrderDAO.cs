using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                orders = context.Orders.OrderBy(o => o.OrderDate).Reverse().ToList();
            }
            catch (Exception)
            {
                throw new Exception("Bug in GetAllOrders function!");
            }
            return orders;
        }

        public List<Order> GetOrdersByMemberID(int id)
        {
            List<Order> list = new List<Order>();
            try
            {
                var context = new FStoreDBContext();
                list = context.Orders.Where(ord => ord.MemberId == id).OrderBy(o => o.OrderDate).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Bug in GetOrderByMemberID function!");
            }
            return list;
        }

        public Order GetOrderByID(int id)
        {
            Order o = null;
            try
            {
                var context = new FStoreDBContext();
                o = context.Orders.SingleOrDefault(ord => ord.OrderId == id);
            }
            catch (Exception)
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
                list = context.Orders.Where(o => o.OrderDate >= start && o.OrderDate <= end).OrderBy(o => o.OrderDate).Reverse().ToList();
            }
            catch (Exception)
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

        public int CreateNewOrderID()
        {
            int id = 0;
            List<Order> list = GetAllOrders();
            Random rd = new Random();
            bool check = true;
            while (check)
            {
                check = false;
                id = rd.Next(1, 32000);
                foreach (var or in list)
                {
                    if (id == or.OrderId)
                    {
                        check = true;
                    }
                }
            }
            return id;
        }

        public bool InsertOrder(Order ord)
        {
            bool isInserted = false;
            try
            {
                var context = new FStoreDBContext();
                Order tmp = context.Orders.SingleOrDefault(o => o.OrderId == ord.OrderId);
                if (tmp != null)
                {
                    return isInserted;
                }
                context.Orders.Add(ord);
                if (context.SaveChanges() != 0)
                {
                    isInserted = !isInserted;
                }
            }
            catch (Exception)
            {
                throw new Exception("Bug in InsertOrder function!");
            }
            return isInserted;
        }

        public bool DeleteOrder(int id)
        {
            bool check = false;
            try
            {
                Order order = GetOrderByID(id);
                if (order != null)
                {
                    var context = new FStoreDBContext();
                    context.Orders.Remove(order);
                    if (context.SaveChanges() != 0)
                    {
                        check = true;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Bug in DeleteOrder function!");
            }
            return check;
        }

        public bool UpdateOrder(Order or)
        {
            bool check = false;
            try
            {
                var context = new FStoreDBContext();
                context.Entry<Order>(or).State = EntityState.Modified;
                if (context.SaveChanges() > 0)
                {
                    check = true;
                }
            }
            catch (Exception)
            {
                throw new Exception("Bug in UpdateOrder function!");
            }
            return check;
        }
    }
}