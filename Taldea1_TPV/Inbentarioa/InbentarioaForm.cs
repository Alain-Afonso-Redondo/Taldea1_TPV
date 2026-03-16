
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Taldea1TPV;

namespace Taldea1TPV.Inbentarioa
{
    public partial class InbentarioaForm : Form
    {
        public InbentarioaForm()
        {
            InitializeComponent();
        }

        private void btnFreskatu_Click(object sender, EventArgs e)
        {
            kargatuOsagaiak();
        }


        private void InbentarioaForm_Load(object sender, EventArgs e)
        {
            kargatuOsagaiak();
        }

        private void kargatuOsagaiak()
        {
            flpOsagaiak.Controls.Clear();

            var controller = new OsagaiakController();
            var osagaiak = controller.LortuOsagaiak();

            foreach (var o in osagaiak)
            {
                flpOsagaiak.Controls.Add(sortuOsagaiTxartela(o));
            }
        }


        private Control sortuOsagaiTxartela(Osagaiak o)
        {
            Panel panel = new Panel
            {
                Width = 715,
                Height = 80,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

          

            Label lblIzena = new Label
            {
                Text = o.Izena,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(15, 10),
                AutoSize = true
            };

            Label lblPrezioa = new Label
            {
                Text = $"{o.azkenPrezioa:0.00} €",
                Location = new Point(15, 45),
                Font = new Font("Segoe UI", 10)
            };

            Label lblStock = new Label
            {
                Text = $"Stock: {o.Stock}",
                Location = new Point(250, 30),
                Font = new Font("Segoe UI", 10)
            };

            Label lblStockMin = new Label
            {
                Text = $"Min: {o.gutxienekoStock}",
                Location = new Point(350, 30),
                Font = new Font("Segoe UI", 10)
            };

            Label lblEskatu = new Label
            {
                Text = (o.eskatu || o.Stock == 0) ? "ESKATU!!" : "",
                ForeColor = Color.DarkOrange,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(500, 30),
                AutoSize = true
            };


            if (o.Stock <= o.gutxienekoStock)
            {
                panel.BackColor = Color.FromArgb(245, 130, 48);
            }

            if (o.Stock == 0)
            {
                panel.BackColor = Color.FromArgb(255, 0, 0);
                lblIzena.ForeColor = Color.FromArgb(255, 255, 255);
                lblPrezioa.ForeColor = Color.FromArgb(255, 255, 255);
                lblStock.ForeColor = Color.FromArgb(255, 255, 255);
                lblStockMin.ForeColor = Color.FromArgb(255, 255, 255);
              
            }
            
            panel.Controls.Add(lblIzena);
            panel.Controls.Add(lblPrezioa);
            panel.Controls.Add(lblStock);
            panel.Controls.Add(lblStockMin);
            panel.Controls.Add(lblEskatu);

            return panel;
        }
    }
}
