using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.PMS.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý HĐ khác")]
    [DefaultProperty("ThongTin")]
    [Appearance("QuanLyHDKhac_Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null or Khoa = 1")] 
    public class QuanLyHDKhac : ThongTinChungPMS 
    {
        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;
       
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public BangChotThuLao BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
                if (BangChotThuLao != null)
                    Khoa = true;
                else
                    Khoa = false;
            }
        }
        //
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "");
            }
        }
        //
        [Aggregated]
        [Association("QuanLyHDKhac-ListChiTietHDKhac")]
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<ChiTietHDKhac> ListChiTietHDKhac
        {
            get
            {
                return GetCollection<ChiTietHDKhac>("ListChiTietHDKhac");
            }
        }
        public QuanLyHDKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
