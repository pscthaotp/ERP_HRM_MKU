using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.NghiepVu.PMS.DanhMuc;

namespace ERP.Module.NghiepVu.PMS.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số môn học")]
    public class HeSoMonHoc : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;     
        private LoaiHocPhan _LoaiHocPhan;        
        private KhoiNganh _KhoiNganh;               
        private decimal _HeSo;


        [Association("QuanLyHeSo-ListHeSoMonHoc")]
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

        [ModelDefault("Caption", "Khối ngành")]
        public KhoiNganh KhoiNganh
        {
            get
            {
                return _KhoiNganh;
            }
            set
            {
                SetPropertyValue("KhoiNganh", ref _KhoiNganh, value);
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

        public HeSoMonHoc(Session session)
            : base(session)
        {
        }


    }
}
