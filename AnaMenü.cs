using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bitirme;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EnSon
{
    public partial class Anamenü : Form
    {
        int min2;
        string date2;
        String isim2;
        MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Password=123;Database=test;Port=3306");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader myreader;
        MySqlDataAdapter da;
        public Anamenü(String isim,int min,String date)
        {
            
            
            InitializeComponent();
            date2 = date;
            min2 = min;
            Egitmen_ad.Text = isim;
            con.Open();
            
            cmd.Connection = con;
            isim2 = isim;
           try
            {
                cmd.CommandText = "select * from eğitmen E,dersler D,verilen_dersler VD where EgitmenIsımSoyısım='" + isim + "'and VD.DersId=D.DersId and E.EgitmenId=VD.EgitmenId";
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    String derskod = myreader.GetString("DersKod");
                    listBox1.Items.Add(derskod);
                    String dersAdı = myreader.GetString("DersAdı");
                    listBox2.Items.Add(dersAdı);
                    String dersAkts = myreader.GetString("DersAkts");
                    listBox3.Items.Add(dersAkts);
                }
                myreader.Close();
            } catch (Exception ex1)
            {
                String a = ex1.Message;
                listBox1.Items.Add("Hata");
                listBox2.Items.Add("Hata");
                listBox3.Items.Add("Hata");
                hata.Text = "Dersler listesindeki hata \n"+a;
            }
            try {
                cmd.CommandText = "select Distinct O.OgrenciId,O.OgrenciNo,O.OgrenciIsim from eğitmen E,dersler D,verilen_dersler VD,ogrenci O,alınan_dersler AD where EgitmenIsımSoyısım='" + isim + "'and VD.DersId="+min+" and E.EgitmenId=VD.EgitmenId and AD.DersId=VD.DersId and AD.OgrenciId=O.OgrenciId";
                myreader = cmd.ExecuteReader();
                listBox4.Items.Add("\n");
                listBox5.Items.Add("\n");
                listBox6.Items.Add("\n");
                
                while (myreader.Read())
                {
                    String Id = myreader.GetString("OgrenciId");
                    listBox4.Items.Add(Id);
                    listBox4.Items.Add("\n");
                    String OgrenciNo = myreader.GetString("OgrenciNo");
                    listBox5.Items.Add(OgrenciNo);
                    listBox5.Items.Add("\n");
                    String OgrenciIsim = myreader.GetString("OgrenciIsim");
                    listBox6.Items.Add(OgrenciIsim);
                    listBox6.Items.Add("\n");               
                }
                myreader.Close();

                cmd.CommandText = "select Distinct EgitmenResim from eğitmen where EgitmenIsımSoyısım='" + isim + "'";
                object val = cmd.ExecuteScalar();
                cmd.CommandText = "select Distinct EgitmenResim from eğitmen where EgitmenIsımSoyısım='" + isim + "'";
                myreader = cmd.ExecuteReader();
                if (myreader.Read())
                {
                    if (val == DBNull.Value)
                    {
                        myreader.Close();
                        cmd.CommandText = "select Distinct EgitmenResim from eğitmen where EgitmenIsımSoyısım='admin'";
                        myreader = cmd.ExecuteReader();
                        if (myreader.Read())
                        {
                            byte[] img = (byte[])myreader["EgitmenResim"];
                            MemoryStream ms = new MemoryStream(img);
                            pictureBox4.Image = System.Drawing.Image.FromStream(ms);
                            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                    else
                    {
                        byte[] img = (byte[])myreader["EgitmenResim"];
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox4.Image = System.Drawing.Image.FromStream(ms);
                        pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                    }
                }
                myreader.Close();
                cmd.CommandText = "select Distinct Y.Yoklama,O.OgrenciId from dersler D,ogrenci O,yoklama Y where Y.DersId="+min+ " and Y.OgrenciId=O.OgrenciId and Y.Tarih='"+date+"'";
                myreader = cmd.ExecuteReader();
                listBox7.Items.Add("\n");
                while (myreader.Read())
                {
                    int a = myreader.GetInt32("Yoklama");
                    int Id = myreader.GetInt32("OgrenciId");
                    if (a > 0)
                    {
                        if (Id == 1)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");


                        }
                        if (Id == 2)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");

                        }
                        if (Id == 3)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");

                        }
                        if (Id== 4)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");

                        }
                        if (Id== 5)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");

                        }
                        if (Id == 6)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");

                        }
                        if (Id == 7)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");


                        }
                        if (Id == 8)
                        {
                            listBox7.Items.Add("Var");
                            listBox7.Items.Add("\n");
                        }
                        
                    }
                    else if(a==0){
                        if (Id == 1)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");                           
                        }
                        if (Id == 2)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");
                        }
                        if (Id == 3)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");
                        }
                        if (Id == 4)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");
                        }
                        if (Id == 5)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");
                        }
                        if (Id == 6)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");

                        }
                        if (Id == 7)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");

                        }
                        if (Id == 8)
                        {
                            listBox7.Items.Add("Yok");
                            listBox7.Items.Add("\n");

                        }
                    }
                }
                myreader.Close();
                cmd.CommandText = "select Distinct I.Durum,O.OgrenciId from dersler D,ogrenci O,ilgi I where I.DersId=" + min + " and I.OgrenciId=O.OgrenciId and I.Tarih='"+date+"'";
                myreader = cmd.ExecuteReader();
                listBox8.Items.Add("\n");
                while (myreader.Read())
                {
                    int a = myreader.GetInt32("Durum");
                    listBox8.Items.Add(a);
                    listBox8.Items.Add("\n");
                }
                myreader.Close();

            }
            catch (Exception ex2) {
                String a=ex2.Message;
                listBox4.Items.Add("Hata");
                listBox5.Items.Add("Hata");
                listBox6.Items.Add("Hata");
                listBox7.Items.Add("Hata");
                listBox8.Items.Add("Hata");
                hata.Text = "Ögrenciler listesindeki hata \n"+a;
            }
            date2.Replace('.', '/');
            DateTime date_new = Convert.ToDateTime(date2);
            dateTimePicker1.Value = date_new;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1-Öğrencinin isim soyisim veya numarasına tıklayarak detaylı bilgi edinebilir veya devamsızlık veya ilgi düzeyini değiştirebilirsiniz.\n2-Derslerin Kodlarına tıklayarak istenilen dersi alan öğrenci listesi ve en son alınan yoklamaya erişilebilir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            this.Dispose();
            SınıfListesi s = new SınıfListesi(isim2,min2,date2);
            s.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DuyuruListe d = new DuyuruListe(isim2,min2,date2);
            d.ShowDialog();
        }
        private void listbox5_changescroll(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) {
                listBox6.TopIndex = listBox5.TopIndex - 3;
                listBox4.TopIndex = listBox6.TopIndex;
                listBox7.TopIndex = listBox6.TopIndex;
                listBox8.TopIndex = listBox7.TopIndex;
            }
                else
                {
                    listBox6.TopIndex = listBox5.TopIndex + 3;
                    listBox4.TopIndex = listBox6.TopIndex;
                    listBox7.TopIndex = listBox4.TopIndex;
                    listBox8.TopIndex = listBox7.TopIndex;
            }
                }
        private void listbox1_changescroll(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                listBox2.TopIndex = listBox1.TopIndex - 3;
                listBox3.TopIndex = listBox2.TopIndex;          
            }
            else
            {
                listBox2.TopIndex = listBox1.TopIndex + 3;
                listBox3.TopIndex = listBox2.TopIndex;     
            }
        }
        private void listbox6_changescroll(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 )
            {
                listBox5.TopIndex = listBox6.TopIndex - 3;
                listBox4.TopIndex = listBox5.TopIndex;
                listBox7.TopIndex = listBox5.TopIndex;
                listBox8.TopIndex = listBox7.TopIndex;
            }
           else
            {
                listBox5.TopIndex = listBox6.TopIndex + 3;
                listBox4.TopIndex = listBox5.TopIndex;
                listBox7.TopIndex = listBox5.TopIndex;
                listBox8.TopIndex = listBox7.TopIndex;
            }
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Giriş g2 = new Giriş();
            g2.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int counter=0;
            Boolean yok=true;
         /*   if(rd1.Checked == true)
            {
                yok = false;
                counter = 1;
            }
           else if (rd21.Checked == true)
            {
                counter = 1;
            }
            else if (rd2.Checked == true)
            {
                yok = false;
                counter = 2;
            }
            else if (rd22.Checked == true)
            {
                counter = 2;

            }
            else if (rd3.Checked == true)
            {
                yok = false;
                counter = 3;
            }
            else if (rd23.Checked == true)
            {
                counter = 3;
            }
            else if (rd4.Checked == true)
            {
                yok = false;
                counter = 4;
            }
            else if (rd24.Checked == true)
            {
                counter = 4;
            }
            else if (rd5.Checked == true)
            {
                yok = false;
                counter = 5;
            }
            else if (rd25.Checked == true)
            {
                counter = 5;
            }
            else if (rd6.Checked == true)
            {
                yok = false;
                counter = 6;
            }
            else if (rd26.Checked == true)
            {
                counter = 6;
            }
            else if (rd7.Checked == true)
            {
                yok = false;
                counter = 7;
            }
            else if (rd27.Checked == true)
            {
                counter = 7;
            }
            else if (rd8.Checked == true)
            {
                yok = false;
                counter = 8;
            }
            else if (rd28.Checked == true)
            {
                counter = 8;
            }
            if (counter != 0 && yok==true)
            {
                try {
                    int devam = 0;
                    myreader.Close();
                    cmd.CommandText = "select Distinct DV.Devamsizlik from devamsizlik DV,ogrenci O where O.OgrenciId=" + counter;
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        devam = myreader.GetInt16("Devamsizlik");
                    }
                    devam = devam + 1;
                    myreader.Close();
                    cmd.CommandText = "UPDATE `test`.`devamsizlik` SET `Devamsizlik` =+ '"+devam+"'WHERE (`OgrenciId` = '"+counter+"') and (`DersId` = '2')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select Distinct OgrenciIsim from ogrenci O where O.OgrenciId=" + counter;
                    myreader = cmd.ExecuteReader();
                    String isim="";
                    while (myreader.Read())
                    {
                        isim = myreader.GetString("OgrenciIsim");
                    }
                    MessageBox.Show(isim + " Devamsızlığı arttırıldı!!");
                }catch(Exception ex)
                {
                    hata.Text = ex.Message;
                }
                myreader.Close();
                }
           else if (counter != 0 && yok == false)
            {
                try
                {
                    int devam = 0;
                    myreader.Close();
                    cmd.CommandText = "select Distinct DV.Devamsizlik from devamsizlik DV,ogrenci O where O.OgrenciId=" + counter;
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        devam = myreader.GetInt16("Devamsizlik");
                    }
                    devam = devam - 1;
                    myreader.Close();
                    cmd.CommandText = "UPDATE `test`.`devamsizlik` SET `Devamsizlik` =+ '" + devam + "'WHERE (`OgrenciId` = '" + counter + "') and (`DersId` = '2')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select Distinct OgrenciIsim from ogrenci O where O.OgrenciId=" + counter;
                    myreader = cmd.ExecuteReader();
                    String isim = "";
                    while (myreader.Read())
                    {
                        isim = myreader.GetString("OgrenciIsim");
                    }
                    MessageBox.Show(isim + " Devamsızlığı düşürüldü!!");
                }
                catch (Exception ex)
                {
                    hata.Text = ex.Message;
                }
                myreader.Close();
            }*/
        }
        private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int min = 0;
            string curItem = listBox1.SelectedItem.ToString();
            try
            {
                cmd.CommandText = "select Distinct DersId from dersler where DersKod='"+curItem+"'";
                min = int.Parse(cmd.ExecuteScalar().ToString());
                this.Dispose();
                Anamenü ana = new Anamenü(isim2, min,date2);
                ana.ShowDialog();
            }catch(Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
        private void listBox5_SelectedIndexChanged(object sender,EventArgs e)
        {
            string curItem = listBox5.SelectedItem.ToString();
            int index = listBox5.SelectedIndex;
            int index2 = listBox6.SelectedIndex;
            
            try
            {            
                if (curItem == "\n")
                {
                    if (index > 3)
                    {
                        listBox5.SetSelected(index - 1, true);
                        listBox6.SetSelected(index - 1, true);
                    }
                    else
                    {
                        listBox5.SetSelected(index + 1, true);
                        listBox6.SetSelected(index + 1, true);
                    }
                }
                else if(index2!=index)
                {
                    listBox6.SetSelected(index , true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listBox6_SelectedIndexChanged(object sender,EventArgs e)
        {
            string curItem = listBox6.SelectedItem.ToString();
            int index = listBox6.SelectedIndex;
            int index2 = listBox5.SelectedIndex;
            try
            {
                if (curItem == "\n")
                {
                    if (index > 3)
                    {
                        listBox6.SetSelected(index - 1, true);
                        listBox5.SetSelected(index - 1, true);
                    }
                    else
                    {
                        listBox6.SetSelected(index + 1, true);
                        listBox5.SetSelected(index + 1, true);
                    }
                }
                if(index!=index2)
                {
                    listBox5.SetSelected(index , true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListBox5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int id = 0;
            string curItem = listBox5.SelectedItem.ToString();
            try
            {
                cmd.CommandText = "select Distinct OgrenciId from ogrenci where OgrenciNo='" + curItem + "'";
                id = int.Parse(cmd.ExecuteScalar().ToString());
                this.Dispose();
                ÖğrenciBireyselSayfa og = new ÖğrenciBireyselSayfa(isim2, curItem,min2, date2);
                og.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string curItem = listBox6.SelectedItem.ToString();
            int id = 0;
            try
            {               
                cmd.CommandText = "select Distinct OgrenciId from ogrenci where OgrenciIsim='" + curItem + "'";
                id = int.Parse(cmd.ExecuteScalar().ToString());
                this.Dispose();
                ÖğrenciBireyselSayfa og = new ÖğrenciBireyselSayfa(isim2, curItem, min2, date2);
                og.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void picker_ValueChanged(object sender, EventArgs e)
        {

           string date3 = "" + dateTimePicker1.Value.Date.Year + "/" + dateTimePicker1.Value.Date.Month + "/" + dateTimePicker1.Value.Date.Day + "";
            if (date3 != date2)
            {
                this.Dispose();
                Anamenü ana = new Anamenü(isim2, min2, date3);
                ana.Show();
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            dlg.Title = "Resim Seç";
            if (!myreader.IsClosed)
            {
                myreader.Close();
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try {
                    string pic = dlg.FileName.ToString();
                    Image im = Image.FromFile(pic);
                    pictureBox4.Image = im;
                    MessageBox.Show("Resim Değiştirildi!!");
                    pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    cmd.CommandText = "Select EgitmenId from eğitmen where EgitmenIsımSoyısım='" + isim2 + "'";
                    String id = cmd.ExecuteScalar().ToString();
                    cmd.CommandText = "Update eğitmen set EgitmenResim=@img where EgitmenId=" + id + "";
                    cmd.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
                    cmd.ExecuteNonQuery();
                    this.Dispose();
                    Anamenü ana = new Anamenü(isim2, min2, date2);
                    ana.ShowDialog();
                }catch(Exception ex)
                {
                    MessageBox.Show("Resim boyutu büyük");
                    this.Dispose();
                    Anamenü ana = new Anamenü(isim2, min2, date2);
                    ana.ShowDialog();
                }
                }

        }
    }
}