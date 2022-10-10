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
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập đơn vị")]
    public class QuyetDinhThanhLapDonVi : QuyetDinh, IBoPhan
    {
        private BoPhan _BoPhan;
        private string _DonViMoi;//Nguyen
        private string _MaDonVi;//Nguyen
        private string _ChucNangDonViMoi;
        private string _NhiemVuDonViMoi;
        private string _TenTiengAnhBoPhanMoi;
        private string _DonViTach;
        private string _NhiemVuDonViKhac;

        [ModelDefault("Caption", "Đơn vị mới")]
        //[ModelDefault("PropertyEditorType", "ERP.Module.BusinessObjects.6._NghiepVu.1._NhanSu.BoPhan.BoPhan")]
        public string DonViMoi
        {
            get
            {
                return _DonViMoi;
            }
            set
            {
                SetPropertyValue("DonViMoi", ref _DonViMoi, value);
            }
        }//Nguyen

        [ModelDefault("Caption", "Mã đơn vị")]
        public string MaDonVi
        {
            get
            {
                return _MaDonVi;
            }
            set
            {
                SetPropertyValue("MaDonVi", ref _MaDonVi, value);
            }
        }//Nguyen

        //[ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("Caption", "Đơn vị quản lý")]
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
            }
        }

        [ModelDefault("Caption", "Chức năng đơn vị mới")]
        public string ChucNangDonViMoi
        {
            get
            {
                return _ChucNangDonViMoi;
            }
            set
            {
                SetPropertyValue("ChucNangDonViMoi", ref _ChucNangDonViMoi, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị tách")]
        public string DonViTach
        {
            get
            {
                return _DonViTach;
            }
            set
            {
                SetPropertyValue("DonViTach", ref _DonViTach, value);
            }
        }

        [ModelDefault("Caption", "Nhiệm vụ đơn vị mới")]
        public string NhiemVuDonViMoi
        {
            get
            {
                return _NhiemVuDonViMoi;
            }
            set
            {
                SetPropertyValue("NhiemVuDonViMoi", ref _NhiemVuDonViMoi, value);
            }
        }

        [ModelDefault("Caption", "Tên tiếng anh đơn vị mới")]
        public string TenTiengAnhBoPhanMoi
        {
            get
            {
                return _TenTiengAnhBoPhanMoi;
            }
            set
            {
                SetPropertyValue("TenTiengAnhBoPhanMoi", ref _TenTiengAnhBoPhanMoi, value);
            }
        }

        [ModelDefault("Caption", "Nhiệm vụ đơn vị khác")]
        public string NhiemVuDonViKhac
        {
            get
            {
                return _NhiemVuDonViKhac;
            }
            set
            {
                SetPropertyValue("NhiemVuDonViKhac", ref _NhiemVuDonViKhac, value);
            }
        }

        //[Aggregated]
        //[ModelDefault("Caption", "Danh sách cán bộ")]
        //[Association("QuyetDinhThanhLapDonVi-ListChiTietQuyetDinhThanhLapDonVi")]
        //public XPCollection<ChiTietQuyetDinhThanhLapDonVi> ListChiTietQuyetDinhThanhLapDonVi
        //{
        //    get
        //    {
        //        return GetCollection<ChiTietQuyetDinhThanhLapDonVi>("ListChiTietQuyetDinhThanhLapDonVi");
        //    }
        //}

        public void CreateListChiTietQuyetDinhThanhLapDonVi(ThongTinNhanVien nhanVien)
        {
            ChiTietQuyetDinhThanhLapDonVi chiTiet = new ChiTietQuyetDinhThanhLapDonVi(Session);
            chiTiet.BoPhan = nhanVien.BoPhan;
            chiTiet.ThongTinNhanVien = nhanVien;
            //this.ListChiTietQuyetDinhThanhLapDonVi.Add(chiTiet);
        }

        public QuyetDinhThanhLapDonVi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapDonVi;

            QuyetDinhMoi = false;

        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted && QuyetDinhMoi)
            {
                ////Nguyen
                //SqlParameter[] pDongBo = new SqlParameter[3];
                //pDongBo[0] = new SqlParameter("@DonViMoi", DonViMoi);
                //pDongBo[1] = new SqlParameter("@DonViQuanLy", BoPhan.Oid);
                //pDongBo[2] = new SqlParameter("@MaDonVi", MaDonVi);
                ////pDongBo[3] = new SqlParameter("@BoPhan", Guid.Empty);
                ////pDongBo[4] = new SqlParameter("@NhanVien", Guid.Empty);

                //DataProvider.ExecuteNonQuery("spd_QuyetDinhThanhLapDonVi_ThemDonVi", CommandType.StoredProcedure, pDongBo);
            }
        }

        protected override void OnDeleting()
        {

            base.OnDeleting();
        }

    }
}
