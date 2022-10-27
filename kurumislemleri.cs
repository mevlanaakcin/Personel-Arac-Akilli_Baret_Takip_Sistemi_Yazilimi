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
    public partial class kurumislemleri : Form
    {
        public kurumislemleri()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=data/database/veritabani.accdb");

        string kayitli;
        string seciliTema()
        {
            baglantim.Open();
            OleDbCommand baretsorgu = new OleDbCommand("SELECT *FROM setting", baglantim);
            OleDbDataReader okunan = baretsorgu.ExecuteReader();
            while (okunan.Read())
            {
                kayitli = okunan.GetValue(1).ToString();
                break;
            }
            baglantim.Close();
            return kayitli;
        }

        void beyazTema()
        {
            this.BackColor = Color.WhiteSmoke;
            labelinfo.BackColor = Color.LightGray; labelinfo.ForeColor = Color.Black;
            label1.BackColor = Color.LightGray; label1.ForeColor = Color.Black;
            label7.BackColor = Color.LightGray; label7.ForeColor = Color.Black;
            groupBox5.BackColor = Color.LightGray; groupBox5.ForeColor = Color.Black;
            pictureBox4.BackColor = Color.LightGray;
            dataGridView1.BackgroundColor = Color.WhiteSmoke; dataGridView1.GridColor = Color.WhiteSmoke; dataGridView1.DefaultCellStyle.BackColor = Color.LightGray;
            //**************MENU TEMA******************************
            pictureBox3.BackColor = Color.LightGray; label32.BackColor = Color.FromArgb(255, 152, 0); label32.ForeColor = Color.Black; pictureBox2.BackColor = Color.FromArgb(255, 152, 0);
            buttondasboard.BackColor = Color.GhostWhite; buttondasboard.ForeColor = Color.Black;
            buttonkurumislemleri.BackColor = Color.GhostWhite; buttonkurumislemleri.ForeColor = Color.Black;
            buttonadminpanel.BackColor = Color.GhostWhite; buttonadminpanel.ForeColor = Color.Black;
            buttonpersonel.BackColor = Color.GhostWhite; buttonpersonel.ForeColor = Color.Black;
            buttonbaretislemleri.BackColor = Color.GhostWhite; buttonbaretislemleri.ForeColor = Color.Black;
            buttonziyaretciislemleri.BackColor = Color.GhostWhite; buttonziyaretciislemleri.ForeColor = Color.Black;
            buttonaracislemleri.BackColor = Color.GhostWhite; buttonaracislemleri.ForeColor = Color.Black;
            buttonturnikeislemleri.BackColor = Color.GhostWhite; buttonturnikeislemleri.ForeColor = Color.Black;
            buttonveritabaniislemleri.BackColor = Color.GhostWhite; buttonveritabaniislemleri.ForeColor = Color.Black;
            buttonhakkinda.BackColor = Color.GhostWhite; buttonhakkinda.ForeColor = Color.Black;
            buttonayarlar.BackColor = Color.GhostWhite; buttonayarlar.ForeColor = Color.Black;
            buttoncikis.BackColor = Color.GhostWhite; buttoncikis.ForeColor = Color.Black;

            //***************COPYRİGHT**********************
            label3.BackColor = Color.LightGray; label3.ForeColor = Color.Black;
        }
        private void kurumlari_goster()
        {
            DataSet kurumtablo = new DataSet();

            //Tablodaki kayıtlı kurum sayısını öğrendik
            baglantim.Open();
            OleDbDataAdapter sayi = new OleDbDataAdapter("SELECT *FROM kurumlar", baglantim);
            kurumtablo.Clear();
            sayi.Fill(kurumtablo, "kurumlar");
            string count = kurumtablo.Tables["kurumlar"].Rows.Count.ToString();
            // baglantim.Close();
            kurumtablo.Clear();
            //*****************************************************************

            OleDbCommand sorgula = new OleDbCommand("SELECT *FROM kurumlar ", baglantim);
            OleDbDataReader kayitokuma = sorgula.ExecuteReader();
            //*******************Bir Tablo Oluşturduk Göstermek İÇin********************
            DataTable table = new DataTable();

            // add columns to datatable
            table.Columns.Add("Vergi No", typeof(string));
            table.Columns.Add("Şirket Adı", typeof(string));
            table.Columns.Add("Telefon No", typeof(string));
            table.Columns.Add("Faaliyet", typeof(string));
            table.Columns.Add("Ulke", typeof(string));
            table.Columns.Add("Sehir", typeof(string));
            table.Columns.Add("Adres", typeof(string));
            table.Columns.Add("e-mail", typeof(string));
            for (int i = 0; i <= Int64.Parse(count); i++)
            {
                while (kayitokuma.Read())
                {
                    table.Rows.Add(kayitokuma.GetValue(2).ToString(), kayitokuma.GetValue(1).ToString(), kayitokuma.GetValue(3).ToString(),
                        kayitokuma.GetValue(10).ToString(), kayitokuma.GetValue(7).ToString(), kayitokuma.GetValue(8).ToString(),
                        kayitokuma.GetValue(6).ToString(), kayitokuma.GetValue(9).ToString());
                    break;
                }
            }
            baglantim.Close();
            textBoxsirketara.Clear();
            dataGridView1.DataSource = table;


        }

        private void kurumislemleri_Load(object sender, EventArgs e)
        {
            if (seciliTema() == "Light Color")
            {
                beyazTema();
            }
            else { }

            kurumlari_goster();
        }

        private void buttonsirketara_Click(object sender, EventArgs e)
        {
            bool kayit_bul = false;
            if (textBoxsirketara.Text.Length != 0)
            {
                DataSet kurumtablo = new DataSet();

                //Tablodaki kayıtlı kurum sayısını öğrendik
                baglantim.Open();
                OleDbDataAdapter sayi = new OleDbDataAdapter("SELECT *FROM kurumlar WHERE sirketadi='" + textBoxsirketara.Text + "'", baglantim);
                kurumtablo.Clear();
                sayi.Fill(kurumtablo, "kurumlar");
                string count = kurumtablo.Tables["kurumlar"].Rows.Count.ToString();
                kurumtablo.Clear();
                //*****************************************************************

                OleDbCommand sorgula = new OleDbCommand("SELECT *FROM kurumlar WHERE sirketadi='" + textBoxsirketara.Text + "'", baglantim);
                OleDbDataReader kayitokuma = sorgula.ExecuteReader();
                //*******************Bir Tablo Oluşturduk Göstermek İÇin********************
                DataTable table = new DataTable();

                // add columns to datatable
                table.Columns.Add("Vergi No", typeof(string));
                table.Columns.Add("Şirket Adı", typeof(string));
                table.Columns.Add("Telefon No", typeof(string));
                table.Columns.Add("Faaliyet", typeof(string));
                table.Columns.Add("Ulke", typeof(string));
                table.Columns.Add("Sehir", typeof(string));
                table.Columns.Add("Adres", typeof(string));
                table.Columns.Add("e-mail", typeof(string));
                for (int i = 0; i <= Int64.Parse(count); i++)
                {
                    while (kayitokuma.Read())
                    {
                        kayit_bul = true;
                        table.Rows.Add(kayitokuma.GetValue(2).ToString(), kayitokuma.GetValue(1).ToString(), kayitokuma.GetValue(3).ToString(),
                            kayitokuma.GetValue(10).ToString(), kayitokuma.GetValue(7).ToString(), kayitokuma.GetValue(8).ToString(),
                            kayitokuma.GetValue(6).ToString(), kayitokuma.GetValue(9).ToString());
                        break;
                    }
                }
                baglantim.Close();
                textBoxsirketara.Clear();
                dataGridView1.DataSource = table;
                
                
                if (kayit_bul == false)
                {
                    MessageBox.Show("Aranan kayit bulunamadı !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen sirket adını boş giriniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        string sirket_adi = "";
        string sirketvergiNO = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sirket_adi = dataGridView1.SelectedCells[1].Value.ToString();
            sirketvergiNO= dataGridView1.SelectedCells[0].Value.ToString();
            MessageBox.Show(sirket_adi);
        }
        private void buttonkurumdelete_Click(object sender, EventArgs e)
        {
            if (sirket_adi == "")
            {
                MessageBox.Show("Sirket seçiniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                DialogResult sor = new DialogResult();
                sor = MessageBox.Show(sirket_adi + " kalıcı olarak silinecektir! Onaylıyor musunuz?", "Sirket Sil !", MessageBoxButtons.YesNo);
                if (sor == DialogResult.Yes)
                {
                    baglantim.Open();
                    OleDbCommand sirketsil = new OleDbCommand("DELETE FROM kurumlar WHERE vergino='" + sirketvergiNO + "'", baglantim);
                    sirketsil.ExecuteNonQuery();
                    MessageBox.Show(sirket_adi + " adlı sirket kaydı silindi!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();

                    kurumlari_goster();


                }
                else { MessageBox.Show("Sirket silme işlemi iptal edildi!"); }
            }
        }

        private void buttontabloyenile_Click(object sender, EventArgs e)
        {
            kurumlari_goster();
        }
        string[] kurumVeri = new string[30];
        private void buttonkurumupdate_Click(object sender, EventArgs e)
        {
            //bool sirket_bul = false;
            if (sirketvergiNO!="")
            {
                baglantim.Open();
                OleDbCommand sirketSor = new OleDbCommand("SELECT *FROM kurumlar WHERE vergino='" + sirketvergiNO + "'", baglantim);
                OleDbDataReader kayitlar = sirketSor.ExecuteReader();

                while (kayitlar.Read())
                {
                    //sirket_bul = true;
                    kurumVeri[0] = kayitlar.GetValue(0).ToString();  //ID
                    kurumVeri[1] = kayitlar.GetValue(1).ToString();  //sirketadi
                    kurumVeri[2] = kayitlar.GetValue(2).ToString();  //vergino
                    kurumVeri[3] = kayitlar.GetValue(3).ToString();  //telefon
                    kurumVeri[4] = kayitlar.GetValue(4).ToString();  //sigortasirketi
                    kurumVeri[5] = kayitlar.GetValue(5).ToString();  //sigortano
                    kurumVeri[6] = kayitlar.GetValue(6).ToString();  //adres
                    kurumVeri[7] = kayitlar.GetValue(7).ToString();  //ulke
                    kurumVeri[8] = kayitlar.GetValue(8).ToString();  //sehir
                    kurumVeri[9] = kayitlar.GetValue(9).ToString();  //email
                    kurumVeri[10] = kayitlar.GetValue(10).ToString();  //faaliyet alanı
                    kurumVeri[11] = kayitlar.GetValue(11).ToString();  //gorev baslangıc
                    kurumVeri[12] = kayitlar.GetValue(12).ToString();  //gorev bitis


                    baglantim.Close();
                    break;
                }
                kurumekle kurum = new kurumekle();
                kurum.sirketID = kurumVeri[0];
                kurum.sirketADI = kurumVeri[1];
                kurum.sirketvergiNO = kurumVeri[2];
                kurum.sirketTelefon= kurumVeri[3];
                kurum.sirketSigortaAdi= kurumVeri[4];
                kurum.sirketSigortaNO= kurumVeri[5];
                kurum.sirketADRES= kurumVeri[6];
                kurum.sirketULKE= kurumVeri[7];
                kurum.sirketSEHIR= kurumVeri[8];
                kurum.sirketMAIL= kurumVeri[9];
                kurum.sirketFAAL= kurumVeri[10];
                kurum.sirketBASLANGIC= kurumVeri[11];
                kurum.sirketBITIS= kurumVeri[12];
                kurum.Show();

            }
            else
            {
                MessageBox.Show("Lütfen kurum seçin !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttondasboard_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            dashboard frm4 = new dashboard();
            frm4.Show();
        }

        private void buttonadminpanel_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            adminpanel frm4 = new adminpanel();
            frm4.Show();
        }

        private void buttonpersonel_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            personelislemleri frm4 = new personelislemleri();
            frm4.Show();
        }

        private void buttonbaretislemleri_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            baretislemleri frm4 = new baretislemleri();
            frm4.Show();
        }

        private void buttonziyaretciislemleri_Click(object sender, EventArgs e)
        {
            ziyaretcikayit frm4 = new ziyaretcikayit();
            frm4.Show();
        }

        private void buttonaracislemleri_Click(object sender, EventArgs e)
        {
            aracekle frm4 = new aracekle();
            frm4.Show();
        }

        private void buttonveritabaniislemleri_Click(object sender, EventArgs e)
        {
            veritabaIslem frm4 = new veritabaIslem();
            frm4.Show();
        }

        private void buttonkurumnew_Click(object sender, EventArgs e)
        {
            kurumekle frm4 = new kurumekle();
            frm4.Show();
        }

        private void buttonayarlar_Click(object sender, EventArgs e)
        {

            Settings form = new Settings();
            form.Show();

        }
        private void buttoncikis_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            login frm4 = new login();
            frm4.Show();
        }
        private void buttonaracekle_Click(object sender, EventArgs e)
        {
            aracekle frm3 = new aracekle();
            frm3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void pictureBox19_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

       
    }
}
