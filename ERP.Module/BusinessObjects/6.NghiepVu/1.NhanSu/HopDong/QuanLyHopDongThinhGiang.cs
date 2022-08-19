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
//
namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [DefaultClassOptions]
    [ImageName("BO_Contract")]
    [DefaultProperty("Nam")]
    [ModelDefault("Caption", "Quản lý hợp đồng thỉnh giảng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "CongTy;NamHoc")]
    //
    public class QuanLyHopDongThinhGiang : BaseObject, ICongTy
    {
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách hợp đồng")]
        [Association("QuanLyHopDongThinhGiang-ListHopDong")]
        public XPCollection<HopDongThinhGiang> ListHopDong
        {
            get
            {
                return GetCollection<HopDongThinhGiang>("ListHopDong");
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
        public QuanLyHopDongThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
            NamHoc = Common.GetCurrentNamHoc(Session);
            CongTy = Common.CongTy(Session);
        }
    }

}
