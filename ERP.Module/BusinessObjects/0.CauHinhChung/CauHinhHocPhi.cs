using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Enum.Systems;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình học phí")]
    public class CauHinhHocPhi : BaseObject
    {
        //
        private int _SoBatDauMaHoaDon;
        private string _MauMaHoaDon;

        private string _MauSoHoaDon;
        private string _KyHieuHoaDon;
        //
        private int _SoBatDauMaBienLai;
        private string _MauMaBienLai;
        //
        private int _SoBatDauKyHieuBienLai;
        private string _MauKyHieuBienLai;
        //
        private int _SoBatDauMaPhieuChi;
        private string _MauMaPhieuChi;
        //
        private int _SoBatDauKyHieuPhieuChi;
        private string _MauKyHieuPhieuChi;
        //
        private int _SoThangBaoLuu;
        //
        private decimal _SoNgayHocBinhQuan;
        private bool _ThuTheoNgay;
        private bool _ThuTheoGiuaThang;
        private bool _ThuTheoTuan;
        //
        private DateTime _NgayApDung;
        //
        private CauHinhChung _CauHinhChung;
        private int _SoTuanBaoLuu;
        private int _SoDongPhieuThu;
        private int _SoDongHoaDon;
        private string _MaDinhDanh;
        private bool _DoDuDiemDanhThu7;

        [ModelDefault("Caption", "Đổ điểm danh thứ 7")]
        public bool DoDuDiemDanhThu7
        {
            get { return _DoDuDiemDanhThu7; }
            set { SetPropertyValue("DoDuDiemDanhThu7", ref _DoDuDiemDanhThu7, value); }
        }
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("CauHinhChung-ListCauHinhHocPhi")]
        [ModelDefault("Caption", "Cấu hình chung")]
        public CauHinhChung CauHinhChung
        {
            get
            {
                return _CauHinhChung;
            }
            set
            {
                SetPropertyValue("CauHinhChung", ref _CauHinhChung, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu mã hóa đơn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoBatDauMaHoaDon
        {
            get
            {
                return _SoBatDauMaHoaDon;
            }
            set
            {
                SetPropertyValue("SoBatDauMaHoaDon", ref _SoBatDauMaHoaDon, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã hóa đơn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MauMaHoaDon
        {
            get
            {
                return _MauMaHoaDon;
            }
            set
            {
                SetPropertyValue("MauMaHoaDon", ref _MauMaHoaDon, value);
            }
        }
        [ModelDefault("Caption", "Mẫu số hóa đơn")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string MauSoHoaDon
        {
            get
            {
                return _MauSoHoaDon;
            }
            set
            {
                SetPropertyValue("MauSoHoaDon", ref _MauSoHoaDon, value);
            }
        }
        [ModelDefault("Caption", "Ký hiệu hóa đơn")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string KyHieuHoaDon
        {
            get
            {
                return _KyHieuHoaDon;
            }
            set
            {
                SetPropertyValue("KyHieuHoaDon", ref _KyHieuHoaDon, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu mã biên lai")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoBatDauMaBienLai
        {
            get
            {
                return _SoBatDauMaBienLai;
            }
            set
            {
                SetPropertyValue("SoBatDauMaBienLai", ref _SoBatDauMaBienLai, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã biên lai")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MauMaBienLai
        {
            get
            {
                return _MauMaBienLai;
            }
            set
            {
                SetPropertyValue("MauMaBienLai", ref _MauMaBienLai, value);
            }
        }


        [ModelDefault("Caption", "Số bắt đầu ký hiệu biên lai")]
        public int SoBatDauKyHieuBienLai
        {
            get
            {
                return _SoBatDauKyHieuBienLai;
            }
            set
            {
                SetPropertyValue("SoBatDauKyHieuBienLai", ref _SoBatDauKyHieuBienLai, value);
            }
        }

        [ModelDefault("Caption", "Mẫu ký hiệu biên lai")]
        public string MauKyHieuBienLai
        {
            get
            {
                return _MauKyHieuBienLai;
            }
            set
            {
                SetPropertyValue("MauKyHieuBienLai", ref _MauKyHieuBienLai, value);
            }
        }


        [ModelDefault("Caption", "Số bắt đầu mã phiếu chi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoBatDauMaPhieuChi
        {
            get
            {
                return _SoBatDauMaPhieuChi;
            }
            set
            {
                SetPropertyValue("SoBatDauMaPhieuChi", ref _SoBatDauMaPhieuChi, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã phiếu chi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MauMaPhieuChi
        {
            get
            {
                return _MauMaPhieuChi;
            }
            set
            {
                SetPropertyValue("MauMaPhieuChi", ref _MauMaPhieuChi, value);
            }
        }


        [ModelDefault("Caption", "Số bắt đầu ký hiệu phiếu chi")]
        public int SoBatDauKyHieuPhieuChi
        {
            get
            {
                return _SoBatDauKyHieuPhieuChi;
            }
            set
            {
                SetPropertyValue("SoBatDauKyHieuPhieuChi", ref _SoBatDauKyHieuPhieuChi, value);
            }
        }

        [ModelDefault("Caption", "Mẫu ký hiệu phiếu chi")]
        public string MauKyHieuPhieuChi
        {
            get
            {
                return _MauKyHieuPhieuChi;
            }
            set
            {
                SetPropertyValue("MauKyHieuPhieuChi", ref _MauKyHieuPhieuChi, value);
            }
        }

        [ModelDefault("Caption", "Số tháng bảo lưu")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public int SoThangBaoLuu
        {
            get
            {
                return _SoThangBaoLuu;
            }
            set
            {
                SetPropertyValue("SoThangBaoLuu", ref _SoThangBaoLuu, value);
            }
        }

        [ModelDefault("Caption", "Số ngày học bình quân")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoNgayHocBinhQuan
        {
            get
            {
                return _SoNgayHocBinhQuan;
            }
            set
            {
                SetPropertyValue("SoNgayHocBinhQuan", ref _SoNgayHocBinhQuan, value);
            }
        }

        [ModelDefault("Caption", "Thu theo ngày")]
        public bool ThuTheoNgay
        {
            get
            {
                return _ThuTheoNgay;
            }
            set
            {
                SetPropertyValue("ThuTheoNgay", ref _ThuTheoNgay, value);
            }
        }

        [ModelDefault("Caption", "Thu theo giữa tháng")]
        public bool ThuTheoGiuaThang
        {
            get
            {
                return _ThuTheoGiuaThang;
            }
            set
            {
                SetPropertyValue("ThuTheoGiuaThang", ref _ThuTheoGiuaThang, value);
            }
        }

        [ModelDefault("Caption", "Thu theo tuần")]
        public bool ThuTheoTuan
        {
            get
            {
                return _ThuTheoTuan;
            }
            set
            {
                SetPropertyValue("ThuTheoTuan", ref _ThuTheoTuan, value);
            }
        }

        [ModelDefault("Caption", "Ngày áp dụng")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayApDung
        {
            get
            {
                return _NgayApDung;
            }
            set
            {
                SetPropertyValue("SoNgayHocBinhQuan", ref _NgayApDung, value);
            }
        }
        [ModelDefault("Caption", "Số tuần bảo lưu (Tối thiểu)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoTuanBaoLuu
        {
            get
            {
                return _SoTuanBaoLuu;
            }
            set
            {
                SetPropertyValue("SoTuanBaoLuu", ref _SoTuanBaoLuu, value);
            }
        }
        [ModelDefault("Caption", "Số dòng phiếu thu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoDongPhieuThu
        {
            get
            {
                return _SoDongPhieuThu;
            }
            set
            {
                SetPropertyValue("SoDongPhieuThu", ref _SoDongPhieuThu, value);
            }
        }
        [ModelDefault("Caption", "Số dòng hóa đơn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoDongHoaDon
        {
            get
            {
                return _SoDongHoaDon;
            }
            set
            {
                SetPropertyValue("SoDongHoaDon", ref _SoDongHoaDon, value);
            }
        }
        [ModelDefault("Caption", "Mã định danh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaDinhDanh
        {
            get
            {
                return _MaDinhDanh;
            }
            set
            {
                SetPropertyValue("MaDinhDanh", ref _MaDinhDanh, value);
            }
        }
        public CauHinhHocPhi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
        }
    }

}
