using FStoreLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
