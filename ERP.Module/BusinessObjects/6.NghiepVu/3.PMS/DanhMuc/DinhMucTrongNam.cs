using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.PMS.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Định mức trong năm")]
    public class DinhMucTrongNam : BaseObject
    {
        private ChucDanh _ChucDanh;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChucVu _ChucVu;
        private HocHam _HocHam;
        private decimal _DinhMucGiangDay;
        private decimal _DinhMucNCKH;
        private bool _MacDinh;

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get { return _ChucDanh; }
            set { SetPropertyValue("ChucDanh", ref _ChucDanh, value); }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }

        [ModelDefault("Caption", "Chức vụ")]
        [Browsable(false)]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set { SetPropertyValue("ChucVu", ref _ChucVu, value); }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Định mức giảng dạy")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DinhMucGiangDay
        {
            get { return _DinhMucGiangDay; }
            set { SetPropertyValue("DinhMucGiangDay", ref _DinhMucGiangDay, value); }
        }

        [ModelDefault("Caption", "Định mức NCKH")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DinhMucNCKH
        {
            get { return _DinhMucNCKH; }
            set { SetPropertyValue("DinhMucNCKH", ref _DinhMucNCKH, value); }
        }

        [ModelDefault("Caption", "Mặc định")]
        public bool MacDinh
        {
            get { return _MacDinh; }
            set { SetPropertyValue("MacDinh", ref _MacDinh, value); }
        }
        public DinhMucTrongNam(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
