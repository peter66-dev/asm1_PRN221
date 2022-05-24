using FStoreLibrary.DataAccess;
using System.Collections.Generic;

namespace FStoreLibrary.Repository
{
    public interface IOrderDetailRepository
    {
        public List<OrderDetail> GetAllOrderDetailsByOrderID(int orderID);
        public bool InsertOrderDetails(List<Product> cart, int orderID, float discount);

    }
}
