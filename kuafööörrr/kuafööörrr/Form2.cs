using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kuafööörrr
{
    public partial class Form2 : Form
    {
        // Kullanıcı adı ve şifre belirle
        private readonly string kullaniciAdi = "a";
        private readonly string sifre = "1234";

        public Form2()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("Column1", "");
            dataGridView1.Columns.Add("Column2", "");
            dataGridView1.Columns.Add("Column3", "DAKİKA ");
            dataGridView1.Columns.Add("Column4", "SANİYE");
            dataGridView1.Columns.Add("Column5", "");
            dataGridView1.Columns.Add("Column6", "");
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Giriş bilgilerini kontrol et
            if (textKullaniciAdi.Text == kullaniciAdi && textSifre.Text == sifre)
            {
                // randevu.txt dosyasını oku ve DataGridView'e yaz
                try
                {
                    using (StreamReader reader = new StreamReader("randevu.txt"))
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
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            // Seçili satırları DataGridView'den sil
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz satırı seçiniz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satır silinirken bir hata oluştu. " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Form3'ü aç
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
