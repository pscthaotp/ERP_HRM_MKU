using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
//
namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_GiaDinh")]
    [DefaultProperty("QuanHeGiaDinh")]
    [ModelDefault("Caption", "Giảm trừ gia cảnh")]
    public class GiamTruGiaCanh : BaseObject
    {
        private NhanVien _NhanVien;
        private string _MaSoThue;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private QuanHeGiaDinh _QuanHeGiaDinh;
        private LoaiGiamTruGiaCanh _LoaiGiamTruGiaCanh;
        private NhanVien _NhanVien_Old;
        private string _GhiChu;
        private bool _NgungGiamTru = false;

        [ModelDefault("Caption", "Cán bộ")]
        [Association("NhanVien-ListGiamTruGiaCanh")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                //Luu vet de update so nguoi phu thuoc
                if (value == null)
                    _NhanVien_Old = _NhanVien;
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Họ tên người thân")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NhanVien.ListQuanHeGiaDinh")]
        public QuanHeGiaDinh QuanHeGiaDinh
        {
            get
            {
                return _QuanHeGiaDinh;
            }
            set
            {
                SetPropertyValue("QuanHeGiaDinh", ref _QuanHeGiaDinh, value);
            }
        }

        [ModelDefault("Caption", "Tên gia cảnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiGiamTruGiaCanh LoaiGiamTruGiaCanh
        {
            get
            {
                return _LoaiGiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("LoaiGiamTruGiaCanh", ref _LoaiGiamTruGiaCanh, value);
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return _MaSoThue;
            }
            set
            {
                SetPropertyValue("MaSoThue", ref _MaSoThue, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Ngừng giảm trừ")]
        public bool NgungGiamTru
        {
            get
            {
                return _NgungGiamTru;
            }
            set
            {
                SetPropertyValue("NgungGiamTru", ref _NgungGiamTru, value);
            }
        }
        public GiamTruGiaCanh(Session session) : base(session) { }

        protected override void OnDeleted()
        {
            base.OnDeleted();

            if (_NhanVien_Old != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=?", _NhanVien_Old.Oid);
                object count = Session.Evaluate<NhanVien>(CriteriaOperator.Parse("ListGiamTruGiaCanh.Count"), filter);
                if (count != null)
                    _NhanVien_Old.NhanVienThongTinLuong.SoNguoiPhuThuoc = (int)count > 0 ? (int)count - 1 : 0;
                //
                _NhanVien_Old.Save();
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            {
                if (NgungGiamTru) // Nếu ngừng thì cập nhật lại trong nhân viên thông tin lương
                {
                    if (NhanVien != null)
                    {
                        NhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc -= 1;
                        NhanVien.NhanVienThongTinLuong.SoThangGiamTru -= 12;
                        //
                        if (NhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc < 0)
                            NhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc = 0;
                        if (NhanVien.NhanVienThongTinLuong.SoThangGiamTru < 0)
                            NhanVien.NhanVienThongTinLuong.SoThangGiamTru = 0;
                    }
                }
            }
        }
    }
}
