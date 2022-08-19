using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.TienLuong.Thuong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Loại thưởng ")]
    [DefaultProperty("TenLoai")]
    public class LoaiKhenThuongPhucLoi : BaseObject, ICongTy
    {
        private string _MaQuanLy;
        private ChiPhiTienLuong _ChiPhiTienLuong;
        private string _TenLoai;
        private CongTy _CongTy;

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
            }
        }


        [ModelDefault("Caption", "Mã quản lý (không được sửa)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Chi phí tiền lương")]
        public ChiPhiTienLuong ChiPhiTienLuong
        {
            get
            {
                return _ChiPhiTienLuong;
            }
            set
            {
                SetPropertyValue("ChiPhiTienLuong", ref _ChiPhiTienLuong, value);
            }
        }

        [ModelDefault("Caption", "Tên loại thưởng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenLoai
        {
            get
            {
                return _TenLoai;
            }
            set
            {
                SetPropertyValue("TenLoai", ref _TenLoai, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết công thức tính")]
        [Association("LoaiKhenThuongPhucLoi-CongThucKhenThuongPhucLoi")]
        public XPCollection<CongThucKhenThuongPhucLoi> CongThucList
        {
            get
            {
                return GetCollection<CongThucKhenThuongPhucLoi>("CongThucList");
            }
        }

        public LoaiKhenThuongPhucLoi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
    }

}
