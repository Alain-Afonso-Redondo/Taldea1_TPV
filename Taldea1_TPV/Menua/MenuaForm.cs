using System;
using System.Windows.Forms;
using Taldea1TPV.Eskariak;
using Taldea1TPV.Eskariak.Erreserbak;

namespace Taldea1TPV.Menua
{
    public partial class MenuaForm : Form
    {
        private readonly Erabiltzaileak _erabiltzailea;

        public MenuaForm(Erabiltzaileak erabiltzailea)
        {
            InitializeComponent();
            _erabiltzailea = erabiltzailea;
            Saioa.UnekoErabiltzailea = erabiltzailea;
            TxatBotoiaLaguntzailea.Erantsi(this);
        }

        private void btnEskaria_Click(object sender, EventArgs e)
        {
            new MahaiakForm(_erabiltzailea).Show();
        }

        private void btnErreserba_Click(object sender, EventArgs e)
        {
            new ErreserbakForm().Show();
        }
    }
}
