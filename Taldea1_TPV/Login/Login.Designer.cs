namespace Taldea1TPV
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.centerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblErab = new System.Windows.Forms.Label();
            this.txbErab = new System.Windows.Forms.TextBox();
            this.lblPasa = new System.Windows.Forms.Label();
            this.txbPasa = new System.Windows.Forms.TextBox();
            this.btnSartu = new System.Windows.Forms.Button();
            this.lblMezua = new System.Windows.Forms.Label();
            this.mainLayout.SuspendLayout();
            this.centerLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 3;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.mainLayout.Controls.Add(this.centerLayout, 1, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(800, 450);
            this.mainLayout.TabIndex = 0;
            // 
            // centerLayout
            // 
            this.centerLayout.ColumnCount = 1;
            this.centerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 474F));
            this.centerLayout.Controls.Add(this.lblErab, 0, 0);
            this.centerLayout.Controls.Add(this.txbErab, 0, 1);
            this.centerLayout.Controls.Add(this.lblPasa, 0, 2);
            this.centerLayout.Controls.Add(this.txbPasa, 0, 3);
            this.centerLayout.Controls.Add(this.btnSartu, 0, 4);
            this.centerLayout.Controls.Add(this.lblMezua, 0, 5);
            this.centerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerLayout.Location = new System.Drawing.Point(163, 3);
            this.centerLayout.Name = "centerLayout";
            this.centerLayout.RowCount = 7;
            this.centerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.centerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.centerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.centerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.centerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.centerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.centerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.centerLayout.Size = new System.Drawing.Size(474, 444);
            this.centerLayout.TabIndex = 0;
            // 
            // lblErab
            // 
            this.lblErab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErab.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblErab.Location = new System.Drawing.Point(3, 123);
            this.lblErab.Name = "lblErab";
            this.lblErab.Size = new System.Drawing.Size(468, 49);
            this.lblErab.TabIndex = 1;
            this.lblErab.Text = "Erabiltzailea";
            this.lblErab.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbErab
            // 
            this.txbErab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbErab.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txbErab.Location = new System.Drawing.Point(3, 175);
            this.txbErab.Name = "txbErab";
            this.txbErab.Size = new System.Drawing.Size(468, 39);
            this.txbErab.TabIndex = 2;
            // 
            // lblPasa
            // 
            this.lblPasa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPasa.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPasa.Location = new System.Drawing.Point(3, 221);
            this.lblPasa.Name = "lblPasa";
            this.lblPasa.Size = new System.Drawing.Size(468, 49);
            this.lblPasa.TabIndex = 3;
            this.lblPasa.Text = "Pasahitza";
            this.lblPasa.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbPasa
            // 
            this.txbPasa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbPasa.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txbPasa.Location = new System.Drawing.Point(3, 273);
            this.txbPasa.Name = "txbPasa";
            this.txbPasa.PasswordChar = '●';
            this.txbPasa.Size = new System.Drawing.Size(468, 39);
            this.txbPasa.TabIndex = 4;
            // 
            // btnSartu
            // 
            this.btnSartu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(107)))), ((int)(((byte)(58)))));
            this.btnSartu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSartu.FlatAppearance.BorderSize = 0;
            this.btnSartu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSartu.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnSartu.ForeColor = System.Drawing.Color.White;
            this.btnSartu.Location = new System.Drawing.Point(3, 322);
            this.btnSartu.Name = "btnSartu";
            this.btnSartu.Size = new System.Drawing.Size(468, 68);
            this.btnSartu.TabIndex = 5;
            this.btnSartu.Text = "Sartu";
            this.btnSartu.UseVisualStyleBackColor = false;
            this.btnSartu.Click += new System.EventHandler(this.btnSartu_Klik);
            // 
            // lblMezua
            // 
            this.lblMezua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMezua.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.lblMezua.ForeColor = System.Drawing.Color.Red;
            this.lblMezua.Location = new System.Drawing.Point(3, 393);
            this.lblMezua.Name = "lblMezua";
            this.lblMezua.Size = new System.Drawing.Size(468, 51);
            this.lblMezua.TabIndex = 6;
            this.lblMezua.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainLayout);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AJA TPV - Sarrera";
            this.Load += new System.EventHandler(this.Login_Load);
            this.mainLayout.ResumeLayout(false);
            this.centerLayout.ResumeLayout(false);
            this.centerLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel centerLayout;
        private System.Windows.Forms.Label lblErab;
        private System.Windows.Forms.TextBox txbErab;
        private System.Windows.Forms.Label lblPasa;
        private System.Windows.Forms.TextBox txbPasa;
        private System.Windows.Forms.Button btnSartu;
        private System.Windows.Forms.Label lblMezua;
    }
}
