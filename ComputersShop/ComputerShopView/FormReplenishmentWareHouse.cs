using System;
using System.Collections.Generic;
using Unity;
using ComputerShopContracts.BindingModels;
using ComputerShopContracts.ViewModels;
using ComputerShopBusinessLogic.BusinessLogics;
using System.Windows.Forms;
using ComputerShopBusinessLogic.ViewModels;
using ComputerShopBusinessLogic.BindingModels;

namespace ComputersShopView
{
    public partial class FormReplenishmentWareHouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int ComponentId
        {
            get { return Convert.ToInt32(comboBoxComponent.SelectedValue); }
            set { comboBoxComponent.SelectedValue = value; }
        }

        public int WareHouse
        {
            get { return Convert.ToInt32(comboBoxComponent.SelectedValue); }
            set { comboBoxComponent.SelectedValue = value; }
        }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        private readonly WareHouseLogic wareHouseLogic;

        public FormReplenishmentWareHouse(ComponentLogic logicComponent, WareHouseLogic logicWareHouse)
        {
            InitializeComponent();
            wareHouseLogic = logicWareHouse;

            List<ComponentViewModel> listComponent = logicComponent.Read(null);
            if (listComponent != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listComponent;
                comboBoxComponent.SelectedItem = null;
            }

            List<WareHouseViewModel> listWareHouses = logicWareHouse.Read(null);
            if (listWareHouses != null)
            {
                comboBoxWareHouse.DisplayMember = "WareHouseName";
                comboBoxWareHouse.ValueMember = "Id";
                comboBoxWareHouse.DataSource = listWareHouses;
                comboBoxWareHouse.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите продукт", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWareHouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }

            wareHouseLogic.Replenishment(new WareHouseReplenishmentBindingModel
            {
                ComponentId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                WareHouseId = Convert.ToInt32(comboBoxWareHouse.SelectedValue),
                Count = Convert.ToInt32(textBoxCount.Text)
            });

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormReplenishmentWareHouse_Load(object sender, EventArgs e)
        {

        }
    }
}