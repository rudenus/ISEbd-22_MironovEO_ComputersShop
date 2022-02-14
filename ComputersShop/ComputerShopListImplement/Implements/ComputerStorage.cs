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
    public class ComputerStorage : IComputerStorage
    {
        private readonly DataListSingleton source;
        public ComputerStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ComputerViewModel> GetFullList()
        {
            var result = new List<ComputerViewModel>();
            foreach (var component in source.Computers)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<ComputerViewModel> GetFilteredList(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<ComputerViewModel>();
            foreach (var computer in source.Computers)
            {
                if (computer.ComputerName.Contains(model.ComputerName))
                {
                    result.Add(CreateModel(computer));
                }
            }
            return result;
        }
        public ComputerViewModel GetElement(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var computer in source.Computers)
            {
                if (computer.Id == model.Id || computer.ComputerName ==
                model.ComputerName)
                {
                    return CreateModel(computer);
                }
            }
            return null;
        }
        public void Insert(ComputerBindingModel model)
        {
            var tempComputer = new Computer
            {
                Id = 1,
                ComputerComponents = new
            Dictionary<int, int>()
            };
            foreach (var computer in source.Computers)
            {
                if (computer.Id >= tempComputer.Id)
                {
                    tempComputer.Id = computer.Id + 1;
                }
            }
            source.Computers.Add(CreateModel(model, tempComputer));
        }
        public void Update(ComputerBindingModel model)
        {
            Computer tempComputer = null;
            foreach (var computer in source.Computers)
            {
                if (computer.Id == model.Id)
                {
                    tempComputer = computer;
                }
            }
        if (tempComputer == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempComputer);
        }
        public void Delete(ComputerBindingModel model)
        {
            for (int i = 0; i < source.Computers.Count; ++i)
            {
                if (source.Computers[i].Id == model.Id)
                {
                    source.Computers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Computer CreateModel(ComputerBindingModel model, Computer computer)
        {
            computer.ComputerName = model.ComputerName;
            computer.Price = model.Price;
            // удаляем убранные
            foreach (var key in computer.ComputerComponents.Keys.ToList())
            {
                if (!model.ComputerComponents.ContainsKey(key))
                {
                    computer.ComputerComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.ComputerComponents)
            {
                if (computer.ComputerComponents.ContainsKey(component.Key))
                {
                    computer.ComputerComponents[component.Key] =
                    model.ComputerComponents[component.Key].Item2;
                }
                else
                {
                    computer.ComputerComponents.Add(component.Key,
                    model.ComputerComponents[component.Key].Item2);
                }
            }
            return computer;
        }
        private ComputerViewModel CreateModel(Computer computer)
        {
            // требуется дополнительно получить список компонентов для изделия с
            //названиями и их количество
        var computerComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in computer.ComputerComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                computerComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new ComputerViewModel
            {
                Id = computer.Id,
                ComputerName = computer.ComputerName,
                Price = computer.Price,
                ComputerComponents = computerComponents
            };
        }
    }

}
