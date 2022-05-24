using FStoreLibrary.DataAccess;
using System.Collections.Generic;

namespace FStoreLibrary.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public List<OrderDetail> GetAllOrderDetailsByOrderID(int orderID) => OrderDetailDAO.Instance.GetAllOrderDetailsByOrderID(orderID);

        public bool InsertOrderDetails(List<Product> cart, int orderID, float discount) => OrderDetailDAO.Instance.InsertOrderDetails(cart, orderID, discount);

    }
}
