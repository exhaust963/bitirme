using bitirme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EnSon
{
    public partial class SınıfListesi : Form
    {
        string date2;
        String isim2;
        int min2;
        MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Password=123;Database=test;Port=3306");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader myreader;
        public SınıfListesi(String isim,int min,string date)
        {
            isim2 = isim;
            date2 = date;
            min2= min;
            con.Open();
            cmd.Connection = con;
            InitializeComponent();
            try
            {
                cmd.CommandText = "select Distinct O.OgrenciIsim from eğitmen E,dersler D,verilen_dersler VD,ogrenci O,alınan_dersler AD where EgitmenIsımSoyısım='" + isim + "'and VD.DersId=" + min + " and E.EgitmenId=VD.EgitmenId and AD.DersId=VD.DersId and AD.OgrenciId=O.OgrenciId";
                myreader = cmd.ExecuteReader();
                while (myreader.Read()) {
                    String OgrenciIsim = myreader.GetString("OgrenciIsim");
                    listBox1.Items.Add(OgrenciIsim);
                }
                myreader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            cmd.CommandText = "select MIN(D.DersId) from eğitmen E,dersler D,verilen_dersler VD where EgitmenIsımSoyısım='" + isim2 + "'and VD.DersId=D.DersId and E.EgitmenId=VD.EgitmenId";
            min2 = int.Parse(cmd.ExecuteScalar().ToString());
            Anamenü ana2 = new Anamenü(isim2, min2, date2);
            ana2.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Giriş g2 = new Giriş();
            g2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DuyuruListe d = new DuyuruListe(isim2,min2,date2);
            d.ShowDialog();
        }
    }
}
