using ComputerShopAppWareHouse.Models;
using ComputerShopBusinessLogic.BindingModels;
using ComputerShopBusinessLogic.ViewModels;
using ComputerShopClientApp;
using ComputerShopContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerShopAppWareHouse.Controllers
{
    public class SecondController : Controller
    {
        private readonly IConfiguration configuration;

        public SecondController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            if (!Program.Authorization)
            {
                return Redirect("~/Home/Privacy");
            }

            return View(APIClient.GetRequest<List<WareHouseViewModel>>($"api/warehouse/getall"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public void Privacy(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                Program.Authorization = password == configuration["Password"];

                if (!Program.Authorization)
                {
                    throw new Exception("Неверный пароль");
                }

                Response.Redirect("Index");
                return;
            }

            throw new Exception("Введите пароль");
        }

        public IActionResult Create()
        {
            if (!Program.Authorization)
            {
                return Redirect("~/Home/Privacy");
            }
            return View();
        }

        [HttpPost]
        public void Create([Bind("WareHouseName, NameOfResponsiblePerson")] WareHouseBindingModel model)
        {
            if (string.IsNullOrEmpty(model.WareHouseName) || string.IsNullOrEmpty(model.ResponsiblePersonFCS))
            {
                return;
            }
            model.WareHouseComponents = new Dictionary<int, (string, int)>();
            APIClient.PostRequest("api/warehouse/create", model);
            Response.Redirect("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<List<WareHouseViewModel>>(
                $"api/warehouse/getall").FirstOrDefault(rec => rec.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        [HttpPost]
        public IActionResult Update(int id, [Bind("Id,WareHouseName,NameOfResponsiblePerson")] WareHouseBindingModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<List<WareHouseViewModel>>(
                $"api/warehouse/getall").FirstOrDefault(rec => rec.Id == id);

            model.WareHouseComponents = warehouse.WareHouseComponents;

            APIClient.PostRequest("api/warehouse/update", model);
            return Redirect("~/Home/Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<List<WareHouseViewModel>>(
                $"api/warehouse/getall").FirstOrDefault(rec => rec.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            APIClient.PostRequest("api/warehouse/delete", new WareHouseBindingModel { Id = id });
            return Redirect("~/Home/Index");
        }

        public IActionResult AddComponent()
        {
            if (!Program.Authorization)
            {
                return Redirect("~/Home/Privacy");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WareHouseViewModel>>("api/warehouse/getall");
            ViewBag.Components = APIClient.GetRequest<List<ComponentViewModel>>($"api/warehouse/getallcomponents");

            return View();
        }

        [HttpPost]
        public IActionResult AddComponent([Bind("WareHouseId, ComponentId, Count")] WareHouseReplenishmentBindingModel model)
        {
            if (model.WareHouseId == 0 || model.ComponentId == 0 || model.Count <= 0)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<List<WareHouseViewModel>>(
                "api/warehouse/getall").FirstOrDefault(rec => rec.Id == model.WareHouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            var component = APIClient.GetRequest<List<WareHouseViewModel>>(
                "api/warehouse/getallcomponents").FirstOrDefault(rec => rec.Id == model.ComponentId);

            if (component == null)
            {
                return NotFound();
            }

            APIClient.PostRequest("api/warehouse/addcomponent", model);
            return Redirect("~/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
