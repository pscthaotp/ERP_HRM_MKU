using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_Resume")]
    [DefaultClassOptions, DefaultProperty("TrinhDoChuyenMon")]
    [ModelDefault("Caption", "Thông tin trình độ")]
    public class NhanVienTrinhDo : BaseObject
    {
        private HocHam _HocHam;
        private ChuyenNganhDaoTao _ChuyenNganhDaoTao;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private TruongDaoTao _TruongDaoTao;
        private HinhThucDaoTao _HinhThucDaoTao;
        private int _NamTotNghiep;
        private TrinhDoTinHoc _TrinhDoTinHoc;
        private NgoaiNgu _NgoaiNgu;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;
        private TrinhDoVanHoa _TrinhDoVanHoa;
        private DateTime _NgayCapBang;        

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trình độ văn hóa*")]
        [ModelDefault("AllowEdit", "True")]
        public TrinhDoVanHoa TrinhDoVanHoa
        {
            get
            {
                return _TrinhDoVanHoa;
            }
            set
            {
                SetPropertyValue("TrinhDoVanHoa", ref _TrinhDoVanHoa, value);
            }
        }

        [ModelDefault("Caption", "Học vị")]
        [ModelDefault("AllowEdit", "False")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
        {
            get
            {
                return _ChuyenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenNganhDaoTao", ref _ChuyenNganhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return _NamTotNghiep;
            }
            set
            {
                SetPropertyValue("NamTotNghiep", ref _NamTotNghiep, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp bằng")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayCapBang
        {
            get
            {
                return _NgayCapBang;
            }
            set
            {
                SetPropertyValue("NgayCapBang", ref _NgayCapBang, value);
            }
        }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return _TrinhDoTinHoc;
            }
            set
            {
                SetPropertyValue("TrinhDoTinHoc", ref _TrinhDoTinHoc, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngoại ngữ chính")]
        [ModelDefault("AllowEdit", "False")]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return _NgoaiNgu;
            }
            set
            {
                SetPropertyValue("NgoaiNgu", ref _NgoaiNgu, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Trình độ ngoại ngữ chính")]
        [ModelDefault("AllowEdit", "False")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return _TrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TrinhDoNgoaiNgu", ref _TrinhDoNgoaiNgu, value);
            }
        }

        public NhanVienTrinhDo(Session session) : base(session) { }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted) //Đồng bộ trình độ hồ sơ cha con trong 1 công ty/trường
            {
                NhanVien nhanVien = Session.FindObject<NhanVien>(CriteriaOperator.Parse("NhanVienTrinhDo=?", this.Oid));
                if (nhanVien != null)
                {
                    if (nhanVien.OidHoSoCha == Guid.Empty)
                    {
                        NhanVien hsCon = Session.FindObject<NhanVien>(CriteriaOperator.Parse("OidHoSoCha=? and CongTy=?", nhanVien.Oid, nhanVien.CongTy));
                        if (hsCon != null)
                        {
                            if  (hsCon.NhanVienTrinhDo.HocHam != nhanVien.NhanVienTrinhDo.HocHam)
                                hsCon.NhanVienTrinhDo.HocHam = nhanVien.NhanVienTrinhDo.HocHam;
                            if (hsCon.NhanVienTrinhDo.TrinhDoChuyenMon != nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon)
                            {
                                hsCon.NhanVienTrinhDo.TrinhDoChuyenMon = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
                                hsCon.NhanVienTrinhDo.TruongDaoTao = nhanVien.NhanVienTrinhDo.TruongDaoTao;
                                hsCon.NhanVienTrinhDo.ChuyenNganhDaoTao = nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao;
                                hsCon.NhanVienTrinhDo.NamTotNghiep = nhanVien.NhanVienTrinhDo.NamTotNghiep;
                                hsCon.NhanVienTrinhDo.NgayCapBang = nhanVien.NhanVienTrinhDo.NgayCapBang;
                                hsCon.NhanVienTrinhDo.HinhThucDaoTao = nhanVien.NhanVienTrinhDo.HinhThucDaoTao;
                            }
                            if ( hsCon.NhanVienTrinhDo.TrinhDoVanHoa != nhanVien.NhanVienTrinhDo.TrinhDoVanHoa)
                                hsCon.NhanVienTrinhDo.TrinhDoVanHoa = nhanVien.NhanVienTrinhDo.TrinhDoVanHoa;
                            if (hsCon.NhanVienTrinhDo.TrinhDoTinHoc != nhanVien.NhanVienTrinhDo.TrinhDoTinHoc)
                                hsCon.NhanVienTrinhDo.TrinhDoTinHoc = nhanVien.NhanVienTrinhDo.TrinhDoTinHoc;
                            if (hsCon.NhanVienTrinhDo.TrinhDoNgoaiNgu != nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu)
                                hsCon.NhanVienTrinhDo.TrinhDoNgoaiNgu = nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu;
                            if (hsCon.NhanVienTrinhDo.NgoaiNgu != nhanVien.NhanVienTrinhDo.NgoaiNgu)
                                hsCon.NhanVienTrinhDo.NgoaiNgu = nhanVien.NhanVienTrinhDo.NgoaiNgu;                            
                        }
                    }
                    else
                    {
                        NhanVien hsCha = Session.FindObject<NhanVien>(CriteriaOperator.Parse("Oid=? and CongTy=?", nhanVien.OidHoSoCha, nhanVien.CongTy));
                        if (hsCha != null)
                        {
                            if(hsCha.NhanVienTrinhDo.HocHam != nhanVien.NhanVienTrinhDo.HocHam)
                                hsCha.NhanVienTrinhDo.HocHam = nhanVien.NhanVienTrinhDo.HocHam;

                            if(hsCha.NhanVienTrinhDo.TrinhDoChuyenMon != nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon)
                            {
                                hsCha.NhanVienTrinhDo.TrinhDoChuyenMon = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
                                hsCha.NhanVienTrinhDo.TruongDaoTao = nhanVien.NhanVienTrinhDo.TruongDaoTao;
                                hsCha.NhanVienTrinhDo.ChuyenNganhDaoTao = nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao;
                                hsCha.NhanVienTrinhDo.NamTotNghiep = nhanVien.NhanVienTrinhDo.NamTotNghiep;
                                hsCha.NhanVienTrinhDo.NgayCapBang = nhanVien.NhanVienTrinhDo.NgayCapBang;
                                hsCha.NhanVienTrinhDo.HinhThucDaoTao = nhanVien.NhanVienTrinhDo.HinhThucDaoTao;
                            }

                            if (hsCha.NhanVienTrinhDo.TrinhDoVanHoa != nhanVien.NhanVienTrinhDo.TrinhDoVanHoa)
                                hsCha.NhanVienTrinhDo.TrinhDoVanHoa = nhanVien.NhanVienTrinhDo.TrinhDoVanHoa;
                            if (hsCha.NhanVienTrinhDo.TrinhDoTinHoc != nhanVien.NhanVienTrinhDo.TrinhDoTinHoc)
                                hsCha.NhanVienTrinhDo.TrinhDoTinHoc = nhanVien.NhanVienTrinhDo.TrinhDoTinHoc;
                            if (hsCha.NhanVienTrinhDo.TrinhDoNgoaiNgu != nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu)
                                hsCha.NhanVienTrinhDo.TrinhDoNgoaiNgu = nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu;
                            if (hsCha.NhanVienTrinhDo.NgoaiNgu != nhanVien.NhanVienTrinhDo.NgoaiNgu)
                                hsCha.NhanVienTrinhDo.NgoaiNgu = nhanVien.NhanVienTrinhDo.NgoaiNgu;
                        }
                    }
                }
            }
        }
    }
}
