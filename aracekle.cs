using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //dosya işlemleri için özellikle resim kullanmak için kullandık
using System.IO.Ports;
using System.Data.OleDb;  // veri tabanı kütüphanesi eklendi

namespace helmet_tracking_system
{
    public partial class aracekle : Form
    {
        public aracekle()
        {
            InitializeComponent();
        }

        Boolean inputkontrol()
        {
            Boolean deger = true;
            if (textBoxplaka.Text == "") { labelplaka.ForeColor = Color.Red; deger = false; } else { labelplaka.ForeColor = Color.White; }
            if (textBoxmarka.Text == "") { labelmarka.ForeColor = Color.Red; deger = false; } else { labelmarka.ForeColor = Color.White; }
            if (textBoxadsoyad.Text == "") { labeladisoyadi.ForeColor = Color.Red; deger = false; } else { labeladisoyadi.ForeColor = Color.White; }
            if (textBoxsirket.Text == "") { labelaracsirket.ForeColor = Color.Red; deger = false; } else { labelaracsirket.ForeColor = Color.White; }
            if (textBoxcepno.Text == "") { labelcepno.ForeColor = Color.Red; deger = false; } else { labelcepno.ForeColor = Color.White; }
            if (textBoxkimlikno.Text == "") { labelkimlikno.ForeColor = Color.Red; deger = false; } else { labelkimlikno.ForeColor = Color.White; }
            if (textBoxemail.Text == "") { labelemail.ForeColor = Color.Red; deger = false; } else { labelemail.ForeColor = Color.White; }
            if (textBoxulke.Text == "" || textBoxsehir.Text == "") { labelulkesehir.ForeColor = Color.Red; deger = false; } else { labelulkesehir.ForeColor = Color.White; }

            return deger;
        }

        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aracekle_Load(object sender, EventArgs e)
        {
            dateTimegirissaati.CustomFormat = "tt hh:mm";
           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBoxplaka.CharacterCasing = CharacterCasing.Upper;  // Yazı tipini büyük harfle oluşmasını sağladık
        }

        private void textBoxkg_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;    //Sadece rakam girmesine izin veriyoruz
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Rakam Girebilirsiniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBoxhacim_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;    //Sadece rakam girmesine izin veriyoruz
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Rakam Girebilirsiniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonarackaydet_Click(object sender, EventArgs e)
        {
            bool kayitdurum = false;
            if (inputkontrol() == true)
            {
                // Plakayı veritabanından sorguluyoruz
                baglantim.Open();
                OleDbCommand sorgu = new OleDbCommand("SELECT * FROM araclar WHERE plaka='" + textBoxplaka.Text + "'", baglantim);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitdurum = true; break;
                }

                baglantim.Close(); // veritabanı baglantisini kapat
                inputkontrol();

                if (kayitdurum == false) // Kayıt Yok ise
                {
                    inputkontrol(); // zorunlu girişler kontrol ediliyor 
                    if (textBoxplaka.Text != "" && textBoxmarka.Text != "" && textBoxadsoyad.Text != "" && textBoxcepno.Text != "" && textBoxkimlikno.Text != "")
                    {
                        try
                        {
                            baglantim.Open();
                            OleDbCommand kurumuekle = new OleDbCommand("INSERT INTO araclar (plaka,marka,sirket,yukcinsi,yukkg,yukhacim,giristarihi,girissaati,adivesoyadi,telefonno,kimlikno,email,ulke,sehir) VALUES('"
                                + textBoxplaka.Text + "','" + textBoxmarka.Text + "','" +
                              textBoxsirket.Text + "','" + textBoxyukcinsi.Text + "','" + textBoxkg.Text + "','" + textBoxhacim.Text + "','"
                              + dateTimePickergiristarihi.Value.ToString("d.M.yyyy") + "','" + dateTimegirissaati.Value.ToString("hh:mm tt") + "','"
                              + textBoxadsoyad.Text + "','" + textBoxcepno.Text + "','" + textBoxkimlikno.Text + "','" + textBoxemail.Text +
                               "','" + textBoxulke.Text + "','" + textBoxsehir.Text + "')", baglantim);
                            kurumuekle.ExecuteNonQuery();
                            baglantim.Close();
                            MessageBox.Show("Araç başarı ile kaydedildi !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                            this.Close();
                        }
                        catch (Exception msg)
                        {
                            MessageBox.Show(msg.Message, "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            baglantim.Close();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Kırmızı olan alanları tekrar kontrol ediniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else // Kayıt var ise
                {
                    MessageBox.Show("Kayıtlı Plaka !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                inputkontrol();
            }
        }
    }
}
