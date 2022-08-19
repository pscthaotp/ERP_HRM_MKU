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
using ERP.Module.NghiepVu.PMS.QuanLy;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NghiepVu.PMS.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số giờ quy chuẩn")]
    public class HeSoGioQuyChuan : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private int _TuTiet;
        private int _DenTiet;
        private decimal _HeSo;
        [Association("QuanLyHeSo-ListHeSoGioQuyChuan")]
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý hệ số")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }


        [ModelDefault("Caption", "Từ tiết")]
        public int TuTiet
        {
            get
            {
                return _TuTiet;
            }
            set
            {
                SetPropertyValue("TuTiet", ref _TuTiet, value);
            }
        }

        [ModelDefault("Caption", "Đến tiết")]
        public int DenTiet
        {
            get
            {
                return _DenTiet;
            }
            set
            {
                SetPropertyValue("DenTiet", ref _DenTiet, value);
            }
        }
        
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        public HeSoGioQuyChuan(Session session)
            : base(session)
        {
        }


    }
}