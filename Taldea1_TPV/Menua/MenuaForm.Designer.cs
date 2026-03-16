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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuaForm));
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pboxLogo = new System.Windows.Forms.PictureBox();
            this.menuLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnInbentarioa = new System.Windows.Forms.Button();
            this.btnEskaria = new System.Windows.Forms.Button();
            this.btnErreserba = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLogo)).BeginInit();
            this.menuLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.Controls.Add(this.menuLayout, 0, 2);
            this.mainLayout.Controls.Add(this.pboxLogo, 0, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.mainLayout.Size = new System.Drawing.Size(800, 450);
            this.mainLayout.TabIndex = 0;
            // 
            // pboxLogo
            // 
            this.pboxLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pboxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pboxLogo.Image")));
            this.pboxLogo.Location = new System.Drawing.Point(200, 3);
            this.pboxLogo.MaximumSize = new System.Drawing.Size(500, 180);
            this.pboxLogo.MinimumSize = new System.Drawing.Size(300, 120);
            this.pboxLogo.Name = "pboxLogo";
            this.pboxLogo.Size = new System.Drawing.Size(400, 129);
            this.pboxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboxLogo.TabIndex = 0;
            this.pboxLogo.TabStop = false;
            // 
            // menuLayout
            // 
            this.menuLayout.ColumnCount = 3;
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.menuLayout.Controls.Add(this.btnInbentarioa, 0, 1);
            this.menuLayout.Controls.Add(this.btnEskaria, 1, 1);
            this.menuLayout.Controls.Add(this.btnErreserba, 2, 1);
            this.menuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuLayout.Location = new System.Drawing.Point(3, 183);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.RowCount = 3;
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.menuLayout.Size = new System.Drawing.Size(794, 264);
            this.menuLayout.TabIndex = 1;
            // 
            // btnInbentarioa
            // 
            this.btnInbentarioa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(70)))));
            this.btnInbentarioa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInbentarioa.FlatAppearance.BorderSize = 0;
            this.btnInbentarioa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInbentarioa.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnInbentarioa.ForeColor = System.Drawing.Color.White;
            this.btnInbentarioa.Location = new System.Drawing.Point(20, 99);
            this.btnInbentarioa.Margin = new System.Windows.Forms.Padding(20);
            this.btnInbentarioa.Name = "btnInbentarioa";
            this.btnInbentarioa.Size = new System.Drawing.Size(224, 65);
            this.btnInbentarioa.TabIndex = 0;
            this.btnInbentarioa.Text = "Inbentarioa";
            this.btnInbentarioa.UseVisualStyleBackColor = false;
            this.btnInbentarioa.Click += new System.EventHandler(this.btnInbentarioa_Click);
            // 
            // btnEskaria
            // 
            this.btnEskaria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(130)))), ((int)(((byte)(48)))));
            this.btnEskaria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEskaria.FlatAppearance.BorderSize = 0;
            this.btnEskaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEskaria.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnEskaria.ForeColor = System.Drawing.Color.White;
            this.btnEskaria.Location = new System.Drawing.Point(284, 99);
            this.btnEskaria.Margin = new System.Windows.Forms.Padding(20);
            this.btnEskaria.Name = "btnEskaria";
            this.btnEskaria.Size = new System.Drawing.Size(224, 65);
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
            this.btnErreserba.Location = new System.Drawing.Point(548, 99);
            this.btnErreserba.Margin = new System.Windows.Forms.Padding(20);
            this.btnErreserba.Name = "btnErreserba";
            this.btnErreserba.Size = new System.Drawing.Size(226, 65);
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
            ((System.ComponentModel.ISupportInitialize)(this.pboxLogo)).EndInit();
            this.menuLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel menuLayout;
        private System.Windows.Forms.PictureBox pboxLogo;
        private System.Windows.Forms.Button btnInbentarioa;
        private System.Windows.Forms.Button btnEskaria;
        private System.Windows.Forms.Button btnErreserba;
    }
}
