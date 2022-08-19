using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.Commons;
//
namespace ERP.Module.NghiepVu.TienLuong.Thuong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng thưởng - phúc lợi cán bộ")]
    [Appearance("BangThuongNhanVien.KhoaSo", TargetItems = "LoaiKhenThuongPhucLoi;KyTinhLuong;NgayLap;ListChiTietThuongNhanVien", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangThuongNhanVien.Unique", DefaultContexts.Save,
        "KyTinhLuong;LoaiKhenThuongPhucLoi;NgayLap;CongTy")]
    public class BangThuongNhanVien : BaseObject, ICongTy
    {
        private LoaiKhenThuongPhucLoi _LoaiKhenThuongPhucLoi;
        private ChungTu _ChungTu;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private CongTy _CongTy;

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Loại Khen thưởng - Phúc lợi")]
        public LoaiKhenThuongPhucLoi LoaiKhenThuongPhucLoi
        {
            get
            {
                return _LoaiKhenThuongPhucLoi;
            }
            set
            {
                SetPropertyValue("LoaiKhenThuongPhucLoi", ref _LoaiKhenThuongPhucLoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    KyTinhLuong = Common.GetKyTinhLuong_ByDate(Session, NgayLap);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField("", DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangThuongNhanVien-ListChiTietThuongNhanVien")]
        public XPCollection<ChiTietThuongNhanVien> ListChiTietThuongNhanVien
        {
            get
            {
                return GetCollection<ChiTietThuongNhanVien>("ListChiTietThuongNhanVien");
            }
        }

        //Dùng để lưu vết
        [Browsable(false)]
        public ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        public BangThuongNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            //
            if (CongTy != null)
                KyTinhLuongList = Common.GetKyTinhLuongList_ByCompanyInfo(Session, CongTy);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            UpdateKyTinhLuongList();
            NgayLap = Common.GetServerCurrentTime();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
        }
    }

}
