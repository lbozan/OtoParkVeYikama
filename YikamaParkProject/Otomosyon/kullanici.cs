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
    public partial class kullanici : Form
    {
        public kullanici()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {


            Application.Exit();
            
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\market.accdb");

        int bak = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            OleDbCommand sql = new OleDbCommand("select * from panel where id=1", baglanti);
            OleDbDataReader adab = null;
            adab = sql.ExecuteReader();
            adab.Read();
            string isim = adab["user"].ToString();
            string sifre = adab["password"].ToString();
          

            if (textBox1.Text == isim && textBox2.Text == sifre)
            {
                bak = 1;

                Form1 acc = new Form1();
                acc.Show();
               
                this.Close();
               

            }
            else
            {
                bak = 0;
                MessageBox.Show("Hata Kullanıcı Adı ve Şifre Tekrar Girin !","Panel Hatası !",MessageBoxButtons.OK,MessageBoxIcon.Hand);

            }
            adab.Close();
            baglanti.Close();
            
            
            }

        private void kullanici_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bak == 0)
            {

                Application.Exit();
            }
        }

        private void kullanici_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bak == 0)
            {

                Application.Exit();
            }
        }

        private void kullanici_Load(object sender, EventArgs e)
        {

        }
    }
    }
