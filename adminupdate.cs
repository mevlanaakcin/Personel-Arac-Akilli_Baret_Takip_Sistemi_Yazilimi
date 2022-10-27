using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;  // veri tabanı kütüphanesi eklendi
//using System.Text.RegularExpressions;  // REGEX kütüphanesi eklendi(Güvenli parola oluşturmada işimize yarıyor)
using System.IO; // Giriş-Çıkış işlemlerine ilişkin kütüphane tanımlandı


namespace helmet_tracking_system
{
    public partial class adminupdate : Form
    {
        public adminupdate()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=data/database/veritabani.accdb");

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string kadi, adi, soyadi, yetki, sifre;
        private void adminupdate_Load(object sender, EventArgs e)
        {
            /*****************FORM ayarları**********************/
            this.StartPosition = FormStartPosition.CenterScreen; // Ekranı ortada konumlandırıcaktır
            comboBoxyetki.Items.Add("User");
            comboBoxyetki.Items.Add("Admin");

            if (yetki=="User") { comboBoxyetki.SelectedIndex = 0;}
            else { comboBoxyetki.SelectedIndex = 1; }

            labelkullaniciadi.Text = kadi;
            textboxupdatename.Text = adi;
            textboxupdatesurname.Text = soyadi;
            textboxupdatepassword.Text = sifre;
            textboxupdatepasswordagain.Text= sifre;
            
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            //adi kontrolü işlemleri
            if (textboxupdatename.Text.Length < 2 || textboxupdatename.Text == ""){label37.ForeColor = Color.Red;}
            else{ label37.ForeColor = Color.White;}
            //soyadi kontrolü işlemleri
            if (textboxupdatesurname.Text.Length < 2 || textboxupdatesurname.Text == ""){label34.ForeColor = Color.Red;}
            else{label34.ForeColor = Color.White;}
            //parola kontrolü işlemleri
            if (textboxupdatepassword.Text == ""){label25.ForeColor = Color.Red;}
            else{label25.ForeColor = Color.White;}
            //parola tekrar kontrolü işlemleri
            if (textboxupdatepasswordagain.Text == "" || textboxupdatepasswordagain.Text != textboxupdatepassword.Text){label38.ForeColor = Color.Red;}
            else{label38.ForeColor = Color.White;}
            if (textboxupdatename.Text != "" && textboxupdatesurname.Text != "" && textboxupdatepassword.Text != "" && textboxupdatepasswordagain.Text != "" && textboxupdatepassword.Text == textboxupdatepasswordagain.Text)
            {
                try
                {
                    // Veri güncelleme işlemleri
                    baglantim.Open();
                    OleDbCommand updatekomutu = new OleDbCommand("update kullanicilar set adi='" + textboxupdatename.Text +
                        "',soyadi='" + textboxupdatesurname.Text + "',yetki='" + comboBoxyetki.SelectedItem.ToString() + 
                        "',parola='" + textboxupdatepassword.Text + "'where kullaniciadi = '" + kadi + "'", baglantim);
                    updatekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    labelkullaniciadi.Text = "Null";
                    textboxupdatename.Clear();
                    textboxupdatesurname.Clear();
                    textboxupdatepassword.Clear();
                    textboxupdatepasswordagain.Clear();

                    MessageBox.Show("Kullanıcı bigileri güncellendi !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   
                }
                catch (Exception hatamesajı)
                {
                    MessageBox.Show(hatamesajı.Message, "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }

            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanları gözden geçiriniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }

            this.Close();
        }
    }
}
