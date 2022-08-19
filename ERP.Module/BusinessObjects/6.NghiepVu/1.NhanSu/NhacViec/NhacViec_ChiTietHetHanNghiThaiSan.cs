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
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nội dung chi tiết - Hết hạn nghỉ thai sản")]
    public class NhacViec_ChiTietHetHanNghiThaiSan : BaseObject
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private TinhTrang _TinhTrang;
        private DateTime _NgayNghiThaiSan;
        private DateTime _NgayHetHanNghiThaiSan;
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

        [ImmediatePostData]
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

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu nghỉ")]
        [ModelDefault("DisplayFormat","dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayNghiThaiSan
        {
            get
            {
                return _NgayNghiThaiSan;
            }
            set
            {
                SetPropertyValue("NgayNghiThaiSan", ref _NgayNghiThaiSan, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn nghỉ thai sản")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayHetHanNghiThaiSan
        {
            get
            {
                return _NgayHetHanNghiThaiSan;
            }
            set
            {
                SetPropertyValue("NgayHetHanNghiThaiSan", ref _NgayHetHanNghiThaiSan, value);
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
        public NhacViec_ChiTietHetHanNghiThaiSan(Session session) : base(session) { }

    }

}
