using System;
using ComputerShopBusinessLogic.BindingModels;
using ComputerShopBusinessLogic.Interfaces;
using ComputerShopBusinessLogic.ViewModels;
using ComputerShopListImplement.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ComputerShopListImplement.Imlements
{
    public class WareHouseStorage : IWareHouseStorage
    {
        private readonly DataListSingleton source;

        public WareHouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<WareHouseViewModel> GetFullList()
        {
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var wareHouse in source.Warehouses)
            {
                result.Add(CreateModel(wareHouse));
            }
            return result;
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.WareHouseName.Contains(model.WareHouseName))
                {
                    result.Add(CreateModel(wareHouse));
                }
            }
            return result;
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.Id == model.Id || wareHouse.WareHouseName ==
                model.WareHouseName)
                {
                    return CreateModel(wareHouse);
                }
            }
            return null;
        }

        public void Insert(WareHouseBindingModel model)
        {
            WareHouse tempWareHouse = new WareHouse
            {
                Id = 1,
                WareHouseComponents = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.Id >= tempWareHouse.Id)
                {
                    tempWareHouse.Id = wareHouse.Id + 1;
                }
            }
            source.Warehouses.Add(CreateModel(model, tempWareHouse));
        }

        public void Update(WareHouseBindingModel model)
        {
            WareHouse tempWareHouse = null;
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.Id == model.Id)
                {
                    tempWareHouse = wareHouse;
                }
            }
            if (tempWareHouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWareHouse);
        }

        public void Delete(WareHouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouses.Count; ++i)
            {
                if (source.Warehouses[i].Id == model.Id)
                {
                    source.Warehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse)
        {
            wareHouse.WareHouseName = model.WareHouseName;
            wareHouse.ResponsiblePersonFCS = model.ResponsiblePersonFCS;
            // удаляем убранные
            foreach (var key in wareHouse.WareHouseComponents.Keys.ToList())
            {
                if (!model.WareHouseComponents.ContainsKey(key))
                {
                    wareHouse.WareHouseComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.WareHouseComponents)
            {
                if (wareHouse.WareHouseComponents.ContainsKey(component.Key))
                {
                    wareHouse.WareHouseComponents[component.Key] =
                    model.WareHouseComponents[component.Key].Item2;
                }
                else
                {
                    wareHouse.WareHouseComponents.Add(component.Key,
                    model.WareHouseComponents[component.Key].Item2);
                }
            }
            return wareHouse;
        }

        private WareHouseViewModel CreateModel(WareHouse wareHouse)
        {
            Dictionary<int, (string, int)> wareHouseComponents = new Dictionary<int, (string, int)>();

            foreach (var wareHouseComponent in wareHouse.WareHouseComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (wareHouseComponent.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                wareHouseComponents.Add(wareHouseComponent.Key, (componentName, wareHouseComponent.Value));
            }
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                WareHouseName = wareHouse.WareHouseName,
                ResponsiblePersonFIO = wareHouse.ResponsiblePersonFCS,
                DateCreate = wareHouse.DateCreate,
                WareHouseComponents = wareHouseComponents
            };
        }

        public void Print()
        {
            foreach (WareHouse warehouse in source.Warehouses)
            {
                Console.WriteLine(warehouse.WareHouseComponents + " " + warehouse.ResponsiblePersonFCS + " " + warehouse.DateCreate);
                foreach (KeyValuePair<int, int> keyValue in warehouse.WareHouseComponents)
                {
                    string componentName = source.Components.FirstOrDefault(component => component.Id == keyValue.Key).ComponentName;
                    Console.WriteLine(componentName + " " + keyValue.Value);
                }
            }
        }
    }
}
