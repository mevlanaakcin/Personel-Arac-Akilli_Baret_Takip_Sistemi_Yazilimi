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
    public partial class kurumekle : Form
    {
        public kurumekle()
        {
            InitializeComponent();
        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");


        public string sirketID = "", sirketADI = "", sirketvergiNO = "", sirketTelefon = "",sirketSigortaAdi="", sirketSigortaNO = "", sirketADRES = "", sirketULKE = "", sirketSEHIR = "", sirketMAIL = "", sirketFAAL = "", sirketBASLANGIC = "", sirketBITIS = "";

        
        void kurumguncelle()
        {
            textBoxsirketadi.Text = sirketADI;
            textBoxvergino.Text = sirketvergiNO;
            textBoxtelefonno.Text = sirketTelefon;
            textBoxsigortasirketi.Text = sirketSigortaAdi;
            textBoxsigortaNo.Text = sirketSigortaNO;
            textBoxadres.Text = sirketADRES;
            textBoxulke.Text = sirketULKE;
            textBoxsehir.Text = sirketSEHIR;
            textBoxemail.Text = sirketMAIL;
            textBoxfaaliyetalani.Text = sirketFAAL;
            dateTimePickergorevbaslangic.Text = sirketBASLANGIC;
            dateTimePickergorevbitis.Text = sirketBITIS;
        }
        Boolean inputkontrol()
        {
            Boolean deger = true;
            if (textBoxsirketadi.Text == "") { labelsirketadi.ForeColor = Color.Red; deger = false; } else { labelsirketadi.ForeColor = Color.White; }
            if (textBoxvergino.Text == "") { labelvergino.ForeColor = Color.Red; deger = false; } else { labelvergino.ForeColor = Color.White; }
            if (textBoxtelefonno.Text == "") { labeltelefonno.ForeColor = Color.Red; deger = false; } else { labeltelefonno.ForeColor = Color.White; }
            if (textBoxadres.Text == "") { labeladres.ForeColor = Color.Red; deger = false; } else { labeladres.ForeColor = Color.White; }
            if (textBoxulke.Text == "" || textBoxsehir.Text == "") { labelulkesehir.ForeColor = Color.Red; deger = false; } else { labelulkesehir.ForeColor = Color.White; }
            if (textBoxemail.Text == "") { labelemail.ForeColor = Color.Red; deger = false; } else { labelemail.ForeColor = Color.White; }
            if (textBoxfaaliyetalani.Text == "") { labelfaaliyetalani.ForeColor = Color.Red; deger = false; } else { labelfaaliyetalani.ForeColor = Color.White; }
            return deger;
        }

        private void buttonguncelle_Click(object sender, EventArgs e)
        {

            bool guncelle = false;
            if (inputkontrol()==true)
            {
                // Vergi numarasını veritabanından sorguluyoruz
                baglantim.Open();
                OleDbCommand sorgula = new OleDbCommand("SELECT * FROM kurumlar WHERE vergino='" +textBoxvergino.Text + "'", baglantim);
                OleDbDataReader kayitli = sorgula.ExecuteReader();

                while (kayitli.Read())
                {
                    string veri = kayitli.GetValue(0).ToString(); //ID
                    if (veri == sirketID)
                    {
                        guncelle = true;
                    }
                    baglantim.Close(); // veritabanı baglantisini kapat
                    break;
                }
               
                
                

                if (guncelle == true) // ID gelen ID ye eşit ise
                {
                    inputkontrol(); // zorunlu girişler kontrol ediliyor 

                    if (textBoxsirketadi.Text != "" && textBoxvergino.Text != "" && textBoxtelefonno.Text != "" && textBoxadres.Text != "" && textBoxulke.Text != "" && textBoxsehir.Text != "" && textBoxemail.Text != "" && textBoxfaaliyetalani.Text != "")
                    {
                        try
                        {
                            baglantim.Open();
                            OleDbCommand kurumuekle = new OleDbCommand("UPDATE kurumlar SET sirketadi='"+textBoxsirketadi.Text+"',vergino='"+textBoxvergino.Text+
                                "',telefon='"+textBoxtelefonno.Text+"',sigortasirketi='"+textBoxsigortasirketi.Text+"',sigortano='"+textBoxsigortaNo.Text+
                                "',adres='"+textBoxadres.Text+"',ulke='"+textBoxulke.Text+"',sehir='"+textBoxsehir.Text+"',email='"+textBoxemail.Text+
                                "',faaliyetalani='"+textBoxfaaliyetalani.Text+"',gorevbaslangic='"+dateTimePickergorevbaslangic.Value.ToString("d.M.yyyy") +
                                "',gorevbitis='"+dateTimePickergorevbitis.Value.ToString("d.M.yyyy")+"' WHERE sirketadi='"+sirketADI+"'", baglantim);
                            kurumuekle.ExecuteNonQuery();
                            baglantim.Close();
                            MessageBox.Show("Kurum başarı ile güncellendi !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            /*
                            proje ekleme kodları burada yazılacaktır

                            */



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
                    MessageBox.Show("Kurum bulunamadı!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //******************************************************************************************
            }
            else
            {
                inputkontrol();
            }
        }

        private void sirketekle_Load(object sender, EventArgs e)
        {
            if (sirketADI!="")
            {
                kurumguncelle();
                button1.Visible = false;

                
            }
            else
            {
                buttonguncelle.Visible = false;
            }
        }

        private void buttonvazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool kayitdurum = false;
            if (inputkontrol() == true)
            {
                // vergi  numarasını veritabanından sorguluyoruz
                baglantim.Open();
                OleDbCommand sorgu = new OleDbCommand("SELECT * FROM kurumlar WHERE vergino='" + textBoxvergino.Text + "'", baglantim);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitdurum = true; break;
                }

                baglantim.Close(); // veritabanı baglantisini kapat

                if (kayitdurum == false) // Kayıt Yok ise
                {
                    inputkontrol(); // zorunlu girişler kontrol ediliyor 

                    if (textBoxsirketadi.Text != "" && textBoxvergino.Text != "" && textBoxtelefonno.Text != "" && textBoxadres.Text != "" && textBoxulke.Text != "" && textBoxsehir.Text != "" && textBoxemail.Text != "" && textBoxfaaliyetalani.Text != "")
                    {
                        try
                        {
                            baglantim.Open();
                            OleDbCommand kurumuekle = new OleDbCommand("INSERT INTO kurumlar (sirketadi,vergino,telefon,sigortasirketi,sigortano,adres,ulke,sehir,email,faaliyetalani,gorevbaslangic,gorevbitis) VALUES('" 
                              +textBoxsirketadi.Text + "','" + textBoxvergino.Text + "','" +textBoxtelefonno.Text+"','"+textBoxsigortasirketi.Text+"','"+
                              textBoxsigortaNo.Text + "','" + textBoxadres.Text + "','" + textBoxulke.Text + "','" + textBoxsehir.Text + "','" + textBoxemail.Text + "','" +
                              textBoxfaaliyetalani.Text + "','" + dateTimePickergorevbaslangic.Value.ToString("d.M.yyyy") + "','" + dateTimePickergorevbitis.Value.ToString("d.M.yyyy") + "')", baglantim);
                            kurumuekle.ExecuteNonQuery();
                            baglantim.Close();
                            MessageBox.Show("Kurum başarı ile kaydedildi !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            /*
                            proje ekleme kodları burada yazılacaktır

                            */



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
                    MessageBox.Show("Kayıtlı Vergi Numarası !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                inputkontrol();
            }
        }
    }
}
