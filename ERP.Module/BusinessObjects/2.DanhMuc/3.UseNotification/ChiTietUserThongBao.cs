using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.HeThong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace ERP.Module.DanhMuc.UseNotification
{
    [ModelDefault("Caption", "Thông báo")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "SecuritySystemUser", "Đã tồn tại.")]
    public class ChiTietUserThongBao : BaseObject
    {
        private QuanLyThongBao _QuanLyThongBao;
        private SecuritySystemUser_Custom _SecuritySystemUser;
        [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [Browsable(false)]
        [Association("QuanLyThongBao-ListChiTietUserThongBao")]
        public QuanLyThongBao QuanLyThongBao
        {
            get
            {
                return _QuanLyThongBao;
            }
            set
            {
                SetPropertyValue("QuanLyThongBao", ref _QuanLyThongBao, value);
            }
        }
        public ChiTietUserThongBao(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}