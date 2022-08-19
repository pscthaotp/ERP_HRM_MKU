using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn loại đơn vị")]
    public class BoPhan_ChonLoaiBoPhan : BaseObject
    {
        //
        private LoaiBoPhanEnum _LoaiBoPhan = LoaiBoPhanEnum.PhongBan;

        [ModelDefault("Caption", "Loại đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiBoPhanEnum LoaiBoPhan
        {
            get
            {
                return _LoaiBoPhan;
            }
            set
            {
                SetPropertyValue("LoaiBoPhan", ref _LoaiBoPhan, value);
            }
        }

        public BoPhan_ChonLoaiBoPhan(Session session) : base(session) { }
    }

}
