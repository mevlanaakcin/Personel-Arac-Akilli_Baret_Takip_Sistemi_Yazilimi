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
using iTextSharp;
using iTextSharp.text;   // itex ile pdf oluşturma yapıyoruz
using iTextSharp.text.pdf; // pdf için gerekli sınıfları barındırır
using System.Threading; // thread işlemleri yapmak için ekledik
using System.Data.OleDb;  // veri tabanı kütüphanesi eklendi

namespace helmet_tracking_system
{
    public partial class baretislemleri : Form
    {
        public baretislemleri()
        {
            InitializeComponent();
        }
        void beyazTema()
        {
            this.BackColor = Color.WhiteSmoke;
            pictureBox4.BackColor = Color.LightGray;
            pictureBox5.BackColor = Color.LightGray;
            pictureBox6.BackColor = Color.LightGray;
            pictureBox7.BackColor = Color.LightGray;
            pictureBox9.BackColor = Color.LightGray;
            pictureBox13.BackColor = Color.LightGray;
            label6.BackColor = Color.LightGray;
            label7.BackColor = Color.LightGray;
            label9.BackColor = Color.LightGray;
            label20.BackColor = Color.LightGray;
            labelbaretuser.BackColor = Color.LightGray;
            labelbareterror.BackColor = Color.LightGray;
            labelbaretpass.BackColor = Color.LightGray;
            labelusererror.BackColor = Color.LightGray;
            label1.BackColor = Color.LightGray; label1.ForeColor = Color.Black;
            label4.BackColor = Color.LightGray; label4.ForeColor = Color.Black;
            label10.BackColor = Color.LightGray; label10.ForeColor = Color.Black;
            label12.BackColor = Color.LightGray; label12.ForeColor = Color.Black;
            label13.BackColor = Color.LightGray; label13.ForeColor = Color.Black;
            label15.BackColor = Color.LightGray; label15.ForeColor = Color.Black;
            label17.BackColor = Color.LightGray; label17.ForeColor = Color.Black;
            label18.BackColor = Color.LightGray; label18.ForeColor = Color.Black;
            label22.BackColor = Color.LightGray; label22.ForeColor = Color.Black;
            label24.BackColor = Color.LightGray; label24.ForeColor = Color.Black;
            label35.BackColor = Color.LightGray; label35.ForeColor = Color.Black;
            labelrate.BackColor = Color.LightGray;
            labelpasscount.BackColor = Color.LightGray;
            labelremovecount.BackColor = Color.LightGray;
            listBoxerrorlist.BackColor = Color.LightGray;
            groupBoxhelmetdata.BackColor = Color.LightGray;
            groupBoxhelmetdata.ForeColor = Color.Black;
            //**************null olan labeller************************
            labeladi.BackColor = Color.LightGray;labeladi.ForeColor = Color.Black;
            labelsoyadi.BackColor = Color.LightGray; labelsoyadi.ForeColor = Color.Black;
            labeltcNO.BackColor = Color.LightGray; labeltcNO.ForeColor = Color.Black;
            labelbaretID.BackColor = Color.LightGray; labelbaretID.ForeColor = Color.Black;
            labelRFID.BackColor = Color.LightGray; labelRFID.ForeColor = Color.Black;
            labelmeslek.BackColor = Color.LightGray; labelmeslek.ForeColor = Color.Black;
            labelsirket.BackColor = Color.LightGray; labelsirket.ForeColor = Color.Black;
            labelgorev.BackColor = Color.LightGray; labelgorev.ForeColor = Color.Black;
            labelizinkarti.BackColor = Color.LightGray; labelizinkarti.ForeColor = Color.Black;
            labelraportarih.BackColor = Color.LightGray; labelraportarih.ForeColor = Color.Black;

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
            label3.BackColor = Color.LightGray;label3.ForeColor = Color.Black;
        }

        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=data/database/veritabani.accdb");
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
       void succesRate(string tcNo)
        {
            labelremovecount.Text = "--";
            labelpasscount.Text = "--";
            labelrate.Text = "% --";
            listBoxerrorlist.Items.Clear();


            string sondeger = "SELECT COUNT (*) FROM " + textBoxarama.Text + " WHERE logdata"; // Veri tabanındaki toplam kaydedilen veri sayısını alıyoruz
            string sorgu = "SELECT *FROM " + tcNo; // Veri tabanı sorgusu oluşturuyoruz

            try
            {
                int sayacremove = 0, sayacopen = 0, sayacpass = 0, sayacclose = 0;
                databasebaret.Open();

                OleDbCommand sondate = new OleDbCommand(sondeger, databasebaret); // son kaydedilen tarihin satır numarasını almak için bağlantı başlatıyoruz
                int datacount = Int32.Parse(sondate.ExecuteScalar().ToString());      // son kaydedilen datanın satır numarasını alıyoruz

                OleDbCommand cmd = new OleDbCommand(sorgu, databasebaret);   // son tarih verisini almak için başlantı başlatıyoruz
                OleDbDataReader okunan = cmd.ExecuteReader();
                int i = 0;
                while (okunan.Read())
                {
                    switch (okunan.GetValue(0).ToString())
                    {
                        case "HELMET REMOVE":
                            sayacremove++;
                            break;
                        case "SYSTEM CLOSE":
                            sayacclose++;
                            break;
                        case "SYSTEM OPEN":
                            sayacopen++;
                            break;
                        case "SYSTEM PASS":
                            sayacpass++;
                            break;

                    }
                    if (i == datacount - 1)
                    {
                        labelremovecount.Text = sayacremove.ToString();
                        labelpasscount.Text = sayacpass.ToString();
                        labelrate.Text = "% " + (((sayacopen-sayacremove) * 100) / sayacopen).ToString();
                        //MessageBox.Show("Remove: " + sayacremove + " Close: " + sayacclose + " Open: " + sayacopen + " Pass: " + sayacpass);
                        break;
                    }
                    i++;
                }
                databasebaret.Close();
            }
            catch(Exception)
            {
                databasebaret.Close();
                labelremovecount.Text = "--";
                labelpasscount.Text = "--";
                labelrate.Text = "% --";
                listBoxerrorlist.Items.Clear();

            }

          

           


            
        }

