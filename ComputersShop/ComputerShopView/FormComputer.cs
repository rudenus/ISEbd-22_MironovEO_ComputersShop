using ComputerShopContracts.BindingModels;
using ComputerShopContracts.BusinessLogicContracts;
using ComputerShopContracts.ViewModels;
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

namespace ComputersShopView
{
    public partial class FormComputer : Form
    {
        public int Id { set { id = value; } }
        private readonly IComputerLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> ComputerComponents;
        public FormComputer(IComputerLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void FormComputer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ComputerViewModel view = _logic.Read(new ComputerBindingModel
                    {
                        Id =
                   id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.ComputerName;
                        textBoxPrice.Text = view.Price.ToString();
                        ComputerComponents = view.ComputerComponents;
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
                ComputerComponents = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (ComputerComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in ComputerComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1,pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormComputerComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (ComputerComponents.ContainsKey(form.Id))
                {
                    ComputerComponents[form.Id] = (form.ComponentName, form.Count);
                }
                else
                {
                    ComputerComponents.Add(form.Id, (form.ComponentName, form.Count));
                }
                LoadData();
            }
        }
        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormComputerComponent>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = ComputerComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ComputerComponents[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        ComputerComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
        
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (ComputerComponents == null || ComputerComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new ComputerBindingModel
                {
                    Id = id,
                    ComputerName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    ComputerComponents = ComputerComponents
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
