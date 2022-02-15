using ComputerShopContracts.BindingModels;
using ComputerShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.StoragesContracts
{
    public interface IComputerStorage
    {
        List<ComputerViewModel> GetFullList();
        List<ComputerViewModel> GetFilteredList(ComputerBindingModel model);
        ComputerViewModel GetElement(ComputerBindingModel model);
        void Insert(ComputerBindingModel model);
        void Update(ComputerBindingModel model);
        void Delete(ComputerBindingModel model);
    }

}
