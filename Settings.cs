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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }


        string kayitliTema;
        string kayitliDil;
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");
        OleDbConnection databasebaret = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/helmet.accdb");
        void beyazTema()
        {
            this.BackColor = Color.WhiteSmoke;
            pictureBox12.BackColor = Color.LightGray;
            labelbaslik.BackColor = Color.LightGray; labelbaslik.ForeColor = Color.Black;
            label1.BackColor = Color.LightGray; label1.ForeColor = Color.Black;
            label21.BackColor = Color.LightGray; label21.ForeColor = Color.Black;
            pictureBox5.BackColor = Color.LightGray;

        }
        string seciliTema()
        {
            baglantim.Open();
            OleDbCommand baretsorgu = new OleDbCommand("SELECT *FROM setting", baglantim);
            OleDbDataReader okunan = baretsorgu.ExecuteReader();
            while (okunan.Read())
            {
                kayitliTema = okunan.GetValue(1).ToString();
                break;
            }
            baglantim.Close();
            return kayitliTema;
        }
        string seciliDil()
        {
            baglantim.Open();
            OleDbCommand dilsorgu = new OleDbCommand("SELECT *FROM setting",baglantim);
            OleDbDataReader oku = dilsorgu.ExecuteReader();
            while (oku.Read())
            {
                kayitliDil = oku.GetValue(2).ToString();
                break;
            }
            baglantim.Close();
            return kayitliDil;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            comboBoxdil.Items.Add("Türkçe");
            comboBoxdil.Items.Add("Deutsche");
            comboBoxdil.Items.Add("English");
            comboBoxdil.Items.Add("Français");
            if (seciliDil()=="Türkçe")
            {
                comboBoxdil.SelectedIndex = 0;
            }
            else if (seciliDil()=="Deutsche")
            {
                comboBoxdil.SelectedIndex = 1;
            }
            else if (seciliDil()=="English")
            {
                comboBoxdil.SelectedIndex = 2;
            }
            else if (seciliDil()=="Français")
            {
                comboBoxdil.SelectedIndex = 3;
            }
            else
            {
                MessageBox.Show("Dil ayarları yapılamadı !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            comboBoxtema.Items.Add("Dark Color");
            comboBoxtema.Items.Add("Light Color");
            
            if (seciliTema()== "Dark Color")
            {
                comboBoxtema.SelectedIndex = 0;
            }
            else
            {
                beyazTema();
                comboBoxtema.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonkaydet_Click(object sender, EventArgs e)
        {
            
            if(seciliTema()!=comboBoxtema.SelectedItem.ToString() || seciliDil()!=comboBoxtema.SelectedItem.ToString())
            {

                try
                {
                    baglantim.Open();
                    OleDbCommand cmd = new OleDbCommand("UPDATE  setting SET arkaplan='" + comboBoxtema.SelectedItem.ToString() + "'", baglantim);
                    cmd.ExecuteNonQuery();
                   // baglantim.Close();

                    //baglantim.Open();
                    OleDbCommand dil = new OleDbCommand("UPDATE  setting SET dil='" + comboBoxdil.SelectedItem.ToString() + "'", baglantim);
                    dil.ExecuteNonQuery();
                    baglantim.Close();


                    MessageBox.Show("Ayarlar başarıyla yapıldı!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Restart(); // uygulama yeniden başlatıldı
                }
                catch (Exception msj)
                {
                    MessageBox.Show(msj+" Ayarlar yapılamadı. Daha sonra tekrar deneyiniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

               
            }
            else
            {

            }



        }
    }
}
