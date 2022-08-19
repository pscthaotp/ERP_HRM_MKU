using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.HeThong;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Phân quyền tính lương")]
    [DefaultProperty("Ten")]
    [ImageName("BO_Category")]
    [RuleCombinationOfPropertiesIsUnique("PhanQuyenTinhLuong.Unique", DefaultContexts.Save, "SecuritySystemUser_Custom;CongTy")]
    public class PhanQuyenTinhLuong : BaseObject, ICongTy
    {
        private SecuritySystemUser_Custom _SecuritySystemUser_Custom;
        private CongTy _CongTy;

        [ImmediatePostData]
        [DataSourceProperty("NguoiSuDungList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Tài khoản")]
        public SecuritySystemUser_Custom SecuritySystemUser_Custom
        {
            get
            {
                return _SecuritySystemUser_Custom;
            }
            set
            {
                SetPropertyValue("SecuritySystemUser_Custom", ref _SecuritySystemUser_Custom, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (value != null)
                {
                    UpdateNguoiSuDungList();
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách công thức lương")]
        [Association("PhanQuyenTinhLuong-ListChiTietPhanQuyenTinhLuong")]
        public XPCollection<ChiTietPhanQuyenTinhLuong> ListChiTietPhanQuyenTinhLuong
        {
            get
            {
                return GetCollection<ChiTietPhanQuyenTinhLuong>("ListChiTietPhanQuyenTinhLuong");
            }
        }

        //[Browsable(false)]
        [Aggregated]
        [ModelDefault("Caption", "Danh sách công thức khen thưởng")]
        [Association("PhanQuyenTinhLuong-ListChiTietPhanQuyenTinhKhenThuong")]
        public XPCollection<ChiTietPhanQuyenTinhKhenThuong> ListChiTietPhanQuyenTinhKhenThuong
        {
            get
            {
                return GetCollection<ChiTietPhanQuyenTinhKhenThuong>("ListChiTietPhanQuyenTinhKhenThuong");
            }
        }

        public void CreateChiTietPhanQuyenTinhLuong(Guid oidCongThucTinhLuong)
        {
            var chiTiet = new ChiTietPhanQuyenTinhLuong(Session);
            chiTiet.PhanQuyenTinhLuong = this;
            chiTiet.CongThucTinhLuong = Session.GetObjectByKey<CongThucTinhLuong>(oidCongThucTinhLuong);
            ListChiTietPhanQuyenTinhLuong.Add(chiTiet);
        }

        public void CreateChiTietPhanQuyenTinhKhenThuong(Guid oidCongThucTinhThuong)
        {
            var chiTiet = new ChiTietPhanQuyenTinhKhenThuong(Session);
            chiTiet.PhanQuyenTinhLuong = this;
            chiTiet.LoaiKhenThuongPhucLoi = Session.GetObjectByKey<LoaiKhenThuongPhucLoi>(oidCongThucTinhThuong);
            ListChiTietPhanQuyenTinhKhenThuong.Add(chiTiet);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
        public PhanQuyenTinhLuong(Session session):base(session)
        {
        }

        [Browsable(false)]
        public XPCollection<SecuritySystemUser_Custom> NguoiSuDungList { get; set; }

        private void UpdateNguoiSuDungList()
        {
            if (NguoiSuDungList == null)
            {
                NguoiSuDungList = new XPCollection<SecuritySystemUser_Custom>(Session);
            }
            if (CongTy != null)
            {
                NguoiSuDungList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", CongTy.Oid);
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNguoiSuDungList();
        }
    }
}
