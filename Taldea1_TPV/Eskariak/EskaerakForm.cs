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
        private readonly Erabiltzaileak _erabiltzailea;
        private readonly int _mahaiaId;
        private int _komensalak;
        private readonly int? _erreserbaId;
        private readonly DateTime _data;
        private readonly string _txanda;
        private int? _eskaeraId;
        private int? _aukeratutakoKategoriaId;
        private List<PlaterakDto> _platerakCache = new List<PlaterakDto>();
        private List<Karritoa> karritoa = new List<Karritoa>();

        public EskaerakForm(
            Erabiltzaileak erabiltzailea,
            int mahaiaId,
            int komensalak,
            int? erreserbaId = null,
            DateTime? data = null,
            string txanda = null)
        {
            InitializeComponent();
            _erabiltzailea = erabiltzailea;
            _mahaiaId = mahaiaId;
            _komensalak = komensalak;
            _erreserbaId = erreserbaId;
            _data = (data ?? DateTime.Today).Date;
            _txanda = string.IsNullOrWhiteSpace(txanda) ? "Bazkaria" : txanda;
        }

        private void EskaerakForm_Load(object sender, EventArgs e)
        {
            lblErabiltzailea.Text = "Erabiltzailea: " + _erabiltzailea.Erabiltzailea;
            kargatuPlaterakCache();
            kargatuKategoriak();
            kargatuEskaeraAktiboa();
        }

        private void kargatuEskaeraAktiboa()
        {
            var komandaController = new KomandakController();
            var eskaeraAktiboa = komandaController.LortuEskaeraAktiboaMahaika(_mahaiaId, _data, _txanda);

            if (eskaeraAktiboa == null)
            {
                _eskaeraId = null;
                karritoa = new List<Karritoa>();
                eguneratuKarritoa();
                return;
            }

            _eskaeraId = eskaeraAktiboa.Id;
            _komensalak = eskaeraAktiboa.Komensalak > 0
                ? eskaeraAktiboa.Komensalak
                : _komensalak;
            karritoa = komandaController.LortuEskaeraProduktuak(eskaeraAktiboa.Id);
            eguneratuKarritoa();
        }

        private void kargatuPlaterakCache()
        {
            var platerakController = new PlaterakController();
            _platerakCache = platerakController
                .LortuPlaterak()
                .Select(p => new PlaterakDto
                {
                    Id = p.Id,
                    Izena = p.Izena,
                    Prezioa = p.Prezioa,
                    stock_aktuala = p.Stock,
                    kategoria_id = p.Kategoriak != null ? p.Kategoriak.Id : 0
                })
                .ToList();
        }

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
                    _aukeratutakoKategoriaId = (int)btn.Tag;
                    kargatuPlaterakKategoriko((int)btn.Tag);
                };

                flpKategoriak.Controls.Add(btn);
            }
        }

        private void kargatuPlaterakKategoriko(int kategoriaId)
        {
            flpPlaterak.Controls.Clear();

            var platerak = _platerakCache
                .Where(p => p.kategoria_id == kategoriaId)
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
                    Text = string.Format("{0:0.00} EUR", p.Prezioa),
                    Location = new Point(10, 40),
                    Size = new Size(160, 18)
                };

                Label lblStock = new Label
                {
                    Text = "Prest erabilgarri",
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

        private void gehituKarritora(PlaterakDto p)
        {
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

            foreach (var produktuKarrito in karritoa.ToList())
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
                    Text = string.Format("x{0}", produktuKarrito.Kopurua),
                    Location = new Point(20, 40),
                    ForeColor = Color.Black
                };

                Label lblPrezioa = new Label
                {
                    Text = string.Format("{0:0.00} EUR", produktuKarrito.Totala),
                    Location = new Point(250, 40),
                    ForeColor = Color.Black
                };

                Button btnPlus = new Button { Text = "+", Location = new Point(120, 35), Size = new Size(30, 25), ForeColor = Color.Black };
                Button btnMinus = new Button { Text = "-", Location = new Point(160, 35), Size = new Size(30, 25), ForeColor = Color.Black };
                Button btnEzabatu = new Button { Text = "X", Location = new Point(310, 35), Size = new Size(30, 25), ForeColor = Color.Black };

                btnPlus.Click += (s, e) =>
                {
                    produktuKarrito.Kopurua++;
                    eguneratuKarritoa();
                };

                btnMinus.Click += (s, e) =>
                {
                    produktuKarrito.Kopurua--;

                    if (produktuKarrito.Kopurua <= 0)
                        karritoa.Remove(produktuKarrito);

                    eguneratuKarritoa();
                };

                btnEzabatu.Click += (s, e) =>
                {
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

            lblTotala.Text = "Totala: " + karritoa.Sum(c => c.Totala).ToString("0.00") + " EUR";
        }

        private void btnEskatu_Klik(object sender, EventArgs e)
        {
            if (!karritoa.Any())
            {
                MessageBox.Show("Karritoa hutsik dago");
                return;
            }

            var komandaController = new KomandakController();
            string errorea;
            bool ok = _eskaeraId.HasValue
                ? komandaController.EguneratuEskaera(_eskaeraId.Value, _komensalak, karritoa, out errorea)
                : komandaController.SortuEskaera(
                    _erabiltzailea.Id,
                    _mahaiaId,
                    _komensalak,
                    _erreserbaId,
                    _data,
                    _txanda,
                    karritoa,
                    out errorea
                );

            if (!ok)
            {
                MessageBox.Show(string.IsNullOrWhiteSpace(errorea) ? "Errorea eskaera sortzean" : errorea);
                return;
            }

            MessageBox.Show("Komanda behar bezala eginda");
            kargatuPlaterakCache();
            freskatuUnekoKategoria();
            kargatuEskaeraAktiboa();
        }

        private void freskatuUnekoKategoria()
        {
            if (_aukeratutakoKategoriaId.HasValue)
                kargatuPlaterakKategoriko(_aukeratutakoKategoriaId.Value);
        }

        private void btnTxat_Click(object sender, EventArgs e)
        {
            new TxatForm(_erabiltzailea.Erabiltzailea).Show();
        }
    }
}
