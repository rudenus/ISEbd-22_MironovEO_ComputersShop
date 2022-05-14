using ComputersShopContracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.ViewModels
{
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [DisplayName("ФИО исполнителя")]
        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }
        [DisplayName("Время на заказ")]
        [Column(title: "Время на заказ", width: 50)]
        public int WorkingTime { get; set; }
        [DisplayName("Время на перерыв")]
        [Column(title: "Время на перерыв", width: 50)]
        public int PauseTime { get; set; }
    }
}
