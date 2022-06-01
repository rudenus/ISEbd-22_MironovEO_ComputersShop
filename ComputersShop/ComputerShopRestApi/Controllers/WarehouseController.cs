using ComputerShopBusinessLogic.BindingModels;
using ComputerShopBusinessLogic.ViewModels;
using ComputerShopContracts.BusinessLogicContracts;
using ComputerShopContracts.ViewModels;
using ComputersShopContracts.BindingModels;
using ComputersShopContracts.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWareHouseLogic wareHouseLogic;
        private readonly IComponentLogic componentLogic;
        public WarehouseController(IWareHouseLogic wareHouseLogic, IComponentLogic componentLogic)
        {
            this.wareHouseLogic = wareHouseLogic;
            this.componentLogic = componentLogic;
        }
        [HttpGet]
        public List<WareHouseRequestViewModel> GetAll() {
            var list = wareHouseLogic.Read(null);
            List<WareHouseRequestViewModel> listReq = new List<WareHouseRequestViewModel>();
            foreach (var item in list)
            {
                var model = new WareHouseRequestViewModel()
                {
                    DateCreate = item.DateCreate,
                    Id = item.Id,
                    ResponsiblePersonFIO = item.ResponsiblePersonFIO,
                    WareHouseName = item.WareHouseName
                };
                var listTuple = new Dictionary<int, Tuple<string, int>>();//кодируется в json только в таком формате
                foreach (var tuple in item.WareHouseComponents)
                {
                    listTuple.Add(tuple.Key, new Tuple<string, int>(tuple.Value.Item1, tuple.Value.Item2));
                }
                model.WareHouseComponents = listTuple;
                listReq.Add(model);
                
            }
            return listReq;
            } 
        [HttpGet]
        public List<ComponentViewModel> GetAllComponents() => componentLogic.Read(null);

        [HttpPost]
        public void Create(WareHouseBindingModel model) => wareHouseLogic.CreateOrUpdate(model);

        [HttpPost]
        public void Update(WareHouseRequestViewModel model)
        {
            var binding = new WareHouseBindingModel()
            {
                DateCreate = model.DateCreate,
                Id=model.Id,
                ResponsiblePersonFCS=model.ResponsiblePersonFIO,
                WareHouseName=model.WareHouseName
            };
            var listTuple = new Dictionary<int, (string, int)>();
            foreach (var tuple in model.WareHouseComponents)
            {
                listTuple.Add(tuple.Key, (tuple.Value.Item1, tuple.Value.Item2));
            }
            binding.WareHouseComponents = listTuple;
            wareHouseLogic.CreateOrUpdate(binding);
        }
            

        [HttpPost]
        public void Delete(WareHouseBindingModel model) => wareHouseLogic.Delete(model);

        [HttpPost]
        public void AddComponent(WareHouseReplenishmentBindingModel model) => wareHouseLogic.Replenishment(model);
    }
}
