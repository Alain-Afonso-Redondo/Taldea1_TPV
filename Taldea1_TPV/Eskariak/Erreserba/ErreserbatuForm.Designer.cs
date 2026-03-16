namespace Taldea1TPV.Eskariak
{
    partial class ErreserbatuForm
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
            this.panelCard = new System.Windows.Forms.Panel();

            this.lblIzenburua = new System.Windows.Forms.Label();

            this.lblIzena = new System.Windows.Forms.Label();
            this.txtIzena = new System.Windows.Forms.TextBox();

            this.lblTelefonoa = new System.Windows.Forms.Label();
            this.txtTelefonoa = new System.Windows.Forms.TextBox();

            this.lblPertsonak = new System.Windows.Forms.Label();
            this.txtPertsonak = new System.Windows.Forms.TextBox();

            this.lblData = new System.Windows.Forms.Label();
            this.dtpData = new System.Windows.Forms.DateTimePicker();

            this.cboxBazkaria = new System.Windows.Forms.CheckBox();
            this.cboxAfaria = new System.Windows.Forms.CheckBox();

            this.lblMahaia = new System.Windows.Forms.Label();
            this.cmbMahaiak = new System.Windows.Forms.ComboBox();

            this.btnErreserbatu = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ===== FORM =====
            this.Text = "OSIS - Erreserba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Load += new System.EventHandler(this.ErreserbatuForm_Load);

            // ===== CARD PANEL =====
            this.panelCard.Size = new System.Drawing.Size(520, 540);
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Location = new System.Drawing.Point(
                (this.ClientSize.Width - 520) / 2,
                (this.ClientSize.Height - 540) / 2
            );
            this.panelCard.Anchor = System.Windows.Forms.AnchorStyles.None;

            // ===== LABEL IZENBURUA =====
            this.lblIzenburua.Text = "ERRESERBA";
            this.lblIzenburua.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblIzenburua.ForeColor = System.Drawing.Color.FromArgb(31, 107, 58);
            this.lblIzenburua.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIzenburua.Height = 60;
            this.lblIzenburua.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // =====  LABLE IZENA =====
            this.lblIzena.Text = "Izena";
            this.lblIzena.Location = new System.Drawing.Point(60, 80);
            this.txtIzena.Location = new System.Drawing.Point(60, 105);
            this.txtIzena.Size = new System.Drawing.Size(400, 28);

            // ===== LABEL TELEFONOA =====
            this.lblTelefonoa.Text = "Telefonoa";
            this.lblTelefonoa.Location = new System.Drawing.Point(60, 150);
            this.txtTelefonoa.Location = new System.Drawing.Point(60, 175);
            this.txtTelefonoa.Size = new System.Drawing.Size(400, 28);

            // ===== LABEL PERTSONAK =====
            this.lblPertsonak.Text = "Pertsona kopurua";
            this.lblPertsonak.Location = new System.Drawing.Point(60, 220);
            this.txtPertsonak.Location = new System.Drawing.Point(60, 245);
            this.txtPertsonak.Size = new System.Drawing.Size(400, 28);

            // ===== LABEL DATA =====
            this.lblData.Text = "Data";
            this.lblData.Location = new System.Drawing.Point(60, 290);
            this.dtpData.Location = new System.Drawing.Point(60, 315);
            this.dtpData.Size = new System.Drawing.Size(400, 28);
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // ===== CBOX BAZKARIA =====
            this.cboxBazkaria.Appearance = System.Windows.Forms.Appearance.Button;
            this.cboxBazkaria.Text = "Bazkaria";
            this.cboxBazkaria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cboxBazkaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboxBazkaria.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.cboxBazkaria.Location = new System.Drawing.Point(60, 360);
            this.cboxBazkaria.Size = new System.Drawing.Size(180, 38);

            // ===== CBOX AFARIA =====
            this.cboxAfaria.Appearance = System.Windows.Forms.Appearance.Button;
            this.cboxAfaria.Text = "Afaria";
            this.cboxAfaria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cboxAfaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboxAfaria.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.cboxAfaria.Location = new System.Drawing.Point(280, 360);
            this.cboxAfaria.Size = new System.Drawing.Size(180, 38);

            // =====  LABEL MAHAIA =====
            this.lblMahaia.Text = "Mahaia";
            this.lblMahaia.Location = new System.Drawing.Point(60, 415);
            this.cmbMahaiak.Location = new System.Drawing.Point(60, 440);
            this.cmbMahaiak.Size = new System.Drawing.Size(400, 28);
            this.cmbMahaiak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ===== BUTTON =====
            this.btnErreserbatu.Text = "ERRESERBATU";
            this.btnErreserbatu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnErreserbatu.BackColor = System.Drawing.Color.FromArgb(243, 134, 58);
            this.btnErreserbatu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnErreserbatu.FlatAppearance.BorderSize = 0;
            this.btnErreserbatu.Size = new System.Drawing.Size(400, 46);
            this.btnErreserbatu.Location = new System.Drawing.Point(60, 485);
            this.btnErreserbatu.Click += new System.EventHandler(this.btnErreserbatu_Click);

            // ===== CONTROLS =====
            this.panelCard.Controls.Add(this.lblIzenburua);
            this.panelCard.Controls.Add(this.lblIzena);
            this.panelCard.Controls.Add(this.txtIzena);
            this.panelCard.Controls.Add(this.lblTelefonoa);
            this.panelCard.Controls.Add(this.txtTelefonoa);
            this.panelCard.Controls.Add(this.lblPertsonak);
            this.panelCard.Controls.Add(this.txtPertsonak);
            this.panelCard.Controls.Add(this.lblData);
            this.panelCard.Controls.Add(this.dtpData);
            this.panelCard.Controls.Add(this.cboxBazkaria);
            this.panelCard.Controls.Add(this.cboxAfaria);
            this.panelCard.Controls.Add(this.lblMahaia);
            this.panelCard.Controls.Add(this.cmbMahaiak);
            this.panelCard.Controls.Add(this.btnErreserbatu);

            
            this.Controls.Add(this.panelCard);

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelCard;

        private System.Windows.Forms.Label lblIzenburua;

        private System.Windows.Forms.Label lblIzena;
        private System.Windows.Forms.TextBox txtIzena;

        private System.Windows.Forms.Label lblTelefonoa;
        private System.Windows.Forms.TextBox txtTelefonoa;

        private System.Windows.Forms.Label lblPertsonak;
        private System.Windows.Forms.TextBox txtPertsonak;

        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DateTimePicker dtpData;

        private System.Windows.Forms.CheckBox cboxBazkaria;
        private System.Windows.Forms.CheckBox cboxAfaria;

        private System.Windows.Forms.Label lblMahaia;
        private System.Windows.Forms.ComboBox cmbMahaiak;

        private System.Windows.Forms.Button btnErreserbatu;
    }
}
