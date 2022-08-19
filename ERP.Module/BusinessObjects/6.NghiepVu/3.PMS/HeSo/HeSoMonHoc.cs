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
using ERP.Module.NghiepVu.PMS.DanhMuc;

namespace ERP.Module.NghiepVu.PMS.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số môn học")]
    public class HeSoMonHoc : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private HeDaoTao _HeDaoTao;
        //private ChuongTrinhDaoTao _ChuongTrinhDaoTao;
        private KhoiNganh _KhoiNganh;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _LoaiHocPhan;
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

        //[ModelDefault("Caption", "Chương trình đào tạo")]
        //public ChuongTrinhDaoTao ChuongTrinhDaoTao
        //{
        //    get
        //    {
        //        return _ChuongTrinhDaoTao;
        //    }
        //    set
        //    {
        //        SetPropertyValue("ChuongTrinhDaoTao", ref _ChuongTrinhDaoTao, value);
        //    }
        //}


        [ModelDefault("Caption", "Mã học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string MaHocPhan
        {
            get
            {
                return _MaHocPhan;
            }
            set
            {
                SetPropertyValue("MaHocPhan", ref _MaHocPhan, value);
            }
        }

        [ModelDefault("Caption", "Tên học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string TenHocPhan
        {
            get
            {
                return _TenHocPhan;
            }
            set
            {
                SetPropertyValue("TenHocPhan", ref _TenHocPhan, value);
            }
        }

        [ModelDefault("Caption", "Loại học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string LoaiHocPhan
        {
            get
            {
                return _LoaiHocPhan;
            }
            set
            {
                SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value);
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
