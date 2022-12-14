using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
//
namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [DefaultProperty("Ten")]
    [ImageName("BO_PhanQuyenBaoCao")]
    [ModelDefault("Caption", "Phân quyền Lớp")]
    public class SecuritySystemRole_Class : BaseObject,ICongTy
    {
        private string _Ten;
        private string _Quyen;
        private CongTy _CongTy;

        //
        [ModelDefault("Caption", "Tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue(DefaultContexts.Save)]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
            }
        }

        [ModelDefault("Caption", "Quyền")]
        [Browsable(false)]
        [Size(-1)]
        public string Quyen
        {
            get
            {
                return _Quyen;
            }
            set
            {
                SetPropertyValue("Quyen", ref _Quyen, value);
            }
        }

        //[Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
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

        public SecuritySystemRole_Class(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
        }
    }

}
