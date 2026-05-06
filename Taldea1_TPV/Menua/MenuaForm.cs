using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Taldea1TPV.Eguraldia;
using Taldea1TPV.Eskariak;
using Taldea1TPV.Eskariak.Erreserbak;

namespace Taldea1TPV.Menua
{
    public partial class MenuaForm : Form
    {
        private readonly Erabiltzaileak _erabiltzailea;
        private Button btnEguraldia;

        public MenuaForm(Erabiltzaileak erabiltzailea)
        {
            InitializeComponent();
            _erabiltzailea = erabiltzailea;
            Saioa.UnekoErabiltzailea = erabiltzailea;
            TxatBotoiaLaguntzailea.Erantsi(this);
            ErantsiEguraldiBotoia();
        }

        private void btnEskaria_Click(object sender, EventArgs e)
        {
            new MahaiakForm(_erabiltzailea).Show();
        }

        private void btnErreserba_Click(object sender, EventArgs e)
        {
            new ErreserbakForm().Show();
        }

        private void ErantsiEguraldiBotoia()
        {
            if (Controls.Find("btnEguraldia", true).Any())
                return;

            btnEguraldia = new Button
            {
                Name = "btnEguraldia",
                Text = "Eguraldia",
                Size = new Size(120, 34),
                BackColor = Color.FromArgb(66, 126, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                UseVisualStyleBackColor = false
            };

            btnEguraldia.FlatAppearance.BorderSize = 0;
            btnEguraldia.Click += (s, e) => IrekiEguraldia();

            Controls.Add(btnEguraldia);
            KokatuEguraldiBotoia();
            btnEguraldia.BringToFront();

            Resize += (s, e) =>
            {
                if (!btnEguraldia.IsDisposed)
                {
                    KokatuEguraldiBotoia();
                    btnEguraldia.BringToFront();
                }
            };
        }

        private void KokatuEguraldiBotoia()
        {
            const int eskuinMarjina = 18;
            const int goiMarjina = 12;
            const int tartea = 10;
            const int txatZabalera = 120;

            btnEguraldia.Location = new Point(
                Math.Max(goiMarjina, ClientSize.Width - txatZabalera - btnEguraldia.Width - eskuinMarjina - tartea),
                goiMarjina);
        }

        private void IrekiEguraldia()
        {
            var eguraldiaIrekita = Application.OpenForms.OfType<EguraldiaForm>().FirstOrDefault();
            if (eguraldiaIrekita != null && !eguraldiaIrekita.IsDisposed)
            {
                eguraldiaIrekita.WindowState = FormWindowState.Normal;
                eguraldiaIrekita.BringToFront();
                eguraldiaIrekita.Focus();
                return;
            }

            new EguraldiaForm().Show(this);
        }
    }
}
