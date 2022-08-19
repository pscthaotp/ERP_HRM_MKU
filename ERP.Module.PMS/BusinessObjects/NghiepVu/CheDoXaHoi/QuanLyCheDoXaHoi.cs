using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using ERP.Module.PMS.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption","Quản lý chế độ xã hội")]
    [DefaultProperty("Caption")]
    public class QuanLyCheDoXaHoi : ThongTinChungPMS
    {
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        [VisibleInDetailView(false)]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1} {2}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Danh sách chi tiết")]
        [Association("QuanLyCheDoXaHoi-ListChiTietCheDoXH")]
        public XPCollection<ChiTietCheDoXaHoi> ListChiTietCheDoXH
        {
            get
            {
                return GetCollection<ChiTietCheDoXaHoi>("ListChiTietCheDoXH");
            }
        }

        public QuanLyCheDoXaHoi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
    }
}
