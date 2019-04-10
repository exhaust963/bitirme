using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bitirme;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EnSon
{
    public partial class Duyurular : Form
    {
        int min2;
        String isim2;
        string date2;
        MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Password=123;Database=test;Port=3306");
        MySqlCommand cmd = new MySqlCommand();
        public Duyurular(String isim,int min,string date)
        {
            date2 = date;
            min2 = min;
            InitializeComponent();
            con.Open();
            cmd.Connection = con;
            isim2 = isim;
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "Select Max(DuyuruId) from duyuru";
                int max=int.Parse(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "select EgitmenId from test.eğitmen where EgitmenIsımSoyısım='" + isim2 + "'";
                int Id= int.Parse(cmd.ExecuteScalar().ToString());
                max = max + 1;
                cmd.CommandText="INSERT INTO duyuru(`DuyuruId`, `DuyuruAciklama`, `EgitmenId`) VALUES((+'"+max+"'),'"+ textBox1.Text+"','"+ Id+"')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kaydedildi!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydedilemedi!!");
                String a = ex.Message;
                hata.Text = a;
            }
            this.Dispose();
            DuyuruListe s = new DuyuruListe(isim2,min2,date2);
            s.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Giriş g2 = new Giriş();
            g2.ShowDialog();
        }
    }
}
