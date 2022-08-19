using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình quyết định")]
    public class CauHinhQuyetDinh : BaseObject
    {
        //
        private string _QuyetDinhThoiViec;
        private string _QuyetDinhThoiChucKiemNhiem;
        private string _QuyetDinhThoiChuc;
        private string _QuyetDinhThanhLapDonVi;
        private string _QuyetDinhSapNhapDonVi;
        private string _QuyetDinhNghiKhongHuongLuong;
        private string _QuyetDinhNghiHuu;
        private string _QuyetDinhNangLuong;
        private string _QuyetDinhMienNhiemKiemNhiem;
        private string _QuyetDinhMienNhiem;
        private string _QuyetDinhDieuDong;
        private string _QuyetDinhHopDong;
        private string _QuyetDinhGiaiTheDonVi;
        private string _QuyetDinhDoiTenDonVi;
        private string _QuyetDinhDiCongTac;
        private string _QuyetDinhChuyenCongTac;
        private string _QuyetDinhChiaTachDonVi;
        private string _QuyetDinhChamDutHopDong;
        private string _QuyetDinhBoNhiemKiemNhiem;
        private string _QuyetDinhBoNhiem;
        private string _QuyetDinhTaiBoNhiem;
        private string _QuyetDinhNangThamNienNghe;
        private string _QuyetDinhNangNgach;
        private string _QuyetDinhTuyenDung;
        private string _QuyetDinhTiepNhan;
        private string _QuyetDinhDaoTao;
        private string _QuyetDinhBietPhai;
        private string _QuyetDinhLuanChuyen;
        private string _QuyetDinhTamHoan;
        private string _QuyetDinhTienLuongChinhThuc;
        private string _QuyetDinhTienLuongThuViec;
        private string _QuyetDinhThongBaoChamDutThuViec;
        private string _QuyetDinhKhenThuong;
        private string _QuyetDinhKyLuat;

        [ModelDefault("Caption", "QĐ bổ nhiệm")]
        public string QuyetDinhBoNhiem
        {
            get
            {
                return _QuyetDinhBoNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiem", ref _QuyetDinhBoNhiem, value);
            }
        }


        [ModelDefault("Caption", "QĐ tái bổ nhiệm")]
        public string QuyetDinhTaiBoNhiem
        {
            get
            {
                return _QuyetDinhTaiBoNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhTaiBoNhiem", ref _QuyetDinhTaiBoNhiem, value);
            }
        }



        [ModelDefault("Caption", "QĐ bổ nhiệm kiêm nhiệm")]
        public string QuyetDinhBoNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhBoNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemKiemNhiem", ref _QuyetDinhBoNhiemKiemNhiem, value);
            }
        }


        [ModelDefault("Caption", "QĐ chấm dứt hợp đồng")]
        public string QuyetDinhChamDutHopDong
        {
            get
            {
                return _QuyetDinhChamDutHopDong;
            }
            set
            {
                SetPropertyValue("QuyetDinhChamDutHopDong", ref _QuyetDinhChamDutHopDong, value);
            }
        }

        [ModelDefault("Caption", "QĐ chia tách đơn vị")]
        public string QuyetDinhChiaTachDonVi
        {
            get
            {
                return _QuyetDinhChiaTachDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhChiaTachDonVi", ref _QuyetDinhChiaTachDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ chuyển công tác")]
        public string QuyetDinhChuyenCongTac
        {
            get
            {
                return _QuyetDinhChuyenCongTac;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenCongTac", ref _QuyetDinhChuyenCongTac, value);
            }
        }


        [ModelDefault("Caption", "QĐ đi công tác")]
        public string QuyetDinhDiCongTac
        {
            get
            {
                return _QuyetDinhDiCongTac;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiCongTac", ref _QuyetDinhDiCongTac, value);
            }
        }


        [ModelDefault("Caption", "QĐ đổi tên đơn vị")]
        public string QuyetDinhDoiTenDonVi
        {
            get
            {
                return _QuyetDinhDoiTenDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhDoiTenDonVi", ref _QuyetDinhDoiTenDonVi, value);
            }
        }


        [ModelDefault("Caption", "QĐ giải thể đơn vị")]
        public string QuyetDinhGiaiTheDonVi
        {
            get
            {
                return _QuyetDinhGiaiTheDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhGiaiTheDonVi", ref _QuyetDinhGiaiTheDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ hợp đồng")]
        public string QuyetDinhHopDong
        {
            get
            {
                return _QuyetDinhHopDong;
            }
            set
            {
                SetPropertyValue("QuyetDinhHopDong", ref _QuyetDinhHopDong, value);
            }
        }


        [ModelDefault("Caption", "QĐ điều động")]
        public string QuyetDinhDieuDong
        {
            get
            {
                return _QuyetDinhDieuDong;
            }
            set
            {
                SetPropertyValue("QuyetDinhDieuDong", ref _QuyetDinhDieuDong, value);
            }
        }

        [ModelDefault("Caption", "QĐ miễn nhiệm")]
        public string QuyetDinhMienNhiem
        {
            get
            {
                return _QuyetDinhMienNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhMienNhiem", ref _QuyetDinhMienNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ miễn nhiệm kiêm nhiệm")]
        public string QuyetDinhMienNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhMienNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhMienNhiemKiemNhiem", ref _QuyetDinhMienNhiemKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ nâng lương")]
        public string QuyetDinhNangLuong
        {
            get
            {
                return _QuyetDinhNangLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangLuong", ref _QuyetDinhNangLuong, value);
            }
        }


        [ModelDefault("Caption", "QĐ nghỉ hưu")]
        public string QuyetDinhNghiHuu
        {
            get
            {
                return _QuyetDinhNghiHuu;
            }
            set
            {
                SetPropertyValue("QuyetDinhNghiHuu", ref _QuyetDinhNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "QĐ nghỉ không hưởng lương")]
        public string QuyetDinhNghiKhongHuongLuong
        {
            get
            {
                return _QuyetDinhNghiKhongHuongLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNghiKhongHuongLuong", ref _QuyetDinhNghiKhongHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ sáp nhập đơn vị")]
        public string QuyetDinhSapNhapDonVi
        {
            get
            {
                return _QuyetDinhSapNhapDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhSapNhapDonVi", ref _QuyetDinhSapNhapDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ thành lập đơn vị")]
        public string QuyetDinhThanhLapDonVi
        {
            get
            {
                return _QuyetDinhThanhLapDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapDonVi", ref _QuyetDinhThanhLapDonVi, value);
            }
        }


        [ModelDefault("Caption", "QĐ miễn nhiệm")]
        public string QuyetDinhThoiChuc
        {
            get
            {
                return _QuyetDinhThoiChuc;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiChuc", ref _QuyetDinhThoiChuc, value);
            }
        }

        [ModelDefault("Caption", "QĐ thôi chức kiêm nhiệm")]
        public string QuyetDinhThoiChucKiemNhiem
        {
            get
            {
                return _QuyetDinhThoiChucKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiChucKiemNhiem", ref _QuyetDinhThoiChucKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ thôi việc")]
        public string QuyetDinhThoiViec
        {
            get
            {
                return _QuyetDinhThoiViec;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiViec", ref _QuyetDinhThoiViec, value);
            }
        }

        [ModelDefault("Caption", "QĐ nâng thâm niên nghề")]
        public string QuyetDinhNangThamNienNghe
        {
            get
            {
                return _QuyetDinhNangThamNienNghe;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangThamNienNghe", ref _QuyetDinhNangThamNienNghe, value);
            }
        }

        [ModelDefault("Caption", "QĐ nâng ngạch")]
        public string QuyetDinhNangNgach
        {
            get
            {
                return _QuyetDinhNangNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangNgach", ref _QuyetDinhNangNgach, value);
            }
        }

        [ModelDefault("Caption", "QĐ tuyển dụng")]
        public string QuyetDinhTuyenDung
        {
            get
            {
                return _QuyetDinhTuyenDung;
            }
            set
            {
                SetPropertyValue("QuyetDinhTuyenDung", ref _QuyetDinhTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "QĐ tiếp nhận")]
        public string QuyetDinhTiepNhan
        {
            get
            {
                return _QuyetDinhTiepNhan;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhan", ref _QuyetDinhTiepNhan, value);
            }
        }

        [ModelDefault("Caption", "QĐ đào tạo")]
        public string QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "QĐ biệt phái")]
        public string QuyetDinhBietPhai
        {
            get
            {
                return _QuyetDinhBietPhai;
            }
            set
            {
                SetPropertyValue("QuyetDinhBietPhai", ref _QuyetDinhBietPhai, value);
            }
        }

        [ModelDefault("Caption", "QĐ luân chuyển")]
        public string QuyetDinhLuanChuyen
        {
            get
            {
                return _QuyetDinhLuanChuyen;
            }
            set
            {
                SetPropertyValue("QuyetDinhLuanChuyen", ref _QuyetDinhLuanChuyen, value);
            }
        }

        [ModelDefault("Caption", "QĐ tạm hoãn")]
        public string QuyetDinhTamHoan
        {
            get
            {
                return _QuyetDinhTamHoan;
            }
            set
            {
                SetPropertyValue("QuyetDinhTamHoan", ref _QuyetDinhTamHoan, value);
            }
        }

        [ModelDefault("Caption", "QĐ tiền lương chính thức")]
        public string QuyetDinhTienLuongChinhThuc
        {
            get
            {
                return _QuyetDinhTienLuongChinhThuc;
            }
            set
            {
                SetPropertyValue("QuyetDinhTienLuongChinhThuc", ref _QuyetDinhTienLuongChinhThuc, value);
            }
        }

        [ModelDefault("Caption", "QĐ tiền lương thử việc")]
        public string QuyetDinhTienLuongThuViec
        {
            get
            {
                return _QuyetDinhTienLuongThuViec;
            }
            set
            {
                SetPropertyValue("QuyetDinhTienLuongThuViec", ref _QuyetDinhTienLuongThuViec, value);
            }
        }

        [ModelDefault("Caption", "QĐ thông báo chấm dứt thử việc")]
        public string QuyetDinhThongBaoChamDutThuViec
        {
            get
            {
                return _QuyetDinhThongBaoChamDutThuViec;
            }
            set
            {
                SetPropertyValue("QuyetDinhThongBaoChamDutThuViec", ref _QuyetDinhThongBaoChamDutThuViec, value);
            }
        }

        [ModelDefault("Caption", "QĐ khen thưởng")]
        public string QuyetDinhKhenThuong
        {
            get
            {
                return _QuyetDinhKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhKhenThuong", ref _QuyetDinhKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ kỷ luật")]
        public string QuyetDinhKyLuat
        {
            get
            {
                return _QuyetDinhKyLuat;
            }
            set
            {
                SetPropertyValue("QuyetDinhKyLuat", ref _QuyetDinhKyLuat, value);
            }
        }

        public CauHinhQuyetDinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            QuyetDinhBoNhiem = "bổ nhiệm nhân sự";
            QuyetDinhTaiBoNhiem = "tái bổ nhiệm nhân sự";
            QuyetDinhBoNhiemKiemNhiem = "bổ nhiệm chức vụ viên chức";
            QuyetDinhChamDutHopDong = "chấm dứt hợp đồng";
            QuyetDinhChiaTachDonVi = "chia tách đơn vị";
            QuyetDinhDiCongTac = "cử viên chức đi công tác";
            QuyetDinhDoiTenDonVi = "đổi tên đơn vị";
            QuyetDinhGiaiTheDonVi = "giải thể đơn vị";
            QuyetDinhHopDong = "hợp đồng viên chức";
            QuyetDinhDieuDong = "điều động viên chức";
            QuyetDinhMienNhiem = "miễn nhiệm chức vụ";
            QuyetDinhMienNhiemKiemNhiem = "miễn nhiệm chức vụ";
            QuyetDinhNangLuong = "nâng lương viên chức";
            QuyetDinhNghiHuu = "cho nghỉ việc để hưởng chế độ BHXH";
            QuyetDinhNghiKhongHuongLuong = "viên chức nghỉ không hưởng lương";
            QuyetDinhSapNhapDonVi = "sáp nhập đơn vị";
            QuyetDinhThanhLapDonVi = "thành lập đơn vị";
            QuyetDinhThoiChuc = "thôi chức vụ viên chức quản lý";
            QuyetDinhThoiChucKiemNhiem = "thôi chức vụ viên chức quản lý";
            QuyetDinhThoiViec = "thôi việc viên chức";
            QuyetDinhNangThamNienNghe = "nâng thâm niên nghề";
            QuyetDinhNangNgach = "nâng ngạch";
            QuyetDinhTuyenDung = "tuyển dụng";
            QuyetDinhTiepNhan = "tiếp nhận";
            QuyetDinhDaoTao = "đào tạo";
            QuyetDinhBietPhai = "biệt phái";
            QuyetDinhLuanChuyen = "luân chuyển";
            QuyetDinhTamHoan = "tạm hoãn";
            QuyetDinhTienLuongChinhThuc = "lương chính thức";
            QuyetDinhTienLuongThuViec = "lương thử việc";
            QuyetDinhThongBaoChamDutThuViec = "thông báo chấm dứt thử việc";
            QuyetDinhKhenThuong = "khen thưởng";
            QuyetDinhKhenThuong = "kỷ luật";
        }
    }
}
