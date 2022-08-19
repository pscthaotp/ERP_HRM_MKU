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
    [ModelDefault("Caption", "Ngành học")]
    [DefaultProperty("TenNganhHoc")]
    [ModelDefault("AllowNew", "false")]
    public class NganhHoc : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNganhHoc;
        private BoPhan _DonViChuQuan;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên ngành học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNganhHoc
        {
            get { return _TenNganhHoc; }
            set { SetPropertyValue("TenNganhHoc", ref _TenNganhHoc, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan DonViChuQuan
        {
            get { return _DonViChuQuan; }
            set { SetPropertyValue("DonViChuQuan", ref _DonViChuQuan, value); }
        }
        public NganhHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
