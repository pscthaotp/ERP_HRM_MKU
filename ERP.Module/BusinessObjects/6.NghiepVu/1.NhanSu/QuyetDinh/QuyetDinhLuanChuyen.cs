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
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.CauHinhChungs;
using ERP.Module.HeThong;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định luân chuyển")]
    public class QuyetDinhLuanChuyen : QuyetDinhCaNhan
    {       
        private ChucVu _ChucVuCu;
        private ChucDanh _ChucDanhCu;
        private DateTime _NgayVaoCongTyCu;
        private ChucVu _ChucVuMoi;
        private ChucDanh _ChucDanhMoi;
        private CongTy _CongTyMoi;
        private BoPhan _BoPhanMoi;

        private NhomPhanBo _NhomPhanBoCu;
        private NhomPhanBo _NhomPhanBoMoi;

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Chức vụ cũ")]       
        public ChucVu ChucVuCu
        {
            get
            {
                return _ChucVuCu;
            }
            set
            {
                SetPropertyValue("ChucVuCu", ref _ChucVuCu, value);
            }
        }
      
        [ModelDefault("Caption", "Chức danh cũ")]
        [ModelDefault("AllowEdit", "False")]
        public ChucDanh ChucDanhCu
        {
            get
            {
                return _ChucDanhCu;
            }
            set
            {
                SetPropertyValue("ChucDanhCu", ref _ChucDanhCu, value);
            }
        }
       
        [ModelDefault("Caption", "Nhóm phân bổ cũ")]
        [ModelDefault("AllowEdit", "False")]
        public NhomPhanBo NhomPhanBoCu
        {
            get
            {
                return _NhomPhanBoCu;
            }
            set
            {
                SetPropertyValue("NhomPhanBoCu", ref _NhomPhanBoCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào Trường cũ")]
        //[ModelDefault("AllowEdit", "False")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayVaoCongTyCu
        {
            get
            {
                return _NgayVaoCongTyCu;
            }
            set
            {
                SetPropertyValue("NgayVaoCongTyCu", ref _NgayVaoCongTyCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
                if (!IsLoading)
                {
                    ChucDanhMoi = null;
                    CapNhatChucDanh();
                }
            }
        }

        [ModelDefault("Caption", "Chức danh mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("CDList")]
        public ChucDanh ChucDanhMoi
        {
            get
            {
                return _ChucDanhMoi;
            }
            set
            {
                SetPropertyValue("ChucDanhMoi", ref _ChucDanhMoi, value);
            }
        }

        [Browsable(false)]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Trường chuyển đến")]
        public CongTy CongTyMoi
        {
            get
            {
                return _CongTyMoi;
            }
            set
            {
                SetPropertyValue("CongTyMoi", ref _CongTyMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị chuyển đến")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanMoi
        {
            get
            {
                return _BoPhanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanMoi", ref _BoPhanMoi, value);
                if (!IsLoading && value != null)
                {
                    CongTyMoi = BoPhanMoi.CongTy;
                }
            }
        }

        [ModelDefault("Caption", "Nhóm phân bổ mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhomPhanBo NhomPhanBoMoi
        {
            get
            {
                return _NhomPhanBoMoi;
            }
            set
            {
                SetPropertyValue("NhomPhanBoMoi", ref _NhomPhanBoMoi, value);
            }
        }
        public QuyetDinhLuanChuyen(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhLuanChuyen;
            //
            QuyetDinhMoi = true;
            //
        }

        protected override void AfterNhanVienChanged()
        {
            CongTyCu = ThongTinNhanVien.CongTy;
            ChucVuCu = ThongTinNhanVien.ChucVu;
            ChucDanhCu = ThongTinNhanVien.ChucDanh;
            NhomPhanBoCu = ThongTinNhanVien.NhomPhanBo;
            NgayVaoCongTyCu = ThongTinNhanVien.NgayVaoCongTy;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Cập nhất thông tin hồ sơ
                    ThongTinNhanVien.ChucVu = ChucVuMoi;
                    ThongTinNhanVien.ChucDanh = ChucDanhMoi;                   
                    ThongTinNhanVien.BoPhan = BoPhanMoi;
                    ThongTinNhanVien.CongTy = BoPhanMoi.CongTy;
                    ThongTinNhanVien.NgayVaoCongTy = NgayHieuLuc;
                    ThongTinNhanVien.NhomPhanBo = NhomPhanBoMoi;

                    JobUpdated = true;
                }

                //Quá trình bổ nhiệm chức vụ
                ProcessesHelper.CreateQuaTrinhLuanChuyen(Session, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Kiểm tra xem quyết định này có phải mới nhất không
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhLuanChuyen> quyetdinh = new XPCollection<QuyetDinhLuanChuyen>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    //
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == this)
                        {         
                   
                            ThongTinNhanVien.ChucVu = ChucVuCu;
                            ThongTinNhanVien.ChucDanh = ChucDanhCu;
                            ThongTinNhanVien.BoPhan = BoPhan;
                            ThongTinNhanVien.CongTy = BoPhan.CongTy;
                            ThongTinNhanVien.NgayVaoCongTy = NgayVaoCongTyCu;
                            ThongTinNhanVien.NhomPhanBo = NhomPhanBoCu;
                        }
                    }
                }

                //Xóa quá trình điều động
                ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhDieuDong>(Session, this.Oid, this.ThongTinNhanVien.Oid);

            }
            base.OnDeleting();
        }

        [Browsable(false)]
        public XPCollection<ChucDanh> CDList { get; set; }

        public void CapNhatChucDanh()
        {
            if (CDList == null)
                CDList = new XPCollection<ChucDanh>(Session);
            //            
            if (ChucVuMoi != null)
                CDList.Filter = CriteriaOperator.Parse("ChucVu.Oid=?", ChucVuMoi.Oid);
        }

        protected override void OnSaved()
        {
            base.OnSaved();

            if (QuyetDinhMoi && BoPhan != BoPhanMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@NgayHieuLuc", NgayHieuLuc);
                param[1] = new SqlParameter("@IDNhanVien", ThongTinNhanVien.Oid);
                param[2] = new SqlParameter("@CongTy", CongTyCu.Oid);
                DataProvider.ExecuteNonQuery("spd_WebChamCong_CapNhatBoPhanChamCong", CommandType.StoredProcedure, param);

                CapNhatThongTinTaiKhoan();
            }
        }

        void CapNhatThongTinTaiKhoan()
        {           
            if (ThongTinNhanVien.OidHoSoCha == Guid.Empty) //Chỉ tạo tài khoản ở tài khoản cha
            {
                //1. Cập nhật tài khoản - ERP
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);
                SecuritySystemUser_Custom taiKhoan = Session.FindObject<SecuritySystemUser_Custom>(filter);
                if (taiKhoan != null)
                {                                 
                    taiKhoan.CongTy = ThongTinNhanVien.CongTy;
                    taiKhoan.BoPhan = ThongTinNhanVien.BoPhan;                   
                    //
                    Session.Save(taiKhoan);
                }                
            }
        }
    }
}
