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
    [ModelDefault("Caption", "Quản lý chấm công")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyChamCong", DefaultContexts.Save, "CongTy;KyChamCong")]
    [Appearance("QuanLyChamCong", TargetItems = "KyChamCong;NgayLap;CongTy;ListChiTietChamCong", Enabled = false, Criteria = "KhoaChamCong")]
    public class CC_QuanLyChamCong : BaseObject,ICongTy
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
        [DataSourceCriteria("!KhoaSo")]
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
                    KyChamCong = Session.FindObject<CC_KyChamCong>(CriteriaOperator.Parse("Thang=? and Nam=? and CongTy.Oid=?", NgayLap.Month, NgayLap.Year, CongTy != null ? CongTy.Oid : Guid.Empty));
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
        [Association("QuanLyChamCong-ListChiTietChamCong")]
        public XPCollection<CC_ChiTietChamCong> ListChiTietChamCong
        {
            get
            {
                //
                return GetCollection<CC_ChiTietChamCong>("ListChiTietChamCong");
            }
        }

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
            }
        }
        public CC_QuanLyChamCong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            NgayLap = Common.GetServerCurrentTime();
            CongTy = Common.CongTy(Session);
        }
    }

}
