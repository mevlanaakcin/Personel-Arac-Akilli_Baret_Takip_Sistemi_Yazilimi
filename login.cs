using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.Data.OleDb kütüphanesi eklendi
using System.Data.OleDb;

namespace helmet_tracking_system
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");

        //Formlar arası veri aktarımında kullanılacak değişkenler

        public static string kullaniciadi, parola, yetki;
        //Yerel değişkenler tanımlayalım
        bool durum = false;

        private void loginbutton_Click(object sender, EventArgs e)
        {
          /*  bool kayit_varmi = false;

            if(textBox1.TextLength == 8)
            {
                baglantim.Open(); // veri tabanı bağlantısını açıyoruz
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where kullaniciadi='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader(); // kayıtokuyucu ile dataları topluyoruz
                //MessageBox.Show(kayitokuma["kullaniciadi"].ToString()+ "  " + kayitokuma["parola"].ToString()+"   " + kayitokuma["yetki"].ToString());
                while (kayitokuma.Read())
                {
                    kayit_varmi = true;

                    //ilk kurulum için aşağıdaki if ile kullanıcı oluşturulacaktır
                    if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Admin")
                    {
                        kullaniciadi = kayitokuma.GetValue(0).ToString();
                        parola = kayitokuma.GetValue(1).ToString();
                        yetki = kayitokuma.GetValue(3).ToString();
                        errormessage.Text = "";
                        durum = true;*/
                        this.Hide();
                        adminpanel frm2 = new adminpanel();
                        frm2.Show();
            /*  break;
          }
          //ilk kurulumdan sonra tanımlanan ilk kullanıcının göreceği ekran olacaktır
          else if ((kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "User"))
          {
              kullaniciadi = kayitokuma.GetValue(0).ToString();
              parola = kayitokuma.GetValue(1).ToString();
              yetki = kayitokuma.GetValue(3).ToString();
              errormessage.Text = "";
              this.Hide();
              //this.Close(); //  ekran kapatıldı
              personelislemleri frm3 = new personelislemleri();
              frm3.Show();
              break;
          }
          else
          {
              errormessage.Text = "Username or password error!";
              textBox1.Text = "";
              textBox2.Text = "";
          }
      }
      if (kayit_varmi == false)
      {
          MessageBox.Show("Böyle bir Kullanıcı yok !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          textBox1.Text = "";
          textBox2.Text = "";
          baglantim.Close();
      }
      baglantim.Close();
  }
  else
  {
      MessageBox.Show("Lütfen 8 haneli bir kullanici adı giriniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
      textBox1.Text = "";
      textBox2.Text = "";

  }*/

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = loginbutton; // Enter e basıldığında logine basılmış olacak
            this.CancelButton = exitbutton; // ESC ye basıldığında çıkışa basılmış olacak
            this.StartPosition = FormStartPosition.CenterScreen; // Ekranı ortada konumlandırıcaktır
            this.errormessage.Text = "";
        }
    }
}
