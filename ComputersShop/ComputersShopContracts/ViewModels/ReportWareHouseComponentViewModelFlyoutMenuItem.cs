using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.ViewModels
{
    public class ReportWareHouseComponentViewModelFlyoutMenuItem
    {
        public ReportWareHouseComponentViewModelFlyoutMenuItem()
        {
            TargetType = typeof(ReportWareHouseComponentViewModelFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}