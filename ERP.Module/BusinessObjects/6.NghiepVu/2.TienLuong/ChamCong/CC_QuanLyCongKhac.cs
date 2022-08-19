using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Enum.TienLuong;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("KyChamCong")]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Quản lý công khác")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyCongKhac", DefaultContexts.Save, "CongTy;KyChamCong;LoaiCongKhac")]
    [Appearance("QuanLyCongKhac ", TargetItems = "KyChamCong,NgayLap,CongTy;ListChiTietCongKhac;LoaiCongKhac", Enabled = false, Criteria = "KhoaChamCong")]
    public class CC_QuanLyCongKhac : BaseObject,ICongTy
    {
        //
        private CC_KyChamCong _KyChamCong;
        private LoaiCongKhacEnum _LoaiCongKhac = LoaiCongKhacEnum.GiuTreThuBay;
        private DateTime _NgayLap;
        private bool _KhoaChamCong;
        private CongTy _CongTy;

        //
        [ImmediatePostData]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Kỳ chấm công")]
        [DataSourceProperty("KyChamCongList")]
        //[DataSourceCriteria("!KhoaSo")]
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

        [ModelDefault("Caption", "Loại công khác")]
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

        [NonPersistent]
        [ModelDefault("Caption", "Khóa chấm công")]
        //[ModelDefault("AllowEdit","False")]
        public bool KhoaChamCong
        {
            get
            {
                return _KhoaChamCong;
            }
            set
            {
                SetPropertyValue("KhoaChamCong", ref _KhoaChamCong, value);
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
                    KyChamCong = Session.FindObject<CC_KyChamCong>(CriteriaOperator.Parse("Thang=? and Nam=? and CongTy.Oid=? and !KhoaSo", NgayLap.Month, NgayLap.Year, CongTy != null ? CongTy.Oid : Guid.Empty));
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết công khác")]
        [Association("QuanLyCongKhac-ListChiTietCongKhac")]
        public XPCollection<CC_ChiTietCongKhac> ListChiTietCongKhac
        {
            get
            {
                //
                return GetCollection<CC_ChiTietCongKhac>("ListChiTietCongKhac");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        //[ModelDefault("AllowEdit", "false")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    KyChamCong = null;
                    UpdateKyChamCongList();
                }
            }
        }
        public CC_QuanLyCongKhac(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<CC_KyChamCong> KyChamCongList { get; set; }

        private void UpdateKyChamCongList()
        {
            //
            if (KyChamCongList == null)
                KyChamCongList = new XPCollection<CC_KyChamCong>(Session);
            KyChamCongList.Criteria = CriteriaOperator.Parse("CongTy.Oid = ? and !KhoaSo", CongTy != null ? CongTy.Oid : Guid.Empty);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            UpdateKyChamCongList();
            //
            NgayLap = Common.GetServerCurrentTime();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateKyChamCongList();
        }
    }
}
