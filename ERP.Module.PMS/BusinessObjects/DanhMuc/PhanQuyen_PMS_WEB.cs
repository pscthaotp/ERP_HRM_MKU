using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.PMS.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Phân quyền sử dụng PMS (WEB)")]
    [Appearance("Yersin_Hide", TargetItems = "HocKy", Visibility = ViewItemVisibility.Hide)]
    public class PhanQuyen_PMS_WEB : ThongTinChungPMS
    {
        [Aggregated]
        [ModelDefault("Caption", "Chi tiết")]
        [Association("PhanQuyen_PMS_WEB-ListChiTiet")]
        public XPCollection<ChiTietPhanQuyen_PMS_WEB> ListChiTiet
        {
            get
            {
                return GetCollection<ChiTietPhanQuyen_PMS_WEB>("ListChiTiet");
            }
        }
        public PhanQuyen_PMS_WEB(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
