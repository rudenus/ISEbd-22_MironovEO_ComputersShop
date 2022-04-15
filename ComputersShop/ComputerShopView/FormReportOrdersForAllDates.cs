using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicContracts;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace ComputerShopView
{
    public partial class FormReportOrdersForAllDates : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IReportLogic logic;
        private readonly ReportViewer reportViewer;
        public FormReportOrdersForAllDates(IReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            reportViewer = new ReportViewer
            {
                Dock = DockStyle.Fill
            };

            reportViewer.LocalReport.LoadReportDefinition(new
            FileStream("../../../ReportOrdersForAllDates.rdlc", FileMode.Open));
            Controls.Clear();
            Controls.Add(reportViewer);
            Controls.Add(reportPanel);
        }

        private void createOrdersListButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dataSource = logic.GetOrdersForAllDates();
                ReportDataSource source = new ReportDataSource("DataSetOrderDate", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                //var parameters = new[] { new ReportParameter("ReportParameterPeriod", "c " + dateTimePickerFrom.Value.ToShortDateString() +
                //" по " + dateTimePickerTo.Value.ToShortDateString()) };
                //reportViewer.LocalReport.SetParameters(parameters);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void saveToPdfButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveOrdersForAllDatesToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                    }
                }
            }
        }
    }
}
