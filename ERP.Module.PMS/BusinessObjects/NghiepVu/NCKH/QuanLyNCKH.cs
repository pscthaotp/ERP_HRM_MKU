using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.BusinessObjects;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý NCKH")]
    [DefaultProperty("ThongTin")]
    [Appearance("QuanLyNCKH_Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null or Khoa = 1")] 
    public class QuanLyNCKH : ThongTinChungPMS
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
        [Association("QuanLyNCKH-ListChiTietNCKH")]
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<ChiTietNCKH> ListChiTietNCKH
        {
            get
            {
                return GetCollection<ChiTietNCKH>("ListChiTietNCKH");
            }
        }
        public QuanLyNCKH(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
