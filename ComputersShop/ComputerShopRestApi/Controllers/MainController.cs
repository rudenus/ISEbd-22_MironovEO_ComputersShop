using ComputerShopContracts.BindingModels;
using ComputerShopContracts.BusinessLogicContracts;
using ComputerShopContracts.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ComputerShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IComputerLogic _product;
        public MainController(IOrderLogic order, IComputerLogic product)
        {
            _order = order;
            _product = product;
        }
        [HttpGet]
        public List<ComputerViewModel> GetProductList() => _product.Read(null)?.ToList();
        [HttpGet]
        public ComputerViewModel GetProduct(int productId) => _product.Read(new
        ComputerBindingModel
        { Id = productId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
        OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
    }
}
