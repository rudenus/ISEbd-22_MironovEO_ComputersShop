using ComputersShopContracts.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.StoragesContracts
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }
        [Column(title: "Логин", width: 100)]
        public string Email { get; set; }
        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }
    }
}
