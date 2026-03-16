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
            bool loginBalidatu = login.BalidatuLogin(erabiltzailea, pasahitza);

            if (loginBalidatu)
            {
                MessageBox.Show(
                    "Ondo logeatu zara! Ongi etorri " + erabiltzailea,
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                var menua = new MenuaForm(erabiltzailea);
                menua.Show();
                this.Hide();
            }
            else
            {
                lblMezua.Text = "Erabiltzaile edo pasahitza okerra.";
                lblMezua.ForeColor = Color.Red;
            }
        }

        
    }
}
