using FStoreLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStoreLibrary.Repository
{
    public interface IOrderDetailRepository
    {
        public List<OrderDetail> GetAllOrderDetailsByOrderID(int orderID);
    }
}
