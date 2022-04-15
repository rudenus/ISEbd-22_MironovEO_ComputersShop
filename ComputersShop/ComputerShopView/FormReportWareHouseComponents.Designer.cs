namespace ComputerShopView
{
    partial class FormReportWareHouseComponents
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveExcelButton = new System.Windows.Forms.Button();
            this.wareHouseComponentsDataGridView = new System.Windows.Forms.DataGridView();
            this.StoreHouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.wareHouseComponentsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // saveExcelButton
            // 
            this.saveExcelButton.Location = new System.Drawing.Point(17, 20);
            this.saveExcelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveExcelButton.Name = "saveExcelButton";
            this.saveExcelButton.Size = new System.Drawing.Size(145, 51);
            this.saveExcelButton.TabIndex = 0;
            this.saveExcelButton.Text = "Сохранить в Excel";
            this.saveExcelButton.UseVisualStyleBackColor = true;
            this.saveExcelButton.Click += new System.EventHandler(this.saveExcelButton_Click);
            // 
            // wareHouseComponentsDataGridView
            // 
            this.wareHouseComponentsDataGridView.AllowUserToAddRows = false;
            this.wareHouseComponentsDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.wareHouseComponentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wareHouseComponentsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StoreHouse,
            this.Material,
            this.Count});
            this.wareHouseComponentsDataGridView.Location = new System.Drawing.Point(17, 98);
            this.wareHouseComponentsDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wareHouseComponentsDataGridView.Name = "wareHouseComponentsDataGridView";
            this.wareHouseComponentsDataGridView.RowHeadersVisible = false;
            this.wareHouseComponentsDataGridView.RowHeadersWidth = 51;
            this.wareHouseComponentsDataGridView.Size = new System.Drawing.Size(627, 643);
            this.wareHouseComponentsDataGridView.TabIndex = 1;
            // 
            // StoreHouse
            // 
            this.StoreHouse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StoreHouse.HeaderText = "Склад";
            this.StoreHouse.MinimumWidth = 6;
            this.StoreHouse.Name = "StoreHouse";
            // 
            // Material
            // 
            this.Material.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Material.HeaderText = "Материал";
            this.Material.MinimumWidth = 6;
            this.Material.Name = "Material";
            // 
            // Count
            // 
            this.Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Count.HeaderText = "Количество";
            this.Count.MinimumWidth = 6;
            this.Count.Name = "Count";
            // 
            // FormReportWareHouseComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 760);
            this.Controls.Add(this.wareHouseComponentsDataGridView);
            this.Controls.Add(this.saveExcelButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormReportWareHouseComponents";
            this.Text = "Материалы по складам";
            this.Load += new System.EventHandler(this.FormReportStoreHouseMaterials_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wareHouseComponentsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveExcelButton;
        private System.Windows.Forms.DataGridView wareHouseComponentsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoreHouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
    }
}