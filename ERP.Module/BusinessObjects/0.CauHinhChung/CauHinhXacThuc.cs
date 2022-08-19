using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Enum.Systems;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình xác thực")]
    public class CauHinhXacThuc : BaseObject
    {
        //
        private bool _XacThuc;
        private bool _PhanQuyenTaiKhoan = false;
        private bool _DongBoTaiKhoan = true;

        //

        [ModelDefault("Caption", "Xác thực đăng nhập")]
        public bool XacThuc
        {
            get
            {
                return _XacThuc;
            }
            set
            {
                SetPropertyValue("XacThuc", ref _XacThuc, value);
            }
        }

        [ModelDefault("Caption", "Phân quyền Tài khoản")]
        public bool PhanQuyenTaiKhoan
        {
            get
            {
                return _PhanQuyenTaiKhoan;
            }
            set
            {
                SetPropertyValue("PhanQuyenTaiKhoan", ref _PhanQuyenTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Đồng bộ tài khoản")]
        public bool DongBoTaiKhoan
        {
            get
            {
                return _DongBoTaiKhoan;
            }
            set
            {
                SetPropertyValue("DongBoTaiKhoan", ref _DongBoTaiKhoan, value);
            }
        }
        public CauHinhXacThuc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
        }
    }

}
