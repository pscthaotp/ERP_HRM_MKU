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
using System;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.PMS.QuanLyGiangDay
{
    [DefaultClassOptions]
    [Appearance("QuanLyKhoiLuongGiangDay_ThinhGiang_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [ModelDefault("Caption", "Quản lý khối lượng giảng dạy(Thỉnh giảng)")]
    public class QuanLyKhoiLuongGiangDay_ThinhGiang : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private bool _Khoa;
        private Guid _BangChotThuLao_ThinhGiang;


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
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [ModelDefault("Caption", "Bảng chốt thừ lao(Thỉnh giảng)")]
        [ModelDefault("AllowEdit", "false")]
        public Guid BangChotThuLao_ThinhGiang
        {
            get { return _BangChotThuLao_ThinhGiang; }
            set { SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Thông tin khối lượng")]
        [Association("QuanLyKhoiLuongGiangDay_ThinhGiang-ListThongTinKhoiLuongGiangDay")]
        public XPCollection<ThongTinKhoiLuongGiangDay> ListThongTinKhoiLuongGiangDay
        {
            get
            {
                return GetCollection<ThongTinKhoiLuongGiangDay>("ListThongTinKhoiLuongGiangDay");
            }
        }
        public QuanLyKhoiLuongGiangDay_ThinhGiang(Session session)
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