        private void baretislemleri_Load(object sender, EventArgs e)
        {
            labelremovecount.Text = "--";
            labelpasscount.Text = "--";
            labelrate.Text = "% --";
            listBoxerrorlist.Items.Clear();

            if (seciliTema() == "Light Color")
            {
                beyazTema();
            }
            else { }

            int baretKullaniciCount = 0;
            int baretPassCount = 0;

            //profil boş duracağından seçili resim noprofil
            pictureBoxprofil.ImageLocation = (Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
            pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;

            // Veri tabanındaki Baret Kullanıcı sayısını ve Baret İzinli sayısını böyle alıyoruz
            baglantim.Open();
            OleDbCommand sor = new OleDbCommand("SELECT * FROM personeller", baglantim);
            OleDbDataReader kayitokuma = sor.ExecuteReader();
            while (kayitokuma.Read())
            {
                if (kayitokuma["baretKullanimi"].ToString()=="True")
                {
                    baretKullaniciCount++;
                }
                if (kayitokuma["baretIzinKarti"].ToString()=="True")
                {
                    baretPassCount++;
                }
              
            }
            baglantim.Close();

            labelbaretuser.Text = baretKullaniciCount.ToString();
            labelbaretpass.Text = baretPassCount.ToString();
            
            pictureBoxprofil.ImageLocation = (Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
            pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;

        }
        string soncekilentarih;

        string name = "", surname = "", IDnumber = "", deviceID = "", rfidNumber = "",missionDate="",healtnumber="",helmet="", job = "", companyName = "", mission = "", passCard="", enddatereport="";
        private void buttonaramayap_Click(object sender, EventArgs e)
        {
            bool kayit = false;
            if (textBoxarama.Text!="")
            {
                
                baglantim.Open();
                OleDbCommand sorgula = new OleDbCommand("SELECT *FROM personeller WHERE kimlikNo='" + textBoxarama.Text + "'", baglantim);
                OleDbDataReader kayitoku = sorgula.ExecuteReader();
                while (kayitoku.Read())
                {
                    kayit = true;
                    labeltcNO.Text = kayitoku.GetValue(1).ToString(); IDnumber = kayitoku.GetValue(1).ToString();
                    labeladi.Text = kayitoku.GetValue(2).ToString();  name= kayitoku.GetValue(2).ToString();
                    labelsoyadi.Text = kayitoku.GetValue(3).ToString(); surname= kayitoku.GetValue(3).ToString();
                    labelbaretID.Text = kayitoku.GetValue(24).ToString(); deviceID=kayitoku.GetValue(24).ToString();
                    healtnumber = kayitoku.GetValue(10).ToString();
                    missionDate = kayitoku.GetValue(16).ToString();
                    helmet= kayitoku.GetValue(22).ToString();
                    labelRFID.Text = kayitoku.GetValue(11).ToString();    rfidNumber= kayitoku.GetValue(11).ToString();
                    labelmeslek.Text = kayitoku.GetValue(13).ToString(); job= kayitoku.GetValue(13).ToString();
                    labelsirket.Text = kayitoku.GetValue(14).ToString(); companyName=kayitoku.GetValue(14).ToString();
                    labelgorev.Text = kayitoku.GetValue(15).ToString();  mission= kayitoku.GetValue(15).ToString();
                    labelizinkarti.Text = kayitoku.GetValue(23).ToString(); passCard= kayitoku.GetValue(23).ToString();
                    try
                    {
                        pictureBoxprofil.ImageLocation = (Application.StartupPath + "\\data\\personimage\\" + textBoxarama.Text + ".jpg");
                        pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch
                    {
                        pictureBoxprofil.ImageLocation = (Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
                        pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    
                    break;

                }
                baglantim.Close();


                succesRate(textBoxarama.Text); // HELMET DATA HESAPLAMA FONKSİYONU

                if (kayit == false)
                {
                    labeltcNO.Text = "Null";
                    labeladi.Text = "Null";
                    labelsoyadi.Text = "Null";
                    labelbaretID.Text = "Null";
                    labelRFID.Text = "Null";
                    labelmeslek.Text = "Null";
                    labelsirket.Text = "Null";
                    labelgorev.Text = "Null";
                    labelizinkarti.Text = "Null";
                    labelraportarih.Text = "Null";
                    pictureBoxprofil.ImageLocation = (Application.StartupPath + "\\data\\personimage\\noprofil.jpg");
                    pictureBoxprofil.SizeMode = PictureBoxSizeMode.StretchImage;
                    MessageBox.Show(textBoxarama.Text + ", Böyle bir kayıt bulunamadı!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    try { 
                    bool sontarihvalue = false;

                    string sorgu = "SELECT *FROM " + textBoxarama.Text; // son tarih verisini çekmek için kullanıyoruz
                    string sontarih = "SELECT COUNT (*) FROM " + textBoxarama.Text + " WHERE savedate"; // Veri tabanındaki toplam yapılan veri okuma sayısını alıyoruz
                    databasebaret.Open();
                    OleDbCommand sondate = new OleDbCommand(sontarih, databasebaret); // son kaydedilen tarihin satır numarasını almak için bağlantı başlatıyoruz
                    int sayac = Int32.Parse(sondate.ExecuteScalar().ToString());      // son kaydedilen tarihin satır numarasını alıyoruz
                    OleDbCommand kimlikdb = new OleDbCommand(sorgu, databasebaret);   // son tarih verisini almak için başlantı başlatıyoruz
                    OleDbDataReader okunan = kimlikdb.ExecuteReader();                 // son tarih verisini alıyoruz
                    int i = 0;
                    while (okunan.Read())
                    {

                        soncekilentarih = okunan.GetValue(3).ToString();
                        if (i == (sayac - 1))
                        {
                            sontarihvalue = true;
                            labelraportarih.Text = soncekilentarih;
                                enddatereport = soncekilentarih;
                            break;
                        }

                        i++;
                    }
                    databasebaret.Close();
                    if (sontarihvalue == false) {  // eğer hiç veri çekilmemiş ise uyarı veriyoruz
                        labelraportarih.Text = "Null";
                        MessageBox.Show("Bu cihazdan daha önce hiç veri çekilmedi!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    }
                    catch (Exception )
                    {
                        databasebaret.Close();
                    }
                }
               

            }
            else
            {
                MessageBox.Show("Lütfen arama kutusunu boş bırakmayınız!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonraporcreate_Click(object sender, EventArgs e)
        {
            //***************************************** rapor oluşturma ekranı*******************************
            Document doc = new Document();
            if (textBoxarama.Text!="" && doc.IsOpen()==false)
            {
               
                try
                {


                    //PdfWriter.GetInstance(doc, new FileStream("data/" + textBoxarama.Text + ".pdf", FileMode.Create)); // dosyayı nerede oluşturacağımızı belirledik

                    PdfWriter file = PdfWriter.GetInstance(doc, new FileStream("data/" + textBoxarama.Text + ".pdf", FileMode.Create));
                    var font_baslik = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_BOLD, 20f);
                    var font_text = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES, 10f);
                    var font_text_head = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES, 10f,BaseColor.BLUE);

                    doc.Open();




                    //***********************Çerçeve **********************************************************
                    PdfContentByte cb = file.DirectContent;
                    //**********üst çizgi****
                    cb.MoveTo(30f, doc.PageSize.Height - 30f);
                    cb.LineTo(doc.PageSize.Width - 120f, doc.PageSize.Height - 30f);
                    cb.Stroke();
                    //**********sol çizgi*****
                    cb.MoveTo(30f, 30f);
                    cb.LineTo(30f, doc.PageSize.Height - 30f);
                    cb.Stroke();
                    //***********sağ çizgi****
                    cb.MoveTo(doc.PageSize.Width - 120f, 30f);
                    cb.LineTo(doc.PageSize.Width - 120f, doc.PageSize.Height - 30f);
                    cb.Stroke();
                    //***********alt çizgi****
                    cb.MoveTo(30f, 30f);
                    cb.LineTo(doc.PageSize.Width - 120f, 30f);
                    cb.Stroke();
                    //*********************************************************************************************
              
                    //**************************LOGO**************************************************
                    Uri logoyol = new Uri(Application.StartupPath + "\\data\\pdfsource\\logo1.jpg");
                    iTextSharp.text.Jpeg logo = new iTextSharp.text.Jpeg(logoyol);
                    logo.Alignment = Element.MULTI_COLUMN_TEXT | Element.ALIGN_RIGHT;
                    //logo.SpacingBefore = 150f;
                    //logo.ScalePercent(24f);
                    logo.SetAbsolutePosition(doc.PageSize.Width - 120f ,doc.PageSize.Height - 236f );
                    logo.ScaleToFit(100f, 200f);

                    //*********************** TOP LİNE **********************************************************
                    cb.MoveTo(60f, 800f);
                    cb.LineTo(450f,800f);
                    cb.Stroke();
                    //***********************************************************************************************
                    Paragraph baslik = new Paragraph("         Helmet Tracking System Person Report",font_baslik);
                    baslik.Alignment =  Element.ALIGN_LEFT;
                    doc.Add(logo);
                    doc.Add(baslik);

                    
                    Paragraph repotDate = new Paragraph("Report Creat Date: "+DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() , font_text);
                    repotDate.SpacingBefore = 30f;
                    repotDate.Alignment = Element.ALIGN_LEFT;
                    doc.Add(repotDate);
                    cb.MoveTo(35f, 725f);
                    cb.LineTo(160f,725f);
                    cb.Stroke();
                    //*********************** BOTTOM LİNE **********************************************************
                    cb.MoveTo(60f, 765f);
                    cb.LineTo(450f, 765f);
                    cb.Stroke();
                    //*******************************************************************************************
                    //*************ANA Yapı Burada Bitiyor*************** devamı içerik kısmı*****************
                    Uri yol = new Uri(Application.StartupPath + "\\data\\personimage\\"+textBoxarama.Text+".jpg");
                    iTextSharp.text.Jpeg resim = new iTextSharp.text.Jpeg(yol);
                    resim.SetAbsolutePosition(doc.PageSize.Width - 250f, doc.PageSize.Height - 150f);
                    //resim.SpacingAfter = 200f;
                    resim.ScaleToFit(100f, 50f);


                    //resim.Border = iTextSharp.text.Rectangle.BOX;
                   // resim.BorderWidth= 36f;
                   // resim.BorderColor = iTextSharp.text.BaseColor.BLACK;
                    doc.Add(resim);
                    
                    Phrase p1 = new Phrase();
                    Phrase p2 = new Phrase();

                    Paragraph newline = new Paragraph("\n");
                    Chunk bosluk = new Chunk("\t"); //tab boşluğu
                    
                    Chunk VIDnumber = new Chunk(IDnumber, font_text_head);
                    Chunk Vname = new Chunk(name, font_text_head);
                    Chunk Vsurname = new Chunk(surname, font_text_head);
                    Chunk Vjob = new Chunk(job , font_text_head);        
                    Chunk Vcompany = new Chunk(companyName, font_text_head);
                    Chunk Vrfid = new Chunk(rfidNumber, font_text_head);
                    Chunk Vmission = new Chunk(mission, font_text_head);
                    Chunk Vhealt = new Chunk(healtnumber, font_text_head);
                    Chunk Vmissiondate = new Chunk(missionDate, font_text_head);
                    Chunk VdeviceID = new Chunk(deviceID, font_text_head);
                    Chunk Vpasscard = new Chunk(passCard, font_text_head);
                    Chunk Vendreport = new Chunk(enddatereport, font_text_head);
                    doc.Add(bosluk);
                    //***************************PERSON NOTİFİCATİON*************************

                    MultiColumnText columns = new MultiColumnText();
                     //columns.AddSimpleColumn(doc.PageSize.Width - 936f, 836f);
                     //columns.AddSimpleColumn(960f, doc.PageSize.Width - 836f);

                    columns.AddSimpleColumn(doc.PageSize.Width - 156f, 36f);

                    columns.AddSimpleColumn(56f, doc.PageSize.Width - 56f);



                    Paragraph notification = new Paragraph("     Person Notification",font_text);
                    notification.SpacingAfter = 9f;
                    notification.Alignment = Element.ALIGN_JUSTIFIED;

                    PdfPTable table = new PdfPTable(4);
                    float[] widths = new float[] { 1f, 1f, 1f,1f };
                    table.TotalWidth = 400f;
                    table.SpacingAfter = 30f;
                    table.LockedWidth = true;
                    table.SetWidths(widths);
                    
                    table.AddCell("ID Number:");
                    table.AddCell(VIDnumber.ToString());
                    table.AddCell("RFID No:");
                    table.AddCell(Vrfid.ToString());
                    table.AddCell("Name:");
                    table.AddCell(Vname.ToString());
                    table.AddCell("Job:");
                    table.AddCell(Vjob.ToString());
                    table.AddCell("Surname: ");
                    table.AddCell(Vsurname.ToString());
                    table.AddCell("Mission:");
                    table.AddCell(Vmission.ToString());
                    table.AddCell("Healt No:");
                    table.AddCell(Vhealt.ToString());
                    table.AddCell("Mission Date:");
                    table.AddCell(missionDate.ToString());

                    //******************************HELMET NOTİFİCATİON***************************************
                    MultiColumnText col = new MultiColumnText();
                    col.AddSimpleColumn(doc.PageSize.Width - 936f, 836f);
                    col.AddSimpleColumn(960f, doc.PageSize.Width - 836f);

                    Paragraph helmetHeader = new Paragraph("     Helmet Notification", font_text);
                    helmetHeader.SpacingAfter = 9f;
                    helmetHeader.Alignment = Element.ALIGN_JUSTIFIED;

                    PdfPTable table2 = new PdfPTable(4);
                    float[] genislik = new float[] { 1f, 1f, 1f, 1f };
                    table2.TotalWidth = 400f;
                    table2.LockedWidth = true;
                    table2.SetWidths(genislik);

                    table2.AddCell("Device ID:");
                    table2.AddCell(VdeviceID.ToString());
                    table2.AddCell("Pass Stick:");
                    table2.AddCell(Vpasscard.ToString());
                    table2.AddCell("Final Report:");
                    table2.AddCell("NULL");
                    table2.AddCell("Last Read:");
                    table2.AddCell(Vendreport.ToString());
                   


                    columns.AddElement(notification);
                    columns.AddElement(table);
                    columns.Alignment = Element.ALIGN_JUSTIFIED;

                    columns.AddElement(helmetHeader);
                    columns.AddElement(table2);
                    //***************************************************************************************************************
                    Paragraph istatistik = new Paragraph("     Helmet Statistic", font_text);
                    istatistik.SpacingAfter = 9f;
                    istatistik.Alignment = Element.ALIGN_JUSTIFIED;
                    columns.AddElement(bosluk);
                    columns.AddElement(istatistik);


                    doc.Add(columns);

                    




                    doc.Close();
                    MessageBox.Show("Baret Raporu Masaüstüne Oluşturuldu!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception el)
                {
                    MessageBox.Show(el.ToString());
                    doc.Close();

                }
            }
            else
            {
                MessageBox.Show("Lütfen arama kutusunu boş bırakmayınız!", "Helmet Tracking System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttoncikis_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            login frm4 = new login();
            frm4.Show();
        }

        private void buttondasboard_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            dashboard frm4 = new dashboard();
            frm4.Show();
        }

        private void buttonpersonel_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            personelislemleri frm4 = new personelislemleri();
            frm4.Show();
        }

        private void buttonadminpanel_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            adminpanel frm4 = new adminpanel();
            frm4.Show();
        }

        private void buttonkurumislemleri_Click(object sender, EventArgs e)
        {
            this.Close(); //  ekran kapatıldı form 4 çağrıldı
            kurumislemleri frm4 = new kurumislemleri();
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

        private void buttonhakkinda_Click(object sender, EventArgs e)
        {
            
        }

      




        /***************************************************************************/
    }
}
