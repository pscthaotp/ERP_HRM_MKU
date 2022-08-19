using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.DanhMuc.PMS
{
    [ModelDefault("Caption","Danh mục khoảng cách")]
    public class DanhMucKhoangCach : BaseObject
    {
        private TinhThanh _DiaDiemA;
        private TinhThanh _DiaDiemB;
        private decimal _SoKm;

        [ModelDefault("Caption", "Địa điểm giảng viên")]
        public TinhThanh DiaDiemA
        {
            get { return _DiaDiemA; }
            set
            {
                SetPropertyValue("DiaDiemA", ref _DiaDiemA, value);
            }
        }

        [ModelDefault("Caption", "Địa điểm phòng học")]
        public TinhThanh DiaDiemB
        {
            get { return _DiaDiemB; }
            set 
            {
                SetPropertyValue("DiaDiemB", ref _DiaDiemB, value);
            }
        }

        [ModelDefault("Captiom", "Số km")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoKm 
        {
            get {return _SoKm; }
            set
            {
                SetPropertyValue("SoKm", ref _SoKm, value); 
            }
        }

        public DanhMucKhoangCach (Session session) : base(session) { }
        public override void AfterConstruction()
        {
 	         base.AfterConstruction();
        }
    }
}
