using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Loại HĐ phân quyền")]
    [DefaultProperty("TenHD_PhanQuyen")]
    public class LoaiHoatDong_PhanQuyen_WEB : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHD_PhanQuyen;
        private bool _HDKhac;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên HĐ phân quyền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenHD_PhanQuyen
        {
            get { return _TenHD_PhanQuyen; }
            set { SetPropertyValue("TenHD_PhanQuyen", ref _TenHD_PhanQuyen, value); }
        }

        [ModelDefault("Caption", "Check HĐ Khác")]
        public bool HDKhac
        {
            get { return _HDKhac; }
            set { SetPropertyValue("HDKhac", ref _HDKhac, value); }
        }
        public LoaiHoatDong_PhanQuyen_WEB(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
