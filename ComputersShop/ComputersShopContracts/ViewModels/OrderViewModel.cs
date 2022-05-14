using ComputerShopContracts.Enums;
using ComputersShopContracts.Attributes;
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
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        public int ComputerId { get; set; }
        public int ClientId { get; set; }
        [Column(title: "Компьютер", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComputerName { get; set; }
        public int? ImplementerId { get; set; }
        [Column(title: "Количество", width: 100)]
        public int Count { get; set; }
        [Column(title: "Клиент", width: 100)]
        public string ClientFIO { get; set; }
        [Column(title: "Исполнитель", width: 100)]
        public string ImplementerFIO { get; set; }
        [Column(title: "Сумма", width: 50)]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 100)]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 100)]
        public DateTime? DateImplement { get; set; }
    }
}
