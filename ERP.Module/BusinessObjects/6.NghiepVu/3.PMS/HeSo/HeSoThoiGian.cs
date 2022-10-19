using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.Enum.PMS;
using ERP.Module.NghiepVu.PMS.DanhMuc;

namespace ERP.Module.NghiepVu.PMS.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số thời gian (ngoài giờ)")]
    public class HeSoThoiGian : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private DayOfWeekEnum _Thu;
        private int _TuTiet;
        private int _DenTiet;
        private decimal _HeSo;
        [Association("QuanLyHeSo-ListHeSoThoiGian")]
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

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get
            {
                return _BacDaoTao;
            }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get
            {
                return _HeDaoTao;
            }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
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

        [ModelDefault("Caption", "Thứ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DayOfWeekEnum Thu
        {
            get
            {
                return _Thu;
            }
            set
            {
                SetPropertyValue("Thu", ref _Thu, value);
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

        public HeSoThoiGian(Session session)
            : base(session)
        {
        }


    }
}