namespace ComputerShopView
{
    partial class FormReportComputerComponents
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Компонент = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Компьютер = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Количество = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Сохранить в Эксель";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonSaveToExcel_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Компонент,
            this.Компьютер,
            this.Количество});
            this.dataGridView.Location = new System.Drawing.Point(4, 82);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.Size = new System.Drawing.Size(794, 366);
            this.dataGridView.TabIndex = 1;
            // 
            // Компонент
            // 
            this.Компонент.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Компонент.HeaderText = "Компонент";
            this.Компонент.MinimumWidth = 6;
            this.Компонент.Name = "Компонент";
            // 
            // Компьютер
            // 
            this.Компьютер.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Компьютер.HeaderText = "Компьютер";
            this.Компьютер.MinimumWidth = 6;
            this.Компьютер.Name = "Компьютер";
            // 
            // Количество
            // 
            this.Количество.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Количество.HeaderText = "Количество";
            this.Количество.MinimumWidth = 6;
            this.Количество.Name = "Количество";
            // 
            // FormReportComputerComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button1);
            this.Name = "FormReportComputerComponents";
            this.Text = "ComponentsByComputer";
            this.Load += new System.EventHandler(this.FormReportProductComponents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Компонент;
        private System.Windows.Forms.DataGridViewTextBoxColumn Компьютер;
        private System.Windows.Forms.DataGridViewTextBoxColumn Количество;
    }
}