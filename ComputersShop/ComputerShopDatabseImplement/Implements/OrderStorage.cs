using ComputerShopContracts.BindingModels;
using ComputerShopContracts.StoragesContracts;
using ComputerShopContracts.ViewModels;
using ComputerShopDatabseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDatabseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                return context.Orders.Include(rec => rec.Computer)
                .Include(rec=>rec.Implementer)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ComputerId = rec.ComputerId,
                    ComputerName = rec.Computer.ComputerName,
                    Count = rec.Count,
                    ClientFIO = rec.Client.ClientFIO,
                    ImplementerFIO = rec.Implementer.ImplementerFIO,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Orders
            .Include(rec => rec.Computer)
             .Include(rec => rec.Client)
            .Include(rec => rec.Implementer)
            .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue &&
            rec.DateCreate.Date == model.DateCreate.Date) ||
             (model.DateFrom.HasValue && model.DateTo.HasValue &&
            rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <=
            model.DateTo.Value.Date) ||
             (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
            (model.SearchStatus.HasValue && model.SearchStatus.Value ==
            rec.Status) ||
            (model.ImplementerId.HasValue && rec.ImplementerId ==
            model.ImplementerId && model.Status == rec.Status)).
            Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                ClientFIO = rec.Client.ClientFIO,
                ImplementerId = rec.ImplementerId,
                ImplementerFIO = rec.Implementer.ImplementerFIO,
                ComputerId = rec.ComputerId,
                ComputerName = rec.Computer.ComputerName,
                Count = rec.Count,
                Sum = rec.Sum,
                Status = rec.Status,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
            })
            .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                Order order = context.Orders.Include(rec => rec.Computer)
                .Include(rec=>rec.Implementer)
                .Include(rec => rec.Client)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    ComputerId = order.ComputerId,
                    ImplementerId= order.ImplementerId,
                    ClientId = order.ClientId,
                    ClientFIO = order.Client.ClientFIO,
                    ComputerName = order.Computer.ComputerName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } :
                null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                Order order = new Order
                {
                    ComputerId = model.ComputerId,
                    Count = model.Count,
                    ImplementerId = model.ImplementerId,
                    ClientId = (int)model.ClientId,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.ComputerId = model.ComputerId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.ImplementerId = model.ImplementerId;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                Computer element = context.Computers.FirstOrDefault(rec => rec.Id == model.ComputerId);
                Implementer impl = context.Implementers.FirstOrDefault(rec => rec.Id == model.ImplementerId);
                if(impl != null)
                {
                    if (impl.Orders == null)
                    {
                        impl.Orders = new List<Order>();
                        context.Implementers.Update(impl);
                        context.SaveChanges();
                    }
                    impl.Orders.Add(order);
                }
                if (element != null )
                {
                    if (element.Orders == null)
                    {
                        element.Orders = new List<Order>();
                    }
                  
                    
                    element.Orders.Add(order);
                    
                    context.Computers.Update(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return order;
        }
    }
}
