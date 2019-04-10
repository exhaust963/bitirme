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
using bitirme;

namespace EnSon
{
    public partial class ÖğrenciBireyselSayfa : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Password=123;Database=test;Port=3306");
        MySqlCommand cmd = new MySqlCommand();
      
        string egitmenisim2;
        string ogrenciisim2;
        int min2;
        string date2;
        public ÖğrenciBireyselSayfa(string egitmenisim,string ogrenciisim,int min,string date)
        {
            egitmenisim2 = egitmenisim;
            ogrenciisim2 = ogrenciisim;
            min2 = min;
            date2 = date;
            con.Open();
            cmd.Connection = con;
            InitializeComponent();
            label9.Text = ogrenciisim2;
            cmd.CommandText = "select Distinct O.OgrenciNo from ogrenci O where O.Ogrenciisim='"+ogrenciisim2+ "'";
            String b =cmd.ExecuteScalar().ToString();
            label10.Text = b;
            cmd.CommandText = "select Distinct O.OgrenciDönem from ogrenci O,yoklama Y where Y.DersId=" + min2 + " and O.Ogrenciisim='" + ogrenciisim2 + "'";
            String c = cmd.ExecuteScalar().ToString();
            label11.Text = c;
            cmd.CommandText = "select Distinct Y.yoklama from ogrenci O,yoklama Y where  Y.DersId=" + min2 + " and O.Ogrenciisim='" + ogrenciisim2 + "' and Y.tarih='" + date2 + "'and O.OgrenciId=Y.OgrenciId";
            object val = cmd.ExecuteScalar();
            if (val != null)
            {
                cmd.CommandText = "select Distinct Y.yoklama from ogrenci O,yoklama Y where Y.DersId=" + min2 + " and O.Ogrenciisim='" + ogrenciisim2 + "' and Y.tarih='" + date2 + "'";
                String d = cmd.ExecuteScalar().ToString();

                if (d == "1")
                {
                    rd1.Checked = true;
                }
                else
                {
                    rd2.Checked = true;
                }
            }
            if(val==null)
            {
                rd1.Checked = false;
                rd2.Checked = true;
            }
            int e;
            cmd.CommandText = "Select Distinct I.Durum from ogrenci O,ilgi I where I.OgrenciId=O.OgrenciId and I.DersId=" + min2 + " and O.Ogrenciisim='" + ogrenciisim2 + "'and I.tarih='" + date2 + "'";
            object val3 = cmd.ExecuteScalar();
            if (val3 == null)
            {
                e = 0;
            }
            else
            {
                cmd.CommandText = "Select Distinct I.Durum from ogrenci O,ilgi I where I.OgrenciId=O.OgrenciId and I.DersId=" + min2 + " and O.Ogrenciisim='" + ogrenciisim2 + "'and I.tarih='" + date2 + "'";
                e = int.Parse(cmd.ExecuteScalar().ToString());
            }
            numericUpDown1.Value = e;

                cmd.CommandText = "select Distinct OgrenciGörüş from ogrenci  where Ogrenciisim='" + ogrenciisim2 + "'";
            object val2 = cmd.ExecuteScalar();
            if (val2 != null)
            {
                cmd.CommandText = "select Distinct OgrenciGörüş from ogrenci  where Ogrenciisim='" + ogrenciisim2 + "'";
                string gor = cmd.ExecuteScalar().ToString();
                textBox1.Text = gor;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void ÖğrenciBireyselSayfa_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Anamenü s = new Anamenü(egitmenisim2, min2, date2);
            s.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Dispose();
            SınıfListesi s = new SınıfListesi(egitmenisim2, min2, date2);
            s.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DuyuruListe d = new DuyuruListe(egitmenisim2, min2, date2);
            d.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Giriş g2 = new Giriş();
            g2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a;
            int b = int.Parse(numericUpDown1.Value.ToString());
            if (rd1.Checked == true)
            {
                a = 1;
            }
            else
            {
                a = 0;
            }
            cmd.CommandText= "select Distinct O.OgrenciId from dersler D, ogrenci O,alınan_dersler AD, ilgi I where AD.DersId = " + min2 + " and AD.OgrenciId = O.OgrenciId and O.Ogrenciisim = '" + ogrenciisim2 + "'";
            string ıd = cmd.ExecuteScalar().ToString();

            cmd.CommandText = "Select Distinct Y.yoklama from ogrenci O,yoklama Y where Y.OgrenciId=O.OgrenciId and Y.DersId=" + min2 + " and O.Ogrenciisim='" + ogrenciisim2 + "'and Y.tarih='" + date2 + "'";
            object val3 = cmd.ExecuteScalar();
            if (val3 == null)
            {
                cmd.CommandText="INSERT INTO yoklama (`Tarih`, `DersId`, `OgrenciId`, `Yoklama`) VALUES('"+date2+"', '"+min2+"', '"+ıd+"', '"+a+"')";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText = "UPDATE yoklama SET `yoklama` = '" + a + "'WHERE DersId='" + min2 + "' and OgrenciId='" + ıd + "'and Tarih='" + date2 + "'";
                cmd.ExecuteNonQuery();

            }
            cmd.CommandText = "Select Distinct I.Durum from ogrenci O,ilgi I where I.OgrenciId=O.OgrenciId and I.DersId=" + min2 + " and O.Ogrenciisim='" + ogrenciisim2 + "'and I.tarih='" + date2 + "'";
            object val = cmd.ExecuteScalar();
            if (val == null)
            {
                cmd.CommandText = "INSERT INTO ilgi (`Tarih`, `DersId`, `OgrenciId`, `Durum`) VALUES ('"+date2+"', '"+min2+"', '"+ıd+"', '"+b+"')";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText = "UPDATE ilgi SET `durum` = '" + b + "'WHERE DersId='" + min2 + "' and OgrenciId='" + ıd + "'and Tarih='" + date2 + "'";
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Güncellenmiştir.");

            this.Dispose();
            ÖğrenciBireyselSayfa say = new ÖğrenciBireyselSayfa(egitmenisim2,ogrenciisim2,min2,date2);
            say.ShowDialog();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "select Distinct O.OgrenciId from dersler D, ogrenci O,alınan_dersler AD, ilgi I where I.DersId = AD.DersId and AD.DersId = " + min2 + " and AD.OgrenciId = O.OgrenciId and O.Ogrenciisim = '" + ogrenciisim2 + "' and I.tarih = '" + date2 + "'";
            string ıd = cmd.ExecuteScalar().ToString();
            if (textBox1.Text != null)
            {
                string b = textBox1.Text;
                cmd.CommandText = "UPDATE ogrenci SET `OgrenciGörüş` = '" + b + "'WHERE  OgrenciId='" + ıd + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Güncellenmiştir.");

                this.Dispose();
                ÖğrenciBireyselSayfa say = new ÖğrenciBireyselSayfa(egitmenisim2, ogrenciisim2, min2, date2);
                say.ShowDialog();
            }
        }
    }
}
