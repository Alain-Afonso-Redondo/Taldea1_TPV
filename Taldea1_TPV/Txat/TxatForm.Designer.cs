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
            this.lblErabiltzaile = new System.Windows.Forms.Label();
            this.txtMezuak = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnBidali = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblErabiltzaile
            // 
            this.lblErabiltzaile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblErabiltzaile.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblErabiltzaile.Location = new System.Drawing.Point(12, 10);
            this.lblErabiltzaile.Name = "lblErabiltzaile";
            this.lblErabiltzaile.Size = new System.Drawing.Size(400, 30);
            this.lblErabiltzaile.TabIndex = 0;
            this.lblErabiltzaile.Text = "Erabiltzailea";
            // 
            // txtMezuak
            // 
            this.txtMezuak.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMezuak.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMezuak.ForeColor = System.Drawing.Color.Black;
            this.txtMezuak.Location = new System.Drawing.Point(12, 45);
            this.txtMezuak.Multiline = true;
            this.txtMezuak.Name = "txtMezuak";
            this.txtMezuak.ReadOnly = true;
            this.txtMezuak.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMezuak.Size = new System.Drawing.Size(560, 300);
            this.txtMezuak.TabIndex = 1;
            
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtInput.Location = new System.Drawing.Point(12, 360);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(450, 30);
            this.txtInput.TabIndex = 2;
            
            // 
            // btnBidali
            // 
            this.btnBidali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(107)))), ((int)(((byte)(58)))));
            this.btnBidali.FlatAppearance.BorderSize = 0;
            this.btnBidali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBidali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBidali.ForeColor = System.Drawing.Color.White;
            this.btnBidali.Location = new System.Drawing.Point(470, 355);
            this.btnBidali.Name = "btnBidali";
            this.btnBidali.Size = new System.Drawing.Size(100, 35);
            this.btnBidali.TabIndex = 3;
            this.btnBidali.Text = "Bidali";
            this.btnBidali.UseVisualStyleBackColor = false;
            this.btnBidali.Click += new System.EventHandler(this.btnBidali_Klik);
            // 
            // TxatForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(600, 430);
            this.Controls.Add(this.lblErabiltzaile);
            this.Controls.Add(this.txtMezuak);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnBidali);
            this.MaximizeBox = false;
            this.Name = "TxatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AJA - Txata";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TxatForm_FormClosing);
            this.Load += new System.EventHandler(this.TxatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblErabiltzaile;
        private System.Windows.Forms.TextBox txtMezuak;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnBidali;
    }
}
