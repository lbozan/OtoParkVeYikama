    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Data.OleDb;
    namespace Otomosyon
    {
    public partial class Form1 : Form
    {
        // ana çalışma
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            listele(2, null);
            buton();
            cretbak(0);
        }
        int toplamyikama;
        int toplamaylik;
        int simdikiyp;
        int simdikikp;
        DialogResult bak;
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\market.accdb");
        public void toplamhesap()
        {

            int h1 = toplamaylik * simdikiyp;
            int h2 = toplamyikama * simdikikp;
            int top = h1 + h2;

            label31.Text = h1.ToString() + " TL.";
            label32.Text = h2.ToString()+ " TL."; 
            label33.Text = top.ToString() + " TL.";
            button7.Enabled = false;
            

        }
        public void cretbak(int i)
        {
            baglanti.Open();
            if (i == 0)
            {

                OleDbCommand all = new OleDbCommand("select * from ucret", baglanti);
                OleDbDataReader oku = null;
                oku = all.ExecuteReader();
                oku.Read();
                string kir = oku["kiralamaparasi"].ToString();
                string yik = oku["yikamaparasi"].ToString();
                textBox9.Text = kir;
                textBox10.Text = yik;
                simdikikp = Convert.ToInt32(kir);
                simdikiyp = Convert.ToInt32(yik);

                oku.Close();
            }
            else if (i == 1)
            {

                OleDbCommand kayt = new OleDbCommand("Update ucret set yikamaparasi='" + textBox9.Text + "', kiralamaparasi='" + textBox10.Text + "'", baglanti);
                kayt.ExecuteNonQuery();

            }


            baglanti.Close();

        }
        //listeleme işlemi
        public void listele(int i, string c)
        {
                listView1.Items.Clear();
            baglanti.Open();
            OleDbCommand goster;
            if (i > 1)
            {

                goster = new OleDbCommand("select * from dosya", baglanti);
            }
            else
            {
                goster = new OleDbCommand("select * from dosya where plaka like '%" + c + "%'", baglanti);
            }

            OleDbDataReader read = null;
            read = goster.ExecuteReader();
            int toplamsay = 0;
            while (read.Read())
            {
                ListViewItem liste = new ListViewItem(read["plaka"].ToString());
                liste.SubItems.Add(read["adisoyadi"].ToString());
                liste.SubItems.Add(read["giristarihi"].ToString());
                liste.SubItems.Add(read["aracturu"].ToString());
                string yikk = read["yikamas"].ToString();
                liste.SubItems.Add(yikk);
                int aylik = Convert.ToInt32(read["ayliksay"]);
                int ay = DateTime.Now.Month;
                int hesap = ay - aylik;
                if (hesap == 0)
                {
                    liste.SubItems.Add("Porç Yok");
                }
                else
                {
                    toplamaylik += hesap;
                    liste.SubItems.Add(hesap.ToString());
                }
                liste.SubItems.Add(read["iletisim"].ToString());
                listView1.Items.Add(liste);
                toplamyikama += Convert.ToInt32(yikk);
                
                if (i > 1) {

                    

                    toplamsay += 1;
                    
                   
                
                }

              
            }
            toolStripStatusLabel1.Text ="Toplam Kullanıcı : "+ toplamsay.ToString();
            read.Close();
            baglanti.Close();
            
        }
        //2 butan deaktif etme..
        public void buton()
        {
            button2.Enabled = false;
            button4.Enabled = false;
            textBox5.Enabled = false;
            textBox8.Enabled = false;
        }
        // ekleme işlemi
        public void ekle()
        {

            baglanti.Open();
            try
            {
                int ay = DateTime.Now.Month;
                string zaman = DateTime.Now.ToString();

                OleDbCommand ekle = new OleDbCommand("INSERT INTO dosya (plaka,adisoyadi,giristarihi,aracturu,yikamas,iletisim,ayliksay) VALUES ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + zaman + "','" + textBox4.Text.ToString() + "','0','" + textBox6.Text.ToString() + "','" + ay + "')", baglanti);

                ekle.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Aynı Plakalı araç Kaydedilemez !", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                baglanti.Close();
                temizle();
                //string text = textBox1.Text;
                listele(2, null);


            }


        }
        public int yikama = 0;
        public int aylikparaa = 0;
        public void sec(string i)
        {
            if (i == "")
            {
                MessageBox.Show("boş deger gönderdin la");
            }
            else
            {
                yikama = 0;
                aylikparaa = 0;
                baglanti.Open();
                string id = i;
                string text1, text2, text3, text4, text5, text6, text7;
                OleDbCommand bak = new OleDbCommand("select * from dosya where plaka='" + id + "'", baglanti);
                OleDbDataReader oku = null;
                oku = bak.ExecuteReader();
                oku.Read();
                text1 = (oku["plaka"].ToString());
                text2 = (oku["adisoyadi"].ToString());
                text3 = (oku["giristarihi"].ToString());
                text4 = (oku["aracturu"].ToString());
                text5 = (oku["yikamas"].ToString());
                text6 = (oku["iletisim"].ToString());
                text7 = (oku["ayliksay"].ToString());
                int cevir = Convert.ToInt32(text7);
                int aylik = DateTime.Now.Month;
                int baka = aylik - cevir;
                textBox1.Text = text1;
                textBox2.Text = text2;
                textBox3.Text = text3;
                textBox4.Text = text4;
                textBox5.Text = text5;
                textBox6.Text = text6;
                textBox8.Text = baka.ToString();
                label13.Text = text1;
                label14.Text = text2;
                label15.Text = text3;
                label16.Text = text4;
                label17.Text = text5;
                label18.Text = text6;
                textBox3.ReadOnly = false;
                oku.Close();

                hesapla(Convert.ToInt32(text5), baka);

                if (baka != 0)
                {

                    label20.Text = baka.ToString();
                    label21.Text = aylikparaa.ToString() + " TL.";

                }
                else
                {
                    label21.Text = "0";
                    label20.Text = "0";

                }

                label23.Text = "Toplam : " + (yikama + aylikparaa).ToString();
                label19.Text = yikama.ToString() + " TL.";

                baglanti.Close();
                ;

            }
        }
        // aylık ve yıkama hesaplama...

        public void hesapla(int dbpara, int dbaylik)
        {


            OleDbCommand cek = new OleDbCommand("select  yikamaparasi, kiralamaparasi from ucret", baglanti);
            OleDbDataReader okum = null;
            okum = cek.ExecuteReader();
            okum.Read();

            yikama = dbpara * Convert.ToInt32(okum["yikamaparasi"]);
            aylikparaa = dbaylik * Convert.ToInt32(okum["kiralamaparasi"]);

            okum.Close();

        }
        // ekle buttonu 
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToString();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "")
            {

                MessageBox.Show("olmaz öle");

            }
            else
            {
                ekle();
            }
        }
        //boş
        private void button4_Click(object sender, EventArgs e)
        {

        }
        //arama 
        private void button3_Click(object sender, EventArgs e)
        {



            string text = textBox7.Text;
            listele(0, text);

        }
        // tıklanmada ilk olay...
        private void listView1_Click(object sender, EventArgs e)
        {
            
            textBox5.Enabled = true;
            textBox8.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            string i = listView1.SelectedItems[0].Text;
            sec(i);

            button1.Enabled = false;
        }
        // bilgileri günçeleme..
        public void guncel()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Boş Kuru Bırakmayın..!");
            }
            else
            {
                baglanti.Open();
                OleDbCommand guncelle = new OleDbCommand("Update dosya Set  adisoyadi='" + textBox2.Text.ToString() + "', aracturu='" + textBox4.Text.ToString() + "', yikamas='" + textBox5.Text.ToString() + "', iletisim='" + textBox6.Text.ToString() + "' where plaka ='" + textBox1.Text.ToString() + "'", baglanti);
                guncelle.ExecuteNonQuery();
                baglanti.Close();
                listele(2, "");
            }
        }
        // günçelemeyi başlatan button..
        private void button2_Click(object sender, EventArgs e)
        {
            bak = MessageBox.Show("Güçelemek istediğinden eminsen", "Onaylamak lazım", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (bak == DialogResult.Yes)
            {

                guncel();
            }
        }
        // silme buttonu
        private void button4_Click_1(object sender, EventArgs e)
        {

            bak = MessageBox.Show("Silmek İstediğinizden Eminmisin Mahmut Abe..", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (bak == DialogResult.Yes)
            {
                baglanti.Open();
                OleDbCommand sil = new OleDbCommand("delete * from dosya where plaka='" + textBox1.Text.ToString() + "'", baglanti);
                sil.ExecuteNonQuery();
                baglanti.Close();
                listele(3, "");

            }
            temizle();


        }
        //boş
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        
        //boş
        private void button5_Click(object sender, EventArgs e)
        {

        }
        // boş
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        // verileri listeleme ve textboxları temizleme buttonu...
        private void button5_Click_1(object sender, EventArgs e)
        {
            listele(2, null);
            temizle();
        }
        // boş
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // verileri listeleme ve textboxları temizleme fonksiyonu...
        public void temizle()
        {

            buton();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "Otomotik tarihleme";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            button1.Enabled = true;
        }
        // temizleme...

        // bakılaçak...
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.F5)
            {
                listele(2, null);
            }
        }
        // boş
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            cretbak(1);
            listele(2, null);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void Kapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Küçült_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            listele(2, null);
            toplamhesap();
         
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2011 .NET 2.0 Tüm Haklar Mahmut ERİN'indir.\n\nCoder ->Dynamic :}","Kısaca Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

    }
    }
