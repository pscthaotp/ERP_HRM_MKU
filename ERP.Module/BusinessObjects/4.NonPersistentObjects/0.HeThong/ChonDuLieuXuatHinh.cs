using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn dữ liệu")]
    public class ChonDuLieuXuatHinh : BaseObject
    {
        //
        private string _Query;
        private string _DuongDan;

        //
        [ModelDefault("Caption", "Thực thi")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string Query
        {
            get
            {
                return _Query;
            }
            set
            {
                SetPropertyValue("Query", ref _Query, value);
            }
        }

        [ModelDefault("Caption", "Đường dẫn xuất")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string DuongDan
        {
            get
            {
                return _DuongDan;
            }
            set
            {
                SetPropertyValue("DuongDan", ref _DuongDan, value);
            }
        }

        public ChonDuLieuXuatHinh(Session session) : base(session) { }
    }

}
