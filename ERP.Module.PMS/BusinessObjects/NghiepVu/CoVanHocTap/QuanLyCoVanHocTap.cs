using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using ERP.Module.PMS.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý cố vấn học tập")]
    public class QuanLyCoVanHocTap : ThongTinChungPMS
    {
        private decimal _DinhMucCVHT;
        [ModelDefault("Caption", "Định mức CVHT _ Mặc định(%)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMucCVHT
        {
            get { return _DinhMucCVHT; }
            set { SetPropertyValue("DinhMucCVHT", ref _DinhMucCVHT, value); }
        }
        [Aggregated]
        [Association("QuanLyCoVanHocTap-ListCoVanHocTap")]
        [ModelDefault("Caption", "Danh sách cố vấn học tập")]
        public XPCollection<CoVanHocTap> ListCoVanHocTap
        {
            get
            {
                return GetCollection<CoVanHocTap>("ListCoVanHocTap");
            }
        }
        public QuanLyCoVanHocTap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
        }
    }
}
