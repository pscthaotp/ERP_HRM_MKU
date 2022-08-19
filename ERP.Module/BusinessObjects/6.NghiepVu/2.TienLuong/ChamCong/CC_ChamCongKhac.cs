using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.Enum.TienLuong;
using ERP.Module.DanhMuc.TienLuong;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("KyChamCong")]
    [ModelDefault("Caption", "Bảng chấm công khác")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "KyChamCong;CongTy;LoaiCongKhac")]
    [Appearance("CC_ChamCongKhac", TargetItems = "*", Enabled = false, Criteria = "Chot")]
    public class CC_ChamCongKhac : BaseObject,ICongTy
    {
        private LoaiCongKhacEnum _LoaiCongKhac = LoaiCongKhacEnum.GiuTreThuBay;
        private CC_KyChamCong _KyChamCong;
        private CongTy _CongTy;
        private DateTime _NgayLap = DateTime.Now.Date;
        private bool _Chot = false;

        //
        [ImmediatePostData]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Kỳ chấm công")]
        [DataSourceCriteria("!KhoaSo")]
        [ModelDefault("AllowEdit", "false")]
        public CC_KyChamCong KyChamCong
        {
            get
            {
                return _KyChamCong;
            }
            set
            {
                SetPropertyValue("KyChamCong", ref _KyChamCong, value);
            }
        }

        [ModelDefault("Caption", "Chốt")]
        [ModelDefault("AllowEdit", "false")]
        public bool Chot
        {
            get
            {
                return _Chot;
            }
            set
            {
                SetPropertyValue("Chot", ref _Chot, value);
            }
        }

        [ModelDefault("Caption", "Loại công khác")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiCongKhacEnum LoaiCongKhac
        {
            get
            {
                return _LoaiCongKhac;
            }
            set
            {
                SetPropertyValue("LoaiCongKhac", ref _LoaiCongKhac, value);
            }
        }

        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("Caption", "Trường")]
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
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "d/MM/yyyy")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading)
                {
                    //
                    KyChamCong = null;
                    //
                    KyChamCong = Session.FindObject<CC_KyChamCong>(CriteriaOperator.Parse("Thang=? and Nam=?", NgayLap.Month, NgayLap.Year));
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết chấm công khác")]
        [Association("ChamCongKhac-ListChiTietChamCongKhac")]
        public XPCollection<CC_ChiTietChamCongKhac> ListChiTietChamCongKhac
        {
            get
            {
                return GetCollection<CC_ChiTietChamCongKhac>("ListChiTietChamCongKhac");
            }
        }
        public CC_ChamCongKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            NgayLap = Common.GetServerCurrentTime();
        }
    }
}
