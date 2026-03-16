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

        public TxatForm(string erabiltzaile)
        {
            InitializeComponent();
            erabiltzaileIzena = erabiltzaile;
            lblErabiltzaile.Text = erabiltzaile;
        }

        private void TxatForm_Load(object sender, EventArgs e)
        {
            zerbitzariraKonexioa();
        }

        private void zerbitzariraKonexioa()
        {
            try
            {
                erabiltzailea = new TcpClient("192.168.2.101", 5555);
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
            if (txtMezuak.InvokeRequired)
            {
                txtMezuak.Invoke(new Action(() => txtMezuak.AppendText(msg + Environment.NewLine)));
            }
            else
            {
                txtMezuak.AppendText(msg + Environment.NewLine);
            }
        }

        private void btnBidali_Klik(object sender, EventArgs e)
        {
            if (idazlea == null) return;

            string mezua = txtInput.Text.Trim();
            if (mezua == "") return;

            idazlea.WriteLine(mezua);
            txtInput.Text = "";
        }

        private void TxatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { erabiltzailea?.Close(); } catch { }
        }
    }
}
