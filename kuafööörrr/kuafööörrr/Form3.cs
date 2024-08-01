using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace kuafööörrr
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            // DataGridView kolonlarını ayarla
            dataGridView1.Columns.Add("Column1", "Personel Adı");
            dataGridView1.Columns.Add("Column2", "Personel Soyadı");
            dataGridView1.Columns.Add("Column3", "Görev");
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Personel listesini yükle
            LoadPersonelList();
        }

        private void LoadPersonelList()
        {
            dataGridView1.Rows.Clear();
            try
            {
                using (StreamReader reader = new StreamReader("personel.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] satir = line.Split(':');
                        dataGridView1.Rows.Add(satir);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya okunurken bir hata oluştu. " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Yeni personel ekle
            string personelAdi = textBox1.Text;
            string personelSoyadi = textBox2.Text;
            string gorev = textBox3.Text;

            if (!string.IsNullOrEmpty(personelAdi) && !string.IsNullOrEmpty(personelSoyadi) && !string.IsNullOrEmpty(gorev))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter("personel.txt", true))
                    {
                        writer.WriteLine($"{personelAdi}:{personelSoyadi}:{gorev}");
                    }
                    MessageBox.Show("Personel eklendi.");
                    LoadPersonelList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Personel eklenirken bir hata oluştu. " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }

        private void SİL_Click(object sender, EventArgs e)
        {
            // Seçili personeli sil
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }

                    // Geçici dosya kullanarak personel.txt dosyasını güncelle
                    using (StreamWriter writer = new StreamWriter("personelTemp.txt"))
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                StringBuilder satir = new StringBuilder();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    satir.Append(cell.Value?.ToString() + ":");
                                }
                                writer.WriteLine(satir.ToString().TrimEnd(':'));
                            }
                        }
                    }

                    // Eski dosyayı sil ve geçici dosyayı yeniden adlandır
                    File.Delete("personel.txt");
                    File.Move("personelTemp.txt", "personel.txt");

                    MessageBox.Show("Personel silindi.");
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz personeli seçiniz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel silinirken bir hata oluştu. " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ekle_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
