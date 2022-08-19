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

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("KyChamCong")]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Quản lý công ngoài giờ")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyCongNgoaiGio", DefaultContexts.Save, "CongTy;KyChamCong")]
    [Appearance("QuanLyCongNgoaiGio", TargetItems = "KyChamCong,NgayLap,CongTy;ListChiTietChamCong", Enabled = false, Criteria = "KhoaChamCong")]
    public class CC_QuanLyCongNgoaiGio : BaseObject,ICongTy
    {
        //
        private CC_KyChamCong _KyChamCong;
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
                if (!IsLoading)
                {
                    //
                    KyChamCong = Session.FindObject<CC_KyChamCong>(CriteriaOperator.Parse("Thang=? and Nam=? and CongTy.Oid=? and !KhoaSo", NgayLap.Month, NgayLap.Year,CongTy != null ? CongTy.Oid : Guid.Empty));
                }
            }
        }

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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết chấm công")]
        [Association("QuanLyCongNgoaiGio-ListChiTietChamCong")]
        public XPCollection<CC_ChiTietCongNgoaiGio> ListChiTietChamCong
        {
            get
            {
                //
                return GetCollection<CC_ChiTietCongNgoaiGio>("ListChiTietChamCong");
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
        public CC_QuanLyCongNgoaiGio(Session session) : base(session) { }

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
