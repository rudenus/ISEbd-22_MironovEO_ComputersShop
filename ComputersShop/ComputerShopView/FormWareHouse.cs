using System;
using Unity;
using System.Collections.Generic;
using ComputerShopContracts.ViewModels;
using ComputerShopContracts.BindingModels;
using ComputerShopBusinessLogic.BusinessLogics;
using System.Windows.Forms;
using ComputerShopBusinessLogic.ViewModels;
using ComputerShopBusinessLogic.BindingModels;

namespace ComputerShopView
{
    public partial class FormWareHouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly WareHouseLogic logic;

        private int? id;

        private Dictionary<int, (string, int)> wareHouseComponents = new Dictionary<int, (string, int)>();
        public FormWareHouse(WareHouseLogic wareHouselogic)
        {
            InitializeComponent();
            this.logic = wareHouselogic;
        }
        private void FormWareHouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WareHouseViewModel view = logic.Read(new WareHouseBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.WareHouseName;
                        textBoxFIO.Text = view.ResponsiblePersonFIO;
                        wareHouseComponents = view.WareHouseComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                wareHouseComponents = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (wareHouseComponents != null)
                {
                    dataGridView1.Rows.Clear();
                    foreach (var wareHouseComponent in wareHouseComponents)
                    {
                        dataGridView1.Rows.Add(new object[] { wareHouseComponent.Key, wareHouseComponent.Value.Item1, wareHouseComponent.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new WareHouseBindingModel
                {
                    Id = id,
                    WareHouseName = textBoxName.Text,
                    ResponsiblePersonFCS = textBoxFIO.Text,
                    DateCreate = DateTime.Now,
                    WareHouseComponents = wareHouseComponents
                }) ;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

