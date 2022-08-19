using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nghỉ không hưởng lương")]
    public class QuyetDinhNghiKhongHuongLuong : QuyetDinhCaNhan
    {
        //
        private TinhTrang _TinhTrang;
        private DateTime _MocNangLuongDieuChinhMoi;
        private DateTime _MocNangLuongDieuChinhCu;
        private bool _CoDongBaoHiem;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private string _LyDoDieuChinhCu;

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    int thang = TuNgay.GetMonthNumber(DenNgay);
                    if (thang > 0)
                    {
                        if (MocNangLuongDieuChinhCu != DateTime.MinValue
                            && ThongTinNhanVien != null
                            && MocNangLuongDieuChinhCu > ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau)
                            MocNangLuongDieuChinhMoi = MocNangLuongDieuChinhCu.AddMonths(thang);
                        else
                            MocNangLuongDieuChinhMoi = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong.AddMonths(thang);
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    int thang = TuNgay.GetMonthNumber(DenNgay);
                    if (thang > 0)
                    {
                        if (MocNangLuongDieuChinhCu != DateTime.MinValue
                            && ThongTinNhanVien != null
                            && MocNangLuongDieuChinhCu > ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau)
                            MocNangLuongDieuChinhMoi = MocNangLuongDieuChinhCu.AddMonths(thang);
                        else
                            MocNangLuongDieuChinhMoi = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong.AddMonths(thang);
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh")]
        public DateTime MocNangLuongDieuChinhMoi
        {
            get
            {
                return _MocNangLuongDieuChinhMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinhMoi", ref _MocNangLuongDieuChinhMoi, value);
            }
        }

        [ModelDefault("Caption", "Có đóng bảo hiểm")]
        public bool CoDongBaoHiem
        {
            get
            {
                return _CoDongBaoHiem;
            }
            set
            {
                SetPropertyValue("CoDongBaoHiem", ref _CoDongBaoHiem, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh")]
        public DateTime MocNangLuongDieuChinhCu
        {
            get
            {
                return _MocNangLuongDieuChinhCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinhCu", ref _MocNangLuongDieuChinhCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Lý do điều chỉnh cũ")]
        public string LyDoDieuChinhCu
        {
            get
            {
                return _LyDoDieuChinhCu;
            }
            set
            {
                SetPropertyValue("LyDoDieuChinhCu", ref _LyDoDieuChinhCu, value);
            }
        }

        //lưu vết tình trạng
        [Browsable(false)]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        public QuyetDinhNghiKhongHuongLuong(Session session) : base(session) { }

        protected override void AfterNhanVienChanged()
        {
            base.AfterNhanVienChanged();

            MocNangLuongDieuChinhCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh;
            LyDoDieuChinhCu = ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh;
            TinhTrang = ThongTinNhanVien.TinhTrang;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhTiepNhan;
            //
            QuyetDinhMoi = true;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (QuyetDinhMoi)
                {
                    //thiết lập tình trạng
                    if (TuNgay <= Common.GetServerCurrentTime())
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ? or TenTinhTrang like ?", "Nghỉ không hưởng lương", "Nghỉ không lương");
                        TinhTrang tinhTrang = Session.FindObject<TinhTrang>(filter);
                        if (tinhTrang == null)
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.MaQuanLy = "NKHL";
                            tinhTrang.TenTinhTrang = "Nghỉ không hưởng lương";
                        }
                        ThongTinNhanVien.TinhTrang = tinhTrang;

                        JobUpdated = true;
                    }

                    //mốc nâng lương điều chỉnh
                    if (MocNangLuongDieuChinhMoi != DateTime.MinValue)
                    {
                        int thang = TuNgay.GetMonthNumber(DenNgay);
                        ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = MocNangLuongDieuChinhMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = string.Concat(ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh, " Nghỉ không hưởng lương ", thang.ToString(), " tháng.");
                    }
                }
            }
        }

        protected override void OnDeleting()
        {
            CriteriaOperator filter;
            if (QuyetDinhMoi)
            {
                if (TinhTrang != null)
                    ThongTinNhanVien.TinhTrang = TinhTrang;
                else
                {
                    filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc");
                    ThongTinNhanVien.TinhTrang = Session.FindObject<TinhTrang>(filter);
                }

                if (MocNangLuongDieuChinhCu != DateTime.MinValue)
                {
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = MocNangLuongDieuChinhCu;
                    ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = LyDoDieuChinhCu;
                }
            }

            base.OnDeleting();
        }
    }

}
