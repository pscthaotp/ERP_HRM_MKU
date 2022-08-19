using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using System.ComponentModel;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định tạm hoãn")]
    public class QuyetDinhTamHoan : QuyetDinhCaNhan
    {
        //
        private DateTime _NgayBatDauHoan;
        private DateTime _NgayHetTamHoan;
        private int _SoThangTamHoan;
        private HopDong _HopDong;
        
        [ModelDefault("Caption", "Hợp đồng")]
        [DataSourceProperty("HDList", DataSourcePropertyIsNullMode.SelectNothing)]
        public HopDong HopDong
        {
            get
            {
                return _HopDong;
            }
            set
            {
                SetPropertyValue("HopDong", ref _HopDong, value);
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Số tháng tạm hoãn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoThangTamHoan
        {
            get
            {
                return _SoThangTamHoan;
            }
            set
            {
                SetPropertyValue("SoThangTamHoan", ref _SoThangTamHoan, value);
                if (NgayHieuLuc != DateTime.MinValue)
                    NgayHetTamHoan = NgayHieuLuc.AddMonths(SoThangTamHoan);                
            }
        }

        [Appearance("NgayBatDauHoan", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("Caption", "Ngày bắt đầu tạm hoãn")]
        public DateTime NgayBatDauHoan
        {
            get
            {
                return _NgayBatDauHoan;
            }
            set
            {
                SetPropertyValue("NgayBatDauHoan", ref _NgayBatDauHoan, value);
                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hết hạn tạm hoãn")]
        [ModelDefault("AllowEdit", "False")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHetTamHoan
        {
            get
            {
                return _NgayHetTamHoan;
            }
            set
            {
                SetPropertyValue("NgayHetTamHoan", ref _NgayHetTamHoan, value);               
            }
        }

        public QuyetDinhTamHoan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhTamHoan;
            //
            QuyetDinhMoi = true;
            //
            UpdateHDList();
        }

        protected override void AfterNhanVienChanged()
        {
            UpdateHDList();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime()) //QuyetDinhMoi && ChucVuCu != ChucVuMoi && 
                {
                    TinhTrang ttTamHoam = Common.GetTinhTrang_ByTenTinhTrang(Session, "Nghỉ không lương"); 
                    //Cập nhất thông tin hồ sơ
                    if (ttTamHoam == null)
                    {
                        ttTamHoam = new TinhTrang(Session);
                        ttTamHoam.MaQuanLy = "NKHL";
                        ttTamHoam.TenTinhTrang = "Nghỉ không lương";
                        ttTamHoam.DaNghiViec = false;
                    }
                    NgayBatDauHoan = NgayHieuLuc;
                    ThongTinNhanVien.TinhTrang = ttTamHoam;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Kiểm tra xem quyết định này có phải mới nhất không
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhTamHoan> quyetdinh = new XPCollection<QuyetDinhTamHoan>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    //
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == this)
                        {
                            TinhTrang ttDangLam = Common.GetTinhTrang_ByTenTinhTrang(Session, "Đang làm việc");
                            if (ttDangLam != null)
                            {
                                ttDangLam = new TinhTrang(Session);
                                ttDangLam.MaQuanLy = "DLV";
                                ttDangLam.TenTinhTrang = "Đang làm việc";
                                ttDangLam.DaNghiViec = false;
                            }
                            ThongTinNhanVien.TinhTrang = ttDangLam;

                            JobUpdated = true;
                        }
                    }
                }
            }

            base.OnDeleting();
        }

        [Browsable(false)]
        public XPCollection<HopDong> HDList { get; set; }

        private void UpdateHDList()
        {
            //
            if (HDList == null)
                HDList = new XPCollection<HopDong>(Session);
            //
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien.Oid=? And !HopDongCu", ThongTinNhanVien != null ? ThongTinNhanVien.Oid : Guid.Empty);
            HDList.Criteria = filter;
        }
    }
}
