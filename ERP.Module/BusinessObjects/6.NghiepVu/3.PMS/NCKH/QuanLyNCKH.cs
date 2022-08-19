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
using ERP.Module.NghiepVu.PMS.QuanLy;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.PMS.NCKH
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Quản lý NCKH")]
    [Appearance("KhoiLuongGiangDay_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyNCKH.Unique", DefaultContexts.Save, "NamHoc;HocKy;ThongTinTruong;")]
    public class QuanLyNCKH : BaseObject
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
        [RuleRequiredField(DefaultContexts.Save)]
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
        [ModelDefault("Caption", "Chi tiết nghiên cứu khoa học")]
        [Association("QuanLyNCKH-ListChiTietNCKH")]
        public XPCollection<ChiTietNCKH> ListChiTietNCKH
        {
            get
            {
                return GetCollection<ChiTietNCKH>("ListChiTietNCKH");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Bảo lưu nghiên cứu khoa học")]
        [Association("QuanLyNCKH-ListBaoLuuNCKH")]
        public XPCollection<BaoLuuNCKH> ListBaoLuuNCKH
        {
            get
            {
                return GetCollection<BaoLuuNCKH>("ListBaoLuuNCKH");
            }
        }

        public QuanLyNCKH(Session session)
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