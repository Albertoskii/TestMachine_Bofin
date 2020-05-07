using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace DAL.Functions
{
    public class OrderDetailsFunctions : IOrderDetails
    {
        public async Task<OrderDetails> AddOrderDetails(OrderDetails od)
        {
            try
            {

                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {
                    if (od.Id == 0)
                    {
                        int id = context.OrderDetails.Max<OrderDetails>(x => x.Id);
                        od.Id = id + 1;
                    }
                    await context.OrderDetails.AddAsync(od);
                    await context.SaveChangesAsync();
                    return od;
                }

            }
            catch (Exception e)
            {
                var error = e;
                return null;
            }
        }
        public async Task<List<OrderDetails>> GetOrderDetails(int? id = null)
        {
            List<OrderDetails> orders = new List<OrderDetails>();

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                if (id == null)
                {
                    orders = await context.OrderDetails.ToListAsync();
                   
                }
                else
                {
                    var result = await context.OrderDetails.FindAsync(id);
                    orders.Add(result);
                }
            }
            return orders;
        }


        public async Task<bool> UpdateOrderDetails(OrderDetails od)
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {

                    var i = await context.Database.ExecuteSqlRawAsync($"EXEC bofin_updateOrderDetails @Id = {od.Id}, @Date = '{ od.Date:yyyy-MM-dd}', @Quantity = {od.Quantity}, @Prod = {od.Prod_Id}");
                    if (i > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [Obsolete]
        public async Task<bool> DeleteOrderDetail(int id)
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {

                    var i = await context.Database.ExecuteSqlRawAsync($"EXEC bofin_deleteOrderDetails @Id = {id}");
                    if (i > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

