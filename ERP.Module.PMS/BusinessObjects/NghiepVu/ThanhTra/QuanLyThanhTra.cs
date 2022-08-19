using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.BusinessObjects;


namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý thanh tra")]
    [DefaultProperty("Caption")]
    public class QuanLyThanhTra : ThongTinChungPMS
    {
        private bool _Khoa;

        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [Aggregated]
        [Association("QuanLyThanhTra-ListChiTiet")]
        [ModelDefault("Caption", "Chi tiết thanh tra")]
        public XPCollection<ChiTietThanhTra> ListChiTiet
        {
            get
            {
                return GetCollection<ChiTietThanhTra>("ListChiTiet");
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1} {2}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");
            }
        }

        public QuanLyThanhTra(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
