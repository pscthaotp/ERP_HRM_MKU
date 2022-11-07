using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.PMS.BoiDuong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Quản lý bồi dưỡng")]
    [Appearance("QuanLyBoiDuong_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyBoiDuong.Unique", DefaultContexts.Save, "NamHoc;HocKy;ThongTinTruong;")]
    public class QuanLyBoiDuong : BaseObject
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
      //  [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [ModelDefault("Caption", "Khóa dữ liệu")]
        [ModelDefault("AllowEdit", "false")]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }


        [Aggregated]
        [ModelDefault("Caption", "Chi tiết bồi dưỡng")]
        [Association("QuanLyBoiDuong-ListChiTietBoiDuong")]
        public XPCollection<ChiTietBoiDuong> ListChiTietBoiDuong
        {
            get
            {
                return GetCollection<ChiTietBoiDuong>("ListChiTietBoiDuong");
            }
        }
       

        public QuanLyBoiDuong(Session session)
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