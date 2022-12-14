using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;

namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Phân quyền đơn vị")]
    [DefaultProperty("Ten")]
    [ImageName("BO_Category")]
    public class SecuritySystemRole_Department : BaseObject,ICongTy
    {
        private string _Ten;
        private string _Quyen;
        private CongTy _CongTy;
        private LoaiPhanMenEnum _LoaiPhanMen;
        //
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Loại phần mềm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiPhanMenEnum LoaiPhanMen
        {
            get
            {
                return _LoaiPhanMen;
            }
            set
            {
                SetPropertyValue("LoaiPhanMen", ref _LoaiPhanMen, value);
            }
        }

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
        public SecuritySystemRole_Department(Session session) :  base(session)   { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            if (Config.TypeApplication.Equals("WebForm"))
            {
                LoaiPhanMen = LoaiPhanMenEnum.Web;
            }
            else
            {
                LoaiPhanMen = LoaiPhanMenEnum.Win;
            }
        }
    }

}
