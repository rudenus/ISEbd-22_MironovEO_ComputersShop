using ComputersShopContracts.BindingModels;
using ComputersShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.BusinessLogicContracts
{
    public interface IReportLogic
    {
        List<ReportComputerComponentViewModel> GetComputerComponent();
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        void SaveComponentsToWordFile(ReportBindingModel model);
        void SaveComputerComponentToExcelFile(ReportBindingModel model);
        void SaveOrdersToPdfFile(ReportBindingModel model);
    }

}
