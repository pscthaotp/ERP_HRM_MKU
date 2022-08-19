using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using ERP.Module.PMS.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý HĐ giảng dạy khác")]
    [Appearance("QuanLyHDGiangDayKhac_Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null or Khoa = 1")] 
    public class QuanLyHDGiangDayKhac : ThongTinChungPMS
    {
        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;
        //
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

        [Aggregated]
        [Association("QuanLyHDGiangDayKhac-ListChiTietHoatDongGiangDayKhac")]
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<ChiTietHoatDongGiangDayKhac> ListChiTietHoatDongGiangDayKhac
        {
            get
            {
                return GetCollection<ChiTietHoatDongGiangDayKhac>("ListChiTietHoatDongGiangDayKhac");
            }
        }
        public QuanLyHDGiangDayKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
