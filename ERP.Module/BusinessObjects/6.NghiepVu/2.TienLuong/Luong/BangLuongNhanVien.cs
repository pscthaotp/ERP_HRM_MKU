using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.ChungTus;

namespace ERP.Module.NghiepVu.TienLuong.Luong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")] 
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng lương nhân viên")]
    [Appearance("BangLuongNhanVien.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KyTinhLuong.KhoaSo or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangLuongNhanVien.Unique", DefaultContexts.Save, "KyTinhLuong;DotTinhLuong;CongTy")]
    public class BangLuongNhanVien : BaseObject, ICongTy
    {
        private ChungTu _ChungTu;
        private DateTime _NgayLap;
        private KyTinhLuong _KyTinhLuong;
        private CongTy _CongTy;
        private DotTinhLuong _DotTinhLuong;
        //

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
                    KyTinhLuong = Common.GetKyTinhLuong_ByDate(Session,NgayLap);
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

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Đợt tính lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DotTinhLuong DotTinhLuong
        {
            get
            {
                return _DotTinhLuong;
            }
            set
            {
                SetPropertyValue("DotTinhLuong", ref _DotTinhLuong, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangLuongNhanVien-ListLuongNhanVien")]
        public XPCollection<LuongNhanVien> ListLuongNhanVien
        {
            get
            {
                return GetCollection<LuongNhanVien>("ListLuongNhanVien");
            }
        }

        //Chỉ sử dụng để an toàn dữ liệu
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

        public BangLuongNhanVien(Session session) : base(session)  { }

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
            //
            NgayLap = Common.GetServerCurrentTime();
            //
            DotTinhLuong = Common.GetDotTinhLuong(Session, "Dot1");
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
            //
        }
    }

}
