using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.CauHinhChungs;

namespace ERP.Module.NghiepVu.PMS.QuanLyGioChuan
{
    [ModelDefault("Caption", "Định mức chức vụ(nhân viên)")]
    public class DinhMucChucVu_NhanVien : BaseObject
    {
        private QuanLyGioChuan _QuanLyGioChuan;

        private NhanVien _NhanVien;
        private decimal _SoGioDinhMuc;
        private decimal _SoGioDinhMuc_NCKH;
        private decimal _SoGioDinhMuc_Khac;
        private string _GhiChu;
        private bool _KhongDongBo;

        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMucChucVuNhanVien")]
        [Browsable(false)]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        //[NonPersistent]
        public string DonVi
        {
            get
            {
                if (NhanVien != null)
                    return NhanVien.BoPhan != null ? NhanVien.BoPhan.TenBoPhan : "";
                else return "";
            }

        }
        [ModelDefault("Caption", "Chức vụ")]
        //[NonPersistent]
        public string ChucVu
        {
            get
            {
                if (NhanVien != null)
                {
                    ThongTinNhanVien ttnv = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid =?", NhanVien.Oid));
                    if (ttnv != null)
                        return ttnv.ChucVu != null ? ttnv.ChucVu.TenChucVu : "";
                    else
                        return "";
                }
                else
                    return "";
            }
        }
        [ModelDefault("Caption", "Chức danh")]
        //[NonPersistent]
        [VisibleInDetailView(false)]
        public string ChucDanh
        {
            get
            {
                if (NhanVien != null)
                    return NhanVien.ChucDanh != null ? NhanVien.ChucDanh.TenChucDanh : "";
                else
                    return "";
            }

        }

        [ModelDefault("Caption", "Định mức giờ giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal SoGioDinhMuc
        {
            get { return _SoGioDinhMuc; }
            set
            {
                SetPropertyValue("SoGioDinhMuc", ref _SoGioDinhMuc, value);
            }
        }
        [ModelDefault("Caption", "Định mức giờ chuẩn NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_NCHK
        {
            get { return _SoGioDinhMuc_NCKH; }
            set { SetPropertyValue("SoGioDinhMuc_NCHK", ref _SoGioDinhMuc_NCKH, value); }
        }
        [ModelDefault("Caption", "Định mức giờ chuẩn TGQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_Khac
        {
            get { return _SoGioDinhMuc_Khac; }
            set { SetPropertyValue("SoGioDinhMuc_Khac", ref _SoGioDinhMuc_Khac, value); }
        }

        [ModelDefault("Caption", "Không đồng bộ")]
        public bool KhongDongBo
        {
            get { return _KhongDongBo; }
            set
            {
                SetPropertyValue("KhongDongBo", ref _KhongDongBo, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        //[Browsable(false)]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public DinhMucChucVu_NhanVien(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CauHinhChung chChung = Session.FindObject<CauHinhChung>(CriteriaOperator.Parse("CongTy =?", Common.CongTy(Session).Oid));

            if (chChung != null)
                SoGioDinhMuc = chChung.SoGioChuan;
        }
    }
}