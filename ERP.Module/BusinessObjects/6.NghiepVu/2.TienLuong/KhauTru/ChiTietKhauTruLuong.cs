using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.TienLuong.KhauTru
{
    [ImageName("BO_KhauTru")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết khấu trừ lương")]
    public class ChiTietKhauTruLuong : BaseObject, IBoPhan
    {
        private BangKhauTruLuong _BangKhauTruLuong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private NhomPhanBo _NhomPhanBo;
        private DateTime _NgayKhauTru;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _GhiChu;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Bảng khấu trừ lương")]
        [Association("BangKhauTruLuong-ListChiTietKhauTruLuong")]
        public BangKhauTruLuong BangKhauTruLuong
        {
            get
            {
                return _BangKhauTruLuong;
            }
            set
            {
                SetPropertyValue("BangKhauTruLuong", ref _BangKhauTruLuong, value);
                if (!IsLoading)
                {
                    UpdateBoPhanList();
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [DataSourceProperty("BoPhanList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {                    
                    if (value.BoPhan != BoPhan)
                        BoPhan = value.BoPhan;
                    NhomPhanBo = value.NhomPhanBo;
                }
            }
        }

        [ModelDefault("Caption", "Nhóm phân bổ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhomPhanBo NhomPhanBo
        {
            get
            {
                return _NhomPhanBo;
            }
            set
            {
                SetPropertyValue("NhomPhanBo", ref _NhomPhanBo, value);
            }
        }

        [ModelDefault("Caption", "Ngày khấu trừ")]
        public DateTime NgayKhauTru
        {
            get
            {
                return _NgayKhauTru;
            }
            set
            {
                SetPropertyValue("NgayKhauTru", ref _NgayKhauTru, value);
            }
        }

        [ModelDefault("Caption", "Số tiền không chịu thuế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }

        [Size(500)]
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

        public ChiTietKhauTruLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //UpdateNhanVienList();
        }
        //

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            //UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
            {
                //NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
                NVList.Criteria = CriteriaOperator.Parse("CongTy = ?", BangKhauTruLuong.CongTy.Oid);
            }
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        private void UpdateBoPhanList()
        {
            //
            if (BoPhanList == null)
                BoPhanList = new XPCollection<BoPhan>(Session);
            //
            BoPhanList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(BangKhauTruLuong.CongTy);
        }
    }

}
