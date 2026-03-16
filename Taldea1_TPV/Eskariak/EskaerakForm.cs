using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    public partial class EskaerakForm : Form
    {
        private string erabiltzailea;
        private int _fakturaId;

        private List<Karritoa> karritoa = new List<Karritoa>();

        public EskaerakForm(string erabiltzailea, int fakturaId)
        {
            InitializeComponent();
            this.erabiltzailea = erabiltzailea;
            _fakturaId = fakturaId;
        }

        private void EskaerakForm_Load(object sender, EventArgs e)
        {
            lblErabiltzailea.Text = "Erabiltzailea: " + erabiltzailea;
            kargatuKategoriak();
        }

        // ================= KATEGORIAK =================
        private void kargatuKategoriak()
        {
            flpKategoriak.Controls.Clear();

            var kategoriakController = new KategoriakController();
            var kategoriak = kategoriakController.LortuKategoriak();

            foreach (var cat in kategoriak)
            {
                Button btn = new Button
                {
                    Text = cat.Izena,
                    Width = 180,
                    Height = 60,
                    BackColor = Color.FromArgb(31, 107, 58),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Tag = cat.Id
                };

                btn.Click += (s, e) =>
                {
                    kargatuPlaterakKategoriko((int)btn.Tag);
                };

                flpKategoriak.Controls.Add(btn);
            }
        }

        // ================= PLATERAK =================
        private void kargatuPlaterakKategoriko(int kategoriaId)
        {
            flpPlaterak.Controls.Clear();

            var platerakController = new PlaterakController();
            var platerak = platerakController
                .LortuPlaterakKategoriatik(kategoriaId)
                .Where(p => p.Stock > 0)
                .ToList();

            foreach (var p in platerak)
            {
                Panel panel = new Panel
                {
                    Width = 180,
                    Height = 100,
                    BackColor = Color.White,
                    Margin = new Padding(10),
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand
                };

                Label lblIzena = new Label
                {
                    Text = p.Izena,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(10, 10),
                    Width = 160
                };

                Label lblPrezioa = new Label
                {
                    Text = $"{p.Prezioa:0.00} €",
                    Location = new Point(10, 40),
                    Size = new Size(160, 18)
                };

                Label lblStock = new Label
                {
                    Text = $"Stock: {p.Stock}",
                    Location = new Point(10, 60),
                    Size = new Size(160, 18)
                };

                EventHandler clickHandler = (s, e) => gehituKarritora(p);

                panel.Click += clickHandler;
                lblIzena.Click += clickHandler;
                lblPrezioa.Click += clickHandler;
                lblStock.Click += clickHandler;

                panel.Controls.Add(lblIzena);
                panel.Controls.Add(lblPrezioa);
                panel.Controls.Add(lblStock);

                flpPlaterak.Controls.Add(panel);
            }
        }

        // ================= STOCK =================
        private bool JaitsiStock(int platerId, int kopurua)
        {
            var platerakController = new PlaterakController();
            return platerakController.JaitsiStock(platerId, kopurua);
        }

        private void ItzuliStock(int platerId, int kopurua)
        {
            var platerakController = new PlaterakController();
            platerakController.ItzuliStock(platerId, kopurua);
        }

        // ================= KARRITOA =================
        private void gehituKarritora(PlaterakDto p)
        {
            if (!JaitsiStock(p.Id, 1))
            {
                MessageBox.Show("Ez dago stock nahikorik");
                return;
            }

            var produktua = karritoa.FirstOrDefault(x => x.PlaterakId == p.Id);

            if (produktua == null)
            {
                karritoa.Add(new Karritoa
                {
                    PlaterakId = p.Id,
                    Izena = p.Izena,
                    Prezioa = p.Prezioa,
                    Kopurua = 1
                });
            }
            else
            {
                produktua.Kopurua++;
            }

            eguneratuKarritoa();
        }

        private void eguneratuKarritoa()
        {
            flpKarritoa.Controls.Clear();

            foreach (var produktuKarrito in karritoa)
            {
                Panel panel = new Panel
                {
                    Width = 380,
                    Height = 80,
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label lblIzena = new Label
                {
                    Text = produktuKarrito.Izena,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(10, 10),
                    Width = 200,
                    ForeColor = Color.Black
                };

                Label lblKantitatea = new Label
                {
                    Text = $"x{produktuKarrito.Kopurua}",
                    Location = new Point(20, 40),
                    ForeColor = Color.Black
                };

                Label lblPrezioa = new Label
                {
                    Text = $"{produktuKarrito.Totala:0.00} €",
                    Location = new Point(250, 40),
                    ForeColor = Color.Black
                };

                Button btnPlus = new Button { Text = "+", Location = new Point(120, 35), Size = new Size(30, 25), ForeColor = Color.Black };
                Button btnMinus = new Button { Text = "-", Location = new Point(160, 35), Size = new Size(30, 25), ForeColor = Color.Black };
                Button btnEzabatu = new Button { Text = "X", Location = new Point(310, 35), Size = new Size(30, 25), ForeColor = Color.Black };

                btnPlus.Click += (s, e) =>
                {
                    if (JaitsiStock(produktuKarrito.PlaterakId, 1))
                    {
                        produktuKarrito.Kopurua++;
                        eguneratuKarritoa();
                    }
                };

                btnMinus.Click += (s, e) =>
                {
                    ItzuliStock(produktuKarrito.PlaterakId, 1);
                    produktuKarrito.Kopurua--;

                    if (produktuKarrito.Kopurua <= 0)
                        karritoa.Remove(produktuKarrito);

                    eguneratuKarritoa();
                };

                btnEzabatu.Click += (s, e) =>
                {
                    ItzuliStock(produktuKarrito.PlaterakId, produktuKarrito.Kopurua);
                    karritoa.Remove(produktuKarrito);
                    eguneratuKarritoa();
                };

                panel.Controls.Add(lblIzena);
                panel.Controls.Add(lblKantitatea);
                panel.Controls.Add(lblPrezioa);
                panel.Controls.Add(btnPlus);
                panel.Controls.Add(btnMinus);
                panel.Controls.Add(btnEzabatu);

                flpKarritoa.Controls.Add(panel);
            }

            lblTotala.Text = "Totala: " +
                karritoa.Sum(c => c.Totala).ToString("0.00") + " €";
        }

        // ================= ESKARIA =================
        private void btnEskatu_Klik(object sender, EventArgs e)
        {
            var komandaController = new KomandakController();

            foreach (var produktuKarrito in karritoa)
            {
                bool ok = komandaController.SortuKomanda(
                    fakturaId: _fakturaId,
                    platerId: produktuKarrito.PlaterakId,
                    kopurua: produktuKarrito.Kopurua
                );

                if (!ok)
                {
                    MessageBox.Show("Errorea komanda sortzean");
                    return;
                }
            }

            

            MessageBox.Show("Komanda behar bezala eginda");

            karritoa.Clear();
            eguneratuKarritoa();
            flpPlaterak.Controls.Clear();


            //var fakturaCtrl = new FakturakController();

            //string ruta = fakturaCtrl.SortuTiketa(
            //    _fakturaId,
            //    jasotakoa: karritoa.Sum(c => c.Totala),
            //    ordainketaModua: "Eskudirua"
            //);

            //if (!string.IsNullOrEmpty(ruta))
            //{
            //    System.Diagnostics.Process.Start(
            //        new System.Diagnostics.ProcessStartInfo
            //        {
            //            FileName = "" + ruta,
            //            UseShellExecute = true
            //        });
            //}

        }



        private void btnTxat_Click(object sender, EventArgs e)
        {
            new TxatForm(erabiltzailea).Show();
        }
    }
}
