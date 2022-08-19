using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNhomChucVu")]
    [ModelDefault("Caption", "Nhóm chức vụ")]
    public class NhomChucVu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhomChucVu;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên nhóm chức vụ")]
        public string TenNhomChucVu
        {
            get
            {
                return _TenNhomChucVu;
            }
            set
            {
                SetPropertyValue("TenNhomChucVu", ref _TenNhomChucVu, value);
            }
        }

        public NhomChucVu(Session session) : base(session) { }
    }
   
}
