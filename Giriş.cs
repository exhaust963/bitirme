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
    public partial class Giriş : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Password=123;Database=test;Port=3306");
        MySqlCommand cmd = new MySqlCommand();

        public Giriş()
        {
            InitializeComponent();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.Connection = con;

            try
            {
                int d = 0;
                for (int i = 0; i < textBox1.Text.Length; i++)
                    if (textBox1.Text[i] == '#')
                    {
                        label2.Text = "SQL INJECTİON";
                        d = 1;
                    }
                for (int i = 0; i < textBox2.Text.Length; i++)
                    if (textBox2.Text[i] == '#')
                    {
                        label2.Text = "SQL INJECTİON";
                        d = 1;
                    }
                if (d == 0)
                {
                    cmd.CommandText = "select count(*) from test.eğitmen where KullanıcıAdı='" + textBox1.Text + "' and Şifre='" + textBox2.Text + "'";
                    int valor = int.Parse(cmd.ExecuteScalar().ToString());
                    if (valor == 1)
                    {
                        this.Hide();
                        cmd.CommandText = "select EgitmenIsımSoyısım from test.eğitmen where KullanıcıAdı='" + textBox1.Text + "'";
                        String isim = cmd.ExecuteScalar().ToString();
                        cmd.CommandText = "select MIN(D.DersId) from eğitmen E,dersler D,verilen_dersler VD where EgitmenIsımSoyısım='" + isim + "'and VD.DersId=D.DersId and E.EgitmenId=VD.EgitmenId";
                        int min = int.Parse(cmd.ExecuteScalar().ToString());
                        string date= DateTime.Now.ToString("yyyy.MM.dd");
                        Anamenü g = new Anamenü(isim,min,date);
                        g.ShowDialog();
                    }
                    else { MessageBox.Show("Yanlış Kullanıcı Adı veya Şifre"); }
                }
            }
            catch (Exception ex) { label2.Text = "Hata" + ex; }
            con.Close();
        }
    }
}
