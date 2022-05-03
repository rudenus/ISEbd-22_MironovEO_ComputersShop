using ComputersShopContracts.BindingModels;
using ComputersShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.StoragesContracts
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
        MessageInfoViewModel GetElement(MessageInfoBindingModel model);
    }

}
