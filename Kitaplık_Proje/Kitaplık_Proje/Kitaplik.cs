using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplık_Proje
{
    public partial class Kitaplik : Form
    {
        public Kitaplik()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Araçlarım\yazılım\Microsoft Access Veri Tabanı\Kitaplik.mdb");

        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        string durum = "";

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("insert into Kitaplar (KitapAd, Yazar, Tur, Sayfa, Durum) values (@p1, @p2, @p3, @p4, @p5)", baglanti);
            komut1.Parameters.AddWithValue("@p1", TxtKitapAd.Text);
            komut1.Parameters.AddWithValue("@p2", TxtKitapYazar.Text);
            komut1.Parameters.AddWithValue("@p3", CmbTur.Text);
            komut1.Parameters.AddWithValue("@p4", TxtKitapSayfa.Text);
            komut1.Parameters.AddWithValue("@p5", durum);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtKitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtKitapYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtKitapSayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Delete From Kitaplar where Kitapid = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKitapid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand guncelle = new OleDbCommand("Update Kitaplar Set KitapAd = @p1, Yazar = @p2, Tur =  @p3, Sayfa = @p4, Durum = @p5 where Kitapid = @p6", baglanti);
            guncelle.Parameters.AddWithValue("@p1", TxtKitapAd.Text);
            guncelle.Parameters.AddWithValue("@p2", TxtKitapYazar.Text);
            guncelle.Parameters.AddWithValue("@p3", CmbTur.Text);
            guncelle.Parameters.AddWithValue("@p4", TxtKitapSayfa.Text);
            if (radioButton1.Checked == true)
            {
                guncelle.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked == true)
            {
                guncelle.Parameters.AddWithValue("@p5", durum);
            }
            guncelle.Parameters.AddWithValue("@p6", TxtKitapid.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Güncellendi", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("Select * From Kitaplar where KitapAd = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKitapBul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            OleDbCommand komut2 = new OleDbCommand("Select * From Kitaplar where KitapAd like '%" + TxtKitapBul.Text + "%'", baglanti);
            DataTable dt2 = new DataTable();
            OleDbDataAdapter da2 = new OleDbDataAdapter(komut2);
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }
    }
}
// Bağlantı Adresi
// Provider=Microsoft.Jet.OLEDB.4.0;Data Source="C:\Araçlarım\yazılım\Microsoft Access Veri Tabanı\Kitaplik.mdb"