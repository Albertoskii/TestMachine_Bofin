
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderLogic
    {
        private IOrderDetails _orderDetails = new DAL.Functions.OrderDetailsFunctions();

        public async Task<Boolean> CreateNewOrder(OrderDetails od)
        {
            try
            {
                var result = await _orderDetails.AddOrderDetails(od);
                if (result.Id > 0) return true;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<OrderDetails>> GetAllOrders()
        {
            try
            {
                var result = await _orderDetails.GetOrderDetails();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<OrderDetails>> GetById(int id)
        {
            try
            {
                var result = await _orderDetails.GetOrderDetails(id);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderDetails.DeleteOrderDetail(id);
        }
        
        public async Task<bool> UpdateOrder(OrderDetails od)
        {
           return await _orderDetails.UpdateOrderDetails(od);
        }
    
    }
}
