using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShopBusinessLogic.BindingModels
{
    public class WareHouseReplenishmentBindingModel
    {
        public int ComponentId { get; set; }
        public int WareHouseId { get; set; }
        public int Count { get; set; }
    }
}
