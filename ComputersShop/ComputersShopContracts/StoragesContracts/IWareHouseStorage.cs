using ComputerShopBusinessLogic.BindingModels;
using ComputerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShopBusinessLogic.Interfaces
{
    public interface IWareHouseStorage
    {
        List<WareHouseViewModel> GetFullList();
        List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model);
        WareHouseViewModel GetElement(WareHouseBindingModel model);
        void Insert(WareHouseBindingModel model);
        void Update(WareHouseBindingModel model);
        void Delete(WareHouseBindingModel model);
        bool TakeFromWareHouses(Dictionary<int, (string, int)> materials, int reinforcedCount);
    }
}
