using ComputerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopBuisnessLogic.OfficePackage.HelperModels
{
    public class WordInfoWareHouse
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<WareHouseViewModel> WareHouses { get; set; }
    }
}
