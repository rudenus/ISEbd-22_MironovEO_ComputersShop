using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.ViewModels
{
    public class ReportOrdersForAllDatesViewModel
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
