using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.CauHinh.HeSo;
using ERP.Module.PMS.DanhMuc;

namespace ERP.Module.PMS.CauHinh
{
    [DefaultClassOptions]
    [DefaultProperty("NgonNguGiangDay")]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số ngôn ngữ")]
    
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyHeSo;NgonNguGiangDay", "Ngôn ngữ giảng dạy đã tồn tại")]
    public class HeSoNgonNgu : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private NgonNguGiangDay _NgonNguGiangDay;
        private decimal _HeSo_NgonNgu;

        
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoNgonNgu")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ImmediatePostData]
        public NgonNguGiangDay NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set
            {
                SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value);
            }
        }


        [ModelDefault("Caption", "Hệ số")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_NgonNgu
        {
            get
            {
                return _HeSo_NgonNgu;
            }
            set
            {
                SetPropertyValue("HeSo_NgonNgu", ref _HeSo_NgonNgu, value);
            }
        }

        public HeSoNgonNgu(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            HeSo_NgonNgu = 0;
        }
    }

}
