using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Taldea1TPV
{
    internal static class TxatBotoiaLaguntzailea
    {
        private const string TxatBotoiIzena = "btnTxatGlobala";

        public static void Erantsi(Form form)
        {
            if (form == null || form.IsDisposed || form is TxatForm)
                return;

            if (form.Controls.Find(TxatBotoiIzena, true).Length > 0)
                return;

            var btnTxat = new Button
            {
                Name = TxatBotoiIzena,
                Text = "Txata",
                Size = new Size(120, 34),
                BackColor = Color.FromArgb(31, 107, 58),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                UseVisualStyleBackColor = false
            };

            btnTxat.FlatAppearance.BorderSize = 0;
            btnTxat.Click += (s, e) => IrekiTxata(form);

            form.Controls.Add(btnTxat);
            KokatuBotoia(form, btnTxat);
            btnTxat.BringToFront();

            form.Resize += (s, e) =>
            {
                if (!btnTxat.IsDisposed)
                {
                    KokatuBotoia(form, btnTxat);
                    btnTxat.BringToFront();
                }
            };
        }

        public static void IrekiTxata(IWin32Window owner = null)
        {
            var erabiltzailea = Saioa.UnekoErabiltzailea;
            if (erabiltzailea == null)
            {
                ErakutsiMezua(
                    owner,
                    "Lehenengo login egin behar da txata irekitzeko.",
                    "Txat desgaituta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var erabiltzaileController = new ErabiltzaileController();
            var txatBaimena = erabiltzaileController.LortuTxatBaimena(erabiltzailea.Id);

            if (!txatBaimena.HasValue)
            {
                ErakutsiMezua(
                    owner,
                    string.IsNullOrWhiteSpace(erabiltzaileController.AzkenErrorea)
                        ? "Ezin izan da txat baimena egiaztatu."
                        : erabiltzaileController.AzkenErrorea,
                    "Errorea",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            erabiltzailea.Txat = txatBaimena.Value;

            if (!erabiltzailea.Txat)
            {
                ErakutsiMezua(
                    owner,
                    "Ez duzu baimena txata erabiltzeko.",
                    "Txat desgaituta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var txatIrekita = Application.OpenForms.OfType<TxatForm>().FirstOrDefault();
            if (txatIrekita != null && !txatIrekita.IsDisposed)
            {
                txatIrekita.WindowState = FormWindowState.Normal;
                txatIrekita.BringToFront();
                txatIrekita.Focus();
                return;
            }

            new TxatForm(erabiltzailea.Erabiltzailea).Show();
        }

        private static void KokatuBotoia(Form form, Control btnTxat)
        {
            const int eskuinMarjina = 18;
            const int goiMarjina = 12;

            btnTxat.Location = new Point(
                Math.Max(goiMarjina, form.ClientSize.Width - btnTxat.Width - eskuinMarjina),
                goiMarjina);
        }

        private static void ErakutsiMezua(
            IWin32Window owner,
            string testua,
            string titulua,
            MessageBoxButtons botoiak,
            MessageBoxIcon ikonoa)
        {
            if (owner != null)
                MessageBox.Show(owner, testua, titulua, botoiak, ikonoa);
            else
                MessageBox.Show(testua, titulua, botoiak, ikonoa);
        }
    }
}
