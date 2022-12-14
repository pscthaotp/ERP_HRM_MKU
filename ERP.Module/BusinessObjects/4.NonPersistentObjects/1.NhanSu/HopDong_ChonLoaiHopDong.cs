using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn loại hợp đồng")]
    public class HopDong_ChonLoaiHopDong : BaseObject
    {
        //
        private LoaiHopDong _LoaiHopDong;

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiHopDong LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }


        public HopDong_ChonLoaiHopDong(Session session) : base(session) { }
    }

}
