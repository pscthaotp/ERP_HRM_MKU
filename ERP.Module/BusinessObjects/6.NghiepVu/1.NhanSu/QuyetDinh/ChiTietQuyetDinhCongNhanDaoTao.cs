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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định công nhận đào tạo")]
    [Appearance("ChiTietQuyetDinhCongNhanDaoTao.DaNopVanBangChungChi", TargetItems = "SoVanBang;NgayCapVB", Visibility = ViewItemVisibility.Hide, Criteria = "DaNopVanBangChungChi=false")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuyetDinhCongNhanDaoTao")]
    //[RuleCriteria("DaNopVanBangChungChi != true and KhongDat = true", CustomMessageTemplate = "Không đạt đào tạo thì không cần nộp văn bằng !!!", SkipNullOrEmptyValues = true)]
    public class ChiTietQuyetDinhCongNhanDaoTao : BaseObject
    {       
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhCongNhanDaoTao _QuyetDinhCongNhanDaoTao;
        private bool _DuocHuongLuongKhiDiHoc;
        private bool _DaNopVanBangChungChi;
        private string _SoVanBang;
        private DateTime _NgayCapVB;
        private bool _KhongDat;
        private TinhTrang _TinhTrangCu;
        private TinhTrang _TinhTrangMoi;

        [Browsable(false)]
        [Association("QuyetDinhCongNhanDaoTao-ListChiTietQuyetDinhCongNhanDaoTao")]
        public QuyetDinhCongNhanDaoTao QuyetDinhCongNhanDaoTao
        {
            get
            {
                return _QuyetDinhCongNhanDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanDaoTao", ref _QuyetDinhCongNhanDaoTao, value);
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

        [ModelDefault("Caption", "Không đạt đào tạo")]
        public bool KhongDat
        {
            get
            {
                return _KhongDat;
            }
            set
            {
                SetPropertyValue("KhongDat", ref _KhongDat, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đã nộp văn bằng chứng chỉ")]        
        public bool DaNopVanBangChungChi
        {
            get
            {
                return _DaNopVanBangChungChi;
            }
            set
            {
                SetPropertyValue("DaNopVanBangChungChi", ref _DaNopVanBangChungChi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số văn bằng/chứng chỉ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "DaNopVanBangChungChi=true")]
        public string SoVanBang
        {
            get
            {
                return _SoVanBang;
            }
            set
            {
                SetPropertyValue("SoVanBang", ref _SoVanBang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày cấp văn bằng/chứng chỉ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "DaNopVanBangChungChi=true")]
        public DateTime NgayCapVB
        {
            get
            {
                return _NgayCapVB;
            }
            set
            {
                SetPropertyValue("NgayCapVB", ref _NgayCapVB, value);
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

        public ChiTietQuyetDinhCongNhanDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
            //
            DuocHuongLuongKhiDiHoc = true;
            TinhTrangMoi = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?","Đang làm việc"));
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && !IsDeleted
                && QuyetDinhCongNhanDaoTao != null
                && !QuyetDinhCongNhanDaoTao.IsDirty)
                QuyetDinhCongNhanDaoTao.IsDirty = true;
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

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && Session is NestedUnitOfWork)
            {
                //1. Tình trạng
                if (QuyetDinhCongNhanDaoTao.NgayHieuLuc <= Common.GetServerCurrentTime() && QuyetDinhCongNhanDaoTao.QuyetDinhMoi && TinhTrangMoi != null)
                    ThongTinNhanVien.TinhTrang = TinhTrangMoi;

                if (QuyetDinhCongNhanDaoTao != null && QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao != null)
                {
                    //Tạo quá trình đạo tạo
                     ProcessesHelper.CreateQuaTrinhDaoTao(Session, QuyetDinhCongNhanDaoTao, ThongTinNhanVien);

                    //Tạo văn bằng, chứng chỉ
                    if (DaNopVanBangChungChi && QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao.ChuongTrinhDaoTao.LoaiVanBangChungChi == LoaiVanBangChungChiEnum.VanBang)
                        DaoTaoHelper.CreateVanBang(Session, QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao, ThongTinNhanVien, QuyetDinhCongNhanDaoTao.NgayHieuLuc.Year);
                    if (DaNopVanBangChungChi && QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao.ChuongTrinhDaoTao.LoaiVanBangChungChi == LoaiVanBangChungChiEnum.ChungChi)
                        DaoTaoHelper.CreateChungChi(Session, QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao, ThongTinNhanVien, NgayCapVB);
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (QuyetDinhCongNhanDaoTao.QuyetDinhMoi)
                {
                    //1. Tình trạng
                    ThongTinNhanVien.TinhTrang = TinhTrangCu;
                }

                //Xóa quá trình
                ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhDaoTao>(Session, QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao.Oid, ThongTinNhanVien.Oid);

                //Xóa văn bằng
                DaoTaoHelper.DeleteVanBang(Session, QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao, ThongTinNhanVien);

                //Xóa chứng chỉ
                DaoTaoHelper.DeleteChungChi(Session, QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao, ThongTinNhanVien);
            
            }

            base.OnDeleting();
        }
    }
}
