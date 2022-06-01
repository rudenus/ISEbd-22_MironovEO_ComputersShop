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
    public partial class FormMail : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IMessageInfoLogic logic;
        public FormMail(IMessageInfoLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormMail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridViewMail.DataSource = list;
                    dataGridViewMail.Columns[0].Visible = false;
                    dataGridViewMail.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
