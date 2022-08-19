using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTinhTrang")]
    [ModelDefault("Caption", "Tình trạng")]
    public class TinhTrang : BaseObject
    {
        private bool _DaNghiViec;
        private string _MaQuanLy;
        private string _TenTinhTrang;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("",DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTinhTrang
        {
            get
            {
                return _TenTinhTrang;
            }
            set
            {
                SetPropertyValue("TenTinhTrang", ref _TenTinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Đã nghỉ việc")]
        public bool DaNghiViec
        {
            get
            {
                return _DaNghiViec;
            }
            set
            {
                SetPropertyValue("DaNghiViec", ref _DaNghiViec, value);
            }
        }

        public TinhTrang(Session session) : base(session) { }

    }

}
