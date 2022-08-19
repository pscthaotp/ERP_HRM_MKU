using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình kỷ luật")]
    public class QuaTrinhKyLuat : BaseObject
    {
        private QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private HinhThucKyLuat _HinhThucKyLuat;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private string _LyDo;
    
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuaTrinhKyLuat")]
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

        [ModelDefault("Caption", "Hình thức kỷ luật")]
        public HinhThucKyLuat HinhThucKyLuat
        {
            get
            {
                return _HinhThucKyLuat;
            }
            set
            {
                SetPropertyValue("HinhThucKyLuat", ref _HinhThucKyLuat, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        public QuaTrinhKyLuat(Session session) : base(session) { }
    }

}
