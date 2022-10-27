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
    public partial class adminpanel : Form
    {
        public adminpanel()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=data/database/veritabani.accdb");
        
        //Aşağıdaki link yerel ağda kullanılacak link olup ağlar arası veritabanı paylaşımı için kullanılacak yöntemdir
            //OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=\\\\192.168.1.111\\database\\veritabani.accdb");
        
        private void personelleri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter
                    ("select kullaniciadi AS[USER NAME], adi AS[NAME],soyadi AS[SURNAME],yetki AS[AUTHORIZATION],parola AS[PASSWORD] from kullanicilar Order By adi ASC",baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message,"Helmet Tracking System Error !",MessageBoxButtons.OK,MessageBoxIcon.Error);
                baglantim.Close();
                throw;
            }
        } 
        private void Form2_Load(object sender, EventArgs e)
        {
            /*****************FORM 2 ayarları**********************/
            this.StartPosition = FormStartPosition.CenterScreen; // Ekranı ortada konumlandırıcaktır
           
           

            //kullanıcı işlemleri
            this.Text = "User Operations";
            label1.Text = "Helmet Tracking System V:1.0.0 | Username: " + login.kullaniciadi +" | "+ "Authorization: " + login.yetki;
            textname.MaxLength = 12;
            textsurname.MaxLength = 12;
            textusername.MaxLength = 8;
            textpassword.MaxLength = 10;
            textpasswordagain.MaxLength = 10;

            toolTip1.SetToolTip(this.textname, "input name");
            toolTip1.SetToolTip(this.textpassword, "sadece sayı giriniz!");
            toolTip1.SetToolTip(this.textpasswordagain, "sadece sayı giriniz!");
            textname.CharacterCasing = CharacterCasing.Upper;
            textsurname.CharacterCasing = CharacterCasing.Upper;


            personelleri_goster();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textname.Text.Length < 2){errorProvider1.SetError(textname, "isim en az 2 harfli olmalı!");}
            else{errorProvider1.Clear();}
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textsurname.Text.Length < 2){errorProvider1.SetError(textsurname, "Soyisim en az 5 harfli olmalı!");}
            else{errorProvider1.Clear();}
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textusername.Text.Length < 8){ errorProvider1.SetError(textusername, "Kullaniciadi en az 8 harfli olmalı!");}
            else{errorProvider1.Clear();}
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textpassword.Text.Length < 10){errorProvider1.SetError(textpassword, "parola en az 10 haneli ve sayı olmalı!");}
            else{errorProvider1.Clear();}
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textpasswordagain.Text.Length < 10){errorProvider1.SetError(textpasswordagain, "parola en az 10 haneli ve sayı olmalı!");}
            else{errorProvider1.Clear();}
            if (textpassword.Text !=textpasswordagain.Text){errorProvider1.SetError(textpasswordagain,"parolalar aynı değil!");}
            else{errorProvider1.Clear();}
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar)==true || char.IsControl(e.KeyChar)==true ||char.IsSeparator(e.KeyChar)==true){e.Handled = false;}
            else{e.Handled = true;}
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true){e.Handled = false;}
            else{e.Handled = true;}
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar)==true || char.IsControl(e.KeyChar)==true || char.IsDigit(e.KeyChar)== true){ e.Handled = false;}
            else{e.Handled = true;}
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)){e.Handled = false;}
            else{e.Handled = true;}
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)){e.Handled = false;}
            else{e.Handled = true;}
        }
        private void topPage1_clear()
        {
            textname.Clear();
            textsurname.Clear();
            textusername.Clear();
            textpassword.Clear();
            textpasswordagain.Clear();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            bool kayitkontrol = false; // access tablosunda böyle bir kayıt yok demek istiyoruz.
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where kullaniciadi='" + textusername.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;

            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                //kullanici adi kontrolü işlemleri
                if(textusername.Text.Length != 8 || textusername.Text == ""){label12.ForeColor = Color.Red;}
                else{label12.ForeColor = Color.White;}
                //adi kontrolü işlemleri
                if (textname.Text.Length < 2 || textname.Text == ""){label10.ForeColor = Color.Red;}
                else {label10.ForeColor = Color.White;}
                //soyadi kontrolü işlemleri
                if (textsurname.Text.Length < 2 || textsurname.Text == ""){label11.ForeColor = Color.Red;}
                else{ label11.ForeColor = Color.White;}
                //parola kontrolü işlemleri
                if (textpassword.Text == ""){label13.ForeColor = Color.Red;}
                else{label13.ForeColor = Color.White;}
                //parola tekrar kontrolü işlemleri
                if (textpasswordagain.Text == "" || textpasswordagain.Text !=textpassword.Text){label14.ForeColor = Color.Red;}
                else{label14.ForeColor = Color.White;}

                if (textusername.Text.Length==8 && textusername.Text!="" && textname.Text!="" && textsurname.Text!=""&& textpassword.Text!=""&& textpasswordagain.Text!="" && textpassword.Text==textpasswordagain.Text)
                {
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu=new OleDbCommand("insert into kullanicilar values  ('"+textusername.Text+"','"+textname.Text+"','"+textsurname.Text+"','"+"User"+"','"+textpasswordagain.Text+"')",baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu !", "Helmet Tracking System",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        topPage1_clear();
                    }
                    catch (Exception hatamesajı)
                    {
                        MessageBox.Show(hatamesajı.Message);
                        baglantim.Close();
                    }
                }

                else { MessageBox.Show("yazı rengi kırmızı olan alanları gözden geçiriniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);}
                

            }

            else
            {
                MessageBox.Show("Girilen Kullanıcı adı daha önceden kayıtlıdır!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textusername.Clear();
                label12.ForeColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
       
        

        private void buttonformchange_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Close(); //  ekran kapatıldı
            personelislemleri frm3 = new personelislemleri();
            frm3.Show();
        }
        /*******************Sürükle Bırak ayarları bölümü*************************/
        int Move;
        int Mouse_X;
        int Mouse_Y;

        private void pictureBox19_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void pictureBox19_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1) {this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);}
        }

        private void pictureBox19_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }


        string kullaniciad = "";
        string ad = "";
        string soyad = "";
        string yetki = "";
        string sifre="";

        private void buttonyenile_Click(object sender, EventArgs e)
        {
            personelleri_goster();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (kullaniciad == "")
            {
                MessageBox.Show("Kullanıcı seçiniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                DialogResult ques = new DialogResult();
                ques = MessageBox.Show("Kullanıcı kalıcı olarak silinecektir! Onaylıyor musunuz?", "Kullanıcı silme !", MessageBoxButtons.YesNo);
                if (ques == DialogResult.Yes)
                {
                    if (kullaniciad.Length == 8 && kullaniciad != "")
                    {
                        baglantim.Open();
                        OleDbCommand deletesorgu = new OleDbCommand("delete from kullanicilar where kullaniciadi='" + kullaniciad + "'", baglantim);
                        deletesorgu.ExecuteNonQuery();
                        MessageBox.Show(kullaniciad + " adlı kullanıcı kaydı silindi!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        baglantim.Close();
                        personelleri_goster();
                    }
                    else { MessageBox.Show("Kullanıcıadını doğru giriniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("Kullanıcı silme işlemi iptal edildi!"); }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /************gridwiev seçili satır bilgileri seçme*/
          //  for (int i = 0; i < dataGridView1.SelectedCells.Count; i++) {}

            kullaniciad=dataGridView1.SelectedCells[0].Value.ToString();
            ad = dataGridView1.SelectedCells[1].Value.ToString();
            soyad = dataGridView1.SelectedCells[2].Value.ToString();
            yetki = dataGridView1.SelectedCells[3].Value.ToString();
            sifre = dataGridView1.SelectedCells[4].Value.ToString();

            MessageBox.Show(kullaniciad+" adlı kullanıcı seçildi !");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            /****************UPDATE İşlemleri****************/
            if (kullaniciad == "")
            {
                MessageBox.Show("Kullanıcı seçiniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                adminupdate updateekran = new adminupdate();
                updateekran.kadi = kullaniciad;
                updateekran.adi = ad;
                updateekran.soyadi = soyad;
                updateekran.yetki = yetki;
                updateekran.sifre = sifre;
                updateekran.Show();
            }
            
        }
        /***************************************************************************/
    }
}
