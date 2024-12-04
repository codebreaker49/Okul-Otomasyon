using DevExpress.Utils.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace okulotomasyon
{
    public partial class FrmVeliler : Form
    {
        public FrmVeliler()
        {
            InitializeComponent();
        }

        DbOkulEntities db = new DbOkulEntities();

        void listele()
        {
            var query = from item in db.TBL_VELILER
                        select new { item.VELIID, item.VELIANNE, item.VELİBABA, item.VELITEL1, item.VELİTEL2, item.VELIMAIL };
            gridControl1.DataSource = query.ToList();

        }

        void temizle()
        {
            txtID.Text = "";
            txtAnne.Text = "";
            txtBaba.Text = "";
            mskTel1.Text = "";
            mskTel2.Text = "";
            txtMail.Text = "";
        }


        private void FrmVeliler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBL_VELILER veli = new TBL_VELILER();
            veli.VELIANNE = txtAnne.Text;
            veli.VELİBABA = txtBaba.Text;
            veli.VELITEL1 = mskTel1.Text;
            veli.VELİTEL2 = mskTel2.Text;
            veli.VELIMAIL = txtMail.Text;
            db.TBL_VELILER.Add(veli);
            db.SaveChanges();
            listele();
            temizle();

        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            txtID.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIID").ToString();
            txtAnne.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIANNE").ToString();
            txtBaba.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELİBABA").ToString();
            mskTel1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELITEL1").ToString();
            mskTel2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELİTEL2").ToString();
            txtMail.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIMAIL").ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIID").ToString());
            var item = db.TBL_VELILER.Find(id);
            item.VELIANNE = txtAnne.Text;
            item.VELİBABA = txtBaba.Text;
            item.VELITEL1 = mskTel1.Text;
            item.VELİTEL2 = mskTel2.Text;
            item.VELIMAIL = txtMail.Text;
            db.SaveChanges();
            listele();
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIID").ToString());
            var item = db.TBL_VELILER.Find(id);
            db.TBL_VELILER.Remove(item);
            db.SaveChanges();
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
