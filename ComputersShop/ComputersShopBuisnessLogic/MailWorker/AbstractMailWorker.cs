using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopBuisnessLogic.MailWorker
{
    public abstract class AbstractMailWorker
    {
        protected string _mailLogin;
        protected string _mailPassword;
        protected string _smtpClientHost;
        protected int _smtpClientPort;
        protected string _popHost;
        protected int _popPort;
        private IMessageInfoLogic _messageInfoLogic;
        public AbstractMailWorker(IMessageInfoLogic messageInfoLogic)
        {
            _messageInfoLogic = messageInfoLogic;
        }
        public void MailConfig(MailConfigBindingModel config)
        {
            _mailLogin = config.MailLogin;
            _mailPassword = config.MailPassword;
            _smtpClientHost = config.SmtpClientHost;
            _smtpClientPort = config.SmtpClientPort;
            _popHost = config.PopHost;
            _popPort = config.PopPort;
        }
        public async void MailSendAsync(MailSendInfoBindingModel info)
        {
            if (string.IsNullOrEmpty(_mailLogin) || string.IsNullOrEmpty(_mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(_smtpClientHost) || _smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) ||
           string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text))
            {
                return;
            }
            await SendMailAsync(info);
        }
        public async void MailCheck()
        {
            if (string.IsNullOrEmpty(_mailLogin) || string.IsNullOrEmpty(_mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(_popHost) || _popPort == 0)
            {
                return;
            }
            if (_messageInfoLogic == null)
            {
                return;
            }
            var list = await ReceiveMailAsync();
            foreach (var mail in list)
            {
                _messageInfoLogic.CreateOrUpdate(mail);
            }
        }
        protected abstract Task SendMailAsync(MailSendInfoBindingModel info);
        protected abstract Task<List<MessageInfoBindingModel>> ReceiveMailAsync();
    }
}
