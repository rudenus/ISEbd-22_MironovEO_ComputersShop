using ComputerShopBusinessLogic.BindingModels;
using ComputerShopBusinessLogic.Interfaces;
using ComputerShopBusinessLogic.ViewModels;
using ComputerShopDatabseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDatabseImplement.Implements
{
    public class WareHouseStorage : IWareHouseStorage
    {
        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse WareHouse, ComputerShopDatabase context)
        {
            WareHouse.WareHouseName = model.WareHouseName;
            WareHouse.NameOfResponsiblePerson = model.ResponsiblePersonFCS;

            if (WareHouse.Id == 0)
            {
                WareHouse.DateCreate = DateTime.Now;
                context.WareHouses.Add(WareHouse);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                List<WareHouseComponent> WareHouseComponents = context.WareHouseComponents
                    .Where(rec => rec.WareHouseId == model.Id.Value)
                    .ToList();

                context.WareHouseComponents.RemoveRange(WareHouseComponents
                    .Where(rec => !model.WareHouseComponents.ContainsKey(rec.ComponentId))
                    .ToList());
                context.SaveChanges();

                foreach (WareHouseComponent updateComponent in WareHouseComponents)
                {
                    updateComponent.Count = model.WareHouseComponents[updateComponent.ComponentId].Item2;
                    model.WareHouseComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }


            foreach (KeyValuePair<int, (string, int)> WareHouseComponent in model.WareHouseComponents)
            {
                context.WareHouseComponents.Add(new WareHouseComponent
                {
                    WareHouseId = WareHouse.Id,
                    ComponentId = WareHouseComponent.Key,
                    Count = WareHouseComponent.Value.Item2
                });
                context.SaveChanges();
            }

            return WareHouse;
        }

        public List<WareHouseViewModel> GetFullList()
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                return context.WareHouses
                    .Include(rec => rec.WareHouseComponents)
                    .ThenInclude(rec => rec.Component)
                    .ToList()
                    .Select(rec => new WareHouseViewModel
                    {
                        Id = rec.Id,
                        WareHouseName = rec.WareHouseName,
                        ResponsiblePersonFIO = rec.NameOfResponsiblePerson,
                        DateCreate = rec.DateCreate,
                        WareHouseComponents = rec.WareHouseComponents
                            .ToDictionary(recWareHouseComponents => recWareHouseComponents.ComponentId,
                            recWareHouseComponents => (recWareHouseComponents.Component?.ComponentName,
                            recWareHouseComponents.Count))
                    })
                    .ToList();
            }
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                return context.WareHouses
                    .Include(rec => rec.WareHouseComponents)
                    .ThenInclude(rec => rec.Component)
                    .Where(rec => rec.WareHouseName.Contains(model.WareHouseName))
                    .ToList()
                    .Select(rec => new WareHouseViewModel
                    {
                        Id = rec.Id,
                        WareHouseName = rec.WareHouseName,
                        ResponsiblePersonFIO = rec.NameOfResponsiblePerson,
                        DateCreate = rec.DateCreate,
                        WareHouseComponents = rec.WareHouseComponents
                            .ToDictionary(recWareHouseComponent => recWareHouseComponent.ComponentId,
                            recWareHouseComponent => (recWareHouseComponent.Component?.ComponentName,
                            recWareHouseComponent.Count))
                    })
                    .ToList();
            }
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                WareHouse WareHouse = context.WareHouses
                    .Include(rec => rec.WareHouseComponents)
                    .ThenInclude(rec => rec.Component)
                    .FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName ||
                    rec.Id == model.Id);

                return WareHouse != null ?
                    new WareHouseViewModel
                    {
                        Id = WareHouse.Id,
                        WareHouseName = WareHouse.WareHouseName,
                        ResponsiblePersonFIO = WareHouse.NameOfResponsiblePerson,
                        DateCreate = WareHouse.DateCreate,
                        WareHouseComponents = WareHouse.WareHouseComponents
                            .ToDictionary(recWareHouseComponent => recWareHouseComponent.ComponentId,
                            recWareHouseComponent => (recWareHouseComponent.Component?.ComponentName,
                            recWareHouseComponent.Count))
                    } :
                    null;
            }
        }

        public void Insert(WareHouseBindingModel model)
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new WareHouse(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(WareHouseBindingModel model)
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        WareHouse WareHouse = context.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);

                        if (WareHouse == null)
                        {
                            throw new Exception("Склад не найден");
                        }

                        CreateModel(model, WareHouse, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(WareHouseBindingModel model)
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                WareHouse WareHouse = context.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);

                if (WareHouse == null)
                {
                    throw new Exception("Склад не найден");
                }
                context.WareHouses.Remove(WareHouse);
                context.SaveChanges();
            }
        }

        public bool TakeFromWareHouses(Dictionary<int, (string, int)> Components, int reinforcedCount)
        {
            using (ComputerShopDatabase context = new ComputerShopDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (KeyValuePair<int, (string, int)> Component in Components)
                        {
                            int requiredComponentCount = Component.Value.Item2 * reinforcedCount;
                            IEnumerable<WareHouseComponent> WareHouseComponents = context.WareHouseComponents
                                .Where(WareHouse => WareHouse.ComponentId == Component.Key);
                            foreach (WareHouseComponent WareHouseComponent in WareHouseComponents)
                            {
                                if (WareHouseComponent.Count <= requiredComponentCount)
                                {
                                    requiredComponentCount -= WareHouseComponent.Count;
                                    context.WareHouseComponents.Remove(WareHouseComponent);
                                }
                                else
                                {
                                    WareHouseComponent.Count -= requiredComponentCount;
                                    requiredComponentCount = 0;
                                    break;
                                }
                            }
                            if (requiredComponentCount != 0)
                            {
                                throw new Exception("Нехватка материалов на складе");
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
