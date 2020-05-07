using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_WEB.Models
{
    public class OrderDetails
    {
        
        public int Id { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }

        public int Prod_Id { get; set; }


    }
}
