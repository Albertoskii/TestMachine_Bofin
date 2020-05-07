using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DAL.Entities;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace API_WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderDetailsController : ControllerBase
    {
        private OrderLogic _order = new OrderLogic();


        [HttpPost]
        [ProducesResponseType(typeof(OrderDetails), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddOrderDetails([FromBody]string order)
        {
            try
            {
                var or = JObject.Parse(order);
                OrderDetails od = new OrderDetails()
                {
                    Id = int.Parse(or["Id"].ToString()),
                    Quantity = int.Parse(or["Quantity"].ToString()),
                    Date = DateTime.Parse(or["Date"].ToString()),
                    Prod_Id = int.Parse(or["Prod_Id"].ToString())
                };

                bool result = await _order.CreateNewOrder(od);
                if (result) return Ok(result);
                return BadRequest();
            }
            catch (Exception e)
            {

                return BadRequest();
            }

        }



        [HttpGet]
        [ProducesResponseType(typeof(OrderDetails), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetails()
        {
            var list = await _order.GetAllOrders();

            var result = mapOrderDetails(list);
            return Ok(JsonSerializer.Serialize(result));
        }



        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderDetails), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetailsById([FromRoute][Required] int id)
        {

            var list = await _order.GetById(id);
            var result = mapOrderDetails(list);
            return Ok(JsonSerializer.Serialize(result.FirstOrDefault()));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderDetails([FromRoute]int id)
        {
            return Ok(await _order.DeleteOrder(id));
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrderDetails([FromBody]string order)
        {
            try
            {
                var or = JObject.Parse(order);
                OrderDetails od = new OrderDetails()
                {
                    Id = int.Parse(or["Id"].ToString()),
                    Quantity = int.Parse(or["Quantity"].ToString() == "" ? "0" : or["Quantity"].ToString()),
                    Date = DateTime.Parse(or["Date"].ToString() ==  "" ? "9999-1-1" : or["Date"].ToString()).Date,
                    Prod_Id = int.Parse(or["Prod_Id"].ToString() == "" ? "0" : or["Prod_Id"].ToString())
                };
                if(await _order.UpdateOrder(od))
                {
                    return Ok();
                }
                return BadRequest();
                
            } catch
            {
                return BadRequest();
            }
            
        }




        private List<Models.OrderDetails> mapOrderDetails(List<DAL.Entities.OrderDetails> list)
        {
            List<Models.OrderDetails> result = new List<Models.OrderDetails>();
            foreach (var item in list)
            {
                Models.OrderDetails or = new Models.OrderDetails()
                {
                    Id = item.Id,
                    Date = item.Date.ToString("yyyy-MM-dd"),
                    Quantity = item.Quantity,
                    Prod_Id = item.Prod_Id
                };
                result.Add(or);
            }
            return result;
        }
    }
}

