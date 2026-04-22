using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
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
        private string _eskaeraEgoera;
        private string _sukaldeaEgoera;
        private string _deskontuKodeaAktiboa;
        private decimal _deskontuPortzentaiaAktiboa;
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
            TxatBotoiaLaguntzailea.Erantsi(this);
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
                _eskaeraEgoera = null;
                _sukaldeaEgoera = null;
                _deskontuKodeaAktiboa = null;
                _deskontuPortzentaiaAktiboa = 0;
                karritoa = new List<Karritoa>();
                eguneratuKarritoa();
                EguneratuFakturaBotoiak();
                return;
            }

            _eskaeraId = eskaeraAktiboa.Id;
            _eskaeraEgoera = eskaeraAktiboa.Egoera;
            _sukaldeaEgoera = eskaeraAktiboa.SukaldeaEgoera;
            _deskontuKodeaAktiboa = string.IsNullOrWhiteSpace(eskaeraAktiboa.DeskontuKodea) ? null : eskaeraAktiboa.DeskontuKodea.Trim();
            _deskontuPortzentaiaAktiboa = string.IsNullOrWhiteSpace(_deskontuKodeaAktiboa)
                ? 0
                : Math.Max(0, Math.Min(100, eskaeraAktiboa.DeskontuPortzentaia));
            _komensalak = eskaeraAktiboa.Komensalak > 0
                ? eskaeraAktiboa.Komensalak
                : _komensalak;
            karritoa = komandaController.LortuEskaeraProduktuak(eskaeraAktiboa.Id);
            eguneratuKarritoa();
            EguneratuFakturaBotoiak();
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
                    Text = "Stock: "+ p.stock_aktuala.ToString(),
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
            if (EskaeraBlokeatutaDago())
            {
                MessageBox.Show("Eskaera hau ordainketara bidalita dago eta ezin da gehiago aldatu.");
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
            var eskaeraBlokeatuta = EskaeraBlokeatutaDago();
            var deskontuaAktibo = _deskontuPortzentaiaAktiboa > 0 && !string.IsNullOrWhiteSpace(_deskontuKodeaAktiboa);

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
                    Text = string.Format("{0:0.00} EUR", KalkulatuKarritokoLerroTotala(produktuKarrito)),
                    Location = new Point(250, 40),
                    ForeColor = Color.Black
                };

                Button btnPlus = new Button { Text = "+", Location = new Point(120, 35), Size = new Size(30, 25), ForeColor = Color.Black, Enabled = !eskaeraBlokeatuta };
                Button btnMinus = new Button { Text = "-", Location = new Point(160, 35), Size = new Size(30, 25), ForeColor = Color.Black, Enabled = !eskaeraBlokeatuta };
                Button btnEzabatu = new Button { Text = "X", Location = new Point(310, 35), Size = new Size(30, 25), ForeColor = Color.Black, Enabled = !eskaeraBlokeatuta };

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

            var totala = KalkulatuKarritoTotala();
            lblTotala.Text = deskontuaAktibo
                ? $"Totala ({_deskontuKodeaAktiboa} - {_deskontuPortzentaiaAktiboa:0.##}%): {totala:0.00} EUR"
                : $"Totala: {totala:0.00} EUR";
            EguneratuFakturaBotoiak();
        }

        private void EguneratuFakturaBotoiak()
        {
            var badagoEskaera = _eskaeraId.HasValue;
            var eskaeraBlokeatuta = EskaeraBlokeatutaDago();
            btnItxiFaktura.Enabled = badagoEskaera && !eskaeraBlokeatuta;
            btnSortuTiket.Enabled = badagoEskaera;
            btnEskatu.Enabled = !eskaeraBlokeatuta;
        }

        private bool EskaeraBlokeatutaDago()
        {
            return string.Equals(_eskaeraEgoera, "ordainketa_pendiente", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(_eskaeraEgoera, "itxita", StringComparison.OrdinalIgnoreCase);
        }

        private void btnEskatu_Klik(object sender, EventArgs e)
        {
            if (EskaeraBlokeatutaDago())
            {
                MessageBox.Show("Eskaera hau ordainketara bidalita dago eta ezin da gehiago aldatu.");
                return;
            }

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

        private void btnItxiFaktura_Click(object sender, EventArgs e)
        {
            if (!_eskaeraId.HasValue)
            {
                MessageBox.Show("Mahai honek ez dauka eskaera aktiborik.");
                return;
            }

            var komandaController = new KomandakController();
            var deskontuGaldera = ErakutsiBaiEzLeihoa(
                "Deskontua",
                "Baduzu deskontu koderik?",
                true);

            if (deskontuGaldera == DialogResult.Cancel)
                return;

            string deskontuKodea = null;
            decimal deskontuPortzentaia = 0;

            if (deskontuGaldera == DialogResult.Yes)
            {
                var kodea = EskatuTestua("Deskontu kodea", "Sartu deskontu kodea:");
                if (string.IsNullOrWhiteSpace(kodea))
                {
                    MessageBox.Show("Deskontu kodea derrigorrezkoa da.");
                    return;
                }

                string deskontuErrorea;
                if (!komandaController.EgiaztatuDeskontuKodea(kodea.Trim(), out deskontuPortzentaia, out deskontuErrorea))
                {
                    MessageBox.Show(
                        string.IsNullOrWhiteSpace(deskontuErrorea) ? "Kodea ez da zuzena edo ez dago aktibo." : deskontuErrorea,
                        "Errorea",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                deskontuKodea = kodea.Trim();
            }

            var baieztatu = ErakutsiBaiEzLeihoa(
                "Itxi faktura",
                "Mahai honetako faktura ordainketara bidali nahi duzu?");

            if (baieztatu != DialogResult.Yes)
                return;

            string errorea;
            var ondo = string.IsNullOrWhiteSpace(deskontuKodea)
                ? komandaController.OrdaintzeraBidali(_eskaeraId.Value, out errorea)
                : komandaController.OrdaintzeraBidali(_eskaeraId.Value, deskontuKodea, deskontuPortzentaia, out errorea);

            if (!ondo)
            {
                MessageBox.Show(
                    string.IsNullOrWhiteSpace(errorea) ? "Ezin izan da faktura itxi." : errorea,
                    "Errorea",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                string.IsNullOrWhiteSpace(deskontuKodea)
                    ? "Faktura ordainketara bidali da."
                    : $"Faktura ordainketara bidali da. Aplikatutako deskontua: {deskontuPortzentaia:0.##}%",
                "Ondo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _deskontuKodeaAktiboa = string.IsNullOrWhiteSpace(deskontuKodea) ? null : deskontuKodea;
            _deskontuPortzentaiaAktiboa = string.IsNullOrWhiteSpace(deskontuKodea) ? 0 : deskontuPortzentaia;
            _eskaeraEgoera = "ordainketa_pendiente";
            EguneratuFakturaBotoiak();
            eguneratuKarritoa();
        }

        private void btnSortuTiket_Click(object sender, EventArgs e)
        {
            if (!_eskaeraId.HasValue)
            {
                MessageBox.Show("Mahai honek ez dauka eskaera aktiborik.");
                return;
            }

            if (!string.Equals(_sukaldeaEgoera, "eginda", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Ezin da tiketa sortu sukaldeko egoera 'eginda' izan arte.", "Egoera: " + (_sukaldeaEgoera ?? "ez dago"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var baieztatu = ErakutsiBaiEzLeihoa(
                "Sortu tiketa",
                "Mahai honetako tiketa sortu nahi duzu?");

            if (baieztatu != DialogResult.Yes)
                return;

            var komandaController = new KomandakController();
            string errorea;
            var pdfBidea = komandaController.SortuFaktura(_eskaeraId.Value, out errorea);

            if (string.IsNullOrWhiteSpace(pdfBidea))
            {
                MessageBox.Show(
                    string.IsNullOrWhiteSpace(errorea) ? "Ezin izan da tiketa sortu." : errorea,
                    "Errorea",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                "Tiketa ondo sortu da.\n" + pdfBidea,
                "Ondo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = pdfBidea,
                    UseShellExecute = true
                });
            }
            catch
            {
            }

            kargatuEskaeraAktiboa();
        }

        private void freskatuUnekoKategoria()
        {
            if (_aukeratutakoKategoriaId.HasValue)
                kargatuPlaterakKategoriko(_aukeratutakoKategoriaId.Value);
        }

        private static string EskatuTestua(string titulua, string galdera)
        {
            using (var leihoa = new Form())
            {
                leihoa.Width = 420;
                leihoa.Height = 180;
                leihoa.Text = titulua;
                leihoa.FormBorderStyle = FormBorderStyle.FixedDialog;
                leihoa.StartPosition = FormStartPosition.CenterParent;
                leihoa.MinimizeBox = false;
                leihoa.MaximizeBox = false;

                var etiketa = new Label
                {
                    Left = 20,
                    Top = 20,
                    Width = 360,
                    Text = galdera
                };

                var testuKaxa = new TextBox
                {
                    Left = 20,
                    Top = 50,
                    Width = 360
                };

                var onartu = new Button
                {
                    Text = "Onartu",
                    DialogResult = DialogResult.OK,
                    Left = 220,
                    Width = 75,
                    Top = 90
                };

                var utzi = new Button
                {
                    Text = "Utzi",
                    DialogResult = DialogResult.Cancel,
                    Left = 305,
                    Width = 75,
                    Top = 90
                };

                leihoa.Controls.Add(etiketa);
                leihoa.Controls.Add(testuKaxa);
                leihoa.Controls.Add(onartu);
                leihoa.Controls.Add(utzi);
                leihoa.AcceptButton = onartu;
                leihoa.CancelButton = utzi;

                return leihoa.ShowDialog() == DialogResult.OK
                    ? testuKaxa.Text.Trim()
                    : null;
            }
        }

        private DialogResult ErakutsiBaiEzLeihoa(string titulua, string galdera, bool utziBotoia = false)
        {
            using (var leihoa = new Form())
            {
                leihoa.Width = 440;
                leihoa.Height = 190;
                leihoa.Text = titulua;
                leihoa.FormBorderStyle = FormBorderStyle.FixedDialog;
                leihoa.StartPosition = FormStartPosition.CenterParent;
                leihoa.MinimizeBox = false;
                leihoa.MaximizeBox = false;

                var etiketa = new Label
                {
                    Left = 20,
                    Top = 20,
                    Width = 390,
                    Height = 60,
                    Text = galdera
                };

                var btnBai = new Button
                {
                    Text = "Bai",
                    DialogResult = DialogResult.Yes,
                    Left = utziBotoia ? 170 : 250,
                    Width = 75,
                    Top = 100
                };

                var btnEz = new Button
                {
                    Text = "Ez",
                    DialogResult = DialogResult.No,
                    Left = utziBotoia ? 255 : 335,
                    Width = 75,
                    Top = 100
                };

                leihoa.Controls.Add(etiketa);
                leihoa.Controls.Add(btnBai);
                leihoa.Controls.Add(btnEz);

                if (utziBotoia)
                {
                    var btnUtzi = new Button
                    {
                        Text = "Utzi",
                        DialogResult = DialogResult.Cancel,
                        Left = 340,
                        Width = 75,
                        Top = 100
                    };
                    leihoa.Controls.Add(btnUtzi);
                    leihoa.CancelButton = btnUtzi;
                }

                leihoa.AcceptButton = btnBai;
                return leihoa.ShowDialog(this);
            }
        }

        private decimal KalkulatuKarritokoLerroTotala(Karritoa lerroa)
        {
            var unitarioa = (decimal)lerroa.Prezioa;
            if (_deskontuPortzentaiaAktiboa > 0 && !string.IsNullOrWhiteSpace(_deskontuKodeaAktiboa))
                unitarioa = unitarioa * (1 - (_deskontuPortzentaiaAktiboa / 100m));

            return Math.Round(unitarioa * lerroa.Kopurua, 2, MidpointRounding.AwayFromZero);
        }

        private decimal KalkulatuKarritoTotala()
        {
            return karritoa.Sum(k => KalkulatuKarritokoLerroTotala(k));
        }

    }
}
