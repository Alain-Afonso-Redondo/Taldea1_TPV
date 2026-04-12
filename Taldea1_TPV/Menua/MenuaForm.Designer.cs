using System.Windows.Forms;

namespace Taldea1TPV.Menua
{
    partial class MenuaForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.menuLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnEskaria = new System.Windows.Forms.Button();
            this.btnErreserba = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            this.menuLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.Controls.Add(this.menuLayout, 0, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(800, 450);
            this.mainLayout.TabIndex = 0;
            // 
            // menuLayout
            // 
            this.menuLayout.ColumnCount = 2;
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.menuLayout.Controls.Add(this.btnEskaria, 0, 0);
            this.menuLayout.Controls.Add(this.btnErreserba, 1, 0);
            this.menuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuLayout.Location = new System.Drawing.Point(3, 3);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.Padding = new System.Windows.Forms.Padding(80, 120, 80, 120);
            this.menuLayout.RowCount = 1;
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuLayout.Size = new System.Drawing.Size(794, 444);
            this.menuLayout.TabIndex = 1;
            // 
            // btnEskaria
            // 
            this.btnEskaria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(130)))), ((int)(((byte)(48)))));
            this.btnEskaria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEskaria.FlatAppearance.BorderSize = 0;
            this.btnEskaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEskaria.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnEskaria.ForeColor = System.Drawing.Color.White;
            this.btnEskaria.Location = new System.Drawing.Point(100, 140);
            this.btnEskaria.Margin = new System.Windows.Forms.Padding(20);
            this.btnEskaria.Name = "btnEskaria";
            this.btnEskaria.Size = new System.Drawing.Size(267, 144);
            this.btnEskaria.TabIndex = 1;
            this.btnEskaria.Text = "Eskariak";
            this.btnEskaria.UseVisualStyleBackColor = false;
            this.btnEskaria.Click += new System.EventHandler(this.btnEskaria_Click);
            // 
            // btnErreserba
            // 
            this.btnErreserba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(70)))));
            this.btnErreserba.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnErreserba.FlatAppearance.BorderSize = 0;
            this.btnErreserba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnErreserba.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnErreserba.ForeColor = System.Drawing.Color.White;
            this.btnErreserba.Location = new System.Drawing.Point(407, 140);
            this.btnErreserba.Margin = new System.Windows.Forms.Padding(20);
            this.btnErreserba.Name = "btnErreserba";
            this.btnErreserba.Size = new System.Drawing.Size(267, 144);
            this.btnErreserba.TabIndex = 2;
            this.btnErreserba.Text = "Erreserbak";
            this.btnErreserba.UseVisualStyleBackColor = false;
            this.btnErreserba.Click += new System.EventHandler(this.btnErreserba_Click);
            // 
            // MenuaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainLayout);
            this.Name = "MenuaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OSIS Menua";
            this.mainLayout.ResumeLayout(false);
            this.menuLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel menuLayout;
        private System.Windows.Forms.Button btnEskaria;
        private System.Windows.Forms.Button btnErreserba;
    }
}
