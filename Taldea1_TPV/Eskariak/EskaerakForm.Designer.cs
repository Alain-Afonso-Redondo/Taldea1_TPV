namespace Taldea1TPV.Eskariak
{
    partial class EskaerakForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblErabiltzailea = new System.Windows.Forms.Label();
            this.btnTxat = new System.Windows.Forms.Button();

            this.flpKategoriak = new System.Windows.Forms.FlowLayoutPanel();
            this.flpPlaterak = new System.Windows.Forms.FlowLayoutPanel();

            this.grpKarritoa = new System.Windows.Forms.GroupBox();
            this.flpKarritoa = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotala = new System.Windows.Forms.Label();
            this.btnEskatu = new System.Windows.Forms.Button();

            // ================= FORM =================
            this.SuspendLayout();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MinimumSize = new System.Drawing.Size(1280, 800);
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.Text = "AJA TPV - Eskariak";

            // ================= HEADER =================
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);

            

            // ================= ERABILTZAILEA ===================
            this.lblErabiltzailea.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblErabiltzailea.Location = new System.Drawing.Point(230, 18);
            this.lblErabiltzailea.Size = new System.Drawing.Size(400, 23);
            this.lblErabiltzailea.Text = "Erabiltzailea:";

            // ================== TXAT ===================
            this.btnTxat.Text = "Txata";
            this.btnTxat.Size = new System.Drawing.Size(120, 32);
            this.btnTxat.BackColor = System.Drawing.Color.FromArgb(31, 107, 58);
            this.btnTxat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTxat.FlatAppearance.BorderSize = 0;
            this.btnTxat.ForeColor = System.Drawing.Color.White;
            this.btnTxat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnTxat.Location = new System.Drawing.Point(0, 14); // se ajusta al resize
            this.btnTxat.Click += new System.EventHandler(this.btnTxat_Click);

            this.pnlHeader.Controls.Add(this.lblErabiltzailea);
            this.pnlHeader.Controls.Add(this.btnTxat);

            // ================= KATEGORIAK =================
            this.flpKategoriak.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpKategoriak.Width = 220;
            this.flpKategoriak.AutoScroll = true;
            this.flpKategoriak.BackColor = System.Drawing.Color.FromArgb(37, 37, 37);
            this.flpKategoriak.Padding = new System.Windows.Forms.Padding(10);

            // ================= PLATERAK =================
            this.flpPlaterak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPlaterak.AutoScroll = true;
            this.flpPlaterak.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.flpPlaterak.Padding = new System.Windows.Forms.Padding(15);

            // ================= KARRITOA =================
            this.grpKarritoa.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpKarritoa.Width = 420;
            this.grpKarritoa.BackColor = System.Drawing.Color.FromArgb(37, 37, 37);
            this.grpKarritoa.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.grpKarritoa.Text = "🛒 Karritoa";

            this.flpKarritoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpKarritoa.AutoScroll = true;
            this.flpKarritoa.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            this.lblTotala.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotala.Height = 30;
            this.lblTotala.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotala.ForeColor = System.Drawing.Color.White;
            this.lblTotala.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotala.Text = "Totala: 0.00 €";

            this.btnEskatu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnEskatu.Height = 45;
            this.btnEskatu.BackColor = System.Drawing.Color.FromArgb(242, 140, 56);
            this.btnEskatu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEskatu.FlatAppearance.BorderSize = 0;
            this.btnEskatu.Text = "ESKATU";
            this.btnEskatu.Click += new System.EventHandler(this.btnEskatu_Klik);

            this.grpKarritoa.Controls.Add(this.flpKarritoa);
            this.grpKarritoa.Controls.Add(this.lblTotala);
            this.grpKarritoa.Controls.Add(this.btnEskatu);

            // ================= CONTROLS =================
            this.Controls.Add(this.flpPlaterak);
            this.Controls.Add(this.grpKarritoa);
            this.Controls.Add(this.flpKategoriak);
            this.Controls.Add(this.pnlHeader);

            this.Load += new System.EventHandler(this.EskaerakForm_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblErabiltzailea;
        private System.Windows.Forms.Button btnTxat;

        private System.Windows.Forms.FlowLayoutPanel flpKategoriak;
        private System.Windows.Forms.FlowLayoutPanel flpPlaterak;

        private System.Windows.Forms.GroupBox grpKarritoa;
        private System.Windows.Forms.FlowLayoutPanel flpKarritoa;
        private System.Windows.Forms.Label lblTotala;
        private System.Windows.Forms.Button btnEskatu;
    }
}
