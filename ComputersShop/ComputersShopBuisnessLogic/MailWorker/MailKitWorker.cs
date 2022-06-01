using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicContracts;
using ComputersShopContracts.StoragesContracts;
using MailKit.Net.Pop3;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopBuisnessLogic.MailWorker
{
    public class MailKitWorker : AbstractMailWorker
    {
        public IClientStorage clientStorage;
        public MailKitWorker(IMessageInfoLogic messageInfoLogic, IClientStorage client, IMessageInfoStorage messageInfoStorage) :
        base(messageInfoLogic, client, messageInfoStorage)
        {
            clientStorage = client;
        }
        protected override async Task SendMailAsync(MailSendInfoBindingModel info)
        {
            using var objMailMessage = new MailMessage();
            using var objSmtpClient = new SmtpClient(_smtpClientHost,
            _smtpClientPort);
            try
            {
                objMailMessage.From = new MailAddress(_mailLogin);
                objMailMessage.To.Add(new MailAddress(info.MailAddress));
                objMailMessage.Subject = info.Subject;
                objMailMessage.Body = info.Text;
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(_mailLogin,
                _mailPassword);
                await Task.Run(() => objSmtpClient.Send(objMailMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected override async Task<List<MessageInfoBindingModel>> ReceiveMailAsync()
        {
            var list = new List<MessageInfoBindingModel>();
            using var client = new Pop3Client();
            await Task.Run(() =>
            {
                try
                {
                    client.Connect(_popHost, _popPort,
                    SecureSocketOptions.SslOnConnect);
                    client.Authenticate(_mailLogin, _mailPassword);
                    for (int i = 0; i < client.Count; i++)
                    {
                        var message = client.GetMessage(i);
                        foreach (var mail in message.From.Mailboxes)
                        {
                            list.Add(new MessageInfoBindingModel
                            {
                                DateDelivery =
                            message.Date.DateTime,
                                ClientId = clientStorage.GetElement(new ClientBindingModel { Email = mail.Address })?.Id,
                                MessageId = message.MessageId,
                                FromMailAddress = mail.Address,
                                Subject = message.Subject,
                                Body = message.TextBody
                            });
                        }
                    }
                }
                finally
                {
                    client.Disconnect(true);
                }
            });
            return list;
        }
    }

}
