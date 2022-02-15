using ComputerShopContracts.BindingModels;
using ComputerShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.BusinessLogicContracts
{
    public interface IComputerLogic
    {
        List<ComputerViewModel> Read(ComputerBindingModel model);
        void CreateOrUpdate(ComputerBindingModel model);
        void Delete(ComputerBindingModel model);
    }

}
