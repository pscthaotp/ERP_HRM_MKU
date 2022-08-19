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
    [ModelDefault("Caption", "Khối ngành")]
    [DefaultProperty("TenKhoiNganh")]
    public class KhoiNganh : BaseObject
    {
        private string _MaQuanLy;
        private string _TenKhoiNganh;
        private KhoiNganh _KhoiNganhCha;

        [ModelDefault("Caption", "Mã quản lý")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên khối ngành")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenKhoiNganh
        {
            get { return _TenKhoiNganh; }
            set { SetPropertyValue("TenKhoiNganh", ref _TenKhoiNganh, value); }
        }


        [ModelDefault("Caption", "Khối ngành cha")]
        //[Browsable(false)]
        public KhoiNganh KhoiNganhCha
        {
            get { return _KhoiNganhCha; }
            set { SetPropertyValue("KhoiNganhCha", ref _KhoiNganhCha, value); }
        }

        public KhoiNganh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
