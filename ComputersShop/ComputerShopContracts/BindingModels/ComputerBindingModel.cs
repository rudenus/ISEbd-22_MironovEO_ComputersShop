using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.BindingModels
{
    public class ComputerBindingModel
    {
        public int? Id { get; set; }
        public string ComputerName { get; set; }
        public decimal Price { get; set; }
    public Dictionary<int, (string, int)> ComputerComponents { get; set; }
    }
}
