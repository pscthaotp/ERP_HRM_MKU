using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
 
namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Biểu mẫu excel")]
    [DefaultProperty("TenBieuMau")]
    [RuleCombinationOfPropertiesIsUnique("BieuMau.Unique", DefaultContexts.Save, "TenBieuMau;TargetTypeName")]
    public class ExcelTemplate : BaseObject
    {
        //
        private FileData _File;
        private string _TenBieuMau;
        private string _TargetTypeName;

        [ModelDefault("Caption", "Tên biểu mẫu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBieuMau
        {
            get
            {
                return _TenBieuMau;
            }
            set
            {
                SetPropertyValue("TenBieuMau", ref _TenBieuMau, value);
            }
        }

        [Browsable(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TargetTypeName
        {
            get
            {
                return _TargetTypeName;
            }
            set
            {
                SetPropertyValue("TargetTypeName", ref _TargetTypeName, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Kiểu dữ liệu đích")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Type TargetType
        {
            get
            {
                if (!String.IsNullOrEmpty(TargetTypeName))
                {
                    ITypeInfo info = XafTypesInfo.Instance.FindTypeInfo(TargetTypeName);
                    if (info != null)
                        return info.Type;
                    return null;
                }
                return null;
            }
            set
            {
                TargetTypeName = (value != null) ? value.FullName : string.Empty;
            }
        }

        [ModelDefault("Caption", "Lưu trữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public FileData File
        {
            get
            {
                return _File;
            }
            set
            {
                SetPropertyValue("File", ref _File, value);
            }
        }

        public ExcelTemplate(Session session) : base(session) { }
    }

}
