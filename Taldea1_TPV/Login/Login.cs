using System;
using System.Drawing;
using System.Windows.Forms;
using Taldea1TPV.Menua;

namespace Taldea1TPV
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void btnSartu_Klik(object sender, EventArgs e)
        {
            string erabiltzailea = txbErab.Text.Trim();
            string pasahitza = txbPasa.Text.Trim();

            var login = new ErabiltzaileController();
            var erabiltzaileLogeatua = login.BalidatuLogin(erabiltzailea, pasahitza);

            if (erabiltzaileLogeatua != null)
            {
                Saioa.UnekoErabiltzailea = erabiltzaileLogeatua;

                MessageBox.Show(
                    "Ondo logeatu zara! Ongi etorri " + erabiltzailea,
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                var menua = new MenuaForm(erabiltzaileLogeatua);
                menua.Show();
                this.Hide();
            }
            else
            {
                Saioa.UnekoErabiltzailea = null;

                lblMezua.Text = string.IsNullOrWhiteSpace(login.AzkenErrorea)
                    ? "Erabiltzaile edo pasahitza okerra."
                    : login.AzkenErrorea;
                lblMezua.ForeColor = Color.Red;
            }
        }
    }
}
