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
    [ModelDefault("Caption", "Copy bảng chốt thông tin tính lương")]
    public class HoSoLuong_ChonLoaiChot : BaseObject
    {
        //
        private LoaiChotEnum _LoaiChot = LoaiChotEnum.CopyDot1_Dot2;

        [ModelDefault("Caption", "Chọn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiChotEnum LoaiChot
        {
            get
            {
                return _LoaiChot;
            }
            set
            {
                SetPropertyValue("LoaiChot", ref _LoaiChot, value);
            }
        }

        public HoSoLuong_ChonLoaiChot(Session session) : base(session){ }
    }

}
