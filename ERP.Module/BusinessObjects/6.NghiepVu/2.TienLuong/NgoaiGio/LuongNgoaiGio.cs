using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.TienLuong.NgoaiGio
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Lương ngoài giờ")]
    //[RuleCombinationOfPropertiesIsUnique("LuongNhanVien.Unique", DefaultContexts.Save, "BangLuongNhanVien;Department;ThongTinNhanVien")]
    [ModelDefault("AllowEdit","False")]
    public class LuongNgoaiGio : BaseObject, IBoPhan
    {
        private BangLuongNgoaiGio _BangLuongNgoaiGio;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private NhomPhanBo _NhomPhanBo;
        private string _GhiChu;

        //Toàn vẹn dữ liệu
        private ChiTietLuong _ChiTietLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng lương nhân viên")]
        [Association("BangLuongNgoaiGio-ListLuongNgoaiGio")]
        public BangLuongNgoaiGio BangLuongNgoaiGio
        {
            get
            {
                return _BangLuongNgoaiGio;
            }
            set
            {
                SetPropertyValue("BangLuongNgoaiGio", ref _BangLuongNgoaiGio, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    //
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("AllowEdit", "False")]
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
            }
        }

        [ModelDefault("Caption", "Nhóm phân bổ")]
        [ModelDefault("AllowEdit", "False")]
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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết lương ngoài giờ")]
        [Association("LuongNgoaiGio-ListChiTietLuongNgoaiGio")]
        public XPCollection<ChiTietLuongNgoaiGio> ListChiTietLuongNgoaiGio
        {
            get
            {
                return GetCollection<ChiTietLuongNgoaiGio>("ListChiTietLuongNgoaiGio");
            }
        }

        #region Lưu vết
        [Browsable(false)]
        public ChiTietLuong ChiTietLuong
        {
            get
            {
                return _ChiTietLuong;
            }
            set
            {
                SetPropertyValue("ChiTietLuong", ref _ChiTietLuong, value);
            }
        }
        #endregion      

        public LuongNgoaiGio(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }
        //

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }
    }
}
