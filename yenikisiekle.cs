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
using iTextSharp;
using iTextSharp.text;   // itex ile pdf oluşturma yapıyoruz
using iTextSharp.text.pdf; // pdf için gerekli sınıfları barındırır
using System.Threading; // thread işlemleri yapmak için ekledik
/*

 // Veri tabanındaki plaka sayısını böyle alıyoruz
             baglantim.Open();
            OleDbCommand oldplakadb = new OleDbCommand("SELECT COUNT (*) FROM araclar", baglantim);
            plakaCountOld = Int32.Parse(oldplakadb.ExecuteScalar().ToString());
            baglantim.Close();
*/
namespace helmet_tracking_system
{
    public partial class AddNewPerson : Form
    {

        string kayitliDil;
        string seciliDil()
        {
            baglantim.Open();
            OleDbCommand dilsorgu = new OleDbCommand("SELECT *FROM setting", baglantim);
            OleDbDataReader oku = dilsorgu.ExecuteReader();
            while (oku.Read())
            {
                kayitliDil = oku.GetValue(2).ToString();
                break;
            }
            baglantim.Close();
            return kayitliDil;
        }


        void ingilizce()
        {
            //grupbox adları
            groupBoxkisiekle.Text = "Add new person";
            groupBox3.Text = "Vehicle information";
            groupBox2.Text = "Add document";
            groupBox4.Text = "Entry/Exit Time";
            groupBoxbaretkkulaniyor.Text = "";
            //kayıt bilgileri
            labelKimlikNo.Text = "Identification Number:";
            labelad.Text = "First Name:";
            labelsoyad.Text = "Surname:";
            labelDtarihi.Text = "Date of birth:";
            labelulke_sehir.Text = "City/Country:";
            labelevadresi.Text = "Address:";
            labelcepNo.Text = "";
            labelemail.Text = "";
            labelaracvarmi.Text = "";
            labelplaka.Text = "";
            labelsigortano.Text = "";
            labelrfid.Text = "";
            labelokul.Text = "";
            labelmeslek.Text = "";
            labelsirket.Text = "";
            labelgorev.Text = "";
            labelgorevsure.Text = "";
            label1.Text = "";
            labelgirissaati.Text = "";
            labelcikissaati.Text = "";
            labelbaretkullanimi.Text = "";
            labelbaretizin.Text = "";
            labelcihazid.Text = "";
            //Butonlar
            button5.Text = "resim yükle";
            buttonfolder.Text = "";
            buttonguncelle.Text = "";
            buttonvazgec.Text = "";
            buttonkisikaydet.Text = "";

        }

        void almanca()
        {
            //grupbox adları
            groupBoxkisiekle.Text = "Füge neuen Person hinzu";
            groupBox3.Text = "Fahrzeug informationen";
            groupBox2.Text = "Dokument hinzufügen";
            groupBox4.Text = "Ein- / Austrittszeit";
            groupBoxbaretkkulaniyor.Text = "";
            //kayıt bilgileri
            labelKimlikNo.Text = "Identifikationsnummer:";
            labelad.Text = "Vorname:";
            labelsoyad.Text = "Name:";
            labelDtarihi.Text = "Geburtsdatum:";
            labelulke_sehir.Text = "Ort/Land:";
            labelevadresi.Text = "Adresse:";
            labelcepNo.Text = "";
            labelemail.Text = "";
            labelaracvarmi.Text = "";
            labelplaka.Text = "";
            labelsigortano.Text = "";
            labelrfid.Text = "";
            labelokul.Text = "";
            labelmeslek.Text = "";
            labelsirket.Text = "";
            labelgorev.Text = "";
            labelgorevsure.Text = "";
            label1.Text = "";
            labelgirissaati.Text = "";
            labelcikissaati.Text = "";
            labelbaretkullanimi.Text = "";
            labelbaretizin.Text = "";
            labelcihazid.Text = "";
            //Butonlar
            button5.Text = "resim yükle";
            buttonfolder.Text = "";
            buttonguncelle.Text = "";
            buttonvazgec.Text = "";
            buttonkisikaydet.Text = "";
        }

