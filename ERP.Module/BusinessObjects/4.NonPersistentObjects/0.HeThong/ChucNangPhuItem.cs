using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.HeThong;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn dữ liệu")]
    public class ChucNangPhuItem : BaseObject
    {
        //
        private bool _Chon;
        private AppMenu _AppMenu;
        private string _PhanHe;

        //
        [ModelDefault("Caption", "Tất cả")]
        [ImmediatePostData]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Phân hệ")]
        public string PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("_PhanHe", ref _PhanHe, value);
            }
        }

        [ModelDefault("Caption", "Tên chức năng")]
        public AppMenu AppMenu
        {
            get
            {
                return _AppMenu;
            }
            set
            {
                SetPropertyValue("AppMenu", ref _AppMenu, value);
            }
        }

        public ChucNangPhuItem(Session session) : base(session) { }
    }

}
