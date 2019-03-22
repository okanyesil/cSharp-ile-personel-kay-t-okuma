
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace G171210057
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Form elemanlarıma methodlar ve form load sırasında erişebilmek için global olarak tanımladım.
        RichTextBox rchText = new RichTextBox();
        GroupBox grpDetay = new GroupBox();//personel detay
        GroupBox grp1 = new GroupBox();//personel içe aktarma
        GroupBox arama = new GroupBox();
        Button btn = new Button();
        TextBox[,] txt = new TextBox[5, 4];//satır,sütün
        Label[,] lbl = new Label[5, 4];
        TextBox txtArama = new TextBox();
        Label lblAra = new Label();
        Button btnAra = new Button();
        string[,] label_ad = { { "TC:", "Evlilik D.", "Fzla Mesai S:", "bürüt Maas:" }, { "Adi:", "Eşi C. M.", "Fzla Mesai Ü:", "Damga Vergisi:" }, { "Soyadı:", "Çocuk S.:", "Taban M:", "Gelir Vergisi" }, { "Yaşı:", "Makam T:", "Vergi M:", "emeklilik kesintisi" }, { "Çalışma S.:", "İdari G:", "Resim Y:", "Net Maaş" } };
        Label[] lblMaas = new Label[5];

        private void Form1_Load(object sender, EventArgs e)
        {

            this.BackColor = Color.LightSeaGreen;
            this.Font = new Font("corbel", 12);

            //form elemanlarının yükseklik özellikliklerini tuttuğum yer
            grpDetay.Height = 300;
            grp1.Height = 300;
            txtArama.Height = 30;
            arama.Height = 300;
            rchText.Height = 220;
            btn.Height = 50;
            this.Height = 700;
            lblAra.Height = 30;
            btnAra.Height = 40;




            //Form elemanlarının genişlik değerlerini tuttğum yer
            grpDetay.Width = 848;
            grp1.Width = 600;
            btn.Width = 100;
            arama.Width = 230;
            rchText.Width = 550;
            txtArama.Width = 120;
            lblAra.Width = 60;
            btn.Width = 100;



            //form elemanlarının adlarını tuttuğum yer
            grpDetay.Text = "Personel Bilgileri";
            grp1.Text = "Personel İçe Aktar";
            btn.Text = "içe aktar";
            arama.Text = "Personel Ara";
            lblAra.Text = "TC:";
            btnAra.Text = "Ara";


            //form elemanlarının arka plan renkleri
            btn.BackColor = Color.Red;
            btnAra.BackColor = Color.Yellow;


            //form elemanlarının yükseklik değerleri
            rchText.Top = 60;
            btn.Top = 20;
            grpDetay.Top = 300;
            txtArama.Top = 30;
            lblAra.Top = 33;
            btnAra.Top = 100;




            //form elemanlarının genişlik değerleri
            grp1.Left = 250;
            rchText.Left = 20;
            btn.Left = 470;
            grpDetay.Left = 5;
            arama.Left = 10;
            txtArama.Left = 70;
            lblAra.Left = 20;
            btnAra.Left = 50;



            //butonlara tıklandığında olayların tetiklenmesini sağladığım yer
            btn.Click += Btn_Click;
            btnAra.Click += BtnAra_Click;




            //form elemanlarımın gerekli yerlere eklendiği yer
            grp1.Controls.Add(rchText);
            this.Controls.Add(grp1);//dosya yükleme kısmı
            this.Controls.Add(grpDetay);
            grp1.Controls.Add(btn);
            this.Controls.Add(arama);
            arama.Controls.Add(txtArama);
            arama.Controls.Add(lblAra);
            arama.Controls.Add(btnAra);





            /*
             * 20 tane textboxı form üzerine tek tek eklemek zor olacağından textbox dizisi oluşturarak
             * textboxlarımı forma ekledim.
             * textboxlarımı 4sütün ve 5satır olarak tasarladım.
             * 2.for döngümün her bitişinde left değerini arttırıp top değerini eski haline getirerek hizalamadım.
             */
            int top = 30;
            int left = 100;
            for (int i = 0; i < 4; i++)//sütun
            {
                for (int j = 0; j < 5; j++) //satır
                {
                    txt[j, i] = new TextBox();
                    txt[j, i].Width = 100;
                    txt[j, i].Height = 20;
                    txt[j, i].Top = top;
                    //   txt[j, i].Text = i + ".satır" + j + ".ci sütün";
                    top += 50;

                    txt[j, i].Left = left;
                    grpDetay.Controls.Add(txt[j, i]);

                }
                left += 200;
                top = 30;

            }



            /*
             Yukarıda textboxa yaptığım işlemlerimi burada da yaptım ve hangi textboxsımın hangi değeri 
             tuttuğunu açıklamış oldum.
             */

            int lblTop = 30;
            int lblLeft = 25;
            for (int i = 0; i < 4; i++)//sutun
            {
                for (int j = 0; j < 5; j++)//satır
                {
                    lbl[j, i] = new Label();
                    lbl[j, i].Width = 100;
                    lbl[j, i].Height = 30;
                    lbl[j, i].Top = lblTop;
                    lblTop += 50;
                    lbl[j, i].Left = lblLeft;
                    lbl[j, i].Text = label_ad[j, i];
                    lbl[j, i].AutoSize = false;
                    lbl[j, i].TextAlign = ContentAlignment.MiddleLeft;
                    grpDetay.Controls.Add(lbl[j, i]);
                }
                lblTop = 30;
                lblLeft += 190;
            }

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            /*
             Richtextboxsımdaki veri bir değişkene çekip gerekli filtrelemelerimi yaptım
                Ardından diziye atadım. tc adında bir değişken yaratıp arayacağım txt değerini içine aktardım.
                Dizimde aradığı sonucu bulduğumda yer adındaki değişkene aktarıp gerekli işlemleri yaptıktan
                değerleri textboxlarıma aktardım.
                
             
             */
            string metin;
            metin = rchText.Text;
            string[] satir = metin.Split('\n');
            int boyut = metin.Split('\n').Length;
            string[,] personel = new string[boyut, 15];
            for (int i = 0; i < satir.Length; i++)
            {
                string[] kelime = satir[i].Split(' ');
                for (int j = 0; j < 15; j++)
                {

                    personel[i, j] = kelime[j];

                }
            }

            string tc;
            tc = txtArama.Text;
            int yer = 0;
           
            for (int i = 0; i < satir.Length; i++)
            {
                if (tc == personel[i, 0])
                {
                    yer = i;
                }
               
            }
            

            
            int b = 0;
            for (int i = 0; i < 3; i++)//sutun satıyı
            {
                for (int j = 0; j < 5; j++)//satir sayısı
                {

                    txt[j, i].Text = personel[yer, b];
                    if (b < 14)
                        b++;

                }
            }
            double burutMaas = BurutHesapla(yer, personel);
            double damgaVergisi = DamgaVergisi(burutMaas);
            double gelirVergisi = GelirVergisi(burutMaas);
            double emekliKesintisi = EmekliKesintisi(burutMaas);
            txt[0, 3].Text = burutMaas.ToString();
            txt[1, 3].Text = damgaVergisi.ToString();
            txt[2, 3].Text = gelirVergisi.ToString();
            txt[3, 3].Text = emekliKesintisi.ToString();
            txt[4, 3].Text = NetMaas(burutMaas, emekliKesintisi, gelirVergisi, damgaVergisi).ToString();
            
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            /*dışarıdan  aldığım dosyayı içeriye aktarmak için bu butonu kullandım */
            OpenFileDialog open = new OpenFileDialog();//txt dosyasını açmak için kullandık
            if (open.ShowDialog() == DialogResult.OK)
            {

                string icerik = File.ReadAllText(open.FileName);
                rchText.Text = icerik;



            }



        }
        private double BurutHesapla(int yer, string[,] personel)
        {
            double burutMaas = 0;
            if (personel[yer, 5].ToUpper() == "H")
            {
                burutMaas = Convert.ToInt32(personel[yer, 8]) + Convert.ToInt32(personel[yer, 9]) + Convert.ToInt32(personel[yer, 10]) + (Convert.ToInt32(personel[yer, 7]) * 30) + (Convert.ToInt32(personel[yer, 12]) * Convert.ToInt32(personel[yer, 13]));
            }
            else if (personel[yer, 5].ToUpper() == "E" && personel[yer, 6].ToUpper() == "E")
            {
                burutMaas = Convert.ToInt32(personel[yer, 8]) + Convert.ToInt32(personel[yer, 9]) + Convert.ToInt32(personel[yer, 10]) + (Convert.ToInt32(personel[yer, 7]) * 30) + (Convert.ToInt32(personel[yer, 12]) * Convert.ToInt32(personel[yer, 13]));
            }
            else if (personel[yer, 5].ToUpper() == "E" && personel[yer, 6].ToUpper() == "H")
            {
                burutMaas = Convert.ToInt32(personel[yer, 8]) + Convert.ToInt32(personel[yer, 9]) + Convert.ToInt32(personel[yer, 10]) + (Convert.ToInt32(personel[yer, 7]) * 30) + (Convert.ToInt32(personel[yer, 12]) * Convert.ToInt32(personel[yer, 13]));
                burutMaas += 200;
            }
            return burutMaas;
        }
        private double DamgaVergisi(double burut)
        {
            double damga = 0;
            damga = (burut * 10) / 100;
            return damga;
        }
        private double GelirVergisi(double burut)
        {
            double gelirVergisi = 0;
            if (burut < 10000)
            {
                gelirVergisi = (burut * 15) / 100;
            }
            else if (burut >= 10000 && burut < 20000)
            {

                gelirVergisi = (burut * 20) / 100;
            }
            else if (burut >= 20000 && burut < 30000)
            {
                gelirVergisi = (burut * 25) / 100;
            }
            else if (burut >= 30000)
            {
                gelirVergisi = (burut * 30) / 100;
            }
            return gelirVergisi;
        }
        private double EmekliKesintisi(double burut)
        {
            double EmekliKesintisi = 0;
            EmekliKesintisi = (burut * 15) / 100;
            return EmekliKesintisi;
        }
        private double NetMaas(double burut, double emekli_kesintisi, double gelirVergisi, double damgaVergisi)
        {
            double netMaas = 0;
            netMaas = burut - (emekli_kesintisi + gelirVergisi + damgaVergisi);
            return netMaas;

        }
    }

}
