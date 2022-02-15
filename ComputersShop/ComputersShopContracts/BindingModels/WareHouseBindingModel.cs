using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShopBusinessLogic.BindingModels
{
    public class WareHouseBindingModel
    {
        public int? Id { get; set; }
        public string WareHouseName { get; set; }
        public string ResponsiblePersonFCS { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> WareHouseComponents { get; set; }
    }
}
