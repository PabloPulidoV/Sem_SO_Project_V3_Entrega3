namespace Sem_SO_Project
{
    partial class AllProcessStats
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
            this.FinallP = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.FinallP)).BeginInit();
            this.SuspendLayout();
            // 
            // FinallP
            // 
            this.FinallP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FinallP.Location = new System.Drawing.Point(28, 12);
            this.FinallP.Name = "FinallP";
            this.FinallP.Size = new System.Drawing.Size(910, 257);
            this.FinallP.TabIndex = 0;
            // 
            // AllProcessStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 291);
            this.Controls.Add(this.FinallP);
            this.Name = "AllProcessStats";
            this.Text = "AllProcessStats";
            ((System.ComponentModel.ISupportInitialize)(this.FinallP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView FinallP;
    }
}