namespace Taldea1TPV
{
    partial class TxatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TxatForm));
            this.lblErabiltzaile = new System.Windows.Forms.Label();
            this.flpMezuak = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSarrera = new System.Windows.Forms.TextBox();
            this.btnBidali = new System.Windows.Forms.Button();
            this.pnlSarrera = new System.Windows.Forms.Panel();
            this.Fitxategi_Botoia = new System.Windows.Forms.Button();
            this.pnlGoiburua = new System.Windows.Forms.Panel();
            this.pnlSarrera.SuspendLayout();
            this.pnlGoiburua.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblErabiltzaile
            // 
            this.lblErabiltzaile.AutoSize = true;
            this.lblErabiltzaile.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblErabiltzaile.ForeColor = System.Drawing.Color.White;
            this.lblErabiltzaile.Location = new System.Drawing.Point(15, 12);
            this.lblErabiltzaile.Name = "lblErabiltzaile";
            this.lblErabiltzaile.Size = new System.Drawing.Size(118, 28);
            this.lblErabiltzaile.TabIndex = 0;
            this.lblErabiltzaile.Text = "Erabiltzailea";
            // 
            // flpMezuak
            // 
            this.flpMezuak.AutoScroll = true;
            this.flpMezuak.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.flpMezuak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMezuak.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMezuak.Location = new System.Drawing.Point(0, 55);
            this.flpMezuak.Name = "flpMezuak";
            this.flpMezuak.Padding = new System.Windows.Forms.Padding(10);
            this.flpMezuak.Size = new System.Drawing.Size(500, 475);
            this.flpMezuak.TabIndex = 1;
            this.flpMezuak.WrapContents = false;
            // 
            // txtSarrera
            // 
            this.txtSarrera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.txtSarrera.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSarrera.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSarrera.Location = new System.Drawing.Point(71, 12);
            this.txtSarrera.Multiline = true;
            this.txtSarrera.Name = "txtSarrera";
            this.txtSarrera.Size = new System.Drawing.Size(294, 35);
            this.txtSarrera.TabIndex = 2;
            // 
            // btnBidali
            // 
            this.btnBidali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.btnBidali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBidali.FlatAppearance.BorderSize = 0;
            this.btnBidali.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(220)))));
            this.btnBidali.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btnBidali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBidali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBidali.ForeColor = System.Drawing.Color.White;
            this.btnBidali.Location = new System.Drawing.Point(380, 10);
            this.btnBidali.Name = "btnBidali";
            this.btnBidali.Size = new System.Drawing.Size(100, 40);
            this.btnBidali.TabIndex = 3;
            this.btnBidali.Text = "BIDALI";
            this.btnBidali.UseVisualStyleBackColor = false;
            this.btnBidali.Click += new System.EventHandler(this.btnBidali_Klik);
            // 
            // pnlSarrera
            // 
            this.pnlSarrera.BackColor = System.Drawing.Color.White;
            this.pnlSarrera.Controls.Add(this.Fitxategi_Botoia);
            this.pnlSarrera.Controls.Add(this.txtSarrera);
            this.pnlSarrera.Controls.Add(this.btnBidali);
            this.pnlSarrera.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSarrera.Location = new System.Drawing.Point(0, 530);
            this.pnlSarrera.Name = "pnlSarrera";
            this.pnlSarrera.Size = new System.Drawing.Size(500, 60);
            this.pnlSarrera.TabIndex = 4;
            // 
            // Fitxategi_Botoia
            // 
            this.Fitxategi_Botoia.Image = ((System.Drawing.Image)(resources.GetObject("Fitxategi_Botoia.Image")));
            this.Fitxategi_Botoia.Location = new System.Drawing.Point(12, 10);
            this.Fitxategi_Botoia.Name = "Fitxategi_Botoia";
            this.Fitxategi_Botoia.Size = new System.Drawing.Size(42, 42);
            this.Fitxategi_Botoia.TabIndex = 0;
            this.Fitxategi_Botoia.UseVisualStyleBackColor = true;
            // 
            // pnlGoiburua
            // 
            this.pnlGoiburua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.pnlGoiburua.Controls.Add(this.lblErabiltzaile);
            this.pnlGoiburua.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGoiburua.Location = new System.Drawing.Point(0, 0);
            this.pnlGoiburua.Name = "pnlGoiburua";
            this.pnlGoiburua.Size = new System.Drawing.Size(500, 55);
            this.pnlGoiburua.TabIndex = 5;
            // 
            // TxatForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(500, 590);
            this.Controls.Add(this.flpMezuak);
            this.Controls.Add(this.pnlGoiburua);
            this.Controls.Add(this.pnlSarrera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TxatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AJA - Txata";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TxatForm_FormClosing);
            this.Load += new System.EventHandler(this.TxatForm_Load);
            this.pnlSarrera.ResumeLayout(false);
            this.pnlSarrera.PerformLayout();
            this.pnlGoiburua.ResumeLayout(false);
            this.pnlGoiburua.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblErabiltzaile;
        private System.Windows.Forms.FlowLayoutPanel flpMezuak;
        private System.Windows.Forms.TextBox txtSarrera;
        private System.Windows.Forms.Button btnBidali;
        private System.Windows.Forms.Panel pnlSarrera;
        private System.Windows.Forms.Panel pnlGoiburua;
        private System.Windows.Forms.Button Fitxategi_Botoia;
    }
}
