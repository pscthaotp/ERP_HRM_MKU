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
    [ModelDefault("Caption", "Chọn loại chức năng")]
    public class SelectTypeAppMenu : BaseObject
    {
        private LoaiChucNangEnum _LoaiChucNang = LoaiChucNangEnum.ThuMucQuanLy;

        [ModelDefault("Caption", "Loại chức năng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiChucNangEnum LoaiChucNang
        {
            get
            {
                return _LoaiChucNang;
            }
            set
            {
                SetPropertyValue("LoaiChucNang", ref _LoaiChucNang, value);
            }
        }

        public SelectTypeAppMenu(Session session)
            : base(session)
        {
        }
    }
}
