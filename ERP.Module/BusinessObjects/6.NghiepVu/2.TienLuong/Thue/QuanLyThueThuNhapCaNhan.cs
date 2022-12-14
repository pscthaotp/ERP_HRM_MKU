using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

//
namespace ERP.Module.NghiepVu.TienLuong.Thue
{
    [DefaultClassOptions]
    [ImageName("BO_HoaDon")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Quản lý thuế TNCN")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyThueThuNhapCaNhan.Unique", DefaultContexts.Save, "KyTinhLuong;ChungTu;CongTy")]
    public class QuanLyThueThuNhapCaNhan : BaseObject, ICongTy
    {
        //
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private ChungTu _ChungTu;
        private CongTy _CongTy;

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

        [Browsable(false)]
        [ModelDefault("Caption", "Chứng từ")]
        //[RuleRequiredField("", DefaultContexts.Save)]
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


        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
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
        [ModelDefault("Caption", "Danh sách thuế TNCN")]
        [Association("QuanLyThueThuNhapCaNhan-ChiTietThueThuNhapCaNhanList")]
        public XPCollection<ChiTietThueThuNhapCaNhan> ChiTietThueThuNhapCaNhanList
        {
            get
            {
                return GetCollection<ChiTietThueThuNhapCaNhan>("ChiTietThueThuNhapCaNhanList");
            }
        }

        public QuanLyThueThuNhapCaNhan(Session session) : base(session) { }

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
