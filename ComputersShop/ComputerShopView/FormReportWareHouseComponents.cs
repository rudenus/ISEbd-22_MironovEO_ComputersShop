using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace ComputerShopView
{
    public partial class FormReportWareHouseComponents : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IReportLogic logic;
        public FormReportWareHouseComponents(IReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void saveExcelButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveWareHouseComponentsToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormReportStoreHouseMaterials_Load(object sender, EventArgs e)
        {
            try
            {
                var wareHouseMaterials = logic.GetWareHouseComponents();
                if (wareHouseMaterials != null)
                {
                    wareHouseComponentsDataGridView.Rows.Clear();

                    foreach (var wareHouse in wareHouseMaterials)
                    {
                        wareHouseComponentsDataGridView.Rows.Add(new object[] { wareHouse.WareHouseName, "", "" });

                        foreach (var component in wareHouse.Components)
                        {
                            wareHouseComponentsDataGridView.Rows.Add(new object[] { "", component.Item1, component.Item2 });
                        }

                        wareHouseComponentsDataGridView.Rows.Add(new object[] { "Итого", "", wareHouse.TotalCount });
                        wareHouseComponentsDataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
