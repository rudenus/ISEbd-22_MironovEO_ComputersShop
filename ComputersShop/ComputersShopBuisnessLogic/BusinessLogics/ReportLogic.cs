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

namespace ComputersShopBuisnessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IComputerStorage _ComputerStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly ComputerSaveToExcel _saveToExcel;
        private readonly ComputerSaveToWord _saveToWord;
        private readonly ComputerSaveToPdf _saveToPdf;
        public ReportLogic(IComputerStorage ComputerStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage,
        ComputerSaveToExcel saveToExcel, ComputerSaveToWord saveToWord,
       ComputerSaveToPdf saveToPdf)
        {
            _ComputerStorage = ComputerStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
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
                Status = x.Status.ToString(),
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
                Title = "Список компонент",
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
    }
}
