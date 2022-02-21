using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopListImplement.Models
{
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> ComputerComponents { get; set; }
    }
}
