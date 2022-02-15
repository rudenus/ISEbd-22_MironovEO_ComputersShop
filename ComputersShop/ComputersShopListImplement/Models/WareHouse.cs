using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShopListImplement.Models
{
    public class WareHouse
    {
        public int Id { get; set; }

        public string WareHouseName { get; set; }

        public string ResponsiblePersonFCS { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, int> WareHouseComponents { get; set; }
    }
}
