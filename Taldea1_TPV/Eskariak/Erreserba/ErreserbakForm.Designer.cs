namespace Taldea1TPV.Eskariak.Erreserbak
{
    partial class ErreserbakForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlEzkerra = new System.Windows.Forms.Panel();
            this.flpErreserbak = new System.Windows.Forms.FlowLayoutPanel();
            this.lblErreserbak = new System.Windows.Forms.Label();
            this.pnlEskuina = new System.Windows.Forms.Panel();
            this.flpMahaiak = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlBotoiak = new System.Windows.Forms.Panel();
            this.lblData = new System.Windows.Forms.Label();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.lblTxanda = new System.Windows.Forms.Label();
            this.cmbTxanda = new System.Windows.Forms.ComboBox();
            this.btnGehitu = new System.Windows.Forms.Button();
            this.btnEditatu = new System.Windows.Forms.Button();
            this.btnEzabatu = new System.Windows.Forms.Button();
            this.btnFreskatu = new System.Windows.Forms.Button();
            this.pnlEzkerra.SuspendLayout();
            this.pnlEskuina.SuspendLayout();
            this.pnlBotoiak.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEzkerra
            // 
            this.pnlEzkerra.BackColor = System.Drawing.Color.White;
            this.pnlEzkerra.Controls.Add(this.flpErreserbak);
            this.pnlEzkerra.Controls.Add(this.lblErreserbak);
            this.pnlEzkerra.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlEzkerra.Location = new System.Drawing.Point(0, 0);
            this.pnlEzkerra.Name = "pnlEzkerra";
            this.pnlEzkerra.Padding = new System.Windows.Forms.Padding(10);
            this.pnlEzkerra.Size = new System.Drawing.Size(280, 940);
            this.pnlEzkerra.TabIndex = 1;
            // 
            // flpErreserbak
            // 
            this.flpErreserbak.AutoScroll = true;
            this.flpErreserbak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpErreserbak.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpErreserbak.Location = new System.Drawing.Point(10, 40);
            this.flpErreserbak.Name = "flpErreserbak";
            this.flpErreserbak.Size = new System.Drawing.Size(260, 890);
            this.flpErreserbak.TabIndex = 0;
            this.flpErreserbak.WrapContents = false;
            // 
            // lblErreserbak
            // 
            this.lblErreserbak.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblErreserbak.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblErreserbak.Location = new System.Drawing.Point(10, 10);
            this.lblErreserbak.Name = "lblErreserbak";
            this.lblErreserbak.Size = new System.Drawing.Size(260, 30);
            this.lblErreserbak.TabIndex = 1;
            this.lblErreserbak.Text = "ERRESERBAK";
            // 
            // pnlEskuina
            // 
            this.pnlEskuina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlEskuina.Controls.Add(this.flpMahaiak);
            this.pnlEskuina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEskuina.Location = new System.Drawing.Point(280, 0);
            this.pnlEskuina.Name = "pnlEskuina";
            this.pnlEskuina.Size = new System.Drawing.Size(1640, 940);
            this.pnlEskuina.TabIndex = 0;
            // 
            // flpMahaiak
            // 
            this.flpMahaiak.AutoScroll = true;
            this.flpMahaiak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMahaiak.Location = new System.Drawing.Point(0, 0);
            this.flpMahaiak.Name = "flpMahaiak";
            this.flpMahaiak.Padding = new System.Windows.Forms.Padding(20);
            this.flpMahaiak.Size = new System.Drawing.Size(1640, 940);
            this.flpMahaiak.TabIndex = 0;
            // 
            // pnlBotoiak
            // 
            this.pnlBotoiak.BackColor = System.Drawing.Color.White;
            this.pnlBotoiak.Controls.Add(this.btnFreskatu);
            this.pnlBotoiak.Controls.Add(this.lblData);
            this.pnlBotoiak.Controls.Add(this.dtpData);
            this.pnlBotoiak.Controls.Add(this.lblTxanda);
            this.pnlBotoiak.Controls.Add(this.cmbTxanda);
            this.pnlBotoiak.Controls.Add(this.btnGehitu);
            this.pnlBotoiak.Controls.Add(this.btnEditatu);
            this.pnlBotoiak.Controls.Add(this.btnEzabatu);
            this.pnlBotoiak.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotoiak.Location = new System.Drawing.Point(0, 940);
            this.pnlBotoiak.Name = "pnlBotoiak";
            this.pnlBotoiak.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBotoiak.Size = new System.Drawing.Size(1920, 110);
            this.pnlBotoiak.TabIndex = 2;
            // 
            // lblData
            // 
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblData.Location = new System.Drawing.Point(20, 15);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(100, 23);
            this.lblData.TabIndex = 0;
            this.lblData.Text = "DATA";
            // 
            // dtpData
            // 
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(20, 40);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(140, 22);
            this.dtpData.TabIndex = 1;
            // 
            // lblTxanda
            // 
            this.lblTxanda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTxanda.Location = new System.Drawing.Point(180, 15);
            this.lblTxanda.Name = "lblTxanda";
            this.lblTxanda.Size = new System.Drawing.Size(100, 23);
            this.lblTxanda.TabIndex = 2;
            this.lblTxanda.Text = "TXANDA";
            // 
            // cmbTxanda
            // 
            this.cmbTxanda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTxanda.Location = new System.Drawing.Point(180, 40);
            this.cmbTxanda.Name = "cmbTxanda";
            this.cmbTxanda.Size = new System.Drawing.Size(140, 24);
            this.cmbTxanda.TabIndex = 3;
            // 
            // btnGehitu
            // 
            this.btnGehitu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(134)))), ((int)(((byte)(58)))));
            this.btnGehitu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGehitu.Location = new System.Drawing.Point(360, 35);
            this.btnGehitu.Name = "btnGehitu";
            this.btnGehitu.Size = new System.Drawing.Size(120, 40);
            this.btnGehitu.TabIndex = 4;
            this.btnGehitu.Text = "GEHITU";
            this.btnGehitu.UseVisualStyleBackColor = false;
            this.btnGehitu.Click += new System.EventHandler(this.btnGehitu_Click);
            // 
            // btnEditatu
            // 
            this.btnEditatu.Enabled = false;
            this.btnEditatu.Location = new System.Drawing.Point(500, 35);
            this.btnEditatu.Name = "btnEditatu";
            this.btnEditatu.Size = new System.Drawing.Size(120, 40);
            this.btnEditatu.TabIndex = 5;
            this.btnEditatu.Text = "EDITATU";
            this.btnEditatu.Click += new System.EventHandler(this.btnEditatu_Click);
            // 
            // btnEzabatu
            // 
            this.btnEzabatu.Enabled = false;
            this.btnEzabatu.Location = new System.Drawing.Point(640, 35);
            this.btnEzabatu.Name = "btnEzabatu";
            this.btnEzabatu.Size = new System.Drawing.Size(120, 40);
            this.btnEzabatu.TabIndex = 6;
            this.btnEzabatu.Text = "EZABATU";
            this.btnEzabatu.Click += new System.EventHandler(this.btnEzabatu_Click);
            // 
            // btnFreskatu
            // 
            this.btnFreskatu.Location = new System.Drawing.Point(781, 35);
            this.btnFreskatu.Name = "btnFreskatu";
            this.btnFreskatu.Size = new System.Drawing.Size(120, 40);
            this.btnFreskatu.TabIndex = 7;
            this.btnFreskatu.Text = "FRESKATU";
            this.btnFreskatu.Click += new System.EventHandler(this.btnFreskatu_Click);
            // 
            // ErreserbakForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1050);
            this.Controls.Add(this.pnlEskuina);
            this.Controls.Add(this.pnlEzkerra);
            this.Controls.Add(this.pnlBotoiak);
            this.Name = "ErreserbakForm";
            this.Text = "Erreserbak";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ErreserbakForm_Load);
            this.pnlEzkerra.ResumeLayout(false);
            this.pnlEskuina.ResumeLayout(false);
            this.pnlBotoiak.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.Panel pnlEzkerra;
        private System.Windows.Forms.Label lblErreserbak;
        private System.Windows.Forms.FlowLayoutPanel flpErreserbak;

        private System.Windows.Forms.Panel pnlEskuina;
        private System.Windows.Forms.FlowLayoutPanel flpMahaiak;

        private System.Windows.Forms.Panel pnlBotoiak;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.Label lblTxanda;
        private System.Windows.Forms.ComboBox cmbTxanda;
        private System.Windows.Forms.Button btnGehitu;
        private System.Windows.Forms.Button btnEditatu;
        private System.Windows.Forms.Button btnEzabatu;
        private System.Windows.Forms.Button btnFreskatu;
    }
}
