using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Taldea1TPV
{
    public partial class TxatForm : Form
    {
        private string erabiltzaileIzena;
        private TcpClient erabiltzailea;
        private StreamReader irakurlea;
        private StreamWriter idazlea;
        private Thread entzunHari;
        private string azkenBidalitakoMezua = "";

        public TxatForm(string erabiltzaile)
        {
            InitializeComponent();
            erabiltzaileIzena = erabiltzaile;
            lblErabiltzaile.Text = "Kaixo, " + erabiltzaile;

            txtSarrera.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter && !e.Shift) {
                    btnBidali_Klik(null, null);
                    e.SuppressKeyPress = true;
                }
            };

            flpMezuak.SizeChanged += (s, e) => {
                foreach (Control c in flpMezuak.Controls) {
                    if (c is Panel p) p.Width = flpMezuak.ClientSize.Width - 25;
                }
            };
        }

        private void TxatForm_Load(object sender, EventArgs e)
        {
            zerbitzariraKonexioa();
        }

        private void zerbitzariraKonexioa()
        {
            try
            {
                erabiltzailea = new TcpClient("localhost", 5555);
                var ns = erabiltzailea.GetStream();

                irakurlea = new StreamReader(ns);
                idazlea = new StreamWriter(ns) { AutoFlush = true };

                idazlea.WriteLine(erabiltzaileIzena);

                entzunHari = new Thread(entzunBuklea);
                entzunHari.IsBackground = true;
                entzunHari.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ezin da txatera konektatu.\n\n" + ex.Message,
                    "Konexio errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void entzunBuklea()
        {
            try
            {
                string lerroa;
                while ((lerroa = irakurlea.ReadLine()) != null)
                {
                    idatziMezua(lerroa);
                }
            }
            catch { }
        }

        private void idatziMezua(string msg)
        {
            string msgNormalizado = msg.Trim();
            string azkenNormalizado = azkenBidalitakoMezua.Trim();

            // El servidor devuelve el nombre al conectar; no lo mostramos como burbuja.
            if (msgNormalizado.Equals(erabiltzaileIzena, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            
            if (!string.IsNullOrEmpty(azkenNormalizado) && msgNormalizado == azkenNormalizado)
            {
                azkenBidalitakoMezua = "";
                return;
            }

            if (flpMezuak.InvokeRequired)
            {
                flpMezuak.Invoke(new Action(() => GehituMezuBurbula(msg)));
            }
            else
            {
                GehituMezuBurbula(msg);
            }
        }

        private void GehituMezuBurbula(string msg)
        {
            bool nireaDa = msg.StartsWith(erabiltzaileIzena + ":");
            string edukia = msg;
            string bidaltzailea = "";

            if (msg.Contains(":"))
            {
                int index = msg.IndexOf(":");
                bidaltzailea = msg.Substring(0, index).Trim();
                edukia = msg.Substring(index + 1).Trim();
            }

            Panel pnlLerroa = new Panel();
            pnlLerroa.Width = flpMezuak.ClientSize.Width - 20;
            pnlLerroa.Height = 80;
            pnlLerroa.Padding = new Padding(5);
            pnlLerroa.Margin = new Padding(0, 2, 0, 2);

            FlowLayoutPanel pnlBurbula = new FlowLayoutPanel();
            pnlBurbula.FlowDirection = FlowDirection.TopDown;
            pnlBurbula.AutoSize = true;
            pnlBurbula.Padding = new Padding(12, 8, 12, 8);
            pnlBurbula.MaximumSize = new System.Drawing.Size((int)(pnlLerroa.Width * 0.65), 0);

            Label lblIzena = new Label();
            lblIzena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblIzena.AutoSize = true;
            lblIzena.Margin = new Padding(0, 0, 0, 3);

            Label lblMezua = new Label();
            lblMezua.Text = edukia;
            lblMezua.AutoSize = true;
            lblMezua.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblMezua.MaximumSize = new System.Drawing.Size(pnlBurbula.MaximumSize.Width - 24, 0);
            lblMezua.Margin = new Padding(0);

            if (nireaDa)
            {
                pnlBurbula.BackColor = System.Drawing.Color.FromArgb(0, 132, 255);
                lblMezua.ForeColor = System.Drawing.Color.White;
                lblIzena.Text = "Zu";
                lblIzena.ForeColor = System.Drawing.Color.White;
                pnlBurbula.Anchor = AnchorStyles.Right;
                pnlBurbula.Dock = DockStyle.Right;
            }
            else
            {
                pnlBurbula.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
                lblMezua.ForeColor = System.Drawing.Color.Black;
                lblIzena.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
                
                if (!string.IsNullOrEmpty(bidaltzailea))
                {
                    lblIzena.Text = bidaltzailea;
                }
                else
                {
                    lblIzena.Text = "Erabiltzailea";
                }
                
                pnlBurbula.Anchor = AnchorStyles.Left;
                pnlBurbula.Dock = DockStyle.Left;
            }

            pnlBurbula.Controls.Add(lblIzena);
            pnlBurbula.Controls.Add(lblMezua);
            pnlLerroa.Controls.Add(pnlBurbula);

            flpMezuak.Controls.Add(pnlLerroa);

            pnlLerroa.PerformLayout();
            flpMezuak.ScrollControlIntoView(pnlLerroa);
        }

        private void btnBidali_Klik(object sender, EventArgs e)
        {
            if (idazlea == null) return;

            string mezua = txtSarrera.Text.Trim();
            if (mezua == "") return;

            string mezuaOsoa = erabiltzaileIzena + ": " + mezua;
            idazlea.WriteLine(mezuaOsoa);
            azkenBidalitakoMezua = mezuaOsoa;
            GehituMezuBurbula(mezuaOsoa);

            txtSarrera.Text = "";
        }

        private void TxatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { erabiltzailea?.Close(); } catch { }
        }
    }
}
