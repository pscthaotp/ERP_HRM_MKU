using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Nhóm môn")]
    [DefaultProperty("TenNhomMon")]
    public class NhomMon : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhomMon;
        private int _ThuTu;

        [ModelDefault("Caption", "Mã quản lý")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên nhóm môn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomMon
        {
            get { return _TenNhomMon; }
            set { SetPropertyValue("TenNhomMon", ref _TenNhomMon, value); }
        }

        [ModelDefault("Caption", "Thứ tự")]
        public int ThuTu
        {
            get { return _ThuTu; }
            set { SetPropertyValue("ThuTu", ref _ThuTu, value); }
        }


        public NhomMon(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
