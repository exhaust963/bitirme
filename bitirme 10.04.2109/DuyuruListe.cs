using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnSon;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace bitirme
{
    public partial class DuyuruListe : Form
    {
        int min2;
        String isim2;
        string date2;
        MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Password=123;Database=test;Port=3306");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader myreader;
        public DuyuruListe(String isim,int min,string date)
        {
            date2 = date;
            min2 = min;
            InitializeComponent();
            con.Open();
            cmd.Connection = con;
            isim2 = isim;
            try
            {
                cmd.CommandText = "select * from eğitmen E,duyuru D where EgitmenIsımSoyısım='" + isim + "'and E.EgitmenId=D.EgitmenId";
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    String duyuruId = myreader.GetString("DuyuruId");
                    listBox1.Items.Add(duyuruId);
                    String Aciklama = myreader.GetString("DuyuruAciklama");
                    listBox2.Items.Add(Aciklama);
                }
                myreader.Close();
            }
            catch (Exception ex1)
            {
                String a = ex1.Message;
                listBox1.Items.Add("Hata");
                listBox2.Items.Add("Hata");
                hata.Text = "Dersler listesindeki hata \n" + a;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Duyurular d = new Duyurular(isim2,min2,date2);
            d.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            cmd.CommandText = "select MIN(D.DersId) from eğitmen E,dersler D,verilen_dersler VD where EgitmenIsımSoyısım='" + isim2 + "'and VD.DersId=D.DersId and E.EgitmenId=VD.EgitmenId";
            min2 = int.Parse(cmd.ExecuteScalar().ToString());
            Anamenü ana2 = new Anamenü(isim2, min2, date2);
            ana2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            SınıfListesi s = new SınıfListesi(isim2,min2,date2);
            s.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Giriş g2 = new Giriş();
            g2.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DuyuruDuzenle g2 = new DuyuruDuzenle(isim2,min2,date2);
            g2.ShowDialog();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DuyuruSil g2 = new DuyuruSil(isim2,min2,date2);
            g2.ShowDialog();
        }
    }
}
