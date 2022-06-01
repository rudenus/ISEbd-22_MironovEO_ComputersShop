using ComputerShopDatabseImplement.Models;
using ComputersShopContracts.BindingModels;
using ComputersShopContracts.StoragesContracts;
using ComputersShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDatabseImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList()
        {
            using var context = new ComputerShopDatabase();
            return context.MessageInfoes
            .Select(rec => new MessageInfoViewModel
            {
                MessageId = rec.MessageId,
                SenderName = rec.SenderName,
                DateDelivery = rec.DateDelivery,
                Subject = rec.Subject,
                Body = rec.Body
            })
            .ToList();
        }
        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ComputerShopDatabase())
            {
                var message = context.MessageInfoes
                .FirstOrDefault(rec => rec.MessageId == model.MessageId);
                return message != null ?
                new MessageInfoViewModel
                {
                    MessageId = message.MessageId,
                    Subject = message.Subject,
                    Body = message.Body,
                    DateDelivery = message.DateDelivery,
                    SenderName = message.SenderName
                } :
                null;
            }
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.MessageInfoes
            .Where(rec => (model.ClientId.HasValue && rec.ClientId ==
            model.ClientId) ||
            (!model.ClientId.HasValue &&
            rec.DateDelivery.Date == model.DateDelivery.Date))
            .Select(rec => new MessageInfoViewModel
            {
                MessageId = rec.MessageId,
                SenderName = rec.SenderName,
                DateDelivery = rec.DateDelivery,
                Subject = rec.Subject,
                Body = rec.Body
            })
            .ToList();
        }
        public void Insert(MessageInfoBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            MessageInfo element = context.MessageInfoes.FirstOrDefault(rec =>
            rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }
            context.MessageInfoes.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            });
            context.SaveChanges();
        }
    }
}
