using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NonPersistentObjects.TienLuong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn kỳ chấm công")]
    public class ChamCong_KyChamCong : BaseObject
    {
        //
        private CC_KyChamCong _KyChamCong;

        //
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Kỳ chấm công")]
        [DataSourceCriteria("!KhoaSo")]
        public CC_KyChamCong KyChamCong
        {
            get
            {
                return _KyChamCong;
            }
            set
            {
                SetPropertyValue("KyChamCong", ref _KyChamCong, value);
            }
        }

        public ChamCong_KyChamCong(Session session) : base(session){ }
    }

}
