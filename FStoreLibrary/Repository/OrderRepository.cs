using FStoreLibrary.DataAccess;
using System;
using System.Collections.Generic;

namespace FStoreLibrary.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetAllOrders() => OrderDAO.Instance.GetAllOrders();

        public Order GetOrderByID(int id) => OrderDAO.Instance.GetOrderByID(id);

        public List<Order> SortOrdersBySalesAscending(List<Order> list) => OrderDAO.Instance.SortOrdersBySalesAscending(list);

        public List<Order> SortOrdersBySalesDescending(List<Order> list) => OrderDAO.Instance.SortOrdersBySalesDescending(list);

        public List<Order> Statistic(DateTime start, DateTime end) => OrderDAO.Instance.Statistic(start, end);

        public int CreateNewOrderID() => OrderDAO.Instance.CreateNewOrderID();

        public bool InsertOrder(Order ord) =>OrderDAO.Instance.InsertOrder(ord);

        public bool DeleteOrder(int id) => OrderDAO.Instance.DeleteOrder(id);

        public bool UpdateOrder(Order or) =>OrderDAO.Instance.UpdateOrder(or);

        public List<Order> GetOrdersByMemberID(int id) => OrderDAO.Instance.GetOrdersByMemberID(id);
    }
}
