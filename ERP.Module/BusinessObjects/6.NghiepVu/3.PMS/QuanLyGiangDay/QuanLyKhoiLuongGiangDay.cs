using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using System;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.PMS.QuanLyGiangDay
{
    [DefaultClassOptions]
    [Appearance("QuanLyKhoiLuongGiangDay_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [ModelDefault("Caption", "Quản lý khối lượng giảng dạy")]
    public class QuanLyKhoiLuongGiangDay : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private bool _Khoa;
        private Guid _BangChotThuLao;


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

        [ModelDefault("Caption", "Bảng chốt thừ lao")]
        [ModelDefault("AllowEdit", "false")]
        public Guid BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set { SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách khối lượng giảng dạy")]
        [Association("QuanLyKhoiLuongGiangDay-ListThongTinKhoiLuongGiangDay")]
        public XPCollection<ThongTinKhoiLuongGiangDay> ListThongTinKhoiLuongGiangDay
        {
            get
            {
                return GetCollection<ThongTinKhoiLuongGiangDay>("ListThongTinKhoiLuongGiangDay");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách hướng dẫn TTTN")]
        [Association("QuanLyKhoiLuongGiangDay-ListChiTietHuongDanTTTN")]
        public XPCollection<ChiTietHuongDanTTTN> ListChiTietHuongDanTTTN
        {
            get
            {
                return GetCollection<ChiTietHuongDanTTTN>("ListChiTietHuongDanTTTN");
            }
        }

        public QuanLyKhoiLuongGiangDay(Session session)
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
