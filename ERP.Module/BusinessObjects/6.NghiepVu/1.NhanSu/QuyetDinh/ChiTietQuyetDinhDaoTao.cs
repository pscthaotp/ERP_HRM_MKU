using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuyetDinhDaoTao")]
    public class ChiTietQuyetDinhDaoTao : BaseObject
    {        
        private TinhTrang _TinhTrangCu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private bool _DuocHuongLuongKhiDiHoc;
        private TinhTrang _TinhTrangMoi;
        private decimal _PhanTramMienGiam; 

        [Browsable(false)]
        [Association("QuyetDinhDaoTao-ListChiTietQuyetDinhDaoTao")]
        public QuyetDinhDaoTao QuyetDinhDaoTao
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
                    TinhTrangCu = value.TinhTrang;
                }
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Được hưởng lương khi đi học")]
        [ModelDefault("AllowEdit", "False")]
        public bool DuocHuongLuongKhiDiHoc
        {
            get
            {
                return _DuocHuongLuongKhiDiHoc;
            }
            set
            {
                SetPropertyValue("DuocHuongLuongKhiDiHoc", ref _DuocHuongLuongKhiDiHoc, value);
            }
        }
        
        [ModelDefault("Caption", "Tình trạng hưởng lương")]
        public TinhTrang TinhTrangMoi
        {
            get
            {
                return _TinhTrangMoi;
            }
            set
            {
                SetPropertyValue("TinhTrangMoi", ref _TinhTrangMoi, value);
                if (!IsLoading && value != null)
                {
                    if (value.TenTinhTrang.Contains("Đang làm việc") || value.TenTinhTrang.Contains("có hưởng lương") || value.TenTinhTrang.Contains("được hưởng lương"))
                        DuocHuongLuongKhiDiHoc = true;
                    else
                        DuocHuongLuongKhiDiHoc = false;
                }
            }
        }

        [Browsable(false)]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        [ModelDefault("Caption", "Phần trăm miễn giảm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramMienGiam
        {
            get
            {
                return _PhanTramMienGiam;
            }
            set
            {
                SetPropertyValue("PhanTramMienGiam", ref _PhanTramMienGiam, value);
            }
        }


        public ChiTietQuyetDinhDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
            //
            DuocHuongLuongKhiDiHoc = true;
            TinhTrangMoi = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && !IsDeleted
                && QuyetDinhDaoTao != null
                && !QuyetDinhDaoTao.IsDirty)
                QuyetDinhDaoTao.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
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
        /*
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //1. Tình trạng
                if (QuyetDinhDaoTao.TuNgay <= Common.GetServerCurrentTime() && QuyetDinhDaoTao.QuyetDinhMoi && TinhTrangMoi != null)
                    ThongTinNhanVien.TinhTrang = TinhTrangMoi;
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (QuyetDinhDaoTao.QuyetDinhMoi)
                {
                    //1. Tình trạng
                    ThongTinNhanVien.TinhTrang = TinhTrangCu;
                }
            }

            base.OnDeleting();
        }
         */
    }
}