        void fransızca()
        {
            //grupbox adları
            groupBoxkisiekle.Text = "Ajouter une nouvelle personne";
            groupBox3.Text = "Informations sur le véhicule";
            groupBox2.Text = "Ajouter un document";
            groupBox4.Text = "Heure d'entrée/sortie";
            groupBoxbaretkkulaniyor.Text = "";
            //kayıt bilgileri
            labelKimlikNo.Text = "Numéro d'identification:";
            labelad.Text = "Prénom:";
            labelsoyad.Text = "Nom de famille:";
            labelDtarihi.Text = "Date de naissance:";
            labelulke_sehir.Text = "Ville/Pays:";
            labelevadresi.Text = "Adresse:";
            labelcepNo.Text = "";
            labelemail.Text = "";
            labelaracvarmi.Text = "";
            labelplaka.Text = "";
            labelsigortano.Text = "";
            labelrfid.Text = "";
            labelokul.Text = "";
            labelmeslek.Text = "";
            labelsirket.Text = "";
            labelgorev.Text = "";
            labelgorevsure.Text = "";
            label1.Text = "";
            labelgirissaati.Text = "";
            labelcikissaati.Text = "";
            labelbaretkullanimi.Text = "";
            labelbaretizin.Text = "";
            labelcihazid.Text = "";
            //Butonlar
            button5.Text = "resim yükle";
            buttonfolder.Text = "";
            buttonguncelle.Text = "";
            buttonvazgec.Text = "";
            buttonkisikaydet.Text = "";
        }


