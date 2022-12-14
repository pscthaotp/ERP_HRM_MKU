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
using ERP.Module.DanhMuc.System;
//
namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [DefaultProperty("Ten")]
    [ImageName("BO_PhanQuyenBaoCao")]
    [ModelDefault("Caption", "Phân quyền duyệt")]
    [RuleCombinationOfPropertiesIsUnique("SecuritySystemRole_Accept", DefaultContexts.Save, "PhanHe;SecuritySystemUser;CongTy")]
    public class SecuritySystemRole_Accept : BaseObject, ICongTy
    {
        private PhanHe _PhanHe;
        private SecuritySystemUser_Custom _SecuritySystemUser;
        private CongTy _CongTy;

        //
        [ModelDefault("Caption", "Phân hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanHe PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("PhanHe", ref _PhanHe, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản")]
        public SecuritySystemUser_Custom SecuritySystemUser
        {
            get
            {
                return _SecuritySystemUser;
            }
            set
            {
                SetPropertyValue("SecuritySystemUser", ref _SecuritySystemUser, value);
            }
        }


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
        public SecuritySystemRole_Accept(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
        }
    }

}
