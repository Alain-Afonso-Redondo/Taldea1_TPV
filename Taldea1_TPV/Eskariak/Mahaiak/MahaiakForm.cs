using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Taldea1TPV.Eskariak
{
    public partial class MahaiakForm : Form
    {
        private readonly Erabiltzaileak _erabiltzailea;
        private int mahaiaZabalera;
        private int mahaiaAltuera;
        private bool hasieraEginda = false;
        private int? _mahaiHautatuaId = null;
        private string _txandaAukeratua = null;
        private Erreserba _hautatutakoErreserba = null;
        private System.Collections.Generic.List<Erreserba> _erreserbak = new System.Collections.Generic.List<Erreserba>();

        public MahaiakForm(Erabiltzaileak erabiltzailea)
        {
            InitializeComponent();
            _erabiltzailea = erabiltzailea;
            TxatBotoiaLaguntzailea.Erantsi(this);
        }

        private void MahaiakForm_Load(object sender, EventArgs e)
        {
            var culture = new CultureInfo("eu-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            EguneratuDataEuskera();

            cboxBazkaria.Click += (s, ev) => AukeratuTxanda("Bazkaria");
            cboxAfaria.Click += (s, ev) => AukeratuTxanda("Afaria");

            cboxBazkaria.CheckedChanged += (s, o) =>
            {
                if (cboxBazkaria.Checked)
                {
                    cboxAfaria.Checked = false;
                    _txandaAukeratua = "Bazkaria";
                    kargatuMahaiak();
                }
            };

            cboxAfaria.CheckedChanged += (s, o) =>
            {
                if (cboxAfaria.Checked)
                {
                    cboxBazkaria.Checked = false;
                    _txandaAukeratua = "Afaria";
                    kargatuMahaiak();
                }
            };

            _txandaAukeratua = "Bazkaria";
            cboxBazkaria.Checked = true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            KalkulatuMahaiTamainak();
            kargatuMahaiak();
            hasieraEginda = true;
        }

        private static readonly CultureInfo euskalCulture = new CultureInfo("eu-ES");

        private void EguneratuDataEuskera()
        {
            string txt = dtimeData.Value.ToString("dddd, dd MMMM yyyy", euskalCulture);
            lblDataEuskera.Text = char.ToUpper(txt[0]) + txt.Substring(1);
        }

        private void dtimeData_ValueChanged(object sender, EventArgs e)
        {
            EguneratuDataEuskera();
            kargatuMahaiak();
        }

        private void AukeratuTxanda(string txanda)
        {
            _txandaAukeratua = txanda;

            cboxBazkaria.Checked = txanda == "Bazkaria";
            cboxAfaria.Checked = txanda == "Afaria";

            cboxBazkaria.BackColor = cboxBazkaria.Checked
                ? Color.FromArgb(31, 107, 58)
                : Color.Transparent;

            cboxAfaria.BackColor = cboxAfaria.Checked
                ? Color.FromArgb(31, 107, 58)
                : Color.Transparent;

            kargatuMahaiak();
        }

        private void KalkulatuMahaiTamainak()
        {
            int w = flpMahaiak.ClientSize.Width;
            int cols = Math.Max(4, w / 300);
            mahaiaZabalera = Math.Min(260, Math.Max(190, (w / cols) - 32));
            mahaiaAltuera = (int)(mahaiaZabalera * 0.7);
        }

        private void kargatuMahaiak()
        {
            flpMahaiak.Controls.Clear();
            _mahaiHautatuaId = null;
            _hautatutakoErreserba = null;

            if (_txandaAukeratua == null)
                return;

            var mahaiCtrl = new MahaiakController();
            var erreserbaCtrl = new ErreserbakController();
            var mahaiak = mahaiCtrl.LortuMahaiak(dtimeData.Value.Date, _txandaAukeratua);
            _erreserbak = erreserbaCtrl
                .LortuErreserbakData(dtimeData.Value.Date)
                .Where(r => string.Equals(r.Txanda, _txandaAukeratua, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var mahai in mahaiak)
            {
                var panel = mahaiaSortu(mahai);
                var erreserba = _erreserbak.FirstOrDefault(r => r.MahaiaId == mahai.Id);
                panel.BackColor = LortuMahaiaKolorea(mahai, erreserba);
                panel.Cursor = Cursors.Hand;

                void hautatuMahaia(object s, EventArgs e)
                {
                    foreach (Control c in flpMahaiak.Controls)
                    {
                        var kontrolMahaia = c.Tag as Mahaiak;
                        var kontrolErreserba = kontrolMahaia == null
                            ? null
                            : _erreserbak.FirstOrDefault(r => r.MahaiaId == kontrolMahaia.Id);
                        c.BackColor = kontrolMahaia == null
                            ? Color.White
                            : LortuMahaiaKolorea(kontrolMahaia, kontrolErreserba);
                    }

                    panel.BackColor = Color.FromArgb(220, 240, 225);
                    _mahaiHautatuaId = mahai.Id;
                    _hautatutakoErreserba = erreserba;
                }

                panel.Click += hautatuMahaia;

                foreach (Control child in panel.Controls)
                {
                    child.Cursor = Cursors.Hand;
                    child.Click += hautatuMahaia;
                }

                panel.Tag = mahai;
                flpMahaiak.Controls.Add(panel);
            }
        }

        private Color LortuMahaiaKolorea(Mahaiak mahai, Erreserba erreserba)
        {
            if (string.Equals(mahai.Egoera, "okupatuta", StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(255, 241, 224);

            if (erreserba != null)
                return Color.FromArgb(255, 230, 230);

            return Color.White;
        }

        private Control mahaiaSortu(Mahaiak mahai)
        {
            var erreserba = _erreserbak.FirstOrDefault(r => r.MahaiaId == mahai.Id);

            Panel p = new Panel
            {
                Width = mahaiaZabalera,
                Height = mahaiaAltuera,
                BackColor = Color.White,
                Margin = new Padding(16),
                Padding = new Padding(14),
                Tag = mahai
            };

            p.Paint += (s, e) =>
                ControlPaint.DrawBorder(e.Graphics, p.ClientRectangle,
                    Color.FromArgb(224, 224, 224), ButtonBorderStyle.Solid);

            Label lblMahaiZenbakia = new Label
            {
                Text = string.Format("MAHAIA {0}", mahai.Zenbakia),
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(29, 80, 91),
                Dock = DockStyle.Top,
                Height = 30
            };

            Label lblPertsonaMax = new Label
            {
                Text = string.Format("{0} pertsona", mahai.Kapazitatea),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Top
            };

            Label lblEgoera = new Label
            {
                Text = LortuMahaiaEgoeraTestua(mahai, erreserba),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = LortuMahaiaEgoeraKolorea(mahai, erreserba),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.BottomRight
            };

            Label lblData = new Label
            {
                Text = erreserba != null
                    ? string.Format("{0:dd/MM/yyyy} - {1}", erreserba.Data, erreserba.Txanda)
                    : string.Format("{0:dd/MM/yyyy} - {1}", dtimeData.Value.Date, _txandaAukeratua),
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Bottom,
                Height = 20
            };

            p.Controls.Add(lblEgoera);
            p.Controls.Add(lblData);
            p.Controls.Add(lblPertsonaMax);
            p.Controls.Add(lblMahaiZenbakia);

            return p;
        }

        private string LortuMahaiaEgoeraTestua(Mahaiak mahai, Erreserba erreserba)
        {
            if (string.Equals(mahai.Egoera, "okupatuta", StringComparison.OrdinalIgnoreCase) && erreserba != null)
                return "ERRESERBATUTA / OKUPATUTA";

            if (string.Equals(mahai.Egoera, "okupatuta", StringComparison.OrdinalIgnoreCase))
                return "OKUPATUTA";

            if (erreserba != null && erreserba.Egoera != "amaituta")
                return "ERRESERBATUTA";

            return "LIBRE";
        }

        private Color LortuMahaiaEgoeraKolorea(Mahaiak mahai, Erreserba erreserba)
        {
            if (string.Equals(mahai.Egoera, "okupatuta", StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(184, 90, 24);

            if (erreserba != null)
                return Color.FromArgb(160, 40, 40);

            return Color.FromArgb(28, 95, 43);
        }

        private void btnAukeratu_Click(object sender, EventArgs e)
        {
            if (_mahaiHautatuaId == null || _txandaAukeratua == null)
            {
                MessageBox.Show("Aukeratu mahaia eta txanda");
                return;
            }

            var mahaiCtrl = new MahaiakController();
            var mahaia = mahaiCtrl.LortuMahaia(_mahaiHautatuaId.Value, dtimeData.Value.Date, _txandaAukeratua);

            if (mahaia == null)
            {
                MessageBox.Show("Mahaia ezin izan da kargatu");
                return;
            }

            var komandaCtrl = new KomandakController();
            var eskaeraAktiboa = komandaCtrl.LortuEskaeraAktiboaMahaika(
                mahaia.Id,
                dtimeData.Value.Date,
                _txandaAukeratua);

            int komensalak;
            if (_hautatutakoErreserba != null)
            {
                komensalak = _hautatutakoErreserba.PertsonaKopurua;
            }
            else if (eskaeraAktiboa != null && eskaeraAktiboa.Komensalak > 0)
            {
                komensalak = eskaeraAktiboa.Komensalak;
            }
            else
            {
                var komensalakAukeratuta = EskatuKomensalak(mahaia);
                if (!komensalakAukeratuta.HasValue)
                    return;

                komensalak = komensalakAukeratuta.Value;
            }

            var f = new EskaerakForm(
                _erabiltzailea,
                mahaia.Id,
                komensalak,
                _hautatutakoErreserba?.Id,
                dtimeData.Value.Date,
                _txandaAukeratua);
            f.Show();
            this.Close();
        }

        private int? EskatuKomensalak(Mahaiak mahaia)
        {
            if (mahaia.Kapazitatea <= 0)
            {
                MessageBox.Show("Mahaia honek ez dauka kapazitaterik esleituta.");
                return null;
            }

            using (Form elkarrizketa = new Form())
            {
                elkarrizketa.Text = string.Format("Mahaia {0} - Komensalak", mahaia.Zenbakia);
                elkarrizketa.StartPosition = FormStartPosition.CenterParent;
                elkarrizketa.FormBorderStyle = FormBorderStyle.FixedDialog;
                elkarrizketa.MinimizeBox = false;
                elkarrizketa.MaximizeBox = false;
                elkarrizketa.ClientSize = new Size(350, 150);

                Label lbl = new Label
                {
                    AutoSize = false,
                    Left = 16,
                    Top = 16,
                    Width = 318,
                    Height = 40,
                    Text = string.Format(
                        "Mahaia ez dago erreserbatuta. Zenbat komensal dira? (1 - {0})",
                        mahaia.Kapazitatea)
                };

                NumericUpDown nudKomensalak = new NumericUpDown
                {
                    Left = 16,
                    Top = 64,
                    Width = 120,
                    Minimum = 1,
                    Maximum = mahaia.Kapazitatea,
                    Value = 1
                };

                Button btnOnartu = new Button
                {
                    Text = "Onartu",
                    DialogResult = DialogResult.OK,
                    Width = 90,
                    Left = 156,
                    Top = 104
                };

                Button btnUtzi = new Button
                {
                    Text = "Utzi",
                    DialogResult = DialogResult.Cancel,
                    Width = 90,
                    Left = 254,
                    Top = 104
                };

                elkarrizketa.Controls.Add(lbl);
                elkarrizketa.Controls.Add(nudKomensalak);
                elkarrizketa.Controls.Add(btnOnartu);
                elkarrizketa.Controls.Add(btnUtzi);
                elkarrizketa.AcceptButton = btnOnartu;
                elkarrizketa.CancelButton = btnUtzi;

                var emaitza = elkarrizketa.ShowDialog(this);
                if (emaitza != DialogResult.OK)
                    return null;

                return Convert.ToInt32(nudKomensalak.Value);
            }
        }

        private void lblDataEuskera_Click(object sender, EventArgs e)
        {
            using (Form f = new Form())
            {
                f.StartPosition = FormStartPosition.Manual;
                f.Size = new Size(260, 220);
                f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                f.ShowInTaskbar = false;

                Point pos = lblDataEuskera.PointToScreen(Point.Empty);

                f.Location = new Point(
                    pos.X,
                    pos.Y + lblDataEuskera.Height + 4
                );

                DateTimePicker p = new DateTimePicker
                {
                    Dock = DockStyle.Fill,
                    Value = dtimeData.Value,
                    Font = new Font("Segoe UI", 10F),
                    Format = DateTimePickerFormat.Long
                };

                p.ValueChanged += (s, ev) =>
                {
                    dtimeData.Value = p.Value;
                    f.Close();
                };

                f.Controls.Add(p);
                f.ShowDialog(this);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!hasieraEginda) return;

            KalkulatuMahaiTamainak();
            foreach (Control c in flpMahaiak.Controls)
            {
                c.Width = mahaiaZabalera;
                c.Height = mahaiaAltuera;
            }
        }
    }
}
