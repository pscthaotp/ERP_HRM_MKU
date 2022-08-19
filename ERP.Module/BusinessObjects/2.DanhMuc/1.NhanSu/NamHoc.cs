using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.Data.SqlClient;
using System.Data;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNamHoc")]
    [ModelDefault("Caption", "Năm học")]
    [Appearance("NamHoc.TanPhu", TargetItems = "ListTuanHoc;ListHocKy", Visibility = ViewItemVisibility.Hide, Criteria = "IsTanPhu")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "NgayBatDau,NgayKetThuc")]

    public class NamHoc : BaseObject
    {
        private DateTime _NgayBatDau;
        private DateTime _NgayKetThuc;
        private string _TenNamHoc;

        public NamHoc(Session session) : base(session) { }

        // Check User
        [NonPersistent]
        [Browsable(false)]
        public bool IsTanPhu { get; set; }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm bắt đầu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBatDau
        {
            get
            {
                return _NgayBatDau;
            }
            set
            {
                SetPropertyValue("NgayBatDau", ref _NgayBatDau, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm kết thúc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayKetThuc
        {
            get
            {
                return _NgayKetThuc;
            }
            set
            {
                SetPropertyValue("NgayKetThuc", ref _NgayKetThuc, value);
            }
        }
        
        //[Persistent]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string TenNamHoc
        {
            get
            {
                return _TenNamHoc;
            }
            set
            {
                SetPropertyValue("TenNamHoc", ref _TenNamHoc, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tuần")]
        [Association("NamHoc-ListTuanHoc")]
        public XPCollection<TuanHoc> ListTuanHoc
        {
            get
            {
                return GetCollection<TuanHoc>("ListTuanHoc");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách học kỳ")]
        [Association("NamHoc-ListHocKy")]
        public XPCollection<HocKy> ListHocKy
        {
            get
            {
                return GetCollection<HocKy>("ListHocKy");
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CheckUserTanPhu();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            CheckUserTanPhu();
        }

        void CheckUserTanPhu()
        {
            CongTy congTy = Common.CongTy(Session);
            //
            if (congTy != null)
            {
                if (congTy.Oid.Equals(Config.KeyTanPhu))
                    IsTanPhu = true;
                else
                    IsTanPhu = false;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();        
            //NgayBatDau = NgayBatDau.SetTime(Enum.Systems.SetTimeEnum.StartDay);
            //NgayKetThuc = NgayKetThuc.SetTime(Enum.Systems.SetTimeEnum.EndDay);

        }
    }

}

