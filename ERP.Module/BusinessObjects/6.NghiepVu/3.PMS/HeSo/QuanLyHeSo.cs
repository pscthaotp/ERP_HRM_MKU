using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.PMS.HeSo
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Quản lý hệ số")]
    [DefaultProperty("TenQuanLyHeSo")]
    public class QuanLyHeSo : BaseObject
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


        [ModelDefault("Caption", "Tên quản lý hệ số")]
        [VisibleInDetailView(false)]
        public string TenQuanLyHeSo
        {
            get
            {

                return string.Format("{0} - {1}", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? HocKy.TenHocKy : "");
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Hệ số lớp đông")]
        [Association("QuanLyHeSo-ListHeSoLopDong")]
        public XPCollection<HeSoLopDong> ListHeSoLopDong
        {
            get
            {
                return GetCollection<HeSoLopDong>("ListHeSoLopDong");
            }
        }

        

        [Aggregated]
        [ModelDefault("Caption", "Hệ số thời gian")]
        [Association("QuanLyHeSo-ListHeSoThoiGian")]
        public XPCollection<HeSoThoiGian> ListHeSoThoiGian
        {
            get
            {
                return GetCollection<HeSoThoiGian>("ListHeSoThoiGian");
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Hệ số môn học")]
        [Association("QuanLyHeSo-ListHeSoMonHoc")]
        public XPCollection<HeSoMonHoc> ListHeSoMonHoc
        {
            get
            {
                return GetCollection<HeSoMonHoc>("ListHeSoMonHoc");
            }
        }

        public QuanLyHeSo(Session session)
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
