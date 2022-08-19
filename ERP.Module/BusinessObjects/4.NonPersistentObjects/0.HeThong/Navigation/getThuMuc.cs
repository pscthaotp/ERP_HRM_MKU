using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using System.ComponentModel;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Get thư mục")]
    public class getThuMuc : BaseObject
    {
        //
        private Guid _Oid;
        private string _MaQuanLy;
        private string _TenPhanhe;
        private string _TenThuMuc;

        [Browsable(false)]
        public Guid Oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                SetPropertyValue("Oid", ref _Oid, value);
            }
        }

        //
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên phân hệ")]
        public string TenPhanHe
        {
            get
            {
                return _TenPhanhe;
            }
            set
            {
                SetPropertyValue("TenPhanHe", ref _TenPhanhe, value);
            }
        }
        [ModelDefault("Caption", "Tên thư mục")]
        public string TenThuMuc
        {
            get
            {
                return _TenThuMuc;
            }
            set
            {
                SetPropertyValue("TenThuMuc", ref _TenThuMuc, value);
            }
        }
        public getThuMuc(Session session) : base(session) { }
    }

}