        public AddNewPerson()
        {
            InitializeComponent();
        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");
        OleDbConnection databasebaret = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/helmet.accdb");
        private string resimYolu = "";

        Boolean inputkontrol()
        {
            Boolean deger = true;
            if (textBoxtcNo.Text=="")                              {labelKimlikNo.ForeColor = Color.Red;deger = false; } else{labelKimlikNo.ForeColor = Color.White;}
            if (textBoxadi.Text == "")                             { labelad.ForeColor = Color.Red; deger = false; } else { labelad.ForeColor = Color.White; }
            if (textBoxsoyadi.Text == "")                          { labelsoyad.ForeColor = Color.Red; deger = false; } else { labelsoyad.ForeColor = Color.White; }
            if (textBoxulke.Text == "" || textBoxsehir.Text == "") { labelulke_sehir.ForeColor = Color.Red; deger = false; } else { labelulke_sehir.ForeColor = Color.White; }
            if (textBoxmeslek.Text == "")                          { labelmeslek.ForeColor = Color.Red; deger = false; } else { labelmeslek.ForeColor = Color.White; }
            if (textBoxrfidNo.Text == "")                          { labelrfid.ForeColor = Color.Red; deger = false; } else { labelrfid.ForeColor = Color.White; }
            if (textBoxsigortaNo.Text == "")                       { labelsigortano.ForeColor = Color.Red; deger = false; } else { labelsigortano.ForeColor = Color.White; }
           
            return deger;
        }
        private void profilGuncelle(){
            /**************************Bir Güncelleme işleminde Atama Yapılacak****************************************/
            textBoxtcNo.Text = tcNo;
            textBoxadi.Text = adi;
            textBoxsoyadi.Text = soyadi;
            dateTimePickerDtarihi.Text = dTarihi;
            textBoxulke.Text = ulke;
            textBoxsehir.Text = sehir;
            textBoxevAdres.Text = evAdresi;
            textBoxcepNo.Text = cepNo;
            textBoxmail.Text = mail;
            textBoxsigortaNo.Text = sigortaNo;
            textBoxrfidNo.Text = rfidNo;
            textBoxokul.Text = egitimDuzeyi;
            textBoxmeslek.Text = meslek;
            comboBoxsirket.SelectedIndex = comboBoxsirket.FindStringExact(sirket);
            textBoxgorevi.Text = gorev;
            textBoxgorevsuresi.Text = gorevSure;
            if (girisSaat != "Sınırsız" && cikisSaat != "Sınırsız")
            {
                radioButtonsinirsiz.Checked = false; radioButtonsinirli.Checked = true;
                dateTimeGirisSaati.Text = girisSaat;dateTimeCikisSaati.Text = cikisSaat;
            }
            else { radioButtonsinirli.Checked = false; radioButtonsinirsiz.Checked = true; }

            if (aracIzin == "False"){radioButtonaracvar.Checked = false; radioButtonaracyok.Checked = true;}

            else
            {
                radioButtonaracvar.Checked = true; radioButtonaracyok.Checked = false;
                comboBoxaracplaka.SelectedIndex = comboBoxaracplaka.FindStringExact(plaka);
            }
            if (baretkullan == "False"){radioButtonbaretvar.Checked = false; radioButtonbaretyok.Checked = true;}
            else
            {
                radioButtonbaretvar.Checked = true; radioButtonbaretyok.Checked = false;
                if (baretIzin == "False"){ radioButtonbaretizinvar.Checked = false; radioButtonbaretizinyok.Checked = true;}
                else{radioButtonbaretizinvar.Checked = true; radioButtonbaretizinyok.Checked = false;}
            }
            textBoxcihazid.Text = baretID;

            /**********************************************************************************************************/

        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            /*********************************** Personel Resmini ekledik **********************************/
            OpenFileDialog Resimsec = new OpenFileDialog();
            Resimsec.Title = "Personel resmi seçiniz";
            Resimsec.Filter = "JPG Dosyaları (*.jpg) | *.jpg";
           
            if (Resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBoxuser.Image = new Bitmap(Resimsec.OpenFile());
                pictureBoxuser.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            resimYolu = Resimsec.FileName;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Close(); // açık kalabilir idye kapatıryoruz
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Diğer formların erişebileceği değişkenler
        public int ID=0; 
        public string tcNo="", adi="", soyadi = "", dTarihi = "", ulke = "", sehir = "", evAdresi = "", cepNo = "", mail = "", sigortaNo = "", rfidNo = "", egitimDuzeyi = "", meslek = "", sirket = "", gorev = "", gorevSure = "", girisSaat = "", cikisSaat = "",evrakno="", aracIzin = "", plaka = "", baretkullan = "", baretIzin = "", baretID = "";

        private void buttonguncelle_Click(object sender, EventArgs e)
        {
            serialPort1.Close(); // açık kalabilir idye kapatıryoruz
            /* Kişi sisteme kaydolma kodları burada yazacaktır*/
            String girisSaat, cikisSaat;
            String deviceID = "";
            String plaka;

            if (radioButtonbaretvar.Checked) { deviceID = textBoxcihazid.Text; }
            else { deviceID = ""; }


            if (radioButtonsinirsiz.Checked) { girisSaat = "Sınırsız"; cikisSaat = "Sınırsız"; }
            else { girisSaat = dateTimeGirisSaati.Value.ToString("hh:mm tt"); cikisSaat = dateTimeCikisSaati.Value.ToString("hh:mm tt"); }
            if (radioButtonaracvar.Checked) { plaka = comboBoxaracplaka.SelectedItem.ToString(); }
            else { plaka = ""; }

            bool tckontrol = false;
            bool kayitdurum = false;
            bool rfdurum = false;
            if (inputkontrol() == true)
            {
                
                // Kimlik numarasının ID sini veritabanından sorguluyoruz
                baglantim.Open();
                OleDbCommand sor = new OleDbCommand("SELECT * FROM personeller WHERE kimlikNo='"+tcNo+"' ", baglantim);
                OleDbDataReader kayitokuma = sor.ExecuteReader();
                while (kayitokuma.Read())
                {
                   int veri = Int32.Parse(kayitokuma.GetValue(0).ToString());  //ID

                    if (veri == ID)
                    {
                        kayitdurum = true;
                    }
                    break;
                }
                /*********************************************************************************************************/
                OleDbCommand rfsor = new OleDbCommand("SELECT * FROM personeller WHERE rfidNo='" + textBoxrfidNo.Text + "' ", baglantim);
                OleDbDataReader rfkayit = rfsor.ExecuteReader();
                while (rfkayit.Read())
                {

                    if (ID.ToString() != rfkayit.GetValue(0).ToString()) // eğer girilen textbox tcsinin ID düzenleme yapılan kişinin ID si ile aynı değilse TRUE yap
                    {
                        rfdurum = true;
                    }
                    break;
                }
                /*********************************************************************************************************/
                if (textBoxtcNo.Text != "")
                {
                    // textboxtaki kimlik numarasını sorgulayarak kayıtlı bir kişi girip girmediğimizi kontrol ediyoruz
                    OleDbCommand bak = new OleDbCommand("SELECT * FROM personeller WHERE kimlikNo='" + textBoxtcNo.Text + "' ", baglantim);
                    OleDbDataReader oku = bak.ExecuteReader();
                    while (oku.Read())
                    {

                        if ( ID.ToString()!=oku.GetValue(0).ToString()) // eğer girilen textbox tcsinin ID düzenleme yapılan kişinin ID si ile aynı değilse TRUE yap
                        {
                            tckontrol = true;
                        }
                        break;
                    }
                    baglantim.Close(); // veritabanı baglantisini kapat
                    inputkontrol();
                }

                if (kayitdurum == true && tckontrol == false) // Kayıt var  ve daha önce kayıtlı bir tc ile kaydedilmeye çalışılmıyor ise
                {
                    if (rfdurum == false)
                      { 
                        inputkontrol(); // zorunlu girişler kontrol ediliyor 
                        bool baretIDstate = false;
                        if (deviceID != "")  // baret daha önceden kayıtlı mı değil mi?
                        {


                            // Aynı baret Id numarası başka birine verilmesin diye önüne geçiyoruz

                            baglantim.Open();
                            OleDbCommand baretsorgu = new OleDbCommand("SELECT *FROM personeller WHERE baretID='" + deviceID + "'", baglantim);
                            OleDbDataReader okunan = baretsorgu.ExecuteReader();
                            while (okunan.Read())
                            {
                                if (ID.ToString()!=okunan.GetValue(0).ToString())
                                {
                                    baretIDstate = true;
                                }
                    
                              break;
                            }
                            baglantim.Close();
                        }

                        if (textBoxtcNo.Text != "" && textBoxadi.Text != "" && textBoxsoyadi.Text != "" && textBoxulke.Text != ""
                        && textBoxsehir.Text != "" && textBoxmeslek.Text != "" && textBoxrfidNo.Text != "" && textBoxsigortaNo.Text != "" && baretIDstate==false)
                        {


                            try
                            {

                                baglantim.Open();
                                OleDbCommand kisiguncelle = new OleDbCommand("UPDATE  personeller SET kimlikNo='" + textBoxtcNo.Text + "',adi='" + textBoxadi.Text + "',soyadi='" + textBoxsoyadi.Text +
                                    "',dogumTarihi='" + dateTimePickerDtarihi.Value.ToString("d.M.yyyy") + "',ulke='" + textBoxulke.Text + "',sehir='" + textBoxsehir.Text + "',evAdresi='" + textBoxevAdres.Text +
                                    "',cepNo='" + textBoxcepNo.Text + "',email='" + textBoxmail.Text + "',sigortaNo='" + textBoxsigortaNo.Text + "',rfidNo='" + textBoxrfidNo.Text + "',egitimduzeyi='" + textBoxokul.Text +
                                    "',meslegi='" + textBoxmeslek.Text + "',sirket='" + comboBoxsirket.SelectedItem.ToString() + "',gorev='" + textBoxgorevi.Text + "',gorevSuresi='" + textBoxgorevsuresi.Text +
                                    "',girisSaati='" + girisSaat + "',cikisSaati='" + cikisSaat + "',evrakNo='" + textBoxtcNo.Text + "',aracGirisIzni='" + radioButtonaracvar.Checked.ToString() +
                                    "',plaka='" + plaka + "',baretKullanimi='" + radioButtonbaretvar.Checked.ToString() + "',baretIzinKarti='" + radioButtonbaretizinvar.Checked.ToString() + "',baretID='" + deviceID +
                                    "' WHERE kimlikNo='" + tcNo + "'", baglantim);
                                kisiguncelle.ExecuteNonQuery();
                                baglantim.Close();

                                if (resimYolu != "" && tcNo != "")
                                {
                                    //*****************Resim Silme İşlemi***************
                                    string path = Application.StartupPath + "\\data\\personimage\\" + tcNo + ".jpg";

                                    var myfile = File.Create(path);
                                    myfile.Close();// dosya herhangi bir şekilde kullanılıyor ise dosyayı kapattık
                                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                                    //****************************************************

                                    if (!Directory.Exists(Application.StartupPath + "\\data\\personimage")) { Directory.CreateDirectory(Application.StartupPath + "\\data\\personimage"); }
                                    else { pictureBoxuser.Image.Save(Application.StartupPath + "\\data\\personimage\\" + textBoxtcNo.Text + ".jpg"); }

                                }
                                else
                                {

                                    if (tcNo != textBoxtcNo.Text)
                                    {
                                        //Resim ismi değiştirme işlemleri
                                        string oldjpgname = Application.StartupPath + "\\data\\personimage\\" + tcNo + ".jpg";
                                        string newjpgname = Application.StartupPath + "\\data\\personimage\\" + textBoxtcNo.Text + ".jpg";
                                        
                                        System.IO.File.Copy(oldjpgname, newjpgname, true);
                                        var jpgfile = File.Create(oldjpgname);
                                        jpgfile.Close();// dosya herhangi bir şekilde kullanılıyor ise dosyayı kapattık
                                        if (System.IO.File.Exists(oldjpgname)) { System.IO.File.Delete(oldjpgname); }


                                       // PDF dosyası var ise dosyanın adını değiştir
                                        if (File.Exists(Application.StartupPath + "\\data\\personpdf\\" + evrakno + ".pdf")==true)
                                        {
                                            //evrak ismi değiştirme işlemleri
                                            string oldpdfname = Application.StartupPath + "\\data\\personpdf\\" + tcNo + ".pdf";
                                            string newpdfname = Application.StartupPath + "\\data\\personpdf\\" + textBoxtcNo.Text + ".pdf";

                                            System.IO.File.Copy(oldpdfname, newpdfname, true);
                                            var pdffile = File.Create(oldpdfname);
                                            pdffile.Close();// dosya herhangi bir şekilde kullanılıyor ise dosyayı kapattık
                                            if (System.IO.File.Exists(oldpdfname)) { System.IO.File.Delete(oldpdfname); }
                                        }
                                       

                                    }
                                    else { }

                                }

                                MessageBox.Show("Kişi başarı ile Güncellendi !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                resimYolu = "";
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
                            if (baretIDstate == true)
                            {
                                MessageBox.Show("Bu baret ID numarası Başka bir kullanıcıya aittir!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Resim seçin ve Kırmızı olan alanları kontrol edin !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                }
                else//kayıtlı olan bir RFID no kulanıldı ise
                {
                    MessageBox.Show("Kayıtlı olan bir RFID numarası girdiniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

             }
            else //kayıtlı olan bir kimlik no kullanıldı ise
                {
                    MessageBox.Show("Kayıtlı olan bir kimlik numarası girdiniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                inputkontrol();

            }

        }
        String[] portlistesi;
        bool connectDevice = false;

        void portlistele()
        {
            comboBoxcom.Items.Clear();  //combox içini temizliyoruz
            portlistesi = SerialPort.GetPortNames(); // Serialport isimlerini diziye ekliyoruz
            foreach (string portadi in portlistesi)   //foreach dizilerin uzunluğu kadar çalışır
            {
                comboBoxcom.Items.Add(portadi);
                if (portlistesi[0] != null)
                {
                    comboBoxcom.SelectedItem = portlistesi[0];
                }
            }

    }

        private void comboBoxcom_MouseClick(object sender, MouseEventArgs e)
        {
            portlistele();
        }

        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            //seçili dil ayarları

            if (seciliDil() == "English")
            {
                ingilizce();
            }
            else if (seciliDil() == "Deutsche")
            {
                almanca();
            }
            else if (seciliDil() == "Français")
            {
                fransızca();
            }

            dateTimeGirisSaati.CustomFormat = "tt hh:mm";
            dateTimeCikisSaati.CustomFormat = "tt hh:mm";

            dateTimeGirisSaati.Enabled = false; dateTimeCikisSaati.Enabled = false; groupBoxbaretkkulaniyor.Enabled = false;
            comboBoxaracplaka.Enabled = false;linkLabelaracekle.Enabled = false;

            //********************Kurumların isimlerini ile çekiyoruz*********************
            baglantim.Open();
            OleDbCommand sorgu = new OleDbCommand("SELECT * FROM kurumlar", baglantim);
            OleDbDataReader kayitokuma = sorgu.ExecuteReader();
            comboBoxsirket.Items.Add ( "-Company-");
            while (kayitokuma.Read())
            {
                comboBoxsirket.Items.Add(kayitokuma["sirketadi"]);
            }
            comboBoxsirket.SelectedIndex = 0;
            //********************Arac plakalarını ile çekiyoruz***************************
            OleDbCommand query = new OleDbCommand("SELECT * FROM araclar", baglantim);
            OleDbDataReader kayitoku = query.ExecuteReader();
            while (kayitoku.Read())
            {
                comboBoxaracplaka.Items.Add(kayitoku["plaka"]);
            }
            baglantim.Close();
            //****************************************************************************
            if (serialPort1.IsOpen == true)// seriport açık kalmış ise kapat
            {
                serialPort1.Close();
            }
            portlistele();


            if (tcNo != "")
            {
                pictureBoxuser.ImageLocation =(Application.StartupPath + "\\data\\personimage\\" + tcNo + ".jpg");
                pictureBoxuser.SizeMode = PictureBoxSizeMode.StretchImage;

                buttonkisikaydet.Visible = false;
                buttonguncelle.Visible = true;

                /***************profil bilgileri ile güncelle*********************************/
                profilGuncelle();
                /*****************************************************************************/
            }
            else
            {
                
                pictureBoxuser.ImageLocation= (Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
                pictureBoxuser.SizeMode = PictureBoxSizeMode.StretchImage;
                buttonkisikaydet.Visible = true;
                buttonguncelle.Visible = false;
            }
            

        }
        private void buttoncon_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectDevice == false)
                {
                    serialPort1.PortName = comboBoxcom.GetItemText(comboBoxcom.SelectedItem);
                    serialPort1.BaudRate = 115200;
                    serialPort1.ReadTimeout = 3000; // okuma için belli bir zaman belirleme 
                    serialPort1.Open();
                    serialPort1.DiscardOutBuffer(); // giden veri hafızasını temizler
                    serialPort1.DiscardInBuffer(); // gelen veri hafızasını temizler
                    connectDevice = true;
                    comboBoxcom.Enabled = false;
                    buttoncon.Text = "Disconnect";
                    buttoncon.BackColor = Color.FromArgb(198, 40, 40);

                    serialPort1.Write("1");
                    string deviceID = serialPort1.ReadLine();
                    labelID.Text = deviceID;
                    textBoxcihazid.Text = deviceID;
                    string devicedate = serialPort1.ReadLine();
                    labeldate.Text = devicedate;
                    string devicestate = serialPort1.ReadLine();
                    labelstate.Text = devicestate;
                    string devicebatt = serialPort1.ReadLine();
                    // fomüle edilmiş % gösterge bildirimi
                    double bat = (((double.Parse(devicebatt) / 100) - 2.7) / 0.0166666666) + 10;
                    bat = Math.Floor(bat);
                    labelbatt.Text = "% " + bat.ToString();

                }
                else
                {
                    labelID.Text = "Null";
                    labeldate.Text = "Null";
                    labelstate.Text = "Null";
                    labelbatt.Text = "Null";
                    serialPort1.Close();
                    comboBoxcom.Enabled = true;
                    connectDevice = false;
                    buttoncon.BackColor = Color.FromArgb(0, 230, 118);
                    buttoncon.Text = "Connect";

                }
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Helmet Tracking System Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serialPort1.Close();
                comboBoxcom.Enabled = true;
                connectDevice = false;
                buttoncon.BackColor = Color.FromArgb(0, 230, 118);
                buttoncon.Text = "Connect";
            }
        }
        private void comboBoxsirket_Click(object sender, EventArgs e)
        {
            comboBoxsirket.Items.Clear();
            /********************Kurumların isimlerini çekiyoruz***************************/
            baglantim.Open();
            OleDbCommand sorgu = new OleDbCommand("SELECT * FROM kurumlar", baglantim);
            OleDbDataReader kayitokuma = sorgu.ExecuteReader();
            comboBoxsirket.Items.Add("-Company-");
            while (kayitokuma.Read())
            {
                comboBoxsirket.Items.Add(kayitokuma["sirketadi"]);
            }
            baglantim.Close();
           //***************************************************
            comboBoxsirket.SelectedIndex = 0;

        }

        private void comboBoxaracplaka_Click(object sender, EventArgs e)
        {
            comboBoxaracplaka.Items.Clear();
            /********************Arac plakalarını çekiyoruz***************************/
            baglantim.Open();
            OleDbCommand query = new OleDbCommand("SELECT * FROM araclar", baglantim);
            OleDbDataReader kayitoku = query.ExecuteReader();
            while (kayitoku.Read())
            {
                comboBoxaracplaka.Items.Add(kayitoku["plaka"]);
            }
            baglantim.Close();
            /***********************************************************************/
            comboBoxaracplaka.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close(); // açık kalabilir idye kapatıryoruz
            /* Kişi sisteme kaydolma kodları burada yazacaktır*/
            String girisSaat,cikisSaat;
            String deviceID="";
            String plaka;

            if (radioButtonbaretvar.Checked) {deviceID = textBoxcihazid.Text;}
            else {deviceID = "";}

           

            if (radioButtonsinirsiz.Checked){girisSaat = "Sınırsız"; cikisSaat = "Sınırsız"; }
            else{ girisSaat = dateTimeGirisSaati.Value.ToString("hh:mm tt");cikisSaat = dateTimeCikisSaati.Value.ToString("hh:mm tt");}
            if (radioButtonaracvar.Checked){plaka = comboBoxaracplaka.SelectedItem.ToString();}
            else{ plaka = ""; }
          
            bool kayitdurum = false;
            bool rfidkontrol = false;
            if (inputkontrol()==true)
            {
                // Kimlik numarasını veritabanından sorguluyoruz
                baglantim.Open();
                OleDbCommand sorgu = new OleDbCommand("SELECT * FROM personeller WHERE kimlikNo='" + textBoxtcNo.Text + "'", baglantim);
                OleDbDataReader kayitokuma = sorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitdurum = true; break;
                }

                // RFİD NOyu veritabanından sorguluyoruz
               
                OleDbCommand rfidsorgu = new OleDbCommand("SELECT * FROM personeller WHERE rfidNo='" + textBoxrfidNo.Text + "'", baglantim);
                OleDbDataReader rfidoku = rfidsorgu.ExecuteReader();
                while (rfidoku.Read())
                {
                    rfidkontrol = true; break;
                }

                baglantim.Close(); // veritabanı baglantisini kapat
                inputkontrol();
                bool baretIDstate = false;
                if (deviceID != "")  // baret daha önceden kayıtlı mı değil mi?
                {
                    baglantim.Open();
                    OleDbCommand baretsorgu = new OleDbCommand("SELECT *FROM personeller WHERE baretID='"+deviceID+"'", baglantim);
                    OleDbDataReader okunan = baretsorgu.ExecuteReader();
                    while (okunan.Read())
                    {

                        baretIDstate = true;
                        break;
                    }
                    baglantim.Close();
                }


                if (kayitdurum == false) // TC no Kaydı Yok ise devam et
                {
                    if (rfidkontrol == false) // KAyıtlı bir RFİD numarası değil ise
                      { 
                        inputkontrol(); // zorunlu girişler kontrol ediliyor 

                      


                        if (resimYolu != "" && textBoxtcNo.Text != "" && textBoxadi.Text != "" && textBoxsoyadi.Text != "" && textBoxulke.Text != ""
                        && textBoxsehir.Text != "" && textBoxmeslek.Text != "" && textBoxrfidNo.Text != "" && textBoxsigortaNo.Text != "" && baretIDstate==false)
                        {

                            try
                            {
                                baglantim.Open();
                                OleDbCommand kisikaydet = new OleDbCommand("INSERT INTO personeller (kimlikNo,adi,soyadi,dogumTarihi,ulke,sehir,evAdresi,cepNo,email,sigortaNo,rfidNo,egitimduzeyi,meslegi,sirket,gorev,gorevSuresi,girisSaati,cikisSaati,evrakNo,aracGirisIzni,plaka,baretKullanimi,baretIzinKarti,baretID) VALUES('"
                                + textBoxtcNo.Text + "','" + textBoxadi.Text + "','" + textBoxsoyadi.Text + "','" + dateTimePickerDtarihi.Value.ToString("d.M.yyyy") + "','" +
                                textBoxulke.Text + "','" + textBoxsehir.Text + "','" + textBoxevAdres.Text + "','" + textBoxcepNo.Text + "','" + textBoxmail.Text + "','" +
                                textBoxsigortaNo.Text + "','" + textBoxrfidNo.Text + "','" + textBoxokul.Text + "','" + textBoxmeslek.Text + "','" + comboBoxsirket.SelectedItem.ToString() + "','" +
                                textBoxgorevi.Text + "','" + textBoxgorevsuresi.Text + "','" + girisSaat + "','" + cikisSaat + "','" + textBoxtcNo.Text + "','" + radioButtonaracvar.Checked.ToString() + "','" +
                                plaka + "','" + radioButtonbaretvar.Checked.ToString() + "','" + radioButtonbaretizinvar.Checked.ToString() + "','" + deviceID + "')", baglantim);
                                kisikaydet.ExecuteNonQuery();
                                baglantim.Close();

                                if (!Directory.Exists(Application.StartupPath + "\\data\\personimage"))
                                {
                                    Directory.CreateDirectory(Application.StartupPath + "\\data\\personimage");
                                }
                                else
                                {
                                    pictureBoxuser.Image.Save(Application.StartupPath + "\\data\\personimage\\" + textBoxtcNo.Text + ".jpg");
                                }
                                //******************** baret tablo oluşturma******************

                                if (deviceID != "")
                                {
                                    databasebaret.Open();
                                    OleDbCommand tablocreat = new OleDbCommand(("CREATE TABLE " + textBoxtcNo.Text + "(" + " logdata varchar,logdate varchar,helmetid varchar,savedate varchar)"), databasebaret);
                                    tablocreat.ExecuteNonQuery();
                                    databasebaret.Close();
                                }
                               
                                //************************************************************
                                MessageBox.Show("Kişi başarı ile kaydedildi !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                resimYolu = "";
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
                            if (baretIDstate == true)
                            {
                                MessageBox.Show("Bu baret ID numarası Başka bir kullanıcıya aittir!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Resim seçin ve Kırmızı olan alanları kontrol edin !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else // KAyıtlı bir RFID numarası var ise
                    {
                        MessageBox.Show("Kayıtlı RFID Numarası!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

            }
            else // Kayıtlı bir bir TC numarası var ise
            {
                MessageBox.Show("Kayıtlı Kimlik Numarası!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            }
            else
            {
                inputkontrol();
               
            }

        }

        private void textBoxtcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;    //Sadece rakam girmesine izin veriyoruz
            if (!Char.IsDigit(chr) && chr!=8)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Rakam Girebilirsiniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar; //Sadece rakam girmesine izin veriyoruz
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Rakam Girebilirsiniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar; //Sadece rakam girmesine izin veriyoruz
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Rakam Girebilirsiniz !", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

     

        private void radioButtonsinirli_CheckedChanged(object sender, EventArgs e)
        {
             dateTimeGirisSaati.Enabled = true; dateTimeCikisSaati.Enabled = true; 
             
        }

        private void radioButtonsinirsiz_CheckedChanged(object sender, EventArgs e)
        {
            dateTimeGirisSaati.Enabled = false; dateTimeCikisSaati.Enabled = false;
        }

        private void buttonfolder_Click(object sender, EventArgs e)
        {
            iTextSharp.text.Document pdfDosya = new iTextSharp.text.Document();
            OpenFileDialog Resimsec = new OpenFileDialog();
            Resimsec.Title = "Evrakları seçiniz";
            Resimsec.Filter = "JPG Dosyaları (*.jpg) | *.jpg";
            Resimsec.Multiselect = true; // birden çok dosya seçmek için
            if (Resimsec.ShowDialog() == DialogResult.OK)
            {
                string[] files = Resimsec.FileNames;

                PdfWriter.GetInstance(pdfDosya, new FileStream("data/personpdf/" + textBoxtcNo.Text+ ".pdf", FileMode.Create));

                for (int i=0;i<files.Length;i++)
                {
                    pdfDosya.Open();
                    Uri yol = new Uri(files[i]);
                    iTextSharp.text.Jpeg resim = new iTextSharp.text.Jpeg(yol);
                    resim.Alignment = Element.ALIGN_CENTER;
                    resim.ScaleToFit(600f, 800f);
                    resim.SpacingBefore=1f;

                    pdfDosya.Add(resim);
                    pdfDosya.NewPage();
                }

                
                pdfDosya.Close();
            }


            

        }

        private void radioButtonbaretvar_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxbaretkkulaniyor.Enabled = true;
            textBoxcihazid.Text = baretID;
        }

        private void radioButtonbaretyok_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxbaretkkulaniyor.Enabled = false;
            textBoxcihazid.Clear();
        }

        private void radioButtonaracyok_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxaracplaka.Enabled = false;
            linkLabelaracekle.Enabled = false;
        }

        private void radioButtonaracvar_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxaracplaka.Enabled = true;
            linkLabelaracekle.Enabled = true;
        }

        private void linkLabelaracekle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aracekle frm3 = new aracekle();
            frm3.Show();
        }

        private void linkLabelsirketekle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            kurumekle frm3 = new kurumekle();
            frm3.Show();
        }
    }
}
