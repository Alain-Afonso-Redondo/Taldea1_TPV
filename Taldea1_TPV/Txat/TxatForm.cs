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
        private bool _lehenMezuaIragazita = false;

        public TxatForm(string erabiltzaile)
        {
            InitializeComponent();
            erabiltzaileIzena = string.IsNullOrWhiteSpace(erabiltzaile) ? "Erabiltzailea" : erabiltzaile;
            lblErabiltzaile.Text = "Kaixo, " + erabiltzaileIzena;

            txtSarrera.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter && !e.Shift)
                {
                    btnBidali_Klik(null, null);
                    e.SuppressKeyPress = true;
                }
            };

            flpMezuak.SizeChanged += (s, e) => {
                foreach (Control c in flpMezuak.Controls)
                {
                    if (c is Panel p) p.Width = flpMezuak.ClientSize.Width - 25;
                }
            };

            Fitxategi_Botoia.Click += Fitxategi_Botoia_Click;
        }

        private void TxatForm_Load(object sender, EventArgs e)
        {
            zerbitzariraKonexioa();
        }

        private void zerbitzariraKonexioa()
        {
            try
            {
                erabiltzailea = new TcpClient("192.168.10.5", 5555);
                var ns = erabiltzailea.GetStream();

                irakurlea = new StreamReader(ns);
                idazlea = new StreamWriter(ns) { AutoFlush = true };

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
            if (flpMezuak.InvokeRequired)
                flpMezuak.Invoke(new Action(() => GehituMezuBurbula(msg)));
            else
                GehituMezuBurbula(msg);
        }

        private void GehituMezuBurbula(string msg)
        {
            string edukia = msg;
            string bidaltzailea = "";

            string[] separadoreak = { ":", ">", "-", "]", "»" };
            int index = -1;
            string sepUsed = "";

            if (msg.StartsWith("[AES]|"))
            {
                index = msg.IndexOf(':');
                sepUsed = ":";
            }
            else
            {
                foreach (var sep in separadoreak)
                {
                    int pos = msg.IndexOf(sep);
                    if (pos > 0 && (index == -1 || pos < index))
                    {
                        index = pos;
                        sepUsed = sep;
                    }
                }
            }

            if (index != -1)
            {
                bidaltzailea = msg.Substring(0, index).Trim();
                edukia = msg.Substring(index + sepUsed.Length).Trim();
            }

            bidaltzailea = TxatCryptoLaguntzailea.DesenkriptatuBeharBada(bidaltzailea);
            edukia = TxatCryptoLaguntzailea.DesenkriptatuBeharBada(edukia);

            bool esArchivo = false;
            string fileNameTemp = "";
            byte[] fileBytesTemp = null;

            if (edukia.StartsWith("[FILE]|"))
            {
                string[] parts = edukia.Split('|');

                if (parts.Length == 3)
                {
                    fileNameTemp = parts[1];
                    fileBytesTemp = Convert.FromBase64String(parts[2]);
                    edukia = "📎 " + fileNameTemp;
                    esArchivo = true;
                }
            }

            bool nireaDa = string.Equals(bidaltzailea, erabiltzaileIzena, StringComparison.OrdinalIgnoreCase);

            Panel pnlLerroa = new Panel();
            pnlLerroa.Width = flpMezuak.ClientSize.Width - 20;
            pnlLerroa.AutoSize = true;
            pnlLerroa.Padding = new Padding(5);
            pnlLerroa.Margin = new Padding(0, 3, 0, 3);

            FlowLayoutPanel pnlBurbula = new FlowLayoutPanel();
            pnlBurbula.FlowDirection = FlowDirection.TopDown;
            pnlBurbula.AutoSize = true;
            pnlBurbula.WrapContents = true;
            pnlBurbula.MaximumSize = new System.Drawing.Size((int)(pnlLerroa.Width * 0.65), 0);

            Label lblIzena = new Label();
            lblIzena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblIzena.AutoSize = true;

            Label lblMezua = new Label();
            lblMezua.Text = edukia;
            lblMezua.AutoSize = true;
            lblMezua.MaximumSize = new System.Drawing.Size(pnlBurbula.MaximumSize.Width - 20, 0);

            if (nireaDa)
            {
                pnlBurbula.BackColor = System.Drawing.Color.FromArgb(0, 132, 255);
                pnlBurbula.BorderStyle = BorderStyle.FixedSingle;
                pnlBurbula.Padding = new Padding(12, 8, 12, 8);
                pnlBurbula.Margin = new Padding(50, 5, 5, 12);

                lblMezua.ForeColor = System.Drawing.Color.White;
                lblIzena.Text = "Zu";
                lblIzena.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                pnlBurbula.BackColor = System.Drawing.Color.White;
                pnlBurbula.BorderStyle = BorderStyle.FixedSingle;
                pnlBurbula.Padding = new Padding(12, 8, 12, 8);
                pnlBurbula.Margin = new Padding(5, 5, 50, 12);

                lblMezua.ForeColor = System.Drawing.Color.Black;
                lblIzena.ForeColor = System.Drawing.Color.Gray;

                lblIzena.Text = bidaltzailea;
                lblIzena.Visible = !string.IsNullOrEmpty(bidaltzailea);
            }

            pnlBurbula.Controls.Add(lblIzena);
            pnlBurbula.Controls.Add(lblMezua);

            if (esArchivo && fileBytesTemp != null)
            {
                Button btnGorde = new Button();
                btnGorde.Text = "Gorde";
                btnGorde.AutoSize = true;

                btnGorde.Click += (s, e) =>
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = fileNameTemp;

                    if (sfd.ShowDialog() == DialogResult.OK)
                        File.WriteAllBytes(sfd.FileName, fileBytesTemp);
                };

                pnlBurbula.Controls.Add(btnGorde);
            }

            pnlLerroa.Controls.Add(pnlBurbula);

            pnlBurbula.Anchor = nireaDa
                ? AnchorStyles.Top | AnchorStyles.Right
                : AnchorStyles.Top | AnchorStyles.Left;

            if (nireaDa)
            {
                pnlBurbula.Left = pnlLerroa.Width - pnlBurbula.Width - 10;
            }
            else
            {
                pnlBurbula.Left = 10;
            }

            flpMezuak.Controls.Add(pnlLerroa);
            flpMezuak.ScrollControlIntoView(pnlLerroa);
        }


        private void btnBidali_Klik(object sender, EventArgs e)
        {
            if (idazlea == null) return;

            string mezua = txtSarrera.Text.Trim();
            if (mezua == "") return;

            string mezuaOsoa = TxatCryptoLaguntzailea.Enkriptatu(erabiltzaileIzena) + ": " + TxatCryptoLaguntzailea.Enkriptatu(mezua);

            idazlea.WriteLine(mezuaOsoa);

            txtSarrera.Text = "";
        }

        private void Fitxategi_Botoia_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(ofd.FileName);

                if (fi.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("❌ Errorea, aukeratutako elementua handiegia da.");
                    return;
                }

                byte[] bytes = File.ReadAllBytes(ofd.FileName);
                string base64 = Convert.ToBase64String(bytes);

                string mensaje = $"[FILE]|{fi.Name}|{base64}";
                string mezuaOsoa = TxatCryptoLaguntzailea.Enkriptatu(erabiltzaileIzena) + ": " + TxatCryptoLaguntzailea.Enkriptatu(mensaje);

                idazlea.WriteLine(mezuaOsoa);

            }

        }



        private void TxatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { erabiltzailea?.Close(); } catch { }
        }
    }
}
