using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình kho")]
    public class CauHinhKho : BaseObject
    {
        // Fields...
        private int _SoBatDauDonDatHangDuTru_Bep;
        private string _MauSoDonDatHangDuTru_Bep;
        private bool _TuDongTaoSoDonDatHangDuTru_Bep = true;
        //
        private int _SoBatDauDonDeNghiMuaHang_Bep;
        private string _MauSoDonDeNghiMuaHang_Bep;
        private bool _TuDongTaoSoDonDeNghiMuaHang_Bep = true;
        //
        private int _SoBatDauDonDeNghiXuatKho_Bep;
        private string _MauSoDonDeNghiXuatKho_Bep;
        private bool _TuDongTaoSoDonDeNghiXuatKho_Bep = true;
        //
        private int _SoBatDauDonDatHang;
        private string _MauSoDonDatHang;
        private bool _TuDongTaoSoDonDatHang = true;
        //
        private int _SoBatDauDonHangMua;
        private string _MauSoDonHangMua;
        private bool _TuDongTaoSoDonHangMua = true;
        //
        private int _SoBatDauHoaDonKho;
        private string _MauSoHoaDonKho;
        private bool _TuDongTaoSoHoaDonKho = true;
        //
        private int _SoBatDauHopDongMuaBan;
        private string _MauSoHopDongMuaBan;
        private bool _TuDongTaoSoHopDongMuaBan = true;
        //
        private int _SoBatDauDonHangTra;
        private string _MauSoDonHangTra;
        private bool _TuDongTaoSoDonHangTra = true;
        //
        private int _SoBatDauPhieuNhap;
        private string _MauSoPhieuNhap;
        private bool _TuDongTaoSoPhieuNhap = true;
        //
        private int _SoBatDauPhieuXuat;
        private string _MauSoPhieuXuat;
        private bool _TuDongTaoSoPhieuXuat = true;
        //
        private int _SoBatDauDeNghiXuatKho;
        private string _MauSoDeNghiXuatKho;
        private bool _TuDongTaoSoDeNghiXuatKho = true;

        #region Đặt hàng dự trù
        [ModelDefault("Caption", "Số bắt đầu đơn đặt hàng dự trù")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDatHangDuTru_Bep")]
        public int SoBatDauDonDatHangDuTru_Bep
        {
            get
            {
                return _SoBatDauDonDatHangDuTru_Bep;
            }
            set
            {
                SetPropertyValue("SoBatDauDonDatHangDuTru", ref _SoBatDauDonDatHangDuTru_Bep, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đơn đặt hàng dự trù")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDatHangDuTru_Bep")]
        public string MauSoDonDatHangDuTru_Bep
        {
            get
            {
                return _MauSoDonDatHangDuTru_Bep;
            }
            set
            {
                SetPropertyValue("MauSoDonDatHangDuTru_Bep", ref _MauSoDonDatHangDuTru_Bep, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đơn đặt hàng dự trù")]
        public bool TuDongTaoSoDonDatHangDuTru_Bep
        {
            get
            {
                return _TuDongTaoSoDonDatHangDuTru_Bep;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDonDatHangDuTru_Bep", ref _TuDongTaoSoDonDatHangDuTru_Bep, value);
            }
        }
        #endregion đặt hàng dự trù

        #region đề nghị mua hàng
        [ModelDefault("Caption", "Số bắt đầu đề nghị mua hàng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDeNghiMuaHang_Bep")]
        public int SoBatDauDonDeNghiMuaHang_Bep
        {
            get
            {
                return _SoBatDauDonDeNghiMuaHang_Bep;
            }
            set
            {
                SetPropertyValue("SoBatDauDonDeNghiMuaHang", ref _SoBatDauDonDeNghiMuaHang_Bep, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị mua hàng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDeNghiMuaHang_Bep")]
        public string MauSoDonDeNghiMuaHang_Bep
        {
            get
            {
                return _MauSoDonDeNghiMuaHang_Bep;
            }
            set
            {
                SetPropertyValue("MauSoDonDeNghiMuaHang_Bep", ref _MauSoDonDeNghiMuaHang_Bep, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị mua hàng")]
        public bool TuDongTaoSoDonDeNghiMuaHang_Bep
        {
            get
            {
                return _TuDongTaoSoDonDeNghiMuaHang_Bep;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDonDeNghiMuaHang_Bep", ref _TuDongTaoSoDonDeNghiMuaHang_Bep, value);
            }
        }
        #endregion đề nghị mua hàng


        #region đề nghị xuất kho
        [ModelDefault("Caption", "Số bắt đầu đề nghị xuất kho")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDeNghiXuatKho_Bep")]
        public int SoBatDauDonDeNghiXuatKho_Bep
        {
            get
            {
                return _SoBatDauDonDeNghiXuatKho_Bep;
            }
            set
            {
                SetPropertyValue("SoBatDauDonDeNghiXuatKho", ref _SoBatDauDonDeNghiXuatKho_Bep, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị xuất kho")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDeNghiXuatKho_Bep")]
        public string MauSoDonDeNghiXuatKho_Bep
        {
            get
            {
                return _MauSoDonDeNghiXuatKho_Bep;
            }
            set
            {
                SetPropertyValue("MauSoDonDeNghiXuatKho_Bep", ref _MauSoDonDeNghiXuatKho_Bep, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị xuất kho")]
        public bool TuDongTaoSoDonDeNghiXuatKho_Bep
        {
            get
            {
                return _TuDongTaoSoDonDeNghiXuatKho_Bep;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDonDeNghiXuatKho_Bep", ref _TuDongTaoSoDonDeNghiXuatKho_Bep, value);
            }
        }
        #endregion đề nghị xuất kho


        [ModelDefault("Caption", "Số bắt đầu đơn đặt hàng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDatHang")]
        public int SoBatDauDonDatHang
        {
            get
            {
                return _SoBatDauDonDatHang;
            }
            set
            {
                SetPropertyValue("SoBatDauDonDatHang", ref _SoBatDauDonDatHang, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đơn đặt hàng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonDatHang")]
        public string MauSoDonDatHang
        {
            get
            {
                return _MauSoDonDatHang;
            }
            set
            {
                SetPropertyValue("MauSoDonDatHang", ref _MauSoDonDatHang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đơn đặt hàng")]
        public bool TuDongTaoSoDonDatHang
        {
            get
            {
                return _TuDongTaoSoDonDatHang;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDonDatHang", ref _TuDongTaoSoDonDatHang, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu đơn hàng mua")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonHangMua")]
        public int SoBatDauDonHangMua
        {
            get
            {
                return _SoBatDauDonHangMua;
            }
            set
            {
                SetPropertyValue("SoBatDauDonHangMua", ref _SoBatDauDonHangMua, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đơn hàng mua")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonHangMua")]
        public string MauSoDonHangMua
        {
            get
            {
                return _MauSoDonHangMua;
            }
            set
            {
                SetPropertyValue("MauSoDonHangMua", ref _MauSoDonHangMua, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đơn hàng mua")]
        public bool TuDongTaoSoDonHangMua
        {
            get
            {
                return _TuDongTaoSoDonHangMua;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDonHangMua", ref _TuDongTaoSoDonHangMua, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu đơn hàng trả")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonHangTra")]
        public int SoBatDauDonHangTra
        {
            get
            {
                return _SoBatDauDonHangTra;
            }
            set
            {
                SetPropertyValue("SoBatDauDonHangTra", ref _SoBatDauDonHangTra, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đơn hàng trả")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDonHangTra")]
        public string MauSoDonHangTra
        {
            get
            {
                return _MauSoDonHangTra;
            }
            set
            {
                SetPropertyValue("MauSoDonHangTra", ref _MauSoDonHangTra, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đơn hàng trả")]
        public bool TuDongTaoSoDonHangTra
        {
            get
            {
                return _TuDongTaoSoDonHangTra;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDonHangTra", ref _TuDongTaoSoDonHangTra, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu hóa đơn kho")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHoaDonKho")]
        public int SoBatDauHoaDonKho
        {
            get
            {
                return _SoBatDauHoaDonKho;
            }
            set
            {
                SetPropertyValue("SoBatDauHoaDonKho", ref _SoBatDauHoaDonKho, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hóa đơn kho")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHoaDonKho")]
        public string MauSoHoaDonKho
        {
            get
            {
                return _MauSoHoaDonKho;
            }
            set
            {
                SetPropertyValue("MauSoHoaDonKho", ref _MauSoHoaDonKho, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số hóa đơn kho")]
        public bool TuDongTaoSoHoaDonKho
        {
            get
            {
                return _TuDongTaoSoHoaDonKho;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHoaDonKho", ref _TuDongTaoSoHoaDonKho, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu hợp đồng mua bán")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongMuaBan")]
        public int SoBatDauHopDongMuaBan
        {
            get
            {
                return _SoBatDauHopDongMuaBan;
            }
            set
            {
                SetPropertyValue("SoBatDauHopDongMuaBan", ref _SoBatDauHopDongMuaBan, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hợp đồng mua bán")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongMuaBan")]
        public string MauSoHopDongMuaBan
        {
            get
            {
                return _MauSoHopDongMuaBan;
            }
            set
            {
                SetPropertyValue("MauSoHopDongMuaBan", ref _MauSoHopDongMuaBan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số hợp đồng mua bán")]
        public bool TuDongTaoSoHopDongMuaBan
        {
            get
            {
                return _TuDongTaoSoHopDongMuaBan;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHopDongMuaBan", ref _TuDongTaoSoHopDongMuaBan, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu phiếu nhập")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoPhieuNhap")]
        public int SoBatDauPhieuNhap
        {
            get
            {
                return _SoBatDauPhieuNhap;
            }
            set
            {
                SetPropertyValue("SoBatDauPhieuNhap", ref _SoBatDauPhieuNhap, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số phiếu nhập")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoPhieuNhap")]
        public string MauSoPhieuNhap
        {
            get
            {
                return _MauSoPhieuNhap;
            }
            set
            {
                SetPropertyValue("MauSoPhieuNhap", ref _MauSoPhieuNhap, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số phiếu nhập")]
        public bool TuDongTaoSoPhieuNhap
        {
            get
            {
                return _TuDongTaoSoPhieuNhap;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoPhieuNhap", ref _TuDongTaoSoPhieuNhap, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu phiếu xuất")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoPhieuXuat")]
        public int SoBatDauPhieuXuat
        {
            get
            {
                return _SoBatDauPhieuXuat;
            }
            set
            {
                SetPropertyValue("SoBatDauPhieuXuat", ref _SoBatDauPhieuXuat, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số phiếu xuất")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoPhieuXuat")]
        public string MauSoPhieuXuat
        {
            get
            {
                return _MauSoPhieuXuat;
            }
            set
            {
                SetPropertyValue("MauSoPhieuXuat", ref _MauSoPhieuXuat, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số phiếu xuất")]
        public bool TuDongTaoSoPhieuXuat
        {
            get
            {
                return _TuDongTaoSoPhieuXuat;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoPhieuXuat", ref _TuDongTaoSoPhieuXuat, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu đề nghị xuất kho")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiXuatKho")]
        public int SoBatDauDeNghiXuatKho
        {
            get
            {
                return _SoBatDauDeNghiXuatKho;
            }
            set
            {
                SetPropertyValue("SoBatDauDeNghiXuatKho", ref _SoBatDauDeNghiXuatKho, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị xuất kho")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiXuatKho")]
        public string MauSoDeNghiXuatKho
        {
            get
            {
                return _MauSoDeNghiXuatKho;
            }
            set
            {
                SetPropertyValue("MauSoDeNghiXuatKho", ref _MauSoDeNghiXuatKho, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị xuất kho")]
        public bool TuDongTaoSoDeNghiXuatKho
        {
            get
            {
                return _TuDongTaoSoDeNghiXuatKho;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDeNghiXuatKho", ref _TuDongTaoSoDeNghiXuatKho, value);
            }
        }

        public CauHinhKho(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            _SoBatDauDonDatHang = 1;
            _MauSoDonDatHang = "MaTruong" + "DD0" + "mmyy" + "{0000#}";

            _SoBatDauDonHangMua = 1;
            _MauSoDonHangMua = "MaTruong" + "DM0" + "mmyy" + "{0000#}";

            _SoBatDauHoaDonKho = 1;
            _MauSoHoaDonKho = "MaTruong" + "HD0" + "mmyy" + "{0000#}";

            _SoBatDauHopDongMuaBan = 1;
            _MauSoHopDongMuaBan = "{00#}/HĐMH";

            _SoBatDauPhieuNhap = 1;
            _MauSoPhieuNhap = "MaTruong" + "PN0" + "mmyy" + "{0000#}";

            _SoBatDauPhieuXuat = 1;
            _MauSoPhieuXuat = "MaTruong" + "PX0" + "mmyy" + "{0000#}";

            _SoBatDauDeNghiXuatKho = 1;
            _MauSoDeNghiXuatKho = "MaTruong" + "DX0" + "mmyy" + "{0000#}";

            _SoBatDauDonHangTra = 1;
            _MauSoDonHangTra = "MaTruong" + "DT0" + "mmyy" + "{0000#}";
        }
    }

}
