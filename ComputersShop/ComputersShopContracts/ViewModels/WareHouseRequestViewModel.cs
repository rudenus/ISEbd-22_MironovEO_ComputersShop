using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.ViewModels
{
    public class WareHouseRequestViewModel
    {
        public int Id { get; set; }
        public string WareHouseName { get; set; }
        public string ResponsiblePersonFIO { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, Tuple<string, int>> WareHouseComponents { get; set; }//если задать кортеж по другому не переведется в json
    }
}
