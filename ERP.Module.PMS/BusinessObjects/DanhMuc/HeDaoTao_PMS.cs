using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
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
    [ModelDefault("Caption", "Hệ đào tạo _PMS")]
    [DefaultProperty("TenHeDaoTao")]
    public class HeDaoTao_PMS : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHeDaoTao;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên hệ đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHeDaoTao
        {
            get { return _TenHeDaoTao; }
            set { SetPropertyValue("TenHeDaoTao", ref _TenHeDaoTao, value); }
        }
        public HeDaoTao_PMS(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
