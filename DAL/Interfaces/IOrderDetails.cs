using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderDetails
    {
        public Task<OrderDetails> AddOrderDetails(OrderDetails od);
        public Task<List<OrderDetails>> GetOrderDetails(int? id = null);
        public Task<bool> DeleteOrderDetail(int id);
        public Task<bool> UpdateOrderDetails(OrderDetails od);

    }
}
