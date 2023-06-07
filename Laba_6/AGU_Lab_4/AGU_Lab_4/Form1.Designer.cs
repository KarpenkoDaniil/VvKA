namespace AGU_Lab_4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CalculateCPU = new System.Windows.Forms.Button();
            this.ResultCPU = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ResultGPU = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CalculateCPU
            // 
            this.CalculateCPU.Location = new System.Drawing.Point(30, 45);
            this.CalculateCPU.Name = "CalculateCPU";
            this.CalculateCPU.Size = new System.Drawing.Size(75, 23);
            this.CalculateCPU.TabIndex = 0;
            this.CalculateCPU.Text = "On CPU";
            this.CalculateCPU.UseVisualStyleBackColor = true;
            this.CalculateCPU.Click += new System.EventHandler(this.CalculateCPU_Click);
            // 
            // ResultCPU
            // 
            this.ResultCPU.AutoSize = true;
            this.ResultCPU.Location = new System.Drawing.Point(27, 97);
            this.ResultCPU.Name = "ResultCPU";
            this.ResultCPU.Size = new System.Drawing.Size(59, 13);
            this.ResultCPU.TabIndex = 1;
            this.ResultCPU.Text = "Результат";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OnGrapicsCard";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ResultGPU
            // 
            this.ResultGPU.AutoSize = true;
            this.ResultGPU.Location = new System.Drawing.Point(452, 97);
            this.ResultGPU.Name = "ResultGPU";
            this.ResultGPU.Size = new System.Drawing.Size(59, 13);
            this.ResultGPU.TabIndex = 3;
            this.ResultGPU.Text = "Результат";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ResultGPU);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ResultCPU);
            this.Controls.Add(this.CalculateCPU);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CalculateCPU;
        private System.Windows.Forms.Label ResultCPU;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ResultGPU;
    }
}

