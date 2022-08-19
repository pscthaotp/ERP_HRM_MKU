using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [DefaultClassOptions]
    [ImageName("BO_Contract")]
    [DefaultProperty("NienDoTaiChinh")]
    [ModelDefault("Caption", "Quản lý hợp đồng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "CongTy;NienDoTaiChinh")]
    //
    public class QuanLyHopDong : BaseObject, ICongTy
    {
        private NienDoTaiChinh _NienDoTaiChinh;
        private NamHoc _NamHoc;
        private CongTy _CongTy;
        //
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Niên độ tài chính")]
        [DataSourceProperty("NienDoTaiChinhList")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NienDoTaiChinh NienDoTaiChinh
        {
            get
            {
                return _NienDoTaiChinh;
            }
            set
            {
                SetPropertyValue("NienDoTaiChinh", ref _NienDoTaiChinh, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách hợp đồng")]
        [Association("QuanLyHopDong-ListHopDong")]
        public XPCollection<HopDong> ListHopDong
        {
            get
            {
                return GetCollection<HopDong>("ListHopDong");
            }
        }

        [ModelDefault("Caption", "Trường/Công ty")]
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
                    NienDoTaiChinh = null;
                    UpdateNienDoTaiChinh();
                }
            }
        }

        [Browsable(false)]
        public XPCollection<NienDoTaiChinh> NienDoTaiChinhList { get; set; }

        public QuanLyHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
            NamHoc = Common.GetCurrentNamHoc(Session);
            CongTy = Common.CongTy(Session);
            NienDoTaiChinh = Common.GetCurrentNienDoTaiChinh(Session, CongTy);
        }

        protected void UpdateNienDoTaiChinh()
        {
            if (NienDoTaiChinhList == null)
                NienDoTaiChinhList = new XPCollection<NienDoTaiChinh>(Session);

            if (CongTy != null)
                NienDoTaiChinhList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", this.CongTy.Oid);
            else
                NienDoTaiChinhList = null;
        }     
    }

}
