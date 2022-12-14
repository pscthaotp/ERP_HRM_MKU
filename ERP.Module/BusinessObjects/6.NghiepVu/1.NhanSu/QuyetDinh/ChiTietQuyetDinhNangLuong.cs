using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using DevExpress.Xpo.DB;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định điều chỉnh lương")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangLuong;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangLuong : BaseObject, IBoPhan
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhNangLuong _QuyetDinhNangLuong;
        //
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private DateTime _NgayHuongLuongCu;
        private DateTime _MocNangLuongCu;
        private decimal _LuongCoBanCu;
        private decimal _LuongKinhDoanhCu;
        private decimal _PhuCapKiemNhiemCu;
        private decimal _PhuCapTrachNhiemCu;
        private decimal _PhuCapBanTruCu;
        private decimal _PhanTramTinhLuongCu;
        //
        private NgachLuong _NgachLuongMoi;
        private BacLuong _BacLuongMoi;
        private DateTime _NgayHuongLuongMoi;
        private decimal _LuongCoBanMoi;
        private decimal _LuongKinhDoanhMoi;
        private decimal _PhuCapKiemNhiemMoi;
        private decimal _PhuCapTrachNhiemMoi;
        private decimal _PhuCapBanTruMoi;
        private decimal _PhanTramTinhLuongMoi;
        private bool _JobUpdated;
        //

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng lương")]
        [Association("QuyetDinhNangLuong-ListChiTietQuyetDinhNangLuong")]
        public QuyetDinhNangLuong QuyetDinhNangLuong
        {
            get
            {
                return _QuyetDinhNangLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangLuong", ref _QuyetDinhNangLuong, value);
                //if (!IsLoading)
                //{
                //    if (value != null)
                //        NgayHuongLuongMoi = value.NgayHieuLuc;
                //}
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
                    AfterNhanVienChanged();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương cũ")]
        [ModelDefault("AllowEdit", "False")]
        public NgachLuong NgachLuongCu
        {
            get
            {
                return _NgachLuongCu;
            }
            set
            {
                SetPropertyValue("NgachLuongCu", ref _NgachLuongCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương cũ")]
        [DataSourceProperty("NgachLuongCu.ListBacLuong")]
        [ModelDefault("AllowEdit", "False")]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;

            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Lương chức danh cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongCoBanCu
        {
            get
            {
                return _LuongCoBanCu;
            }
            set
            {
                SetPropertyValue("LuongCoBanCu", ref _LuongCoBanCu, value);
            }
        }

        [ModelDefault("Caption", "Lương bổ sung(HQCV) cũ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongKinhDoanhCu
        {
            get
            {
                return _LuongKinhDoanhCu;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanhCu", ref _LuongKinhDoanhCu, value);
            }
        }

        [ModelDefault("Caption", "PC kiêm nhiệm cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal PhuCapKiemNhiemCu
        {
            get
            {
                return _PhuCapKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiemCu", ref _PhuCapKiemNhiemCu, value);
            }
        }

        [ModelDefault("Caption", "PC chủ nhiệm cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal PhuCapTrachNhiemCu
        {
            get
            {
                return _PhuCapTrachNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCu", ref _PhuCapTrachNhiemCu, value);
            }
        }

        [ModelDefault("Caption", "PC bán trú cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal PhuCapBanTruCu
        {
            get
            {
                return _PhuCapBanTruCu;
            }
            set
            {
                SetPropertyValue("PhuCapBanTruCu", ref _PhuCapBanTruCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương mới")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuongMoi
        {
            get
            {
                return _NgachLuongMoi;
            }
            set
            {
                SetPropertyValue("NgachLuongMoi ", ref _NgachLuongMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương mới")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NgachLuongMoi.ListBacLuong", DataSourcePropertyIsNullMode.SelectNothing)]
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
                if (!IsLoading && value != null)
                {
                    LuongKinhDoanhMoi = value.LuongKinhDoanh;
                    LuongCoBanMoi = value.LuongCoBan;
                }
            }
        }

        [ModelDefault("Caption", "Lương chức danh mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongCoBanMoi
        {
            get
            {
                return _LuongCoBanMoi;
            }
            set
            {
                SetPropertyValue("LuongCoBanMoi", ref _LuongCoBanMoi, value);
            }
        }

        [ModelDefault("Caption", "Lương bổ sung(HQCV) mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongKinhDoanhMoi
        {
            get
            {
                return _LuongKinhDoanhMoi;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanhMoi", ref _LuongKinhDoanhMoi, value);
            }
        }

        [ModelDefault("Caption", "PC kiêm nhiệm mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKiemNhiemMoi
        {
            get
            {
                return _PhuCapKiemNhiemMoi;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiemMoi", ref _PhuCapKiemNhiemMoi, value);
            }
        }

        [ModelDefault("Caption", "PC chủ nhiệm mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTrachNhiemMoi
        {
            get
            {
                return _PhuCapTrachNhiemMoi;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemMoi", ref _PhuCapTrachNhiemMoi, value);
            }
        }

        [ModelDefault("Caption", "PC bán trú mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapBanTruMoi
        {
            get
            {
                return _PhuCapBanTruMoi;
            }
            set
            {
                SetPropertyValue("PhuCapBanTruMoi", ref _PhuCapBanTruMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
            }
        }


        [ModelDefault("Caption", "Mức hưởng lương cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal PhanTramTinhLuongCu
        {
            get
            {
                return _PhanTramTinhLuongCu;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuongCu", ref _PhanTramTinhLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mức hưởng lương mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramTinhLuongMoi
        {
            get
            {
                return _PhanTramTinhLuongMoi;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuongMoi", ref _PhanTramTinhLuongMoi, value);
            }
        }

        [Browsable(false)]
        public bool JobUpdated
        {
            get
            {
                return _JobUpdated;
            }
            set
            {
                SetPropertyValue("JobUpdated", ref _JobUpdated, value);
            }
        }

        public ChiTietQuyetDinhNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
            //
            JobUpdated = false;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && !IsDeleted
                && QuyetDinhNangLuong != null
                && !QuyetDinhNangLuong.IsDirty)
                QuyetDinhNangLuong.IsDirty = true;
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

        private void AfterNhanVienChanged()
        {
            BoPhan = ThongTinNhanVien.BoPhan;
            NgachLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            LuongCoBanCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan;
            LuongKinhDoanhCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            PhuCapKiemNhiemCu = PhuCapKiemNhiemMoi = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
            PhuCapTrachNhiemCu = PhuCapTrachNhiemMoi = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem;
            PhuCapBanTruCu = PhuCapBanTruMoi = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapBanTru;            
            PhanTramTinhLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong;
            MocNangLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh != DateTime.MinValue ? ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh : ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau;
        
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted && Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                if (QuyetDinhNangLuong.QuyetDinhMoi)
                {
                    //Cập nhật thông tin vào hồ sơ
                    if (NgayHuongLuongMoi <= Common.GetServerCurrentTime())
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBanMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanhMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem = PhuCapTrachNhiemMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapKiemNhiemMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapBanTru = PhuCapBanTruMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = PhanTramTinhLuongMoi;

                        JobUpdated = true;//Đã cập nhật vào hồ sơ rồi thì ko chạy job
                    }
                }

                //Cập nhật đến ngày của diễn biến lương trước đó = ngày hưởng lương mới - 1
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhNangLuong, ThongTinNhanVien, NgayHuongLuongMoi, true);

                //Tạo diễn biến lương khi lưu quyết định
                ProcessesHelper.CreateDienBienLuong(Session, QuyetDinhNangLuong, ThongTinNhanVien, this);
               
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (QuyetDinhNangLuong.QuyetDinhMoi)
                { //1.
                    //Lưu ý: Kiểm tra diễn biến lương mới nhất lấy quyết định ra kiểm tra với quyết định hiện tại
                    //Tránh trường hợp nếu xóa quyết định này mà đã lập quyết định khác mới hơn
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay>?", ThongTinNhanVien, NgayHuongLuongMoi);
                    SortProperty sort = new SortProperty("TuNgay", SortingDirection.Descending);
                    using (XPCollection<DienBienLuong> dblList = new XPCollection<DienBienLuong>(Session, filter, sort))
                    {
                        dblList.TopReturnedObjects = 1;
                        //
                        if (dblList.Count == 0 ||
                            (dblList.Count == 1 && dblList[0].QuyetDinh == QuyetDinhNangLuong))
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau = MocNangLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBanCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanhCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem = PhuCapTrachNhiemCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapKiemNhiemCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapBanTru = PhuCapBanTruCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = PhanTramTinhLuongCu;

                        }
                    }
                }

                //Cập nhật đến ngày của diễn biến lương và đến năm của quá trình tham gia bảo hiểm trước đó = null
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhNangLuong, ThongTinNhanVien, NgayHuongLuongMoi, false);

                //Xóa diễn biến lương
                ProcessesHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(Session, this.QuyetDinhNangLuong.Oid, this.ThongTinNhanVien.Oid);
            }

            base.OnDeleting();
        }
    }

}
