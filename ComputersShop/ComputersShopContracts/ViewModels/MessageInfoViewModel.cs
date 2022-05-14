using ComputersShopContracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        [Column(title: "Номер", width: 50)]
        public string MessageId { get; set; }
        [DisplayName("Отправитель")]
        [Column(title: "Отправитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SenderName { get; set; }
        [DisplayName("Дата письма")]
        [Column(title: "Дата письма", width: 150)]
        public DateTime DateDelivery { get; set; }
        [DisplayName("Заголовок")]
        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }
        [DisplayName("Текст")]
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }

}
