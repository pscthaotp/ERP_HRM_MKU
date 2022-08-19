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
    [ImageName("BO_DanhHieuKhenThuong")]
    [ModelDefault("Caption", "Danh hiệu khen thưởng")]
    [DefaultProperty("TenDanhHieu")]
    public class DanhHieuKhenThuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDanhHieu;
        private bool _CaNhan;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên danh hiệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDanhHieu
        {
            get
            {
                return _TenDanhHieu;
            }
            set
            {
                SetPropertyValue("TenDanhHieu", ref _TenDanhHieu, value);
            }
        }
          
        [ModelDefault("Caption", "Cá nhân")]
        public bool CaNhan
        {
            get
            {
                return _CaNhan;
            }
            set
            {
                SetPropertyValue("CaNhan", ref _CaNhan, value);
            }
        }

        public DanhHieuKhenThuong(Session session) : base(session) { }
    }

}
