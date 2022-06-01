using ComputersShopContracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.ViewModels
{
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        [Column(title: "Наименование компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }

}
