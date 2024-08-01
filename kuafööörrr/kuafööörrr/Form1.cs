
/****************************************************************************
**					SAKARYA ÜNİVERSİTESİ
**				BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**				    BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**				   NESNEYE DAYALI PROGRAMLAMA DERSİ
**					2023-2024 BAHAR DÖNEMİ
**	
**				ÖDEV NUMARASI..........:proje
**				ÖĞRENCİ ADI............:YUNUS EMRE SEVİNDİK
**				ÖĞRENCİ NUMARASI.......:G221210044
**                         DERSİN ALINDIĞI GRUP...:A GRUBU
**
****************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace kuafööörrr
{
    public partial class Form1 : Form
    {
        string dosyaYolu = System.Reflection.Assembly.GetExecutingAssembly().Location + "\\randevu.txt";
        // Saç kesim tipleri ve fiyatlarını içeren yer
        private Dictionary<string, decimal> haircutPrices = new Dictionary<string, decimal>
        {
            { "Saç", 50.00m },
            { "Sakal", 30.00m },
            { "Saç + Sakal", 75.00m },
            { "Ense", 120.00m },
            { "AĞDA", 220.00m },
            { "KAŞ ALMA", 720.00m },
            { "SAÇ BOYAMA", 1220.00m },
            { "MASKE", 520.00m },
            { "SAÇ KIVIRCIKLAŞTIRMA", 260.00m },
            { "MANİKÜR", 90.00m },
            { "PEDİKÜR", 970.00m },
            { "BIYIK", 420.00m },
        };

        // Personel isimleri ve ücretlerini içeren yer
        private Dictionary<string, decimal> personnelPrices = new Dictionary<string, decimal>();

        public Form1()
        {
            InitializeComponent();
            LoadPersonnelData();
            InitializeComboBoxes();
            InitializeMaskedTextBox();
            dateTimePicker1.MinDate = DateTime.Today;
        }

        private void LoadPersonnelData()
        {
            try
            {
                using (StreamReader reader = new StreamReader("personel.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 3)
                        {
                            string personnelName = $"{parts[0]} {parts[1]}";
                            decimal price = GetPriceForRole(parts[2]);
                            personnelPrices[personnelName] = price;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel dosyası okunurken bir hata oluştu. " + ex.Message);
            }
        }

        private decimal GetPriceForRole(string role)
        {
            switch (role)
            {
                case "Kuaför":
                    return 100.00m;
                case "Berber":
                    return 120.00m;
                case "Güzellik Uzmanı":
                    return 150.00m;
                default:
                    return 100.00m;
            }
        }

        private void InitializeComboBoxes()
        {
            // Personel isimlerini comboBox1'e ekleyin
            comboBox1.Items.AddRange(personnelPrices.Keys.ToArray());
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            // Saç kesim tiplerini comboBox3'e ekleyin
            comboBox3.Items.AddRange(haircutPrices.Keys.ToArray());
            comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged;
        }

        private void InitializeMaskedTextBox()
        {
            maskedTextBox1.Mask = "00000000000"; // 11 haneli telefon numarası
            maskedTextBox1.MaskInputRejected += maskedTextBox1_MaskInputRejected;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedPersonnel = comboBox1.SelectedItem.ToString();
                if (personnelPrices.ContainsKey(selectedPersonnel))
                {
                    decimal price = personnelPrices[selectedPersonnel];
                    label6.Text = $"Personel Ücreti: {price:C}";
                }
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                string selectedHaircut = comboBox3.SelectedItem.ToString();
                if (haircutPrices.ContainsKey(selectedHaircut))
                {
                    decimal price = haircutPrices[selectedHaircut];
                    label6.Text = $"Fiyat: {price:C}";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveAppointment();
        }

        private void SaveAppointment()
        {
            // TextBox'lardan bilgileri al
            string name = textBox1.Text;
            string surname = textBox2.Text;

            // MaskedTextBox'dan bilgi al
            string phoneNumber = maskedTextBox1.Text;

            // ComboBox'lardan bilgileri al
            string personnelName = comboBox1.SelectedItem?.ToString() ?? "";
            string appointmentTime = comboBox2.Value.ToString() ?? "";
            string haircutType = comboBox3.SelectedItem?.ToString() ?? "";

            // DateTimePicker'dan tarihi al
            string appointmentDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            // Bilgileri randevu.txt dosyasına yaz
            using (StreamWriter sw = new StreamWriter("randevu.txt", true))
            {
                sw.WriteLine($"İsim: {name}");
                sw.WriteLine($"Soyisim: {surname}");
                sw.WriteLine($"Telefon No: {phoneNumber}");
                sw.WriteLine($"PersonelAd: {personnelName}");
                sw.WriteLine($"Randevu Saati: {appointmentTime}");
                sw.WriteLine($"Randevu Tipi: {haircutType}");
                if (haircutPrices.ContainsKey(haircutType))
                {
                    decimal price = haircutPrices[haircutType];
                    sw.WriteLine($"Fiyat: {price:C}");
                }
                sw.WriteLine($"Randevu Tarihi: {appointmentDate}");
                sw.WriteLine("----------");
            }

            // Kullanıcıya bilgilendirme mesajı göster
            MessageBox.Show("Randevu kaydedildi!");

            // TextBox'ları ve diğer kontrolleri temizle
            textBox1.Clear();
            textBox2.Clear();
            maskedTextBox1.Clear();
            comboBox3.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            label6.Text = string.Empty;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = "Geçersiz Giriş";
            toolTip.Show("Telefon numarası sadece 11 rakamdan oluşmalıdır.", maskedTextBox1, maskedTextBox1.Location, 2000);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
