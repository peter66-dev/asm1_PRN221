using FStoreLibrary.DataAccess;
using System;
using System.Collections.Generic;

namespace FStoreLibrary.Repository
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrders();

        public Order GetOrderByID(int id);

        public List<Order> Statistic(DateTime start, DateTime end);

        public List<Order> SortOrdersBySalesAscending(List<Order> list);

        public List<Order> SortOrdersBySalesDescending(List<Order> list);

        public int CreateNewOrderID();

        public bool InsertOrder(Order ord);

        public bool DeleteOrder(int id);

        public bool UpdateOrder(Order or);

        public List<Order> GetOrdersByMemberID(int id);

    }
}
