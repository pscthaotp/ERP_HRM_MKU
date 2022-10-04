using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenPhanLoaiBangLuong")]
    [ModelDefault("Caption", "Phân loại bảng lương")]
    public class PhanLoaiBangLuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenPhanLoaiBangLuong;

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

        [ModelDefault("Caption", "Tên phân loại bảng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenPhanLoaiBangLuong
        {
            get
            {
                return _TenPhanLoaiBangLuong;
            }
            set
            {
                SetPropertyValue("TenPhanLoaiBangLuong", ref _TenPhanLoaiBangLuong, value);
            }
        }


        public PhanLoaiBangLuong(Session session) : base(session) { }
    }
}
