using ComputerShopContracts.BindingModels;
using ComputerShopContracts.BusinessLogicContracts;
using ComputerShopContracts.ViewModels;
using ComputersShopBuisnessLogic.BusinessLogics;
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

namespace ComputersShopView
{
    public partial class FormCreateOrder : Form
    {
        private readonly IComputerLogic _logicCompucter;
        private readonly IOrderLogic _logicOrder;
        private readonly IClientLogic _logicClient;
        public FormCreateOrder(IComputerLogic logicCompucter, IOrderLogic logicOrder, IClientLogic logicClient)
        {
            InitializeComponent();
            _logicCompucter = logicCompucter;
            _logicOrder = logicOrder;
            _logicClient = logicClient;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                var list = _logicCompucter.Read(null);
                foreach (var component in list)
                {
                    comboBoxComputer.DisplayMember = "ComputerName";
                    comboBoxComputer.ValueMember = "Id";
                    comboBoxComputer.DataSource = list;
                    comboBoxComputer.SelectedItem = null;
                }
                var listClients = _logicClient.Read(null);
                foreach (var client in listClients)
                {
                    comboBoxClient.DataSource = listClients;
                    comboBoxClient.DisplayMember = "ClientFIO";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxComputer.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxComputer.SelectedValue);
                    ComputerViewModel computer = _logicCompucter.Read(new ComputerBindingModel
                    {Id = id}
                    )?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * computer?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ComboBoxComputer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComputer.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicOrder.CreateOrder(new CreateOrderBindingModel
                {
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    ComputerId = Convert.ToInt32(comboBoxComputer.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
