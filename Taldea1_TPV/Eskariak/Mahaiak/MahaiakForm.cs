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
            var mahaiak = mahaiCtrl.LortuMahaiak();
            _erreserbak = erreserbaCtrl
                .LortuErreserbakData(dtimeData.Value.Date)
                .Where(r => string.Equals(r.Txanda, _txandaAukeratua, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var mahai in mahaiak)
            {
                var panel = mahaiaSortu(mahai);
                var erreserba = _erreserbak.FirstOrDefault(r => r.MahaiaId == mahai.Id);
                panel.BackColor = erreserba != null
                    ? Color.FromArgb(255, 230, 230)
                    : Color.White;
                panel.Cursor = Cursors.Hand;

                panel.Click += (s, e) =>
                {
                    foreach (Control c in flpMahaiak.Controls)
                    {
                        var kontrolMahaia = c.Tag as Mahaiak;
                        var kontrolErreserba = kontrolMahaia == null
                            ? null
                            : _erreserbak.FirstOrDefault(r => r.MahaiaId == kontrolMahaia.Id);
                        c.BackColor = kontrolErreserba != null
                            ? Color.FromArgb(255, 230, 230)
                            : Color.White;
                    }

                    panel.BackColor = Color.FromArgb(220, 240, 225);
                    _mahaiHautatuaId = mahai.Id;
                    _hautatutakoErreserba = erreserba;
                };

                panel.Tag = mahai;
                flpMahaiak.Controls.Add(panel);
            }
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
                Text = erreserba != null
                    ? "ERRESERBATUTA"
                    : "LIBRE",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = erreserba != null
                    ? Color.FromArgb(160, 40, 40)
                    : Color.FromArgb(28, 95, 43),
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

        private void btnAukeratu_Click(object sender, EventArgs e)
        {
            if (_mahaiHautatuaId == null || _txandaAukeratua == null)
            {
                MessageBox.Show("Aukeratu mahaia eta txanda");
                return;
            }

            var mahaiCtrl = new MahaiakController();
            var mahaia = mahaiCtrl.LortuMahaia(_mahaiHautatuaId.Value);

            if (mahaia == null)
            {
                MessageBox.Show("Mahaia ezin izan da kargatu");
                return;
            }

            var komensalak = _hautatutakoErreserba != null
                ? _hautatutakoErreserba.PertsonaKopurua
                : mahaia.Kapazitatea;

            var f = new EskaerakForm(_erabiltzailea, mahaia.Id, komensalak, _hautatutakoErreserba != null ? (int?)_hautatutakoErreserba.Id : null);
            f.Show();
            this.Close();
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
