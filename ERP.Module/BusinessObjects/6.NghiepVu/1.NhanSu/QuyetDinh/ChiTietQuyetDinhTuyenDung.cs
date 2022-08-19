using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhTuyenDung;ThongTinNhanVien")]
    [Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class ChiTietQuyetDinhTuyenDung : BaseObject
    {
        //
        private bool IsEnable = true;
        private DateTime _NgayBoNhiemNgach;
        private int _ThoiGianTapSu = 12;
        private DateTime _NgayHuongLuong;
        private decimal _PhanTramTinhLuong = 85;
        private decimal _HeSoLuong;
        private BacLuong _BacLuong;
        private NgachLuong _NgachLuong;
        private decimal _LuongCoBan;
        private decimal _LuongKinhDoanh;
        private QuyetDinhTuyenDung _QuyetDinhTuyenDung;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _NgayVaoTruong;
        private UngVien _UngVien;

        [Browsable(false)]
        [Association("QuyetDinhTuyenDung-ListChiTietQuyetDinhTuyenDung")]
        public QuyetDinhTuyenDung QuyetDinhTuyenDung
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;                   
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuong = value.NhanVienThongTinLuong.BacLuong;
                    NgayHuongLuong = value.NhanVienThongTinLuong.NgayHuongLuong;
                    NgayBoNhiemNgach = value.NhanVienThongTinLuong.NgayBoNhiemNgach;
                    PhanTramTinhLuong = value.NhanVienThongTinLuong.PhanTramTinhLuong;
                    LuongCoBan = value.NhanVienThongTinLuong.LuongCoBan;
                    LuongKinhDoanh = value.NhanVienThongTinLuong.LuongKinhDoanh;
                }
            }
        }

        [ModelDefault("Caption", "Ngày vào trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayVaoTruong
        {
            get
            {
                return _NgayVaoTruong;
            }
            set
            {
                SetPropertyValue("NgayVaoTruong", ref _NgayVaoTruong, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayHuongLuong = value;
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

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "% tính lương")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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
        [ModelDefault("Caption", "Ngày hưởng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
                if (!IsLoading && NgayHuongLuong != DateTime.MinValue && ThoiGianTapSu > 0)
                {
                    NgayBoNhiemNgach = NgayHuongLuong.AddMonths(ThoiGianTapSu).AddDays(-1);
                }

            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thời gian thử việc (tháng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianTapSu
        {
            get
            {
                return _ThoiGianTapSu;
            }
            set
            {
                SetPropertyValue("ThoiGianTapSu", ref _ThoiGianTapSu, value);
                if (!IsLoading && NgayHuongLuong != DateTime.MinValue && ThoiGianTapSu > 0)
                {
                    NgayBoNhiemNgach = NgayHuongLuong.AddMonths(ThoiGianTapSu).AddDays(-1);
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

        [Browsable(false)]
        [ModelDefault("Caption", "Ứng viên (lưu vết)")]
        public UngVien UngVien
        {
            get
            {
                return _UngVien;
            }
            set
            {
                SetPropertyValue("UngVien", ref _UngVien, value);
            }
        }

        public ChiTietQuyetDinhTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && !IsDeleted && QuyetDinhTuyenDung != null && !QuyetDinhTuyenDung.IsDirty)
                QuyetDinhTuyenDung.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (ThongTinNhanVien != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=? && GCRecord IS NULL", ThongTinNhanVien);
                ThongTinNhanVien thongTinNhanVien = Session.FindObject<ThongTinNhanVien>(filter);
                if (thongTinNhanVien != null && thongTinNhanVien.TinhTrang.DaNghiViec == false)
                    IsEnable = false;
            }

            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (QuyetDinhTuyenDung.QuyetDinhMoi && Oid != Guid.Empty && Session is NestedUnitOfWork)
                {
                    if (UngVien != null && UngVien.ThongTinNhanVien != null && UngVien.NhuCauTuyenDung.ViTriTuyenDung.BoPhan.CongTy.Oid == ThongTinNhanVien.CongTy.Oid)
                    {
                        if (ThongTinNhanVien.TinhTrang.DaNghiViec == true && ThongTinNhanVien.NgayNghiViec < NgayVaoTruong)
                            ThongTinNhanVien.TinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                        if (UngVien.NhuCauTuyenDung.ViTriTuyenDung.BoPhan != ThongTinNhanVien.BoPhan)
                        {
                            ThongTinNhanVien.BoPhan = Session.GetObjectByKey<BoPhan>(UngVien.NhuCauTuyenDung.ViTriTuyenDung.BoPhan.Oid);
                            ThongTinNhanVien.CongTy = Session.GetObjectByKey<BoPhan>(UngVien.NhuCauTuyenDung.ViTriTuyenDung.BoPhan.Oid).CongTy;
                        }
                    }

                    ThongTinNhanVien.NgayVaoCongTy = NgayVaoTruong;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBan;
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanh;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgach;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = PhanTramTinhLuong;
                }

                if (QuyetDinhTuyenDung != null && QuyetDinhTuyenDung.IsDirty)
                {
                    //update dien bien luong
                    ProcessesHelper.CreateDienBienLuong(Session, QuyetDinhTuyenDung, ThongTinNhanVien, this);

                    if (UngVien != null && UngVien.ThongTinNhanVien != null)
                        ProcessesHelper.CreateLichSuBanThan(Session, ThongTinNhanVien, QuyetDinhTuyenDung, NgayVaoTruong, "Làm việc lại sau khi nghỉ việc");
                    else
                        ProcessesHelper.CreateLichSuBanThan(Session, ThongTinNhanVien, QuyetDinhTuyenDung, NgayVaoTruong, "Làm việc lần đầu");
                }

                QuyetDinhTuyenDung.IsDirty = true;
            
            }
        }

        protected override void OnDeleting()
        {        
            if (QuyetDinhTuyenDung.QuyetDinhMoi)
            {
                if (UngVien != null && UngVien.ThongTinNhanVien != null)
                {
                    ThongTinNhanVien.TinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ việc"));
                    ThongTinNhanVien.BoPhan = Session.GetObjectByKey<BoPhan>(UngVien.ThongTinNhanVien.BoPhan.Oid);
                    ThongTinNhanVien.CongTy = Session.GetObjectByKey<CongTy>(UngVien.ThongTinNhanVien.CongTy.Oid);                    
                }

                //reset data
                ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = null;
                ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = null;
                ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = DateTime.MinValue;
                ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = 100;
                ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = 0;
                ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = 0;
            }

            //Update đến ngày của diễn biến lương và đến năm của quá trình tham gia bảo hiểm trước đó = null
            ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhTuyenDung, ThongTinNhanVien, NgayHuongLuong, false);

            //xóa diễn biến lương
            ProcessesHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhTuyenDung.Oid, ThongTinNhanVien.Oid));

            //xóa lịch sử bản thân
            ProcessesHelper.DeleteQuaTrinh<LichSuBanThan>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhTuyenDung.Oid, ThongTinNhanVien.Oid));
           
            base.OnDeleting();
        }
    }
}
