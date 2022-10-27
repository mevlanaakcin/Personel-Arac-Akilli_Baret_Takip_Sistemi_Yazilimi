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
    public partial class ziyaretcikayit : Form
    {
        public ziyaretcikayit()
        {
            InitializeComponent();
        }
        Boolean inputkontrol()
        {
            Boolean deger = true;
            if (textBoxadi.Text == "") { labelad.ForeColor = Color.Red; deger = false; } else { labelad.ForeColor = Color.White; }
            if (textBoxsoyadi.Text == "") { labelsoyad.ForeColor = Color.Red; deger = false; } else { labelsoyad.ForeColor = Color.White; }
            if (textBoxrfidNo.Text == "") { labelrfidNo.ForeColor = Color.Red; deger = false; } else { labelrfidNo.ForeColor = Color.White; }

            return deger;
        }

        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");
        private void ziyaretcikayit_Load(object sender, EventArgs e)
        {
            pictureBoxziyaretciuser.Image = Image.FromFile(Application.StartupPath + "\\data\\visitimage\\noprofil.jpg");
            pictureBoxziyaretciuser.SizeMode = PictureBoxSizeMode.StretchImage;

            dateTimeGirisSaati.CustomFormat = "tt hh:mm";
            groupBoxaracbilgileri.Enabled = false;

            /********************Arac plakalarını çekiyoruz***************************/
            baglantim.Open();
            OleDbCommand query = new OleDbCommand("SELECT * FROM araclar", baglantim);
            OleDbDataReader kayitoku = query.ExecuteReader();
            while (kayitoku.Read())
            {
                comboBoxplaka.Items.Add(kayitoku["plaka"]);
            }
            baglantim.Close();
            comboBoxplaka.SelectedIndex = 0;
            /******************************************************************************/

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string plaka;

            if (radioButtonaracvar.Checked) { plaka = comboBoxplaka.SelectedItem.ToString(); }
            else { plaka = ""; }

            inputkontrol();
           

                    if ( textBoxadi.Text != "" && textBoxsoyadi.Text != "" &&  textBoxrfidNo.Text != "" )
                    {

                        try
                        {
                            baglantim.Open();
                            OleDbCommand kurumuekle = new OleDbCommand("INSERT INTO ziyaretciler (adi,soyadi,kimlikNo,cepNo,email,sigortaNo,sirket,aracizinvar,plaka,rfidNo,giristarihi,girissaati,ziyaretsebebi) VALUES('"
                            + textBoxadi.Text + "','" + textBoxsoyadi.Text + "','" + textBoxkimlikNo.Text + "','" +textBoxcepNo.Text  + "','" +
                            textBoxmail.Text + "','" + textBoxsigortaNo.Text +"','"+ textBoxsirket.Text + "','"+ radioButtonaracvar.Checked.ToString() +
                            "','"+plaka+"','"+textBoxrfidNo.Text+"','"+ dateTimePickergiristarih.Value.ToString("d.M.yyyy")+
                            "','"+dateTimeGirisSaati.Value.ToString("hh:mm tt") +"','"+textBoxZsebebi.Text+ "')", baglantim);
                            kurumuekle.ExecuteNonQuery();
                            baglantim.Close();

                            if (!Directory.Exists(Application.StartupPath + "\\data\\visitimage"))
                            {
                                Directory.CreateDirectory(Application.StartupPath + "\\data\\visitimage");
                            }
                            else
                            {
                                pictureBoxziyaretciuser.Image.Save(Application.StartupPath + "\\data\\visitimage\\" + textBoxrfidNo.Text + ".jpg");
                            }

                            MessageBox.Show("Ziyaretçi başarı ile kaydedildi !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                           
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
                        MessageBox.Show("Resim seçin ve Kırmızı olan alanları kontrol edin !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        inputkontrol();
                     }

                
            
            
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Personel resmi seçiniz";
            resimsec.Filter = "JPG Dosyalar (*.jpg) | *.jpg";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBoxziyaretciuser.Image = new Bitmap(resimsec.OpenFile());
                pictureBoxziyaretciuser.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void radioButtonaracvar_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxaracbilgileri.Enabled = true;
        }

        private void radioButtonaracyok_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxaracbilgileri.Enabled = false;
        }

        private void linkLabelaracekle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aracekle frm3 = new aracekle();
            frm3.Show();
        }
    }
}
