namespace Taldea1TPV.Eskariak
{
    partial class MahaiakForm
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
            this.headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblDataEuskera = new System.Windows.Forms.Label();
            this.cboxBazkaria = new System.Windows.Forms.CheckBox();
            this.cboxAfaria = new System.Windows.Forms.CheckBox();
            this.flpMahaiak = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAukeratu = new System.Windows.Forms.Button();
            this.dtimeData = new System.Windows.Forms.DateTimePicker();

            this.mainLayout.SuspendLayout();
            this.headerLayout.SuspendLayout();
            this.SuspendLayout();

            // ===== FORM =====
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Mahaiak";

            // ===== MAIN LAYOUT =====
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.RowCount = 4;
            this.mainLayout.ColumnCount = 1;

            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));

            // ===== DATA =====
            this.lblDataEuskera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataEuskera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDataEuskera.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblDataEuskera.ForeColor = System.Drawing.Color.FromArgb(29, 80, 91);
            this.lblDataEuskera.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblDataEuskera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDataEuskera.Click += new System.EventHandler(this.lblDataEuskera_Click);

            // ===== HEADER =====
            this.headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLayout.ColumnCount = 3;

            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));

            // Bazkaria
            this.cboxBazkaria.Appearance = System.Windows.Forms.Appearance.Button;
            this.cboxBazkaria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxBazkaria.Text = "Bazkaria";
            this.cboxBazkaria.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cboxBazkaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboxBazkaria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(29, 80, 91);
            this.cboxBazkaria.FlatAppearance.BorderSize = 1;
            this.cboxBazkaria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Afaria
            this.cboxAfaria.Appearance = System.Windows.Forms.Appearance.Button;
            this.cboxAfaria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxAfaria.Text = "Afaria";
            this.cboxAfaria.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cboxAfaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboxAfaria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(29, 80, 91);
            this.cboxAfaria.FlatAppearance.BorderSize = 1;
            this.cboxAfaria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ===== MAHAIAK =====
            this.flpMahaiak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMahaiak.Padding = new System.Windows.Forms.Padding(24);
            this.flpMahaiak.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.flpMahaiak.AutoScroll = true;

            // ===== AUKERATU =====
            this.btnAukeratu.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAukeratu.Width = 200;
            this.btnAukeratu.Margin = new System.Windows.Forms.Padding(0, 0, 32, 16);
            this.btnAukeratu.Text = "AUKERATU";
            this.btnAukeratu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAukeratu.BackColor = System.Drawing.Color.FromArgb(31, 107, 58);
            this.btnAukeratu.ForeColor = System.Drawing.Color.White;
            this.btnAukeratu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAukeratu.FlatAppearance.BorderSize = 0;
            this.btnAukeratu.Click += new System.EventHandler(this.btnAukeratu_Click);

            // ===== DATETIME =====
            this.dtimeData.Visible = false;
            this.dtimeData.ValueChanged += new System.EventHandler(this.dtimeData_ValueChanged);

            // ===== CONTROLS =====
            this.headerLayout.Controls.Add(this.cboxBazkaria, 1, 0);
            this.headerLayout.Controls.Add(this.cboxAfaria, 2, 0);

            this.mainLayout.Controls.Add(this.lblDataEuskera, 0, 0);
            this.mainLayout.Controls.Add(this.headerLayout, 0, 1);
            this.mainLayout.Controls.Add(this.flpMahaiak, 0, 2);
            this.mainLayout.Controls.Add(this.btnAukeratu, 0, 3);

            this.Controls.Add(this.mainLayout);
            this.Controls.Add(this.dtimeData);

            this.Load += new System.EventHandler(this.MahaiakForm_Load);

            this.mainLayout.ResumeLayout(false);
            this.headerLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel headerLayout;
        private System.Windows.Forms.Label lblDataEuskera;
        private System.Windows.Forms.CheckBox cboxBazkaria;
        private System.Windows.Forms.CheckBox cboxAfaria;
        private System.Windows.Forms.FlowLayoutPanel flpMahaiak;
        private System.Windows.Forms.Button btnAukeratu;
        private System.Windows.Forms.DateTimePicker dtimeData;
    }
}
