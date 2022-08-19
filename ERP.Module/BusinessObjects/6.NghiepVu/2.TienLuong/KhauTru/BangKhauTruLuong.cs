using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;

//
namespace ERP.Module.NghiepVu.TienLuong.KhauTru
{
    [DefaultClassOptions]
    [ImageName("BO_KhauTru")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng khấu trừ lương")]
    [Appearance("BangKhauTruLuong.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KyTinhLuong.KhoaSo or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangKhauTruLuong.Unique", DefaultContexts.Save, "KyTinhLuong;LoaiKhauTruLuong;CongTy")]
    public class BangKhauTruLuong : BaseObject, ICongTy
    {
        private ChungTu _ChungTu;
        private CongTy _CongTy;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private LoaiKhauTruLuong _LoaiKhauTruLuong;

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

        [ModelDefault("Caption", "Loại khấu trừ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiKhauTruLuong LoaiKhauTruLuong
        {
            get
            {
                return _LoaiKhauTruLuong;
            }
            set
            {
                SetPropertyValue("LoaiKhauTruLuong", ref _LoaiKhauTruLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField("", DefaultContexts.Save)]
        //[ModelDefault("AllowEdit", "False")]
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
        [ModelDefault("Caption", "Chi tiết khấu trừ lương")]
        [Association("BangKhauTruLuong-ListChiTietKhauTruLuong")]
        public XPCollection<ChiTietKhauTruLuong> ListChiTietKhauTruLuong
        {
            get
            {
                return GetCollection<ChiTietKhauTruLuong>("ListChiTietKhauTruLuong");
            }
        }

        //Dùng để lưu trữ
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

        public BangKhauTruLuong(Session session): base(session) { }

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
