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
        string erabiltzailea;
        private int mahaiaZabalera;
        private int mahaiaAltuera;
        private bool hasieraEginda = false;

        private int? _mahaiHautatuaId = null;


        // ===== TXANDA =====
        private string _txandaAukeratua = null; // "Bazkaria" | "Afaria"

        public MahaiakForm(string erabiltzailea)
        {
            InitializeComponent();
            this.erabiltzailea = erabiltzailea;
        }

        private void MahaiakForm_Load(object sender, EventArgs e)
        {
            var culture = new CultureInfo("eu-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            EguneratuDataEuskera();

            // ===== TXANDA CLICK =====
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



        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            KalkulatuMahaiTamainak();
            kargatuMahaiak();
            hasieraEginda = true;
        }

        private static readonly CultureInfo euskalCulture = new CultureInfo("eu-ES");

        // ================= DATA =================
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

        // ================= TXANDA =================
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

        // ================= TAMAINA =================
        private void KalkulatuMahaiTamainak()
        {
            int w = flpMahaiak.ClientSize.Width;
            int cols = Math.Max(4, w / 300);
            mahaiaZabalera = Math.Min(260, Math.Max(190, (w / cols) - 32));
            mahaiaAltuera = (int)(mahaiaZabalera * 0.7);
        }

        // ================= MAHAIAK =================
        private void kargatuMahaiak()
        {
            flpMahaiak.Controls.Clear();
            _mahaiHautatuaId = null;

            if (_txandaAukeratua == null)
                return;

            var mahaiCtrl = new MahaiakController();
            var erreserbaCtrl = new ErreserbakController();
            var erreserbaMahaiCtrl = new ErreserbaMahaiController();

            var mahaiak = mahaiCtrl.LortuMahaiak();

            var erreserbak = erreserbaCtrl.LortuErreserbak()
                .Where(e =>
                    e.Data.Date == dtimeData.Value.Date &&
                    e.Txanda == _txandaAukeratua)
                .ToList();

            var mahaiOkupatuak = erreserbak
                .SelectMany(e => erreserbaMahaiCtrl.LortuMahaiakErreserbarentzat(e.Id))
                .Distinct()
                .ToList();

            foreach (var mahai in mahaiak)
            {
                var panel = mahaiaSortu(mahai);

                if (mahaiOkupatuak.Contains(mahai.Id))
                {
                    panel.BackColor = Color.FromArgb(255, 230, 230); // OKUPATUTA
                    panel.Cursor = Cursors.Hand;

                   
                    var lblEgoera = panel.Tag as Label;
                    if (lblEgoera != null)
                    {
                        lblEgoera.Text = "OKUPATUTA";
                        lblEgoera.ForeColor = Color.FromArgb(160, 40, 40);
                    }

                    panel.Click += (s, e) =>
                    {
                        foreach (Control c in flpMahaiak.Controls)
                            c.BackColor = Color.White;

                        panel.BackColor = Color.FromArgb(220, 240, 225);
                        _mahaiHautatuaId = mahai.Id;
                    };
                }

                else
                {
                    panel.BackColor = Color.FromArgb(235, 235, 235); // EZ DISPONIBLE
                }

                flpMahaiak.Controls.Add(panel);
            }
        }


        // ================= PANEL MAHAIA =================
        private Control mahaiaSortu(Mahaiak mahai)
        {
            Panel p = new Panel
            {
                Width = mahaiaZabalera,
                Height = mahaiaAltuera,
                BackColor = Color.White,
                Margin = new Padding(16),
                Padding = new Padding(14),
                Tag = mahai.Id
            };

            p.Paint += (s, e) =>
                ControlPaint.DrawBorder(e.Graphics, p.ClientRectangle,
                    Color.FromArgb(224, 224, 224), ButtonBorderStyle.Solid);

            Label lblMahaiZenbakia = new Label
            {
                Text = $"MAHAIA {mahai.MahaiZenbakia}",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(29, 80, 91),
                Dock = DockStyle.Top,
                Height = 30
            };

            Label lblPertsonaMax = new Label
            {
                Text = $"{mahai.PertsonaMax} pertsona",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Top
            };

            Label lblEgoera = new Label
            {
                Text = "LIBRE",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 95, 43),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.BottomRight
            };

            
            p.Tag = lblEgoera;

            p.Controls.Add(lblEgoera);
            p.Controls.Add(lblPertsonaMax);
            p.Controls.Add(lblMahaiZenbakia);

            return p;
        }


        // ================= AUKERATU MAHAIA =================
        private void btnAukeratu_Click(object sender, EventArgs e)
        {
            if (_mahaiHautatuaId == null || _txandaAukeratua == null)
            {
                MessageBox.Show("Aukeratu mahaia eta txanda");
                return;
            }

            var erreserbaCtrl = new ErreserbakController();
            var erreserbaMahaiCtrl = new ErreserbaMahaiController();
            

            var erreserbak = erreserbaCtrl.LortuErreserbak()
                .Where(o =>
                    o.Data.Date == dtimeData.Value.Date &&
                    o.Txanda == _txandaAukeratua)
                .ToList();

            var erreserba = erreserbak.FirstOrDefault(o =>
                erreserbaMahaiCtrl
                    .LortuMahaiakErreserbarentzat(o.Id)
                    .Contains(_mahaiHautatuaId.Value));

            if (erreserba == null)
            {
                MessageBox.Show("Ez dago erreserbarik mahaia honentzat");
                return;
            }

            // ===== FAkTURA =====

            var fakturaCtrl = new FakturakController();
            var faktura = fakturaCtrl.SortuEdoLortuFakturaErreserbatik(erreserba.Id);

            if (faktura == null)
            {
                MessageBox.Show("Errorea faktura sortzean");
                return;
            }

            var f = new EskaerakForm(erabiltzailea, faktura.Id);
            f.Show();
            this.Close();


        }


        // ================= DATE PICKER POPUP =================
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
