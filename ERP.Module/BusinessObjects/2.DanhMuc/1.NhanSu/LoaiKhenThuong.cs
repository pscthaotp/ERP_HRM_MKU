using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [DefaultProperty("TenLoaiKhenThuong")]
    [ImageName("BO_LoaiKhenThuong")]
    [ModelDefault("Caption", "Loại khen thưởng")]
    public class LoaiKhenThuong : BaseObject
    {
        private string _TenLoaiKhenThuong;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên loại khen thưởng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenLoaiKhenThuong
        {
            get
            {
                return _TenLoaiKhenThuong;
            }
            set
            {
                SetPropertyValue("TenLoaiKhenThuong", ref _TenLoaiKhenThuong, value);
            }
        }

        public LoaiKhenThuong(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
