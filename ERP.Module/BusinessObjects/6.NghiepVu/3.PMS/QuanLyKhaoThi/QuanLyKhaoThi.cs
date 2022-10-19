using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.PMS.HeSo;

namespace ERP.Module.NghiepVu.PMS.QuanLyKhaoThi
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Quản lý khảo thí")]
    [DefaultProperty("TenQuanLyKhaoThi")]
    public class QuanLyKhaoThi : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private bool _Khoa;


        [ModelDefault("Caption", "Trường")]
        [VisibleInDetailView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        public BoPhan ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }


        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [ModelDefault("Caption", "Khóa dữ liệu")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }


        [ModelDefault("Caption", "Tên quản lý khảo thí")]
        [VisibleInDetailView(false)]
        public string TenQuanLyKhaoThi
        {
            get
            {

                return string.Format("{0} - {1}", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? HocKy.TenHocKy : "");
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Chi tiết coi thi")]
        [Association("QuanLyKhaoThi-ListChiTietCoiThi")]
        public XPCollection<ChiTietCoiThi> ListChiTietCoiThi
        {
            get
            {
                return GetCollection<ChiTietCoiThi>("ListChiTietCoiThi");
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Chi tiết chấm bài")]
        [Association("QuanLyKhaoThi-ListChiTietChamBai")]
        public XPCollection<ChiTietChamBai> ListChiTietChamBai
        {
            get
            {
                return GetCollection<ChiTietChamBai>("ListChiTietChamBai");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết ra đề")]
        [Association("QuanLyKhaoThi-ListChiTietRaDe")]
        public XPCollection<ChiTietRaDe> ListChiTietRaDe
        {
            get
            {
                return GetCollection<ChiTietRaDe>("ListChiTietRaDe");
            }
        }

        public QuanLyKhaoThi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = Common.CongTy(Session);
        }

    }
}
