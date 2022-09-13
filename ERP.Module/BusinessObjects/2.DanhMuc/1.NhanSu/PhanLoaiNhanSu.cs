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
    [DefaultProperty("TenPhanLoaiNhanSu")]
    [ModelDefault("Caption", "Phân loại nhân sự")]
    public class PhanLoaiNhanSu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenPhanLoaiNhanSu;

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

        [ModelDefault("Caption", "Tên công việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenPhanLoaiNhanSu
        {
            get
            {
                return _TenPhanLoaiNhanSu;
            }
            set
            {
                SetPropertyValue("TenPhanLoaiNhanSu", ref _TenPhanLoaiNhanSu, value);
            }
        }


        public PhanLoaiNhanSu(Session session) : base(session) { }
    }
}
