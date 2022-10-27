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
    public partial class personelislemleri : Form
    {
        public personelislemleri()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=data/database/veritabani.accdb");
        OleDbConnection databasebaret = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/helmet.accdb");
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
            tabPage1.BackColor = Color.LightGray; labelinfo.BackColor = Color.LightGray; labelinfo.ForeColor = Color.Black;
            tabPage3.BackColor = Color.LightGray; labelraporinfo.BackColor = Color.LightGray;labelraporinfo.ForeColor = Color.Black;
            label1.BackColor = Color.LightGray; label1.ForeColor = Color.Black;
            groupBox5.BackColor = Color.LightGray; groupBox5.ForeColor = Color.Black;
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
        private void personelleri_goster()
        {
            DataSet tablo = new DataSet();

            //Tablodaki kayıtlı personel sayısını öğrendik
            baglantim.Open();
            OleDbDataAdapter sayi=new OleDbDataAdapter("SELECT *FROM personeller",baglantim);
            tablo.Clear();
            sayi.Fill(tablo, "personeller");
            string count = tablo.Tables["personeller"].Rows.Count.ToString();
           // baglantim.Close();
            tablo.Clear();
            //*****************************************************************
           
            OleDbCommand sorgula = new OleDbCommand("SELECT *FROM personeller ", baglantim);
            OleDbDataReader kayitokuma = sorgula.ExecuteReader();
            //*******************Bir Tablo Oluşturduk Göstermek İÇin********************
            DataTable table = new DataTable();

            // add columns to datatable
            table.Columns.Add("KimlikNo", typeof(string));
            table.Columns.Add("Adı", typeof(string));
            table.Columns.Add("Soyadı", typeof(string));
            table.Columns.Add("Doğum Tarihi", typeof(string));
            table.Columns.Add("CepNo", typeof(string));
            table.Columns.Add("RfidNo", typeof(string));
            table.Columns.Add("Sirket", typeof(string));
            table.Columns.Add("Gorev", typeof(string));
            table.Columns.Add("Baret", typeof(string));
            table.Columns.Add("BaretID", typeof(string));
            for (int i = 0; i <= Int64.Parse(count); i++)
            {
                while (kayitokuma.Read())
                {
                    table.Rows.Add(kayitokuma.GetValue(1).ToString(), kayitokuma.GetValue(2).ToString(), kayitokuma.GetValue(3).ToString(),
                        kayitokuma.GetValue(4).ToString(), kayitokuma.GetValue(8).ToString(), kayitokuma.GetValue(11).ToString(),
                        kayitokuma.GetValue(14).ToString(), kayitokuma.GetValue(15).ToString(), kayitokuma.GetValue(22).ToString(),
                        kayitokuma.GetValue(24).ToString());
                    break;
                }
            }
            baglantim.Close();
            textBoxarama.Clear();
            dataGridView1.DataSource = table;


        }
        private void Form3_Load(object sender, EventArgs e)
            
        {
            if (seciliTema() == "Light Color")
            {
                beyazTema();
            }
            else { }
           
            personelleri_goster();
            this.StartPosition = FormStartPosition.CenterScreen; // Ekranı ortada konumlandırıcaktır
            //personel işlemleri ayarları
            if (login.yetki!="Admin")
            {
                buttonadminpanel.Enabled = false;

            }
            else
            {
                buttonadminpanel.Visible = true;
            }
            buttonpersonel.Enabled = false;
            //kullanıcı işlemleri
            this.Text = "Device Connection";
            label2.Text = "Helmet Tracking System V:1.0.0 | Username: " + login.kullaniciadi + " | " + "Authorization: " + login.yetki;
            //groupbox.enabled=false grup boxu kullanılmaz hale getiren kod

            pictureBoxprofil.Image = Image.FromFile(Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
            pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
       

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen==true)
            {
                serialPort1.Close();
            }
        }

       

        private void buttonformchange_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Close(); //  ekran kapatıldı
            adminpanel frm2 = new adminpanel();
            frm2.Show();
        }

        

        private void buttondasboard_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            dashboard frm4 = new dashboard();
            frm4.Show();
        }

       

        private void button14_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            login frm4 = new login();
            frm4.Show();
        }

        private void buttonpersonelekle_Click(object sender, EventArgs e)
        {
            AddNewPerson frm3 = new AddNewPerson();
            frm3.Show();
        }

        private void buttonziyaretekle_Click(object sender, EventArgs e)
        {
            ziyaretcikayit frm3 = new ziyaretcikayit();
            frm3.Show();
        }

        private void buttonaracekle_Click(object sender, EventArgs e)
        {
            aracekle frm3 = new aracekle();
            frm3.Show();
        }

        private void buttonkurumislemleri_Click(object sender, EventArgs e)
        {
            this.Close();  //  ekran kapatıldı
            kurumislemleri frm3 = new kurumislemleri();
            frm3.Show();
        }

        private void buttonbaretislemleri_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı 
            baretislemleri frm4 = new baretislemleri();
            frm4.Show();
        }

        private void buttonziyaretciislemleri_Click(object sender, EventArgs e)
        {
            ziyaretcikayit frm3 = new ziyaretcikayit();
            frm3.Show();
        }

        private void buttonaracislemleri_Click(object sender, EventArgs e)
        {
            aracekle frm3 = new aracekle();
            frm3.Show();
        }

        private void buttonveritabaniislemleri_Click(object sender, EventArgs e)
        {
            veritabaIslem frm3 = new veritabaIslem();
            frm3.Show();
        }

        private void buttonhakkinda_Click(object sender, EventArgs e)
        {

        }
        private void buttonayarlar_Click(object sender, EventArgs e)
        {
            Settings form = new Settings();
            form.Show();
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
       
        private void button5_Click(object sender, EventArgs e)
        {

            bool kayit_bul = false;
            if (textBoxarama.Text.Length!=0)
            {
                baglantim.Open();
                OleDbCommand sorgula = new OleDbCommand("SELECT *FROM personeller WHERE kimlikNo='"+textBoxarama.Text+"'", baglantim);
                OleDbDataReader kayitokuma = sorgula.ExecuteReader();
                while (kayitokuma.Read())
                {
                    
                    kayit_bul = true;
                    
                    /*******************Bir Tablo Oluşturduk Göstermek İÇin********************/
                    DataTable table = new DataTable();

                    // add columns to datatable
                    table.Columns.Add("KimlikNo", typeof(string));
                    table.Columns.Add("Adı", typeof(string));
                    table.Columns.Add("Soyadı", typeof(string));
                    table.Columns.Add("Doğum Tarihi", typeof(string));
                    table.Columns.Add("CepNo", typeof(string));
                    table.Columns.Add("RfidNo", typeof(string));
                    table.Columns.Add("Sirket", typeof(string));
                    table.Columns.Add("Gorev", typeof(string));
                    table.Columns.Add("Baret", typeof(string));
                    table.Columns.Add("BaretID", typeof(string));

                    table.Rows.Add(kayitokuma.GetValue(1).ToString(),kayitokuma.GetValue(2).ToString(),kayitokuma.GetValue(3).ToString(),
                        kayitokuma.GetValue(4).ToString(),kayitokuma.GetValue(8).ToString(),kayitokuma.GetValue(11).ToString(),
                        kayitokuma.GetValue(14).ToString(),kayitokuma.GetValue(15).ToString(),kayitokuma.GetValue(22).ToString(),
                        kayitokuma.GetValue(24).ToString());

                    dataGridView1.DataSource = table;
                    /**********************************************************************************/
                    //kullanıcı resmi çekme kodları

                    try
                    {
                        pictureBoxprofil.ImageLocation= (Application.StartupPath + "\\data\\personimage\\" + textBoxarama.Text + ".jpg");
                        pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch 
                    {
                        pictureBoxprofil.ImageLocation= (Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
                        pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    baglantim.Close();
                    textBoxarama.Clear();
                    break;
                }
                if (kayit_bul==false)
                {
                    MessageBox.Show("Aranan kayit bulunamadı !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen kimlik no bilgisini doğru giriniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        string kimlikno = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            kimlikno = dataGridView1.SelectedCells[0].Value.ToString();
            try
            {
                pictureBoxprofil.ImageLocation= (Application.StartupPath + "\\data\\personimage\\" + kimlikno + ".jpg");
                pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                pictureBoxprofil.ImageLocation= (Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
                pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (kimlikno == "")
            {
                MessageBox.Show("Personel seçiniz!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                DialogResult ques = new DialogResult();
                ques = MessageBox.Show(dataGridView1.SelectedCells[1].Value.ToString()+" kalıcı olarak silinecektir! Onaylıyor musunuz?", "Personel Sil !", MessageBoxButtons.YesNo);
                if (ques == DialogResult.Yes)
                {
                    baglantim.Open();
                    OleDbCommand deletesorgu = new OleDbCommand("DELETE from personeller WHERE kimlikNo='" + kimlikno + "'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    baglantim.Close();
                       
                    personelleri_goster();
                    
                    
                    //**********************************Resim Silme İşlemi*******************************
                    string path = Application.StartupPath+"\\data\\personimage\\" +kimlikno + ".jpg";

                    var myfile = File.Create(path);
                    myfile.Close();// dosya herhangi ir şekilde kullanılıyor ise dosyayı kapattık
                    if (System.IO.File.Exists(path)){System.IO.File.Delete(path);}
                    //***********************************************************************************

                    //**********************************Evrak Silme işlemi*******************************
                    string dosya = Application.StartupPath + "\\data\\personpdf\\" + kimlikno + ".pdf";

                    var file = File.Create(dosya);
                    file.Close();
                    if (System.IO.File.Exists(dosya)) { System.IO.File.Delete(dosya); }
                    //***********************************************************************************

                    //***********************Baret Tablosu silme ****************************************
                    databasebaret.Open();
                    OleDbCommand tablodelete = new OleDbCommand("DROP TABLE " + kimlikno + "", databasebaret);
                    tablodelete.ExecuteNonQuery();
                    databasebaret.Close();
                    //***********************************************************************************
                    MessageBox.Show(kimlikno + " adlı kullanıcı kaydı silindi!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else { MessageBox.Show("Personel silme işlemi iptal edildi!"); }
            }
        }

        private void buttontabloyenile_Click(object sender, EventArgs e)
        {
            personelleri_goster();
        }
        private void buttonevrak_Click(object sender, EventArgs e)
        {
            if(File.Exists(Application.StartupPath + "\\data\\personpdf\\" + kimlikno + ".pdf") == true) // evrak var ise dosyayı aç
            {
                string uri = Application.StartupPath + "\\data\\personpdf\\" + kimlikno + ".pdf";
                System.Diagnostics.Process.Start(uri);
            }
            else// yok ise açma
            {
                MessageBox.Show("Personele ait herhangi bir evrak yok!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        string[] veri=new string[30];
        private void button9_Click(object sender, EventArgs e)
        {
          
            //bool kayit = false;
            if (kimlikno!="") 
            {
                baglantim.Open();
                OleDbCommand sorgula = new OleDbCommand("SELECT *FROM personeller WHERE kimlikNo='" + kimlikno + "'", baglantim);
                OleDbDataReader kayitoku = sorgula.ExecuteReader();
                
                while (kayitoku.Read())
                {

                    //kayit = true;
                    veri[0] = kayitoku.GetValue(0).ToString();  //ID
                    veri[1] = kayitoku.GetValue(1).ToString();  //kimlikno
                    veri[2] = kayitoku.GetValue(2).ToString();  //adi
                    veri[3] = kayitoku.GetValue(3).ToString();  //soyadi
                    veri[4] = kayitoku.GetValue(4).ToString();  //dTarihi
                    veri[5] = kayitoku.GetValue(5).ToString();  //ulke
                    veri[6] = kayitoku.GetValue(6).ToString();  //sehir
                    veri[7] = kayitoku.GetValue(7).ToString();  //evadresi
                    veri[8] = kayitoku.GetValue(8).ToString();  //cepno
                    veri[9] = kayitoku.GetValue(9).ToString();  //mail
                    veri[10] = kayitoku.GetValue(10).ToString();//sigortano
                    veri[11] = kayitoku.GetValue(11).ToString();//rfidno 
                    veri[12] = kayitoku.GetValue(12).ToString();//egitimduzeyi
                    veri[13] = kayitoku.GetValue(13).ToString();// meslek
                    veri[14] = kayitoku.GetValue(14).ToString();//sirket
                    veri[15] = kayitoku.GetValue(15).ToString();//gorev
                    veri[16] = kayitoku.GetValue(16).ToString();//gorevsuresi
                    veri[17] = kayitoku.GetValue(17).ToString();//girissaati
                    veri[18] = kayitoku.GetValue(18).ToString();//cikissaati
                    veri[19] = kayitoku.GetValue(19).ToString();//evrakno
                    veri[20] = kayitoku.GetValue(20).ToString();//aracGirisizini
                    veri[21] = kayitoku.GetValue(21).ToString();//plaka
                    veri[22] = kayitoku.GetValue(22).ToString();//baretkullan
                    veri[23] = kayitoku.GetValue(23).ToString();//baretİzin
                    veri[24] = kayitoku.GetValue(24).ToString();//baretID

                    baglantim.Close();
                    break;
                }
                // yenikisiekle isim olarak  güncellendiği içinAddNewPerson olarak ismi böyle kaldı


                AddNewPerson yenikisiekle = new AddNewPerson();
                yenikisiekle.ID = Int32.Parse(veri[0]);
                yenikisiekle.tcNo = veri[1];
                yenikisiekle.adi = veri[2];
                yenikisiekle.soyadi = veri[3];
                yenikisiekle.dTarihi = veri[4];
                yenikisiekle.ulke = veri[5];
                yenikisiekle.sehir = veri[6];
                yenikisiekle.evAdresi = veri[7];
                yenikisiekle.cepNo = veri[8];
                yenikisiekle.mail = veri[9];
                yenikisiekle.sigortaNo = veri[10];
                yenikisiekle.rfidNo = veri[11];
                yenikisiekle.egitimDuzeyi = veri[12];
                yenikisiekle.meslek = veri[13];
                yenikisiekle.sirket = veri[14];
                yenikisiekle.gorev = veri[15];
                yenikisiekle.gorevSure = veri[16];
                yenikisiekle.girisSaat = veri[17];
                yenikisiekle.cikisSaat = veri[18];
                yenikisiekle.evrakno = veri[19];
                yenikisiekle.aracIzin = veri[20];
                yenikisiekle.plaka = veri[21];
                yenikisiekle.baretkullan = veri[22];
                yenikisiekle.baretIzin = veri[23];
                yenikisiekle.baretID = veri[24];
                yenikisiekle.Show();
            }
            else
            {
                MessageBox.Show("Lütfen personel seçin !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /***************************************************************************/
        }

      
    }
}
