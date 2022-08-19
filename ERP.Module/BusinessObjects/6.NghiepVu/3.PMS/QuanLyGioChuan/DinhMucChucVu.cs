using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.PMS.QuanLyGioChuan
{
    [ModelDefault("Caption", "Định mức chức vụ")]
    public class DinhMucChucVu : BaseObject
    {
        private QuanLyGioChuan _QuanLyGioChuan;
        private ChucVu _ChucVu;
        private decimal _DinhMucGiangDay;
        private decimal _DinhMuc_NCKH;
        private decimal _DinhMuc_Khac;
        private int _SoGiangVienToiThieu;
        private int _SoSVToiThieu;
        private string _GhiChu;


        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMucChucVu")]
        [Browsable(false)]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }
        [ModelDefault("Caption", "Chức vụ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        //[ModelDefault("Caption", "Chức vụ Đảng")]
        //[ImmediatePostData]
        //public ChucVuDang ChucVuDang
        //{
        //    get { return _ChucVuDang; }
        //    set
        //    {
        //        _ChucVuDang = value;
        //    }
        //}
        //[ModelDefault("Caption", "Chức vụ Đoàn")]
        //[ImmediatePostData]
        //public ChucVuDoan ChucVuDoan
        //{
        //    get { return _ChucVuDoan; }
        //    set
        //    {
        //        _ChucVuDoan = value;
        //    }
        //}
        //[ModelDefault("Caption", "Chức vụ Đoàn thể")]
        //[ImmediatePostData]
        //public ChucVuDoanThe ChucVuDoanThe
        //{
        //    get { return _ChucVuDoanThe; }
        //    set
        //    {
        //        _ChucVuDoanThe = value;
        //    }
        //}
        //[ModelDefault("Caption", "Định mức (%)")]
        //[ModelDefault("DisplayFormat", "N1")]
        //[ModelDefault("EditMask", "N1")]
        //[ImmediatePostData]
        //public decimal DinhMuc
        //{
        //    get { return _DinhMuc; }
        //    set
        //    {
        //        SetPropertyValue("DinhMuc", ref _DinhMuc, value);
        //        if (!IsLoading)
        //        {
        //            PSC_HRM.Module.CauHinh.CauHinhChung cauHinh = HamDungChung.CauHinhChung;
        //            if (cauHinh != null)
        //            {
        //                SoGioChuan = cauHinh.SoGioChuan * DinhMuc / 100;
        //                SoGioDinhMuc_NCHK = cauHinh.SoGioChuan_NCHK;
        //                SoGioDinhMuc_Khac = cauHinh.SoGioChuan_Khac;
        //            }
        //        }
        //    }
        //}

        [ModelDefault("Caption", "Định mức giảng dạy")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMucGiangDay
        {
            get { return _DinhMucGiangDay; }
            set { SetPropertyValue("DinhMucGiangDay", ref _DinhMucGiangDay, value); }
        }
        [ModelDefault("Caption", "Định mức (NCKH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMuc_NCKH
        {
            get { return _DinhMuc_NCKH; }
            set { SetPropertyValue("DinhMuc_NCKH", ref _DinhMuc_NCKH, value); }
        }
        [ModelDefault("Caption", "Đinh mức(Khác)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMuc_Khac
        {
            get { return _DinhMuc_Khac; }
            set { SetPropertyValue("DinhMuc_Khac", ref _DinhMuc_Khac, value); }
        }

        [ModelDefault("Caption", "Số giảng viên (Tối thiểu)")]
        public int SoGiangVienToiThieu
        {
            get { return _SoGiangVienToiThieu; }
            set { SetPropertyValue("SoGiangVienToiThieu", ref _SoGiangVienToiThieu, value); }
        }

        [ModelDefault("Caption", "Số sinh viên (Tối thiểu)")]
        public int SoSVToiThieu
        {
            get { return _SoSVToiThieu; }
            set { SetPropertyValue("SoSVToiThieu", ref _SoSVToiThieu, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }


        public DinhMucChucVu(Session session) : base(session) { }
    }
}