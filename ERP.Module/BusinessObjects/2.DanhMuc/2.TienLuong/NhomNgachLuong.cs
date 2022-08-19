using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNhomNgachLuong")]
    [ModelDefault("Caption", "Nhóm Cấp bậc")]
    public class NhomNgachLuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhomNgachLuong;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên nhóm ngạch")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomNgachLuong
        {
            get
            {
                return _TenNhomNgachLuong;
            }
            set
            {
                SetPropertyValue("TenNhomNgachLuong", ref _TenNhomNgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        public NhomNgachLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
    }

}
