using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.HeThong;
using DevExpress.Data.Filtering;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Nhật ký")]
    public class AuditDataItemNonPer : BaseObject
    {
        private string _UserName;
        private DateTime _ModifiedOn;
        private string _PropertyName;
        private string _OldValue;
        private string _NewValue;
        private string _Description;

        [ModelDefault("Caption", "Tài khoản")]
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                SetPropertyValue("UserName", ref _UserName, value);
            }
        }


        [ModelDefault("Caption", "Ngày giờ")]
        public DateTime ModifiedOn
        {
            get
            {
                return _ModifiedOn;
            }
            set
            {
                SetPropertyValue("ModifiedOn", ref _ModifiedOn, value);
            }
        }

        [ModelDefault("Caption", "Cột")]
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                SetPropertyValue("PropertyName", ref _PropertyName, value);
            }
        }

        [ModelDefault("Caption", "Giá trị cũ")]
        public string OldValue
        {
            get
            {
                return _OldValue;
            }
            set
            {
                SetPropertyValue("OldValue", ref _OldValue, value);
            }
        }

        [ModelDefault("Caption", "Giá trị mới")]
        public string NewValue
        {
            get
            {
                return _NewValue;
            }
            set
            {
                SetPropertyValue("NewValue", ref _NewValue, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Ghi chú")]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue("Description", ref _Description, value);
            }
        }

        public AuditDataItemNonPer(Session session) : base(session) { }
    }

}
