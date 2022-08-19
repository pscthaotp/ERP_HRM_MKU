using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

//
namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Lịch sử bản thân")]
    public class LichSuBanThan : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayTao;
        private string _CongTy;
        private string _ChucDanh;
        private string _TuNam;
        private string _DenNam;
        private string _LyDoNghiViec;
        private string _GhiChu;
        private QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListLichSuBanThan")]
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

        [ModelDefault("Caption", "Ngày tạo")]        
        public DateTime NgayTao
        {
            get
            {
                return _NgayTao;
            }
            set
            {
                SetPropertyValue("NgayTao", ref _NgayTao, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        public string CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public string ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public string TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public string DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [ModelDefault("Caption", "Lý do nghỉ việc")]
        public string LyDoNghiViec
        {
            get
            {
                return _LyDoNghiViec;
            }
            set
            {
                SetPropertyValue("LyDoNghiViec", ref _LyDoNghiViec, value);
            }
        }

        [Size(4000)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định")]
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
                    NgayTao = value.NgayQuyetDinh;
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

        public LichSuBanThan(Session session) : base(session) { }

        protected override void OnSaving()
        {
            if (!IsDeleted && (NgayTao == DateTime.MinValue || NgayTao == null))
            {
                NgayTao = Common.GetServerCurrentTime();
            }
            base.OnSaving();
        }
    }
}
