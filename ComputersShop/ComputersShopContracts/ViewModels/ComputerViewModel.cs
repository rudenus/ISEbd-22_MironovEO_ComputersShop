using ComputersShopContracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.ViewModels
{
    public class ComputerViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [DisplayName("Компьютеры")]
        [Column(title: "Название компьютера", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComputerName { get; set; }
        [DisplayName("Цена")]
        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ComputerComponents { get; set; }
    }

}
