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
    [ModelDefault("Caption", "Get chức năng")]
    public class getChucNang : BaseObject
    {
        //
        private Guid _Oid;
        private Guid _ThuMucQuanLy;
        private string _TenChucNang;
        private string _TenPhanHe;
        private string _KeyObject;
        private string _LoaiView;
        private string _LoaiObject;
        private string _ListViewId;
        private string _LoaiCustom;
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
        [Browsable(false)]
        public Guid ThuMucQuanLy
        {
            get
            {
                return _ThuMucQuanLy;
            }
            set
            {
                SetPropertyValue("ThuMucQuanLy", ref _ThuMucQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên chức năng")]
        public string TenChucNang
        {
            get
            {
                return _TenChucNang;
            }
            set
            {
                SetPropertyValue("TenChucNang", ref _TenChucNang, value);
            }
        }

        [ModelDefault("Caption", "TenPhanHe")]
        public string TenPhanHe
        {
            get
            {
                return _TenPhanHe;
            }
            set
            {
                SetPropertyValue("TenPhanHe", ref _TenPhanHe, value);
            }
        }



        [ModelDefault("Caption", "KeyObject")]
        public string KeyObject
        {
            get
            {
                return _KeyObject;
            }
            set
            {
                SetPropertyValue("KeyObject", ref _KeyObject, value);
            }
        }
        [ModelDefault("Caption", "LoaiView")]
        public string LoaiView
        {
            get
            {
                return _LoaiView;
            }
            set
            {
                SetPropertyValue("LoaiView", ref _LoaiView, value);
            }
        }
        [ModelDefault("Caption", "LoaiObject")]
        public string LoaiObject
        {
            get
            {
                return _LoaiObject;
            }
            set
            {
                SetPropertyValue("LoaiObject", ref _LoaiObject, value);
            }
        }
        [ModelDefault("Caption", "ListViewId")]
        public string ListViewId
        {
            get
            {
                return _ListViewId;
            }
            set
            {
                SetPropertyValue("ListViewId", ref _ListViewId, value);
            }
        }
        [ModelDefault("Caption", "LoaiCustom")]
        public string LoaiCustom
        {
            get
            {
                return _LoaiCustom;
            }
            set
            {
                SetPropertyValue("LoaiCustom", ref _LoaiCustom, value);
            }
        }
        public getChucNang(Session session) : base(session) { }
    }

}
