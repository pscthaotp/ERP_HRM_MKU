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
    [ModelDefault("Caption", "Hệ số lớp đông")]
    public class HeSoLopDong : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private LoaiHocPhan _LoaiHocPhan;
        private int _TuKhoan;
        private int _DenKhoan;
        private KhoiNganh _KhoiNganh;
        //private string _HeSoLopDong;
        //private string _LopHocPhan;
        //private int _SiSo;
        private decimal _HeSo;
        [Association("QuanLyHeSo-ListHeSoLopDong")]
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

        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhan LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }


        [ModelDefault("Caption", "Từ khoản")]
        public int TuKhoan
        {
            get
            {
                return _TuKhoan;
            }
            set
            {
                SetPropertyValue("TuKhoan", ref _TuKhoan, value);
            }
        }

        [ModelDefault("Caption", "Đến khoản")]
        public int DenKhoan
        {
            get
            {
                return _DenKhoan;
            }
            set
            {
                SetPropertyValue("DenKhoan", ref _DenKhoan, value);
            }
        }

        [ModelDefault("Caption", "Khối ngành")]     
        public KhoiNganh KhoiNganh
        {
            get { return _KhoiNganh; }
            set { SetPropertyValue("KhoiNganh", ref _KhoiNganh, value); }
        }


        //[ModelDefault("Caption", "Công thức hệ số")]
        //[ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.btnEdit_CongThucPMS")]
        //[Size(-1)]
        //public string HeSo_LopDong
        //{
        //    get { return _HeSoLopDong; }
        //    set { _HeSoLopDong = value; }
        //}

        //[ModelDefault("Caption", "Lớp học phần")]
        //[ModelDefault("AllowEdit", "false")]
        //public string LopHocPhan
        //{
        //    get
        //    {
        //        return _LopHocPhan;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LopHocPhan", ref _LopHocPhan, value);
        //    }
        //}

        //[ModelDefault("Caption", "Sỉ số")]
        //[ModelDefault("AllowEdit", "false")]
        //public int SiSo
        //{
        //    get
        //    {
        //        return _SiSo;
        //    }
        //    set
        //    {
        //        SetPropertyValue("SiSo", ref _SiSo, value);
        //    }
        //}

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]       
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        public HeSoLopDong(Session session)
            : base(session)
        {
        }


    }
}
