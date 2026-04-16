using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Taldea1TPV.Eskariak
{
    public partial class ErreserbatuForm : Form
    {
        private string txandaAukeratua = null;

        private bool _editatzen = false;
        private Erreserba _erreserbaEditatzen = null;
        private int? _mahaiEditatuaId = null;

       
        public ErreserbatuForm()
        {
            InitializeComponent();
            _editatzen = false;
            TxatBotoiaLaguntzailea.Erantsi(this);
        }

        public ErreserbatuForm(Erreserba erreserba)
        {
            InitializeComponent();
            _editatzen = true;
            _erreserbaEditatzen = erreserba;
            TxatBotoiaLaguntzailea.Erantsi(this);
        }

       
        private void ErreserbatuForm_Load(object sender, EventArgs e)
        {
            dtpData.MinDate = DateTime.Today;
            dtpData.Value = DateTime.Today;

            cboxBazkaria.Click += (s, ev) => AukeratuTxanda("Bazkaria");
            cboxAfaria.Click += (s, ev) => AukeratuTxanda("Afaria");

            dtpData.ValueChanged += (s, ev) => KargatuMahaiLibre();
            txtPertsonak.TextChanged += (s, ev) => KargatuMahaiLibre();

            if (_editatzen)
                KargatuDatuakEdizioan();
        }

        
        private void KargatuDatuakEdizioan()
        {
            dtpData.MinDate = new DateTime(1753, 1, 1);
            dtpData.Value = _erreserbaEditatzen.Data;

            txtIzena.Text = _erreserbaEditatzen.Izena;
            txtTelefonoa.Text = _erreserbaEditatzen.Telefonoa;
            txtPertsonak.Text = _erreserbaEditatzen.PertsonaKopurua.ToString();

            AukeratuTxanda(_erreserbaEditatzen.Txanda);

            var erreserbaMahaiCtrl = new ErreserbaMahaiController();
            var mahaiIds = erreserbaMahaiCtrl
                .LortuMahaiakErreserbarentzat(_erreserbaEditatzen.Id);

            if (mahaiIds.Any())
                _mahaiEditatuaId = mahaiIds.First();
        }

        
        private bool DataEdoTxandaAldaketa()
        {
            if (!_editatzen || _erreserbaEditatzen == null)
                return false;

            return
                dtpData.Value.Date != _erreserbaEditatzen.Data.Date ||
                txandaAukeratua != _erreserbaEditatzen.Txanda;
        }

        // ================= TXANDA =================
        private void AukeratuTxanda(string txanda)
        {
            txandaAukeratua = txanda;

            cboxBazkaria.BackColor = txanda == "Bazkaria"
                ? Color.FromArgb(31, 107, 58)
                : Color.LightGray;

            cboxAfaria.BackColor = txanda == "Afaria"
                ? Color.FromArgb(31, 107, 58)
                : Color.LightGray;

            KargatuMahaiLibre();
        }

        // ================= MAHAIAK LIBRE =================
        private void KargatuMahaiLibre()
        {
            cmbMahaiak.DataSource = null;

            if (txandaAukeratua == null)
                return;

            if (!int.TryParse(txtPertsonak.Text, out int pertsonak))
                return;

            var mahaiCtrl = new MahaiakController();
            var erreserbaCtrl = new ErreserbakController();
            var erreserbaMahaiCtrl = new ErreserbaMahaiController();

            var mahaiGuztiak = mahaiCtrl.LortuMahaiak();

            var erreserbakEguneko = erreserbaCtrl
                .LortuErreserbakData(dtpData.Value.Date)
                .Where(e =>
                    e.Txanda == txandaAukeratua &&
                    (!_editatzen || e.Id != _erreserbaEditatzen.Id))
                .ToList();

            var mahaiOkupatuak = erreserbakEguneko
                .SelectMany(e => erreserbaMahaiCtrl.LortuMahaiakErreserbarentzat(e.Id))
                .Distinct()
                .ToList();

            bool mantenduMahaiaEditatzen =
                _editatzen &&
                _mahaiEditatuaId != null &&
                !DataEdoTxandaAldaketa();

            var libre = mahaiGuztiak
                .Where(m =>
                    (!mahaiOkupatuak.Contains(m.Id) ||
                     (mantenduMahaiaEditatzen && m.Id == _mahaiEditatuaId)) &&
                    m.Kapazitatea >= pertsonak)
                .ToList();

            cmbMahaiak.DataSource = libre;
            cmbMahaiak.DisplayMember = "Zenbakia";
            cmbMahaiak.ValueMember = "Id";

            if (mantenduMahaiaEditatzen)
                cmbMahaiak.SelectedValue = _mahaiEditatuaId;
        }

        // ================= ERRESERBATU =================
        private void btnErreserbatu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIzena.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoa.Text) ||
                cmbMahaiak.SelectedItem == null ||
                string.IsNullOrEmpty(txandaAukeratua))
            {
                MessageBox.Show("Datu guztiak bete behar dira");
                return;
            }

            if (!int.TryParse(txtPertsonak.Text, out int pertsonak))
            {
                MessageBox.Show("Pertsona kopurua ez da zuzena");
                return;
            }

            int mahaiId = (int)cmbMahaiak.SelectedValue;

            var erreserbaCtrl = new ErreserbakController();
            var erreserbaMahaiCtrl = new ErreserbaMahaiController();

            // ===== EDITATU =====
            if (_editatzen)
            {
                _erreserbaEditatzen.Izena = txtIzena.Text.Trim();
                _erreserbaEditatzen.Telefonoa = txtTelefonoa.Text.Trim();
                _erreserbaEditatzen.PertsonaKopurua = pertsonak;
                _erreserbaEditatzen.Data = dtpData.Value.Date;
                _erreserbaEditatzen.Txanda = txandaAukeratua;
                _erreserbaEditatzen.MahaiaId = mahaiId;

                var eguneratuta = erreserbaCtrl.EguneratuErreserba(_erreserbaEditatzen);
                if (!eguneratuta)
                {
                    var mezua = string.IsNullOrWhiteSpace(erreserbaCtrl.AzkenErrorea)
                        ? "Errorea erreserba eguneratzean"
                        : erreserbaCtrl.AzkenErrorea;
                    MessageBox.Show(mezua);
                    return;
                }

                var mahaiaEguneratuta = erreserbaMahaiCtrl.EguneratuMahaiErreserban(
                    _erreserbaEditatzen.Id,
                    mahaiId
                );

                if (!mahaiaEguneratuta)
                {
                    MessageBox.Show("Errorea erreserbaren mahaia eguneratzean");
                    return;
                }

                MessageBox.Show("Erreserba eguneratuta!");
                Close();
                return;
            }

            // ===== SORTU =====
            var erreserba = new Erreserba
            {
                Izena = txtIzena.Text.Trim(),
                Telefonoa = txtTelefonoa.Text.Trim(),
                PertsonaKopurua = pertsonak,
                Txanda = txandaAukeratua,
                Data = dtpData.Value.Date,
                MahaiaId = mahaiId,
                Egoera = "sortua"
            };

            var sortua = erreserbaCtrl.SortuErreserba(erreserba);

            if (sortua == null)
            {
                var mezua = string.IsNullOrWhiteSpace(erreserbaCtrl.AzkenErrorea)
                    ? "Errorea erreserba sortzean"
                    : erreserbaCtrl.AzkenErrorea;
                MessageBox.Show(mezua);
                return;
            }

            var mahaiaGehituta = erreserbaMahaiCtrl.GehituMahaiErreserbara(sortua.Id, mahaiId);
            if (!mahaiaGehituta)
            {
                MessageBox.Show("Erreserba sortu da baina mahaia ezin izan da lotu");
                return;
            }

            MessageBox.Show("Erreserba behar bezala sortu da!");
            Close();
        }
    }
}
