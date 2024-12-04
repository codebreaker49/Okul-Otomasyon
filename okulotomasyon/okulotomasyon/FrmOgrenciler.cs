using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraEditors;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using DevExpress.XtraGrid.Views.Base.ViewInfo;

namespace okulotomasyon
{
    public partial class FrmOgrenciler : Form
    {
        public FrmOgrenciler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            //5.Sınıf
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Execute OgrenciVeli5", bgl.baglanti());
            da1.Fill(dt1);
            GrdCtrl5.DataSource = dt1;

            //6.Sınıf
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute OgrenciVeli5", bgl.baglanti());
            da2.Fill(dt2);
            GrdCtrl6.DataSource = dt2;

            //7.Sınıf
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Execute OgrenciVeli5", bgl.baglanti());
            da3.Fill(dt3);
            GrdCtrl7.DataSource = dt3;

            //8.Sınıf
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("Execute OgrenciVeli5", bgl.baglanti());
            da4.Fill(dt4);
            GrdCtrl8.DataSource = dt4;
        }

        void velilistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select VELIID, (VELIANNE + ' | ' + VELİBABA) AS 'VELIANNEBABA' FROM TBL_VELILER", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "VELIID";
            lookUpEdit1.Properties.DisplayMember = "VELIANNEBABA";
            lookUpEdit1.Properties.DataSource = dt;

        }

        void sehirekle()
        {
            SqlCommand komut = new SqlCommand("Select * from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[2]);
            }
            bgl.baglanti().Close();


        }

        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskOgrenciNo.Text = "";
            mskTC.Text = "";
            RdBtnErkek.Checked = false;
            RdBtnBayan.Checked = false;
            cmbSınıf.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            dateEdit1.Text = "";
            Rchadres.Text = "";
            pictureEdit1.Text = "";
        }

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            listele();
            sehirekle();
            temizle();
            velilistele();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();
            Cmbilce.Text = "";

            SqlCommand komut = new SqlCommand("Select * from TBL_ILCELER where il_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[2]);
            }
            bgl.baglanti().Close();
        }

        public string cinsiyet;

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_OGRENCİLER(OGRAD,OGRSOYAD,OGRNO,OGRSINIF,OGRDOGTAR,OGRCINSIYET,OGRTC,OGRIL,OGRILCE,OGRADRES,OGRFOTO) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskOgrenciNo.Text);
            komut.Parameters.AddWithValue("@p4", cmbSınıf.Text);
            komut.Parameters.AddWithValue("@p5", dateEdit1.Text);
            if (RdBtnErkek.Checked == true)
            {
                komut.Parameters.AddWithValue("@p6", cinsiyet = "E");
            }
            else
            {
                komut.Parameters.AddWithValue("@p6", cinsiyet = "B");
            }

            komut.Parameters.AddWithValue("@p7", mskTC.Text);
            komut.Parameters.AddWithValue("@p8", Cmbil.Text);
            komut.Parameters.AddWithValue("@p9", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p10", Rchadres.Text);
            komut.Parameters.AddWithValue("@p11", Path.GetFileName(yeniyol));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dr != null)
            {
                txtID.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTC.Text = dr["OGRTC"].ToString();
                mskOgrenciNo.Text = dr["OGRNO"].ToString();
                cmbSınıf.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    RdBtnErkek.Checked = true;
                }
                else
                {
                    RdBtnBayan.Checked = true;
                }
                Cmbil.Text = dr["OGRIL"].ToString();
                Cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                Rchadres.Text = dr["OGRADRES"].ToString();
                yeniyol = "C:\\Users\\bilal\\OneDrive\\Masaüstü\\C# Final\\Okul Otomasyon\\okulotomasyon\\okulotomasyon\\" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);
            }
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                txtID.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTC.Text = dr["OGRTC"].ToString();
                mskOgrenciNo.Text = dr["OGRNO"].ToString();
                cmbSınıf.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    RdBtnErkek.Checked = true;
                }
                else
                {
                    RdBtnBayan.Checked = true;
                }
                Cmbil.Text = dr["OGRIL"].ToString();
                Cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                Rchadres.Text = dr["OGRADRES"].ToString();
                yeniyol = "C:\\Users\\bilal\\OneDrive\\Masaüstü\\C# Final\\Okul Otomasyon\\okulotomasyon\\okulotomasyon\\" +"\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);
            }
        }

        private void gridView3_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);

            if (dr != null)
            {
                txtID.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTC.Text = dr["OGRTC"].ToString();
                mskOgrenciNo.Text = dr["OGRNO"].ToString();
                cmbSınıf.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    RdBtnErkek.Checked = true;
                }
                else
                {
                    RdBtnBayan.Checked = true;
                }
                Cmbil.Text = dr["OGRIL"].ToString();
                Cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                Rchadres.Text = dr["OGRADRES"].ToString();
                yeniyol = "C:\\Users\\bilal\\OneDrive\\Masaüstü\\C# Final\\Okul Otomasyon\\okulotomasyon\\okulotomasyon\\" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);
            }
        }
        private void gridView4_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);

            if (dr != null)
            {
                txtID.Text = dr["OGRID"].ToString();
                txtAd.Text = dr["OGRAD"].ToString();
                txtSoyad.Text = dr["OGRSOYAD"].ToString();
                mskTC.Text = dr["OGRTC"].ToString();
                mskOgrenciNo.Text = dr["OGRNO"].ToString();
                cmbSınıf.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    RdBtnErkek.Checked = true;
                }
                else
                {
                    RdBtnBayan.Checked = true;
                }
                Cmbil.Text = dr["OGRIL"].ToString();
                Cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                Rchadres.Text = dr["OGRADRES"].ToString();
                yeniyol = "C:\\Users\\bilal\\OneDrive\\Masaüstü\\C# Final\\Okul Otomasyon\\okulotomasyon\\okulotomasyon\\" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);
            }
        }

        public string yeniyol;
        
        private void BtnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası| *.jpg;*.png;*.nif| Tüm Dosyalar| *.*";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            yeniyol = "C:\\Users\\bilal\\OneDrive\\Masaüstü\\C# Final\\Okul Otomasyon\\okulotomasyon\\okulotomasyon\\" +"\\resimler\\" + Guid.NewGuid().ToString() + ".jpg";
            File.Copy(dosyayolu, yeniyol);
            pictureEdit1.Image = Image.FromFile(yeniyol);

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_OGRENCİLER set OGRAD=@p1,OGRSOYAD=@p2,OGRNO=@p3,OGRSINIF=@p4,OGRDOGTAR=@p5,OGRCINSIYET=@p6,OGRTC=@p7,OGRIL=@p8,OGRILCE=@p9,OGRADRES=@p10,OGRFOTO=@p11 where OGRID=@p12",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskOgrenciNo.Text);
            komut.Parameters.AddWithValue("@p4", cmbSınıf.Text);
            komut.Parameters.AddWithValue("@p5", dateEdit1.Text);
            if (RdBtnErkek.Checked == true)
            {
                komut.Parameters.AddWithValue("@p6", cinsiyet = "E");
            }
            else
            {
                komut.Parameters.AddWithValue("@p6", cinsiyet = "B");
            }

            komut.Parameters.AddWithValue("@p7", mskTC.Text);
            komut.Parameters.AddWithValue("@p8", Cmbil.Text);
            komut.Parameters.AddWithValue("@p9", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p10", Rchadres.Text);
            komut.Parameters.AddWithValue("@p11", Path.GetFileName(yeniyol));
            komut.Parameters.AddWithValue("@p12", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_OGRENCİLER where OGRID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgisi Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            FrmNufusCuzdani frm = new FrmNufusCuzdani();

            if (dr != null)
            {
                frm.ad = dr["OGRAD"].ToString();
                frm.soyad = dr["OGRSOYAD"].ToString();
                frm.tc = dr["OGRTC"].ToString();
                frm.cinsiyet = dr["OGRCINSIYET"].ToString();
                frm.dogtarihi = dr["OGRDOGTAR"].ToString();
                frm.uzanti= "C:\\Users\\bilal\\OneDrive\\Masaüstü\\C# Final\\Okul Otomasyon\\okulotomasyon\\okulotomasyon\\" + "\\resimler\\" + dr["OGRFOTO"].ToString();
            }
            frm.Show();
        }
    }
}