using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using System.Drawing;
//
namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_Bank")]
    [DefaultProperty("TenPhanLoaiNguoiKy")]
    [ModelDefault("Caption", "Phân loại người ký")]
    public class PhanLoaiNguoiKy : BaseObject
    {
        private string _MaQuanLy;
        private string _TenPhanLoaiNguoiKy;
        private LoaiCongTy _LoaiCongTy;
        //
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

        [ModelDefault("Caption", "Tên phân loại người ký")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenPhanLoaiNguoiKy
        {
            get
            {
                return _TenPhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("TenPhanLoaiNguoiKy", ref _TenPhanLoaiNguoiKy, value);
            }
        }

        [ModelDefault("Caption", "Loại Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiCongTy LoaiCongTy
        {
            get
            {
                return _LoaiCongTy;
            }
            set
            {
                SetPropertyValue("LoaiCongTy", ref _LoaiCongTy, value);
            }
        }


        public PhanLoaiNguoiKy(Session session) : base(session) { }
    }

}
