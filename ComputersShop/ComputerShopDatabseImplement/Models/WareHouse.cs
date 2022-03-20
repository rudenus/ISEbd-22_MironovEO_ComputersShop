using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDatabseImplement.Models
{
    public class WareHouse
    {
        public int Id { get; set; }

        [Required]
        public string WareHouseName { get; set; }

        [Required]
        public string NameOfResponsiblePerson { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("WareHouseId")]
        public virtual List<WareHouseComponent> WareHouseComponents { get; set; }
    }
}
