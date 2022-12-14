using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
//
namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ImageName("BO_Position")]
    [DefaultProperty("NhanNguoiKy")]
    [ModelDefault("Caption", "Người ký tên báo cáo")]
    [RuleCombinationOfPropertiesIsUnique("ReportSigners", DefaultContexts.Save, "SecuritySystemUser_Custom;TenNguoiKy")]
    public class ReportSigners : BaseObject
    {
        private string _NhanNguoiKy;
        private string _TenNguoiKy;
        private string _Code;
        private BoPhan _BoPhan;
        private SecuritySystemUser_Custom _SecuritySystemUser_Custom;

        [ModelDefault("Caption", "Nhãn người ký")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NhanNguoiKy
        {
            get
            {
                return _NhanNguoiKy;
            }
            set
            {
                SetPropertyValue("NhanNguoiKy", ref _NhanNguoiKy, value);
                if (!IsLoading && value != string.Empty)
                {
                    GetCode();
                }
            }
        }

        [ModelDefault("Caption", "Tên người ký")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNguoiKy
        {
            get
            {
                return _TenNguoiKy;
            }
            set
            {
                SetPropertyValue("TenNguoiKy", ref _TenNguoiKy, value);
            }
        }

        [ModelDefault("Caption", "Code")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [ModelDefault("AllowEdit","False")]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Người sử dụng")]
        [Association("SecuritySystemUser_Custom-ListNguoiKyTenBaoCao")]
        public SecuritySystemUser_Custom SecuritySystemUser_Custom
        {
            get
            {
                return _SecuritySystemUser_Custom;
            }
            set
            {
                SetPropertyValue("SecuritySystemUser_Custom", ref _SecuritySystemUser_Custom, value);
            }
        }

        public ReportSigners(Session session) : base(session) { }

        private void GetCode()
        {
            this.Code = ERP.Module.Extends.StringHelpers.ReplaceVietnameseChar(ERP.Module.Extends.StringHelpers.ToTitleCase(this.NhanNguoiKy)).Replace(" ", String.Empty);
        }
    }

}
