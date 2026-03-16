using System.Drawing;
using System.Windows.Forms;

namespace Taldea1TPV.Inbentarioa
{
    partial class InbentarioaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flpOsagaiak;
        private System.Windows.Forms.Button btnFreskatu;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flpOsagaiak = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFreskatu = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ====== FRESKATU BOTOIA ======
            this.btnFreskatu.Text = "Freskatu";
            this.btnFreskatu.Size = new System.Drawing.Size(90, 30);
            this.btnFreskatu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(107)))), ((int)(((byte)(58)))));
            this.btnFreskatu.FlatAppearance.BorderSize = 0;
            this.btnFreskatu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFreskatu.ForeColor = System.Drawing.Color.White;
            this.btnFreskatu.Location = new System.Drawing.Point(780, 10);
            this.btnFreskatu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnFreskatu.Cursor = Cursors.Hand;
            this.btnFreskatu.Click += new System.EventHandler(this.btnFreskatu_Click);

            // ====== FLOWLAYOUT OSAGAIAK ======
            this.flpOsagaiak.Dock = DockStyle.Fill;
            this.flpOsagaiak.AutoScroll = true;
            this.flpOsagaiak.Padding = new Padding(20);
            this.flpOsagaiak.BackColor = Color.FromArgb(245, 247, 246);

            // ====== FORM ======
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.Add(this.btnFreskatu);
            this.Controls.Add(this.flpOsagaiak);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Inbentarioa - Osagaiak";
            this.Load += new System.EventHandler(this.InbentarioaForm_Load);

            this.ResumeLayout(false);
        }


    }
}
