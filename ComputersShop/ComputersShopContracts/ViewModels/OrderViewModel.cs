using ComputerShopContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ComputerId { get; set; }
        public int ClientId { get; set; }
        public string ComputerName { get; set; }
        public int? ImplementerId { get; set; }
        public int Count { get; set; }
        public string ClientFIO { get; set; }
        public string ImplementerFIO { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
