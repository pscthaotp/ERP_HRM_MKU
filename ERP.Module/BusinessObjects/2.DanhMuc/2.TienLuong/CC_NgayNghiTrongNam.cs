using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Persistent.Validation;
using ERP.Module.HeThong;
using ERP.Module.Commons;

//
namespace ERP.Module.DanhMuc.TienLuong
{
    [ImageName("BO_List")]
    [DefaultProperty("TenNgayNghi")]
    [ModelDefault("Caption", "Ngày nghỉ trong năm")]
    [RuleCriteria("LoaiNgayNghi != 'NgayThuong' and LoaiNgayNghi != 'NghiCuoiTuan'", CustomMessageTemplate = "Loại ngày nghỉ phải khác Nghỉ cuối tuần hoặc Ngày thường", SkipNullOrEmptyValues = false)]
    public class CC_NgayNghiTrongNam : BaseObject, ICongTy
    {
        private LoaiNgayNghiEnum _LoaiNgayNghi;
        private string _TenNgayNghi;
        private DateTime _NgayNghi;
        private CongTy _CongTy;
        private SecuritySystemUser_Custom _NguoiTao;
        private DateTime _NgayTao;

        [ModelDefault("Caption", "Loại ngày nghỉ")]
        public LoaiNgayNghiEnum LoaiNgayNghi
        {
            get
            {
                return _LoaiNgayNghi;
            }
            set
            {
                SetPropertyValue("LoaiNgayNghi", ref _LoaiNgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Tên ngày nghỉ")]
        public string TenNgayNghi
        {
            get
            {
                return _TenNgayNghi;
            }
            set
            {
                SetPropertyValue("TenNgayNghi", ref _TenNgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ")]
        public DateTime NgayNghi
        {
            get
            {
                return _NgayNghi;
            }
            set
            {
                SetPropertyValue("NgayNghi", ref _NgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        public CongTy CongTy
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

        [ModelDefault("AllowEdit","False")]
        [ModelDefault("Caption", "Người tạo")]
        public SecuritySystemUser_Custom NguoiTao
        {
            get
            {
                return _NguoiTao;
            }
            set
            {
                SetPropertyValue("NguoiTao", ref _NguoiTao, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
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

        public CC_NgayNghiTrongNam(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
        }
    }

}
