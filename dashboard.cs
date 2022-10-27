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
    public partial class dashboard : Form
    {
        String[] portlistesi;
        bool connectDevice = false;
        public dashboard()
        {
            InitializeComponent();
        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");
        OleDbConnection databasebaret = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/helmet.accdb");



        string kayitliTema;
        string kayitliDil;
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
            // menu dil ayarları
            label32.Text = "MENU";
            buttondasboard.Text = "Dashboard";
            buttonkurumislemleri.Text = "Institution Trans.";
            buttonadminpanel.Text = "Admin Panel";
            buttonpersonel.Text = "Personnel Trans.";
            buttonbaretislemleri.Text = "Hard hat Trans.";
            buttonziyaretciislemleri.Text = "Visitor Trans.";
            buttonaracislemleri.Text = "Vehicle Trans.";
            buttonturnikeislemleri.Text = "Turnstile Trans.";
            buttonveritabaniislemleri.Text = "Database Trans.";
            buttonhakkinda.Text = "About";
            buttonayarlar.Text = "Setting";
            buttoncikis.Text = "Exit";
            // profil dil ayarları
            label2.Text = "First Name:";
            label14.Text = "Surname:";
            label17.Text = "ID No:";
            label16.Text = "Mobil Phone Number:";
            label12.Text = "RFID Card Number:";
            label24.Text = "Job:";
            label13.Text = "Affiliate Company:";
            label35.Text = "Task:";
            label33.Text = "Duration of Work:";
            label23.Text = "Intelligent Helmet:";
            label34.Text = "Mandatory Entry Time:";
            label18.Text = "Mandatory Leaving Time:";
            // göstergeler
            label1.Text = "Number of ID cards issued";
            label4.Text = "Number of visitors";
            label6.Text = "Number of entering employees";
            //Bölüm başlıkları
            labelakis.Text = "Daily entry / exit events";
        }
        void almanca()
        {
            // menu dil ayarları
            label32.Text = "MENÜ";
            buttondasboard.Text = "Instrumententafel";
            buttonkurumislemleri.Text = "Institutionen Trans.";
            buttonadminpanel.Text = "Admin Panel";
            buttonpersonel.Text = "Personal Trans.";
            buttonbaretislemleri.Text = "Schutzhelm Trans.";
            buttonziyaretciislemleri.Text = "Besucher Trans.";
            buttonaracislemleri.Text = "Fahrzeug Trans.";
            buttonturnikeislemleri.Text = "Drehkreuz Betrieb";
            buttonveritabaniislemleri.Text = "Datenbank Trans.";
            buttonhakkinda.Text = "Über";
            buttonayarlar.Text = "Einstellungen";
            buttoncikis.Text = "Austritt";
            // profil dil ayarları
            label2.Text = "Vorname:";
            label14.Text = "Name:";
            label17.Text = "Ausweis Nr.:";
            label16.Text = "Handy Nr.:";
            label12.Text = "RFID Karten Nummer:";
            label24.Text = "Beruf:";
            label13.Text = "Angehörige Firma:";
            label35.Text = "Aufgabe:";
            label33.Text = "Dauer der Arbeit:";
            label23.Text = "Inteligente Helm:";
            label34.Text = "Obligatorische Eintrittszeit:";
            label18.Text = "Obligatorische Austrittszeit:";
            // göstergeler
            label1.Text = "Anzahl der ausgestellten ID-Karten";
            label4.Text = "Anzahl der Besucher";
            label6.Text = "Anzahl der eintretende Mitarbeiter";
            //Bölüm başlıkları
            labelakis.Text = "Tägliche Ein- / Ausstiegsereignisse";
        }

        void fransızca()
        {
            // menu dil ayarları
            label32.Text = "MENU";
            buttondasboard.Text = "Tableau de bord";
            buttonkurumislemleri.Text = "Trans. Institution.";
            buttonadminpanel.Text = "Admin Panel";
            buttonpersonel.Text = "Opér. du personnel";
            buttonbaretislemleri.Text = "Options de casque";
            buttonziyaretciislemleri.Text = "Options des visiteurs";
            buttonaracislemleri.Text = "Trans. de véhicules";
            buttonturnikeislemleri.Text = "Opér. de tourniquet";
            buttonveritabaniislemleri.Text = "Trans. de base de données";
            buttonhakkinda.Text = "à propos de";
            buttonayarlar.Text = "Paramètres";
            buttoncikis.Text = "Sortie";
            // profil dil ayarları
            label2.Text = "Prénom:";
            label14.Text = "Nom de famille:";
            label17.Text = "ID No.:";
            label16.Text = "Numéro de téléphone:";
            label12.Text = "Numéro de carte RFID:";
            label24.Text = "Emploi:";
            label13.Text = "Companie partenaire:";
            label35.Text = "Tâche:";
            label33.Text = "Durée des travaux:";
            label23.Text = "Casque intelligent:";
            label34.Text = "Heure d'entrée obligatoire:";
            label18.Text = "Temps de départ obligatoire:";
            // göstergeler
            label1.Text = "Nombre de cartes d'identité émises";
            label4.Text = "Nombre de visiteurs";
            label6.Text = "Nombre d'employés entrants";
            //Bölüm başlıkları
            labelakis.Text = "Événements quotidiens d'entrée / sortie";
        }


        void beyazTema()
        {
            this.BackColor = Color.WhiteSmoke;
            pictureBox4.BackColor = Color.LightGray;
            pictureBox6.BackColor = Color.LightGray;
            pictureBox7.BackColor = Color.LightGray;
            pictureBox9.BackColor = Color.LightGray;
            pictureBox10.BackColor = Color.LightGray;
            pictureBox12.BackColor = Color.LightGray;
            pictureBox14.BackColor = Color.LightGray;
            pictureBox16.BackColor = Color.WhiteSmoke;
            labelprofil.BackColor = Color.LightGray; labelprofil.ForeColor = Color.Black;
            labelvericek.BackColor = Color.LightGray; labelvericek.ForeColor = Color.Black;
            labelakis.BackColor = Color.LightGray; labelakis.ForeColor = Color.Black;
            label10.BackColor = Color.LightGray; label10.ForeColor = Color.Black;
            label1.BackColor = Color.LightGray;
            label3.BackColor = Color.LightGray; label3.ForeColor = Color.Black;
            label4.BackColor = Color.LightGray;
            label6.BackColor = Color.LightGray;
            label2.BackColor = Color.WhiteSmoke; label2.ForeColor = Color.Black;
            label12.BackColor = Color.WhiteSmoke; label12.ForeColor = Color.Black;
            label13.BackColor = Color.WhiteSmoke; label13.ForeColor = Color.Black;
            label14.BackColor = Color.WhiteSmoke; label14.ForeColor = Color.Black;
            label16.BackColor = Color.WhiteSmoke; label16.ForeColor = Color.Black;
            label17.BackColor = Color.WhiteSmoke; label17.ForeColor = Color.Black;
            label18.BackColor = Color.WhiteSmoke; label18.ForeColor = Color.Black;
            label23.BackColor = Color.WhiteSmoke; label23.ForeColor = Color.Black;
            label24.BackColor = Color.WhiteSmoke; label24.ForeColor = Color.Black;
            label33.BackColor = Color.WhiteSmoke; label33.ForeColor = Color.Black;
            label34.BackColor = Color.WhiteSmoke; label34.ForeColor = Color.Black;
            label35.BackColor = Color.WhiteSmoke; label35.ForeColor = Color.Black;
            labeladi.BackColor = Color.WhiteSmoke; labeladi.ForeColor = Color.Black;
            labelsoyadi.BackColor = Color.WhiteSmoke; labelsoyadi.ForeColor = Color.Black;
            labeltcno.BackColor = Color.WhiteSmoke; labeltcno.ForeColor = Color.Black;
            labelcepno.BackColor = Color.WhiteSmoke; labelcepno.ForeColor = Color.Black;
            labelrfidno.BackColor = Color.WhiteSmoke; labelrfidno.ForeColor = Color.Black;
            labelmeslek.BackColor = Color.WhiteSmoke; labelmeslek.ForeColor = Color.Black;
            labelsirket.BackColor = Color.WhiteSmoke; labelsirket.ForeColor = Color.Black;
            labelgorev.BackColor = Color.WhiteSmoke; labelgorev.ForeColor = Color.Black;
            labelgorevsuresi.BackColor = Color.WhiteSmoke; labelgorevsuresi.ForeColor = Color.Black;
            labelbaret.BackColor = Color.WhiteSmoke; labelbaret.ForeColor = Color.Black;
            labelgirissaati.BackColor = Color.WhiteSmoke; labelgirissaati.ForeColor = Color.Black;
            labelcikissaati.BackColor = Color.WhiteSmoke; labelcikissaati.ForeColor = Color.Black;
            label11.BackColor = Color.LightGray; label11.ForeColor = Color.Black;
            label20.BackColor = Color.LightGray; label20.ForeColor = Color.Black;
            label21.BackColor = Color.LightGray; label21.ForeColor = Color.Black;
            label29.BackColor = Color.LightGray; label29.ForeColor = Color.Black;
            label31.BackColor = Color.LightGray; label31.ForeColor = Color.Black;
            labeldeviceID.BackColor = Color.LightGray;labeldeviceID.ForeColor = Color.Black;
            labeldateDevice.BackColor = Color.LightGray;labeldateDevice.ForeColor = Color.Black;
            labelstateDevice.BackColor = Color.LightGray;labelstateDevice.ForeColor = Color.Black;
            labelbattDevice.BackColor = Color.LightGray; labelbattDevice.ForeColor = Color.Black;
            dataGridView1.BackgroundColor = Color.WhiteSmoke; dataGridView1.GridColor = Color.WhiteSmoke; dataGridView1.DefaultCellStyle.BackColor = Color.LightGray;

            labelkimliksayisi.BackColor = Color.LightGray;
            labelziyaretcisayisi.BackColor = Color.LightGray;
            labelgirisyapanpersonel.BackColor = Color.LightGray;
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

        }
        void portlistele()
        {
            comboBoxport.Items.Clear();  //combox içini temizliyoruz
            portlistesi = SerialPort.GetPortNames(); // Serialport isimlerini diziye ekliyoruz
            foreach (string portadi in portlistesi)   //foreach dizilerin uzunluğu kadar çalışır
            {
                comboBoxport.Items.Add(portadi);
                if (portlistesi[0] != null)
                {
                    comboBoxport.SelectedItem = portlistesi[0];
                }
            }


        }

        

       
        private void Form4_Load(object sender, EventArgs e)
        {
            if (seciliTema() == "Light Color")
            {
                beyazTema();
            }
            else { }

            //seçili dil ayarları

            if (seciliDil() == "English")
            {
                ingilizce();
            }
            else if(seciliDil()== "Deutsche")
            {
                almanca();
            }
            else if (seciliDil() == "Français")
            {
                fransızca();
            }



            // Veri tabanındaki RFID Kimlik Kart sayısını böyle alıyoruz
            baglantim.Open();
            OleDbCommand kimlikdb = new OleDbCommand("SELECT COUNT (*) FROM personeller WHERE rfidNo", baglantim);
            labelkimliksayisi.Text = kimlikdb.ExecuteScalar().ToString();
            baglantim.Close();
            if (serialPort1.IsOpen == true)// seriport açık kalmış ise kapat
            {
                serialPort1.Close();
            }
            portlistele();
            this.StartPosition = FormStartPosition.CenterScreen; // Ekranı ortada konumlandırıcaktır
            //FORM 2 ayarları
            if (login.yetki != "Admin")
            {
              
            }
            else
            {
                
            }

            pictureProfildashboard.Image = Image.FromFile(Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
            pictureProfildashboard.SizeMode = PictureBoxSizeMode.StretchImage;
            buttoncollect.Enabled = false;
            this.Text = "Dashboard";
            labelheader.Text = "Helmet Tracking System V:1.0.0 | User: " + login.kullaniciadi + " | " + "Authorization: " + login.yetki;
        }
        private void comboBoxport_Click(object sender, EventArgs e)
        {
            portlistele();
        }

        string deviceID = "";
        private void buttonconnectdevice_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectDevice == false)
                {
                    serialPort1.PortName = comboBoxport.GetItemText(comboBoxport.SelectedItem);
                    serialPort1.BaudRate = 115200;
                    serialPort1.ReadTimeout = 3000; // okuma için belli bir zaman belirleme 
                    serialPort1.Open();
                    buttoncollect.Enabled = true;
                    serialPort1.DiscardOutBuffer(); // giden veri hafızasını temizler
                    serialPort1.DiscardInBuffer(); // gelen veri hafızasını temizler
                    connectDevice = true;
                    comboBoxport.Enabled = false;
                    buttonconnectdevice.Text = "Disconnect";
                    buttonconnectdevice.BackColor = Color.FromArgb(198, 40, 40);

                    serialPort1.Write("1");
                    deviceID = serialPort1.ReadLine();
                    //deviceID = deviceID.Trim();
                    labeldeviceID.Text = deviceID;
                    string devicedate = serialPort1.ReadLine();
                    labeldateDevice.Text = devicedate;
                    string devicestate = serialPort1.ReadLine();
                    labelstateDevice.Text = devicestate;
                    string devicebatt = serialPort1.ReadLine();
                    // fomüle edilmiş % gösterge bildirimi
                    double bat = (((double.Parse(devicebatt)/100)-2.7)/0.0166666666)+10;
                    bat = Math.Floor(bat);
                    labelbattDevice.Text = "% "+bat.ToString();
                
                }
                else
                {
                    labeldeviceID.Text = "Null";
                    labeldateDevice.Text = "Null";
                    labelstateDevice.Text = "Null";
                    labelbattDevice.Text = "Null";
                    serialPort1.Close();
                    buttoncollect.Enabled = false;
                    comboBoxport.Enabled = true;
                    connectDevice = false;
                    buttonconnectdevice.BackColor = Color.FromArgb(0, 230, 118);
                    buttonconnectdevice.Text = "Connect";

                }
            }
            catch(Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Helmet Tracking System Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serialPort1.Close();
                buttoncollect.Enabled = false;
                comboBoxport.Enabled = true;
                connectDevice = false;
                buttonconnectdevice.BackColor = Color.FromArgb(0, 230, 118);
                buttonconnectdevice.Text = "Connect";
            }
        }


        int sayac = 0;
        string kelime;
        string id = "";
        private void buttoncollect_Click(object sender, EventArgs e)
        {

            if (deviceID!="")
            {
                baglantim.Open();
                OleDbCommand baretsorgu = new OleDbCommand("SELECT *FROM personeller WHERE baretID='" + deviceID + "'", baglantim);
                OleDbDataReader okunan = baretsorgu.ExecuteReader();
                while (okunan.Read())
                {
                    
                    id = okunan.GetValue(1).ToString();
                    break;
                    
                }
                MessageBox.Show("ID : " + id);
                baglantim.Close();

                if (id != "")
                {
                    serialPort1.Write("3");
                    timer1.Start();
                }
                else
                {
                    MessageBox.Show("Bu cihaz kayıtlı değildir!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Cihaz kimlik bilgisi alınamadı!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
}
         
       
        private void timer1_Tick(object sender, EventArgs e)
        {

           
                kelime = serialPort1.ReadLine();
                string[] dizi;
                dizi = kelime.Split('|');


                if (kelime.Contains("#")) // gelen veriyi okuduk contains burada bize boolean bir veri geri dönderir
                {
                    kelime = "";
                    //serialPort1.Write("0"); // CİHAZIN İÇİNDEKİ VERİLERİ TEMİZLEME KODU
                                      
                    serialPort1.Close();
                    timer1.Stop();
                    buttoncollect.Enabled = false;
                    comboBoxport.Enabled = true;
                    connectDevice = false;
                    buttonconnectdevice.BackColor = Color.FromArgb(0, 230, 118);
                    buttonconnectdevice.Text = "Connect";
                    labeldeviceID.Text = "Null";
                    labeldateDevice.Text = "Null";
                    labelstateDevice.Text = "Null";
                    labelbattDevice.Text = "Null";
                MessageBox.Show("Verilerin, veri tabanına aktarımı tamamlandı!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
                else
                {


                    try
                    {
                        databasebaret.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into " + id + "(logdata,logdate,helmetid,savedate)" + "values  ('" + dizi[0] + "','" + dizi[1] + "','"+deviceID + "','" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + "')", databasebaret);
                        eklekomutu.ExecuteNonQuery();
                        databasebaret.Close();


                    }
                    catch (Exception hatamesajı)
                    {
                        MessageBox.Show(hatamesajı.Message);
                        databasebaret.Close();
                    }

                    sayac++;


                }
          
        }
        private void buttonclose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)// seriport açık kalmış ise kapat
            {
                serialPort1.Close();
            }
            Application.Exit();
        }

        private void buttonminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       

        private void buttonadminpanelpage_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            adminpanel frm4 = new adminpanel();
            frm4.Show();
        }

       

        private void buttonkurumislemleri_Click(object sender, EventArgs e)
        {
            this.Close();  //  ekran kapatıldı 
            kurumislemleri frm3 = new kurumislemleri();
            frm3.Show();
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

        private void buttoncikis_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            login frm4 = new login();
            frm4.Show();
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








        /***************************************************************************/
    }
}
