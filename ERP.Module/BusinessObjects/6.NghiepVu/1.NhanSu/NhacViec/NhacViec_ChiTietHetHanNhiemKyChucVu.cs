using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;


//
namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nội dung chi tiết - Hết hạn nhiệm kỳ chức vụ")]
    public class NhacViec_ChiTietHetHanNhiemKyChucVu : BaseObject
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;       
        private QuyetDinh _QuyetDinh;
        private DateTime _NgayBoNhiem;
        private DateTime _NgayHetNhiemKy;
        private string _GhiChu;

        [ModelDefault("Caption", "Đơn vị")]
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

        [ModelDefault("Caption", "Cán bộ")]
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
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }

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

        public NhacViec_ChiTietHetHanNhiemKyChucVu(Session session) : base(session) { }

    }

}
