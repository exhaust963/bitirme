using EnSon;
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

namespace bitirme
{
    public partial class DuyuruSil : Form
    {
        int min2;
        int a;
        string date2;
        String isim2;
        MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Password=123;Database=test;Port=3306");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader myreader;
        public DuyuruSil(String isim,int min,string date)
        {
            date2 = date;
            min2 = min;
            InitializeComponent();
            con.Open();
            cmd.Connection = con;
            isim2 = isim;
            cmd.CommandText = "Select MIN(DuyuruId) from duyuru D,eğitmen E where E.EgitmenIsımSoyısım='" + isim2 + "'and D.EgitmenId=E.egitmenId";
            a = int.Parse(cmd.ExecuteScalar().ToString());
            numericUpDown1.Value = a;
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int a2 = int.Parse(numericUpDown1.Value.ToString());
                cmd.CommandText = "Select EgitmenId from eğitmen where EgitmenIsımSoyısım='" + isim2 + "'";
                int EgitmenId = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "Select * From duyuru WHERE (`DuyuruId` = '" + a2 + "') and (`EgitmenId` = '" + EgitmenId + "')";
                myreader = cmd.ExecuteReader();
                cmd.CommandText = "Delete From duyuru WHERE (`DuyuruId` = '" + a2 + "') and (`EgitmenId` = '" + EgitmenId + "')";
                if (!myreader.HasRows) {
                    MessageBox.Show("Belirtilen Id değerine sahip bir duyuru yok veya duyuru size ait değil");
                    myreader.Close();
                }
                else{
                    myreader.Close();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Silinmiştir.");
                }
   
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Silinememiştir.");
            }

            this.Dispose();
            DuyuruListe d = new DuyuruListe(isim2,min2,date2);
            d.ShowDialog();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int counter = 0;
            try
            {
                int a2 = int.Parse(numericUpDown1.Value.ToString());
                cmd.CommandText = "Select DuyuruAciklama from duyuru D,eğitmen E where E.EgitmenIsımSoyısım='" + isim2 + "'and D.DuyuruId='" + a2 + "' and E.EgitmenId=D.EgitmenId";
                object val = cmd.ExecuteScalar();
                if (val == null || val == DBNull.Value)
                {
                    counter = 1;
                    if (a > a2)
                    {
                        cmd.CommandText = "Select MAX(DuyuruId) from duyuru D,eğitmen E where E.EgitmenIsımSoyısım='" + isim2 + "'and D.EgitmenId=E.egitmenId and DuyuruId <'" + a + "'";
                        object val2 = cmd.ExecuteScalar();
                        if (val2 == null || val2 == DBNull.Value)
                        {
                            MessageBox.Show("Daha düşük ID ye sahip duyuru yoktur.!!");
                        }
                        else
                        {
                            a = int.Parse(cmd.ExecuteScalar().ToString());
                        }
                    }
                    else if (a < a2)
                    {
                        cmd.CommandText = "Select MIN(DuyuruId) from duyuru D,eğitmen E where E.EgitmenIsımSoyısım='" + isim2 + "'and D.EgitmenId=E.egitmenId and DuyuruId >'" + a + "'";
                        object val3 = cmd.ExecuteScalar();
                        if (val3 == null || val3 == DBNull.Value)
                        {
                            MessageBox.Show("Daha yüksek ID ye sahip duyuru yoktur.!!");
                        }
                        else
                        {
                            a = int.Parse(cmd.ExecuteScalar().ToString());
                        }
                    }
                }
                if (counter == 0)
                {
                    cmd.CommandText = "Select DuyuruAciklama from duyuru D,eğitmen E where E.EgitmenIsımSoyısım='" + isim2 + "'and D.DuyuruId='" + a2 + "' and E.EgitmenId=D.EgitmenId";
                    numericUpDown1.Value = a2;
                    a = a2;
                }
                else
                {
                    cmd.CommandText = "Select DuyuruAciklama from duyuru D,eğitmen E where E.EgitmenIsımSoyısım='" + isim2 + "'and D.DuyuruId='" + a + "' and E.EgitmenId=D.EgitmenId";
                    numericUpDown1.Value = a;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}