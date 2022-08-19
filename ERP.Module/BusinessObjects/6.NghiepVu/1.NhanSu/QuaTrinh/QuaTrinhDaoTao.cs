using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Quá trình đào tạo")]
    public class QuaTrinhDaoTao : BaseObject
    {
        private QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _SoVanBang;
        private QuocGia _QuocGia;
        private ChuongTrinhDaoTao _ChuongTrinhDaoTao;
        private TruongDaoTao _TruongDaoTao;
        private ChuyenNganhDaoTao _ChuyenMonDaoTao;
        private HinhThucDaoTao _HinhThucDaoTao;
        private TrinhDoChuyenMon _BangCap;
        private int _NamTotNghiep;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuaTrinhDaoTao")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        [Browsable(false)]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    SoQuyetDinh = value.SoQuyetDinh;
                    NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
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

        [ModelDefault("Caption", "Quốc gia")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }

        [ModelDefault("Caption", "Chương trình đào tạo")]
        public ChuongTrinhDaoTao ChuongTrinhDaoTao
        {
            get
            {
                return _ChuongTrinhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuongTrinhDaoTao", ref _ChuongTrinhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
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

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenNganhDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
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

        [ModelDefault("Caption", "Văn bằng được cấp")]
        public TrinhDoChuyenMon BangCap
        {
            get
            {
                return _BangCap;
            }
            set
            {
                SetPropertyValue("BangCap", ref _BangCap, value);
            }
        }

        [ModelDefault("Caption", "Số văn bằng")]
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

        public QuaTrinhDaoTao(Session session) : base(session) { }

    }
}
