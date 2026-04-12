using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Taldea1TPV.Eskariak.Erreserbak
{
    public partial class ErreserbakForm : Form
    {
    
        private DateTime _dataAukeratua = DateTime.Today;
        private string _txandaAukeratua = null; 
        private Erreserba _erreserbaHautatua = null;

        public ErreserbakForm()
        {
            InitializeComponent();

            
            flpErreserbak.FlowDirection = FlowDirection.TopDown;
            flpErreserbak.WrapContents = false;
            flpErreserbak.AutoScroll = true;

            
            dtpData.Value = DateTime.Today;
            dtpData.ValueChanged += (s, e) =>
            {
                _dataAukeratua = dtpData.Value.Date;
                _erreserbaHautatua = null;
                KargatuDena();
            };

            // ===== FILTRO TXANDA =====
            cmbTxanda.Items.AddRange(new[] { "Guztiak", "Bazkaria", "Afaria" });
            cmbTxanda.SelectedIndex = 0;
            cmbTxanda.SelectedIndexChanged += (s, e) =>
            {
                _txandaAukeratua =
                    cmbTxanda.SelectedItem.ToString() == "Guztiak"
                        ? null
                        : cmbTxanda.SelectedItem.ToString();

                _erreserbaHautatua = null;
                KargatuDena();
            };
        }

        private void ErreserbakForm_Load(object sender, EventArgs e)
        {
            KargatuDena();
        }

        
        private void KargatuDena()
        {
            KargatuErreserbak();
            KargatuMahaiak();
        }

        // =================== ERRESERBAK =========================
        private void KargatuErreserbak()
        {
            flpErreserbak.Controls.Clear();

            var ctrl = new ErreserbakController();
            var erreserbak = ctrl.LortuErreserbakData(_dataAukeratua)
                .Where(e =>
                    (_txandaAukeratua == null || e.Txanda == _txandaAukeratua))
                .ToList();

            foreach (var e in erreserbak)
                flpErreserbak.Controls.Add(ErreserbaListaSortu(e));
        }

        // =================== MAHAIAK ============================
        private void KargatuMahaiak()
        {
            flpMahaiak.Controls.Clear();

            var mahaiCtrl = new MahaiakController();
            var erreserbaCtrl = new ErreserbakController();
            var erreserbaMahaiCtrl = new ErreserbaMahaiController();

            var mahaiak = mahaiCtrl.LortuMahaiak();

            
            var erreserbak = erreserbaCtrl.LortuErreserbakData(_dataAukeratua)
                .Where(e =>
                    (_txandaAukeratua == null || e.Txanda == _txandaAukeratua))
                .ToList();

          
            var mahaiOkupatuak = erreserbak
                .SelectMany(e => erreserbaMahaiCtrl.LortuMahaiakErreserbarentzat(e.Id))
                .Distinct()
                .ToList();

            foreach (var mahai in mahaiak)
            {
                var panel = MahaiaSortuBisuala(mahai);

                if (mahaiOkupatuak.Contains(mahai.Id))
                    panel.BackColor = Color.FromArgb(255, 230, 230); 

                
                if (_erreserbaHautatua != null)
                {
                    var mahaiIds = erreserbaMahaiCtrl
                        .LortuMahaiakErreserbarentzat(_erreserbaHautatua.Id);

                    if (mahaiIds.Contains(mahai.Id))
                        panel.BackColor = Color.FromArgb(220, 240, 225);
                }

                flpMahaiak.Controls.Add(panel);
            }
        }

        // =================== PANEL MAHAIA =======================
        private Control MahaiaSortuBisuala(Mahaiak mahai)
        {
            Panel panel = new Panel
            {
                Width = 160,
                Height = 110,
                Margin = new Padding(12),
                Padding = new Padding(12),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = mahai.Id
            };

            panel.Controls.Add(new Label
            {
                Text = $"MAHAIA {mahai.Zenbakia}",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(29, 80, 91)
            });

            return panel;
        }

        // =================== PANEL ERRESERBA ====================
        private Control ErreserbaListaSortu(Erreserba erreserba)
        {
            Panel panel = new Panel
            {
                Height = 70,
                Width = flpErreserbak.Width - 25,
                Margin = new Padding(6),
                Padding = new Padding(8),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            panel.Controls.Add(new Label
            {
                Text = erreserba.Izena,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(8, 6),
                Size = new Size(150, 20)
            });

            panel.Controls.Add(new Label
            {
                Text = erreserba.Telefonoa,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(8, 30),
                ForeColor = Color.DimGray
            });

            panel.Controls.Add(new Label
            {
                Text = erreserba.Data.ToString("dd/MM/yyyy"),
                Font = new Font("Segoe UI", 9F),
                Location = new Point(170, 6)
            });

            panel.Controls.Add(new Label
            {
                Text = erreserba.Txanda,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(170, 30),
                ForeColor = erreserba.Txanda == "Afaria"
                    ? Color.FromArgb(31, 107, 58)
                    : Color.FromArgb(243, 134, 58)
            });

            panel.Click += (s, e) =>
            {
                foreach (Control c in flpErreserbak.Controls)
                    c.BackColor = Color.White;

                panel.BackColor = Color.FromArgb(230, 245, 238);
                _erreserbaHautatua = erreserba;

                btnEditatu.Enabled = true;
                btnEzabatu.Enabled = true;

                KargatuMahaiak();
            };

            return panel;
        }

        private void btnGehitu_Click(object sender, EventArgs e)
        {
            var f = new ErreserbatuForm();
            f.FormClosed += (s, ev) => KargatuDena();
            f.ShowDialog();
        }

        private void btnEditatu_Click(object sender, EventArgs e)
        {
            if (_erreserbaHautatua == null)
                return;

            var f = new ErreserbatuForm(_erreserbaHautatua);
            f.FormClosed += (s, ev) => KargatuDena();
            f.ShowDialog();
        }

        private void btnEzabatu_Click(object sender, EventArgs e)
        {
            if (_erreserbaHautatua == null)
                return;

            var konfirmazioa = MessageBox.Show(
                $"Ziur zaude erreserba ezabatu nahi duzula?\n\n{_erreserbaHautatua.Izena}",
                "Erreserba ezabatu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (konfirmazioa != DialogResult.Yes)
                return;

            var erreserbaCtrl = new ErreserbakController();

            bool ondoEzabatua =
                erreserbaCtrl.EzabatuErreserba(_erreserbaHautatua.Id);

            if (!ondoEzabatua)
            {
                MessageBox.Show("Errorea erreserba ezabatzean");
                return;
            }

            MessageBox.Show("Erreserba behar bezala ezabatua");

            _erreserbaHautatua = null;
            btnEditatu.Enabled = false;
            btnEzabatu.Enabled = false;

            KargatuDena();
        }



        private void btnFreskatu_Click(Object sender, EventArgs e)
        {
            KargatuDena();
        }

    }
}
