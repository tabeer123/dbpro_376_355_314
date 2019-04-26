using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Food.Models
{
    public class OrderFoodViewModel
    {
        public class OrderProductView
        {

            public virtual FoodItem Foodid { get; set; }
            public int Orderid { get; set; }



            public int quantity { get; set; }


            public virtual Order order { get; set; }
        }
    }
}
