using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.PMS.BusinessObjects;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý công tác phí")]
    public class QuanLyCongTacPhi : ThongTinChungPMS
    {
        [Aggregated]
        [ModelDefault("Caption", "Chi tiết công tác phí")]
        [Association("QuanLyCongTacPhi-ListChiTietCongTacPhi")]
        public XPCollection<ChiTietCongTacPhi> ListChiTietCongTacPhi
        {
            get
            {
                return GetCollection<ChiTietCongTacPhi>("ListChiTietCongTacPhi");
            }
        }
        public QuanLyCongTacPhi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            NamHoc = Common.GetCurrentNamHoc(Session);
            CongTy = Common.CongTy(Session);
        }

    }
}
