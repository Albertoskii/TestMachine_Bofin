using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get ; set; }

        public int Quantity{get; set;}

        public int Prod_Id { get; set; }


    }
}
