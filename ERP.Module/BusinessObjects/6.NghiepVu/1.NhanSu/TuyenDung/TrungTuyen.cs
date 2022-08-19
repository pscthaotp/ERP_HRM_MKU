using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [DefaultProperty("UngVien")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Trúng tuyển")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyTuyenDung;UngVien;NhuCauTuyenDung")]
    //[Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class TrungTuyen : BaseObject
    {
        // Fields...
        private bool IsEnable = true;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private NhuCauTuyenDung _NhuCauTuyenDung;
        private UngVien _UngVien;
        private int _ThoiGianThuViec = 12;
        private DateTime _NgayNhanViec;
        private DateTime _NgayBoNhiemNgach;
        private decimal _PhanTramTinhLuong = 85;
        //private decimal _HeSoLuong;
        private BacLuong _BacLuong;
        private NgachLuong _NgachLuong;
        private decimal _LuongCoBan;
        private decimal _LuongKinhDoanh;
        private TrangThaiNhanViecEnum _TrangThaiNhanViec;
        private bool _DaTaoHoSo = false;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListTrungTuyen")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
                if (!IsLoading && value != null)
                {
                    UngVien = null;
                    UpdateUngVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ứng viên")]
        [DataSourceProperty("QuanLyTuyenDung.ListUngVien")]
        [RuleRequiredField(DefaultContexts.Save)]
        public UngVien UngVien
        {
            get
            {
                return _UngVien;
            }
            set
            {
                SetPropertyValue("UngVien", ref _UngVien, value);
                if (!IsLoading && value != null)
                    NhuCauTuyenDung = value.NhuCauTuyenDung;
            }
        }

        [ModelDefault("Caption", "Vị trí ứng tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListNhuCauTuyenDung")]
        public NhuCauTuyenDung NhuCauTuyenDung
        {
            get
            {
                return _NhuCauTuyenDung;
            }
            set
            {
                SetPropertyValue("NhuCauTuyenDung", ref _NhuCauTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày nhận việc*")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TrangThaiNhanViec = 1")]
        public DateTime NgayNhanViec
        {
            get
            {
                return _NgayNhanViec;
            }
            set
            {
                SetPropertyValue("NgayNhanViec", ref _NgayNhanViec, value);
                if (!IsLoading && NgayNhanViec != DateTime.MinValue && ThoiGianThuViec > 0)
                {
                    NgayBoNhiemNgach = NgayNhanViec.AddMonths(ThoiGianThuViec).AddDays(-1);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                    BacLuong = null;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                {
                    LuongCoBan = value.LuongCoBan;
                    LuongKinhDoanh = value.LuongKinhDoanh;
                }
            }
        }

        //[ModelDefault("Caption", "Hệ số lương")]
        //[ModelDefault("EditMask", "N2")]
        //[ModelDefault("DisplayFormat", "N2")]
        //public decimal HeSoLuong
        //{
        //    get
        //    {
        //        return _HeSoLuong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
        //    }
        //}

        [ModelDefault("Caption", "% tính lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhanTramTinhLuong
        {
            get
            {
                return _PhanTramTinhLuong;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuong", ref _PhanTramTinhLuong, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Lương gộp")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongGop
        {
            get
            {
                return LuongCoBan + LuongKinhDoanh;
            }
        }

        [ModelDefault("Caption", "Lương chức danh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongCoBan
        {
            get
            {
                return _LuongCoBan;
            }
            set
            {
                SetPropertyValue("LuongCoBan", ref _LuongCoBan, value);
            }
        }

        [ModelDefault("Caption", "Lương bổ sung(HQCV)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongKinhDoanh
        {
            get
            {
                return _LuongKinhDoanh;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanh", ref _LuongKinhDoanh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thời gian thử việc (tháng)")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianThuViec
        {
            get
            {
                return _ThoiGianThuViec;
            }
            set
            {
                SetPropertyValue("ThoiGianThuViec", ref _ThoiGianThuViec, value);
                if (!IsLoading && NgayNhanViec != DateTime.MinValue && ThoiGianThuViec > 0)
                {
                    NgayBoNhiemNgach = NgayNhanViec.AddMonths(ThoiGianThuViec).AddDays(-1);
                }
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc thử việc")]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return _NgayBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgach", ref _NgayBoNhiemNgach, value);
            }
        }

        [ModelDefault("Caption", "Trạng thái nhận việc")]
        public TrangThaiNhanViecEnum TrangThaiNhanViec
        {
            get
            {
                return _TrangThaiNhanViec;
            }
            set
            {
                SetPropertyValue("TrangThaiNhanViec", ref _TrangThaiNhanViec, value);
            }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Đã tạo hồ sơ")]
        public bool DaTaoHoSo
        {
            get
            {
                return _DaTaoHoSo;
            }
            set
            {
                SetPropertyValue("DaTaoHoSo", ref _DaTaoHoSo, value);
            }
        }

        public TrungTuyen(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (QuanLyTuyenDung == null) return;
            QuyetDinhTuyenDung quyetDinh = Session.FindObject<QuyetDinhTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=?", QuanLyTuyenDung.Oid));
            if (quyetDinh == null) return;
            CriteriaOperator filter;
            filter = CriteriaOperator.Parse("QuyetDinhTuyenDung=? && GCRecord IS NULL", quyetDinh.Oid);
            using (XPCollection<ChiTietQuyetDinhTuyenDung> ctList = new XPCollection<ChiTietQuyetDinhTuyenDung>(Session, filter))
            {
                foreach (ChiTietQuyetDinhTuyenDung ctItem in ctList)
                {
                    if (UngVien.CMND == ctItem.ThongTinNhanVien.CMND)
                        IsEnable = false;
                }
            }
        }

        [Browsable(false)]
        public XPCollection<UngVien> UngVienList { get; set; }

        private void UpdateUngVienList()
        {
            if (UngVienList == null)
                UngVienList = new XPCollection<UngVien>(Session);
            else
                UngVienList.Reload();

            //chỉ lấy các ứng viên ở vòng tuyển dụng cuối cùng
            List<Guid> oid = new List<Guid>();
            CriteriaOperator filter;
            object thuTu;
            foreach (ChiTietTuyenDung item in QuanLyTuyenDung.ListChiTietTuyenDung)
            {
                filter = CriteriaOperator.Parse("ChiTietTuyenDung=?",
                    item.Oid);
                thuTu = Session.Evaluate<BuocTuyenDung>(CriteriaOperator.Parse("Max(ThuTu)"), filter);
                if (thuTu != null)
                {
                    filter = CriteriaOperator.Parse("VongTuyenDung.ChiTietTuyenDung=? and VongTuyenDung.BuocTuyenDung.ThuTu=?",
                        item.Oid, thuTu);
                    using (XPCollection<ChiTietVongTuyenDung> ctList = new XPCollection<ChiTietVongTuyenDung>(Session, filter))
                    {
                        foreach (ChiTietVongTuyenDung ctItem in ctList)
                        {
                            oid.Add(ctItem.UngVien.Oid);
                        }
                    }
                }
            }
            UngVienList.Criteria = new InOperator("Oid", oid);
        }
    }
}
