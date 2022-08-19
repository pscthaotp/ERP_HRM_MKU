using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.TienLuong.TruyLuong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng truy thu")]
    [Appearance("BangTruyThu.KhoaSo", TargetItems = "*", Enabled = false,Criteria = "KyTinhLuong.KhoaSo or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangTruyThu.Unique", DefaultContexts.Save, "KyTinhLuong;CongTy")]
    public class BangTruyThu : BaseObject, ICongTy
    {
        private ChungTu _ChungTu;
        private CongTy _CongTy;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("KyTinhLuongList")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        //[ModelDefault("AllowEdit", "False")]
        [RuleRequiredField("", DefaultContexts.Save)]
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
        [ModelDefault("Caption", "Danh sách truy thu")]
        [Association("BangTruyThu-ListTruyThuNhanVien")]
        public XPCollection<TruyThuNhanVien> ListTruyThuNhanVien
        {
            get
            {
                return GetCollection<TruyThuNhanVien>("ListTruyThuNhanVien");
            }
        }

        //Dùng để lưu vết
        [Browsable(false)]
        [ModelDefault("Caption", "Chứng từ")]
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

        public BangTruyThu(Session session) : base(session) { }

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
            //
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
        }
    }

}

