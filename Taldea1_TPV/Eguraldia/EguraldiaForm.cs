using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Taldea1TPV.Eguraldia
{
    public class EguraldiaForm : Form
    {
        private readonly TableLayoutPanel taula;
        private readonly Label lblTitulua;
        private readonly Label lblEgoera;

        public EguraldiaForm()
        {
            Text = "AJA - Eguraldia";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(780, 420);
            ClientSize = new Size(920, 500);
            BackColor = Color.FromArgb(245, 247, 246);
            AutoScaleMode = AutoScaleMode.Dpi;

            var edukiontzia = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(18)
            };
            edukiontzia.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            edukiontzia.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            edukiontzia.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));

            lblTitulua = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 70, 110),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Eguraldi iragarpena"
            };

            taula = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoScroll = true
            };

            lblEgoera = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(90, 100, 110),
                TextAlign = ContentAlignment.MiddleLeft
            };

            edukiontzia.Controls.Add(lblTitulua, 0, 0);
            edukiontzia.Controls.Add(taula, 0, 1);
            edukiontzia.Controls.Add(lblEgoera, 0, 2);
            Controls.Add(edukiontzia);

            Load += (s, e) => KargatuEguraldia();
        }

        private void KargatuEguraldia()
        {
            try
            {
                var xmlBidea = AurkituXmlBidea();
                var iragarpena = IrakurriIragarpena(xmlBidea);

                lblTitulua.Text = string.Format(
                    "Eguraldi iragarpena - {0}, {1}",
                    iragarpena.Udalerria,
                    iragarpena.Probintzia);
                

                MarraztuTaula(iragarpena.Egunak);
            }
            catch (Exception ex)
            {
                lblEgoera.Text = "Errorea: " + ex.Message;
                MessageBox.Show(this, ex.Message, "Eguraldia ezin da kargatu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Iragarpena IrakurriIragarpena(string xmlBidea)
        {
            var dokumentua = XDocument.Load(xmlBidea);
            var erroa = dokumentua.Root;
            if (erroa == null || erroa.Name.LocalName != "iragarpena")
                throw new InvalidDataException("XML fitxategiak ez du 'iragarpena' erro etiketa.");

            var egunak = erroa.Elements("eguna")
                .Select(eguna => new Eguna
                {
                    Data = IrakurriData((string)eguna.Attribute("data")),
                    Izena = (string)eguna.Attribute("izena") ?? string.Empty,
                    Deskribapena = (string)eguna.Element("eguraldia")?.Element("deskribapena") ?? string.Empty,
                    Irudia = (string)eguna.Element("eguraldia")?.Element("irudia") ?? string.Empty,
                    Batezbestekoa = (string)eguna.Element("tenperatura")?.Attribute("batezbestekoa") ?? "-",
                    Maximoa = (string)eguna.Element("tenperatura")?.Attribute("maximoa") ?? "-",
                    Minimoa = (string)eguna.Element("tenperatura")?.Attribute("minimoa") ?? "-"
                })
                .ToList();

            if (egunak.Count == 0)
                throw new InvalidDataException("XML fitxategian ez dago egunik.");

            return new Iragarpena
            {
                Probintzia = (string)erroa.Attribute("probintzia") ?? string.Empty,
                Udalerria = (string)erroa.Attribute("udalerria") ?? string.Empty,
                Egunak = egunak
            };
        }

        private void MarraztuTaula(IReadOnlyList<Eguna> egunak)
        {
            taula.SuspendLayout();
            taula.Controls.Clear();
            taula.ColumnStyles.Clear();
            taula.RowStyles.Clear();
            taula.ColumnCount = egunak.Count;
            taula.RowCount = 5;

            for (var i = 0; i < egunak.Count; i++)
                taula.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / egunak.Count));

            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            taula.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));

            for (var zutabea = 0; zutabea < egunak.Count; zutabea++)
            {
                var eguna = egunak[zutabea];
                taula.Controls.Add(SortuGoiburua(eguna), zutabea, 0);
                taula.Controls.Add(SortuIrudia(eguna), zutabea, 1);
                taula.Controls.Add(SortuTestua(eguna.Deskribapena, 10F, Color.FromArgb(50, 65, 80), ContentAlignment.MiddleCenter), zutabea, 2);
                taula.Controls.Add(SortuTestua("Batezbestekoa: " + eguna.Batezbestekoa + " C", 11F, Color.FromArgb(40, 85, 135), ContentAlignment.MiddleCenter), zutabea, 3);
                taula.Controls.Add(SortuMinMax(eguna), zutabea, 4);
            }

            taula.ResumeLayout();
        }

        private static Control SortuGoiburua(Eguna eguna)
        {
            var testua = string.Format("{0}. {1:dd}", LaburtuEguna(eguna.Izena), eguna.Data);
            return SortuTestua(testua, 12F, Color.White, ContentAlignment.MiddleCenter, Color.FromArgb(70, 116, 178), FontStyle.Bold);
        }

        private Control SortuIrudia(Eguna eguna)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(145, 185, 222),
                Padding = new Padding(8)
            };

            var picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            var irudiBidea = AurkituIrudiBidea(eguna.Irudia);
            if (!string.IsNullOrEmpty(irudiBidea))
                picture.Image = Image.FromFile(irudiBidea);

            panel.Controls.Add(picture);
            return panel;
        }

        private static Control SortuMinMax(Eguna eguna)
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.White
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            panel.Controls.Add(SortuTestua(eguna.Minimoa, 12F, Color.FromArgb(45, 75, 145), ContentAlignment.MiddleRight), 0, 0);
            panel.Controls.Add(SortuTestua("/", 12F, Color.Black, ContentAlignment.MiddleCenter), 1, 0);
            panel.Controls.Add(SortuTestua(eguna.Maximoa + " C", 12F, Color.Red, ContentAlignment.MiddleLeft), 2, 0);
            return panel;
        }

        private static Label SortuTestua(
            string testua,
            float tamaina,
            Color kolorea,
            ContentAlignment lerrokatzea,
            Color? atzekoKolorea = null,
            FontStyle estiloa = FontStyle.Regular)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Text = testua,
                Font = new Font("Segoe UI", tamaina, estiloa),
                ForeColor = kolorea,
                BackColor = atzekoKolorea ?? Color.White,
                TextAlign = lerrokatzea,
                AutoEllipsis = true,
                Padding = new Padding(4)
            };
        }

        private static string AurkituXmlBidea()
        {
            var baseBidea = AppDomain.CurrentDomain.BaseDirectory;
            var hautagaiak = new List<string>
            {
                Path.Combine(baseBidea, "Xml_eraldatuta.xml"),
                Path.Combine(baseBidea, "Taldea1_Java_XML", "Xml_eraldatuta.xml"),
                Path.Combine(Environment.CurrentDirectory, "Xml_eraldatuta.xml"),
                Path.Combine(Environment.CurrentDirectory, "..", "Taldea1_Java_XML", "Xml_eraldatuta.xml")
            };

            var direktorioa = new DirectoryInfo(baseBidea);
            for (var i = 0; i < 6 && direktorioa != null; i++)
            {
                hautagaiak.Add(Path.Combine(direktorioa.FullName, "Xml_eraldatuta.xml"));
                hautagaiak.Add(Path.Combine(direktorioa.FullName, "Taldea1_Java_XML", "Xml_eraldatuta.xml"));
                hautagaiak.Add(Path.Combine(direktorioa.FullName, "..", "Taldea1_Java_XML", "Xml_eraldatuta.xml"));
                direktorioa = direktorioa.Parent;
            }

            var bidea = hautagaiak
                .Select(Path.GetFullPath)
                .FirstOrDefault(File.Exists);

            if (bidea == null)
                throw new FileNotFoundException("Ezin izan da Xml_eraldatuta.xml aurkitu.");

            return bidea;
        }

        private static string AurkituIrudiBidea(string irudia)
        {
            if (string.IsNullOrWhiteSpace(irudia))
                return null;

            var fitxategia = irudia.Trim() + ".png";
            var baseBidea = AppDomain.CurrentDomain.BaseDirectory;
            var hautagaiak = new List<string>
            {
                Path.Combine(baseBidea, "Properties", "Eguraldi_Irudiak", fitxategia),
                Path.Combine(Environment.CurrentDirectory, "Properties", "Eguraldi_Irudiak", fitxategia)
            };

            var direktorioa = new DirectoryInfo(baseBidea);
            for (var i = 0; i < 6 && direktorioa != null; i++)
            {
                hautagaiak.Add(Path.Combine(direktorioa.FullName, "Properties", "Eguraldi_Irudiak", fitxategia));
                hautagaiak.Add(Path.Combine(direktorioa.FullName, "Taldea1_TPV", "Properties", "Eguraldi_Irudiak", fitxategia));
                direktorioa = direktorioa.Parent;
            }

            return hautagaiak
                .Select(Path.GetFullPath)
                .FirstOrDefault(File.Exists);
        }

        private static DateTime IrakurriData(string data)
        {
            DateTime emaitza;
            return DateTime.TryParse(data, CultureInfo.InvariantCulture, DateTimeStyles.None, out emaitza)
                ? emaitza
                : DateTime.Today;
        }

        private static string LaburtuEguna(string izena)
        {
            if (string.IsNullOrWhiteSpace(izena))
                return string.Empty;

            return izena.Length <= 3 ? izena.ToLowerInvariant() : izena.Substring(0, 3).ToLowerInvariant();
        }

        private class Iragarpena
        {
            public string Probintzia { get; set; }
            public string Udalerria { get; set; }
            public List<Eguna> Egunak { get; set; }
        }

        private class Eguna
        {
            public DateTime Data { get; set; }
            public string Izena { get; set; }
            public string Deskribapena { get; set; }
            public string Irudia { get; set; }
            public string Batezbestekoa { get; set; }
            public string Maximoa { get; set; }
            public string Minimoa { get; set; }
        }
    }
}
