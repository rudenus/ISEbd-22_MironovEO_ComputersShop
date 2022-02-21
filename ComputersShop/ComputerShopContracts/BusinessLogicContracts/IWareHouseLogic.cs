using ComputerShopBusinessLogic.BindingModels;
using ComputerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShopContracts.BusinessLogicContracts
{
    public interface IWareHouseLogic
    {
        public List<WareHouseViewModel> Read(WareHouseBindingModel model);
        void CreateOrUpdate(WareHouseBindingModel model);
        void Delete(WareHouseBindingModel model);
        void Replenishment(WareHouseReplenishmentBindingModel model);
    }
}
