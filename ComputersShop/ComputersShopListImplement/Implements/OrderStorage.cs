using ComputerShopContracts.BindingModels;
using ComputerShopContracts.StoragesContracts;
using ComputerShopContracts.ViewModels;
using ComputerShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;
        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetFullList()
        {
            var result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                result.Add(CreateModel(order));
            }
            return result;
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if(((model.ClientId.HasValue && order.ClientId == model.ClientId) ||
                !model.DateFrom.HasValue && !model.DateTo.HasValue && order.DateCreate.Date == model.DateCreate.Date) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate.Date >= model.DateFrom.Value.Date
                && order.DateCreate.Date <= model.DateTo.Value.Date))
                    {
                    result.Add(CreateModel(order));
                }
            }
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || order.ComputerId ==
                model.ComputerId)
                {
                    return CreateModel(order);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order { Id = 1 };
            foreach (var order in source.Orders)
            {
                if (order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.ComputerId = model.ComputerId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateImplement = model.DateImplement;
            order.DateCreate = model.DateCreate;
            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string computerName = "";
            foreach (var computer in source.Computers)
            {
                if (computer.Id == order.ComputerId)
                {
                    computerName = computer.ComputerName;
                }
            }

            return new OrderViewModel
            {
                Id = order.Id,
                ComputerId = order.ComputerId,
                ComputerName = computerName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
