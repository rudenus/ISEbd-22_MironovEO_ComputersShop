using ComputerShopContracts.BindingModels;
using ComputerShopContracts.StoragesContracts;
using ComputersShopBuisnessLogic.OfficePackage;
using ComputersShopBuisnessLogic.OfficePackage.HelperModels;
using ComputersShopContracts.BusinessLogicContracts;
using ComputersShopContracts.BindingModels;
using ComputersShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopBusinessLogic.Interfaces;

namespace ComputersShopBuisnessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IComputerStorage _ComputerStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IWareHouseStorage _wareHouseStorage;
        private readonly ComputerSaveToExcel _saveToExcel;
        private readonly ComputerSaveToWord _saveToWord;
        private readonly ComputerSaveToPdf _saveToPdf;
        public ReportLogic(IComputerStorage ComputerStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage,
        ComputerSaveToExcel saveToExcel, ComputerSaveToWord saveToWord,
       ComputerSaveToPdf saveToPdf, IWareHouseStorage wareHouseStorage)
        {
            _ComputerStorage = ComputerStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _wareHouseStorage = wareHouseStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        public List<ReportComputerComponentViewModel> GetComputerComponent()
        {
            var Computers = _ComputerStorage.GetFullList();
            var list = new List<ReportComputerComponentViewModel>();
            foreach (var Computer in Computers)
            {
                var record = new ReportComputerComponentViewModel
                {
                    ComputerName = Computer.ComputerName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in Computer.ComputerComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom =
           model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                ComputerName = x.ComputerName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Computers = _ComputerStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveComputerComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компьютеров",
                ComputerComponents = GetComputerComponent()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
        public List<ReportWareHouseComponentViewModel> GetWareHouseComponents()
        {
            var components = _componentStorage.GetFullList();
            var wareHouses = _wareHouseStorage.GetFullList();
            var records = new List<ReportWareHouseComponentViewModel>();
            foreach (var wareHouse in wareHouses)
            {
                var record = new ReportWareHouseComponentViewModel
                {
                    WareHouseName = wareHouse.WareHouseName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (wareHouse.WareHouseComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(
                            component.ComponentName, wareHouse.WareHouseComponents[component.Id].Item2));

                        record.TotalCount += wareHouse.WareHouseComponents[component.Id].Item2;
                    }
                }
                records.Add(record);
            }
            return records;
        }
        public List<ReportOrdersForAllDatesViewModel> GetOrdersForAllDates()
        {
            return _orderStorage.GetFullList()
                .GroupBy(order => order.DateCreate.ToShortDateString())
                .Select(rec => new ReportOrdersForAllDatesViewModel
                {
                    Date = Convert.ToDateTime(rec.Key),
                    Count = rec.Count(),
                    Sum = rec.Sum(order => order.Sum)
                })
                .ToList();
        }
        public void SaveWareHousesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDocWareHouse(new WordInfoWareHouse
            {
                FileName = model.FileName,
                Title = "Список складов",
                WareHouses = _wareHouseStorage.GetFullList()
            });
        }

        public void SaveWareHouseComponentsToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateDocWareHouse(new ExcelInfoWareHouse
            {
                FileName = model.FileName,
                Title = "Список складов",
                WareHouseComponents = GetWareHouseComponents()
            });
        }

        public void SaveOrdersForAllDatesToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDocOrdersForAllDates(new PdfInfoOrdersForAllDates
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrdersForAllDates()
            });
        }

    }
}
