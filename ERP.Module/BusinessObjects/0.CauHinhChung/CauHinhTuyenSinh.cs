using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình tuyển sinh")]
    public class CauHinhTuyenSinh : BaseObject
    {
        private NamHoc _NamHoc;
        
        //
        private int _SoBatDauMaKhachHang;
        private int _SoBatDauSoHoSo;
        private int _SoBatDauMaDanhSachCho;
        private int _SoBatDauMaDuThi;
        private int _SoBatDauMaMember;

        private bool _TuDongTaoSoHoSo;
        private bool _TuDongTaoMaKhachHang;
        private bool _TuDongTaoMaDanhSachCho;
        private bool _TuDongTaoMaDuThi;
        private bool _TuDongTaoMaMember;
        //
       
        private string _MauMaKhachHang;
        private string _MauSoHoSo;
        private string _MauMaDanhSachCho;
        private string _MauMaDuThi;
        private string _MauMaMember;
        
        //
        private int _ThoiGianPhanHoiYKien = 24;
        [ModelDefault("Caption", "Năm học mặc định")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }
        //
        [ModelDefault("Caption", "Số giờ")]
        public int ThoiGianPhanHoiYKien
        {
            get
            {
                return _ThoiGianPhanHoiYKien;
            }
            set
            {
                SetPropertyValue("ThoiGianPhanHoiYKien", ref _ThoiGianPhanHoiYKien, value);
            }
        }

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã khách hàng")]
        public bool TuDongTaoMaKhachHang
        {
            get
            {
                return _TuDongTaoMaKhachHang;
            }
            set
            {
                SetPropertyValue("TuDongTaoMaKhachHang", ref _TuDongTaoMaKhachHang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số hồ sơ")]
        public bool TuDongTaoSoHoSo
        {
            get
            {
                return _TuDongTaoSoHoSo;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHoSo", ref _TuDongTaoSoHoSo, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu mã khách hàng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaKhachHang")]
        public int SoBatDauMaKhachHang
        {
            get
            {
                return _SoBatDauMaKhachHang;
            }
            set
            {
                SetPropertyValue("SoBatDauMaKhachHang", ref _SoBatDauMaKhachHang, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu số hồ sơ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHoSo")]
        public int SoBatDauSoHoSo
        {
            get
            {
                return _SoBatDauSoHoSo;
            }
            set
            {
                SetPropertyValue("SoBatDauSoHoSo", ref _SoBatDauSoHoSo, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã khách hàng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaKhachHang")]
        public string MauMaKhachHang
        {
            get
            {
                return _MauMaKhachHang;
            }
            set
            {
                SetPropertyValue("MauMaKhachHang", ref _MauMaKhachHang, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hồ sơ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHoSo")]
        public string MauSoHoSo
        {
            get
            {
                return _MauSoHoSo;
            }
            set
            {
                SetPropertyValue("MauSoHoSo", ref _MauSoHoSo, value);
            }
        }

        #region Cấu hình tạo mã member
        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã member")]
        public bool TuDongTaoMaMember
        {
            get
            {
                return _TuDongTaoMaMember;
            }
            set
            {
                SetPropertyValue("TuDongTaoMaMember", ref _TuDongTaoMaMember, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã Member")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaMember")]
        public string MauMaMember
        {
            get
            {
                return _MauMaMember;
            }
            set
            {
                SetPropertyValue("MauMaMember", ref _MauMaMember, value);
            }
        }
        [ModelDefault("Caption", "Số bắt đầu số member")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaMember")]
        public int SoBatDauMaMember
        {
            get
            {
                return _SoBatDauMaMember;
            }
            set
            {
                SetPropertyValue("SoBatDauMaMember", ref _SoBatDauMaMember, value);
            }
        }
        #endregion

        #region cấu hình mã Dự thi
        [ModelDefault("Caption", "Mẫu mã dự thi")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaDuThi")]
        public string MauMaDuThi
        {
            get
            {
                return _MauMaDuThi;
            }
            set
            {
                SetPropertyValue("MauMaDuThi", ref _MauMaDuThi, value);
            }
        }


        [ModelDefault("Caption", "Số bắt đầu mã dự thi")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaDuThi")]
        public int SoBatDauMaDuThi
        {
            get
            {
                return _SoBatDauMaDuThi;
            }
            set
            {
                SetPropertyValue("SoBatDauMaDuThi", ref _SoBatDauMaDuThi, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã dự thi")]
        public bool TuDongTaoMaDuThi
        {
            get
            {
                return _TuDongTaoMaDuThi;
            }
            set
            {
                SetPropertyValue("TuDongTaoMaDuThi", ref _TuDongTaoMaDuThi, value);
            }
        }

        #endregion

        #region cấu hình mã danh sách chờ

        [ModelDefault("Caption", "Số bắt đầu mã danh sách chờ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaDanhSachCho")]
        public int SoBatDauMaDanhSachCho
        {
            get
            {
                return _SoBatDauMaDanhSachCho;
            }
            set
            {
                SetPropertyValue("SoBatDauMaDanhSachCho", ref _SoBatDauMaDanhSachCho, value);
            }
        }
        [ModelDefault("Caption", "Mẫu mã danh sách chờ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaDanhSachCho")]
        public string MauMaDanhSachCho
        {
            get
            {
                return _MauMaDanhSachCho;
            }
            set
            {
                SetPropertyValue("MauMaDanhSachCho", ref _MauMaDanhSachCho, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã danh sách chờ")]
        public bool TuDongTaoMaDanhSachCho
        {
            get
            {
                return _TuDongTaoMaDanhSachCho;
            }
            set
            {
                SetPropertyValue("TuDongTaoMaDanhSachCho", ref _TuDongTaoMaDanhSachCho, value);
            }
        }
        #endregion
        public CauHinhTuyenSinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            TuDongTaoMaKhachHang = true;
            SoBatDauMaKhachHang = 1;
            MauMaKhachHang = "KH{00#}";
            //
            TuDongTaoSoHoSo = true;
            SoBatDauSoHoSo = 1;
            MauSoHoSo = "HS{00#}";
        }
    }

}
