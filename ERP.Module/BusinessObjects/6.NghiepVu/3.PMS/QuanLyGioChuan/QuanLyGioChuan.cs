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

namespace ERP.Module.NghiepVu.PMS.QuanLyGioChuan
{
    [ModelDefault("Caption", "Quản lý giờ chuẩn")]
    public class QuanLyGioChuan : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy", DataSourcePropertyIsNullMode.SelectAll)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }


        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMucChucVu")]
        [ModelDefault("Caption", "Định mức chức vụ")]
        public XPCollection<DinhMucChucVu> ListDinhMucChucVu
        {
            get
            {
                return GetCollection<DinhMucChucVu>("ListDinhMucChucVu");
            }
        }//dinhmucgiochuan

        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMucChucVuNhanVien")]
        [ModelDefault("Caption", "Định mức chức vụ nhân viên")]
        public XPCollection<DinhMucChucVu_NhanVien> ListDinhMucChucVuNhanVien
        {
            get
            {
                return GetCollection<DinhMucChucVu_NhanVien>("ListDinhMucChucVuNhanVien");
            }
        }//dinhmucgiochuan

        public QuanLyGioChuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            NamHoc = Common.GetCurrentNamHoc(Session);
            ThongTinTruong = Common.CongTy(Session);
        }
    }
}