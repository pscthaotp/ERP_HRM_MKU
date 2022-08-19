using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("KyTinhLuong")]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Quản lý công giảng dạy")]
    [RuleCombinationOfPropertiesIsUnique("CC_QuanLyCongGiangDay", DefaultContexts.Save, "CongTy;KyTinhLuong")]
    [Appearance("CC_QuanLyCongGiangDay.KhoaSo ", TargetItems = "*", Enabled = false, Criteria = "KhoaSo")]
    public class CC_QuanLyCongGiangDay : BaseObject,ICongTy
    {
        //
        private KyTinhLuong _KyTinhLuong;
        private DateTime _TuNgay;
        private DateTime _DenNgay;       
        private CongTy _CongTy;
        private bool _KhoaSo;

        //
        [ImmediatePostData]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        //[DataSourceCriteria("KhoaSo")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
                if (!IsLoading && KyTinhLuong != null)
                {
                    TuNgay = KyTinhLuong.TuNgay;
                    DenNgay = KyTinhLuong.DenNgay;
                }
            }
        }      

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết công giảng dạy")]
        [Association("QuanLyCongKhac-ListChiTietCongKhac")]
        public XPCollection<CC_ChiTietCongGiangDay> ListChiTietCongGiangDay
        {
            get
            {
                //
                return GetCollection<CC_ChiTietCongGiangDay>("ListChiTietCongGiangDay");
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
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }
        public CC_QuanLyCongGiangDay(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            //
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);
            //         
            if (CongTy != null)           
                KyTinhLuongList = Common.GetKyTinhLuongNotBlockList_ByCompanyInfo(Session, CongTy);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            UpdateKyTinhLuongList();
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and CongTy.Oid=?", Common.GetServerCurrentTime().Month, Common.GetServerCurrentTime().Year, CongTy != null ? CongTy.Oid : Guid.Empty));
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
