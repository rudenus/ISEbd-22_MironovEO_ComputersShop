using ComputerShopBusinessLogic.BindingModels;
using ComputerShopBusinessLogic.ViewModels;
using ComputerShopContracts.BusinessLogicContracts;
using ComputerShopContracts.ViewModels;
using ComputersShopContracts.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public List<WareHouseViewModel> GetAll() => wareHouseLogic.Read(null);
        [HttpGet]
        public List<ComponentViewModel> GetAllComponents() => componentLogic.Read(null);

        [HttpPost]
        public void Create(WareHouseBindingModel model) => wareHouseLogic.CreateOrUpdate(model);

        [HttpPost]
        public void Update(WareHouseBindingModel model) => wareHouseLogic.CreateOrUpdate(model);

        [HttpPost]
        public void Delete(WareHouseBindingModel model) => wareHouseLogic.Delete(model);

        [HttpPost]
        public void AddMaterial(WareHouseReplenishmentBindingModel model) => wareHouseLogic.Replenishment(model);
    }
}
